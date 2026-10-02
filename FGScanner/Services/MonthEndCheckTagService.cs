using FGScanner.DTOs;
using FGScanner.Models;
using FGScanner.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FGScanner.Services
{
    public sealed class MonthEndCheckTagService
    {
        private readonly InventoryDbContext _context;

        public MonthEndCheckTagService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task EnsureSnapshotSchemaAsync()
        {
            if (!_context.Database.IsRelational())
            {
                return;
            }

            const string sql = @"
IF OBJECT_ID(N'[dbo].[inventory_count_batch]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[inventory_count_batch]
    (
        [id] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_inventory_count_batch] PRIMARY KEY,
        [batch_code] NVARCHAR(50) NOT NULL,
        [cutoff_date] DATE NOT NULL,
        [warehouse_id] NVARCHAR(20) NOT NULL,
        [generation_mode] NVARCHAR(20) NOT NULL,
        [rack_location] NVARCHAR(100) NOT NULL CONSTRAINT [DF_inventory_count_batch_rack] DEFAULT(''),
        [revision] INT NOT NULL,
        [batch_sequence] INT NOT NULL,
        [created_at] DATETIME2 NOT NULL,
        [created_by] NVARCHAR(255) NOT NULL,
        [prefill_system_count] BIT NOT NULL,
        [tag_count] INT NOT NULL,
        [status] NVARCHAR(20) NOT NULL,
        CONSTRAINT [UQ_inventory_count_batch_code] UNIQUE ([batch_code]),
        CONSTRAINT [UQ_inventory_count_batch_sequence] UNIQUE ([cutoff_date], [warehouse_id], [batch_sequence])
    );
END;

IF OBJECT_ID(N'[dbo].[inventory_count_tag_snapshot]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[inventory_count_tag_snapshot]
    (
        [id] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_inventory_count_tag_snapshot] PRIMARY KEY,
        [batch_id] INT NOT NULL,
        [sequence_number] INT NOT NULL,
        [tag_number] NVARCHAR(50) NOT NULL,
        [erp_location] NVARCHAR(100) NOT NULL,
        [rack_location] NVARCHAR(100) NOT NULL,
        [location_description] NVARCHAR(100) NOT NULL,
        [part_number] NVARCHAR(255) NOT NULL,
        [production_version] NVARCHAR(100) NOT NULL,
        [material_name] NVARCHAR(255) NOT NULL,
        [unit] NVARCHAR(20) NOT NULL,
        [system_boxes] INT NOT NULL,
        [system_quantity] INT NOT NULL,
        CONSTRAINT [UQ_inventory_count_tag_number] UNIQUE ([tag_number]),
        CONSTRAINT [UQ_inventory_count_tag_sequence] UNIQUE ([batch_id], [sequence_number]),
        CONSTRAINT [FK_inventory_count_tag_batch] FOREIGN KEY ([batch_id])
            REFERENCES [dbo].[inventory_count_batch] ([id]) ON DELETE CASCADE
    );
END;";

            await _context.Database.ExecuteSqlRawAsync(sql);
        }

        public async Task<InventoryCheckTagBatchSummary> FindLatestBatchAsync(
            DateOnly cutoffDate,
            string warehouseId,
            bool perRack,
            string rackLocation)
        {
            await EnsureSnapshotSchemaAsync();
            string warehouse = Normalize(warehouseId);
            string rack = perRack ? Normalize(rackLocation) : string.Empty;
            string mode = perRack ? "RACK" : "ALL";

            var batch = await _context.InventoryCountBatches
                .AsNoTracking()
                .Where(batch =>
                    batch.CutoffDate == cutoffDate &&
                    batch.WarehouseId == warehouse &&
                    batch.GenerationMode == mode &&
                    batch.RackLocation == rack &&
                    batch.Status == "FINALIZED")
                .OrderByDescending(batch => batch.Revision)
                .FirstOrDefaultAsync();

            return batch == null ? null : ToSummary(batch);
        }

        public async Task<List<InventoryCheckTagBatchSummary>> ListBatchesAsync()
        {
            await EnsureSnapshotSchemaAsync();
            var batches = await _context.InventoryCountBatches
                .AsNoTracking()
                .Where(batch => batch.Status == "FINALIZED")
                .OrderByDescending(batch => batch.CreatedAt)
                .Take(500)
                .ToListAsync();

            return batches.Select(ToSummary).ToList();
        }

        public async Task<InventoryCheckTagBatchData> CreateSnapshotAsync(
            DateOnly cutoffDate,
            string warehouseId,
            bool perRack,
            string rackLocation,
            bool prefillSystemCount,
            string generatedBy)
        {
            await EnsureSnapshotSchemaAsync();
            string warehouse = Normalize(warehouseId);
            string rack = perRack ? Normalize(rackLocation) : string.Empty;
            string mode = perRack ? "RACK" : "ALL";

            var tags = await GenerateAsync(
                cutoffDate,
                warehouse,
                rack,
                string.Empty,
                prefillSystemCount,
                generatedBy,
                exactRackMatch: perRack);

            if (tags.Count == 0)
            {
                throw new InvalidOperationException(
                    "No positive inventory balances matched the selected cutoff and scope.");
            }

            IDbContextTransaction databaseTransaction = null;
            if (_context.Database.IsRelational())
            {
                databaseTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            }

            try
            {
                int revision = (await _context.InventoryCountBatches
                    .Where(batch =>
                        batch.CutoffDate == cutoffDate &&
                        batch.WarehouseId == warehouse &&
                        batch.GenerationMode == mode &&
                        batch.RackLocation == rack)
                    .MaxAsync(batch => (int?)batch.Revision) ?? 0) + 1;

                int batchSequence = (await _context.InventoryCountBatches
                    .Where(batch =>
                        batch.CutoffDate == cutoffDate &&
                        batch.WarehouseId == warehouse)
                    .MaxAsync(batch => (int?)batch.BatchSequence) ?? 0) + 1;

                string batchCode =
                    $"CT-{cutoffDate:yyyyMMdd}-{warehouse}-B{batchSequence:D3}-R{revision:D2}";
                string warehouseSequence = warehouse.Length > 0
                    ? warehouse[warehouse.Length - 1].ToString()
                    : "0";
                DateOnly monthStart = new(cutoffDate.Year, cutoffDate.Month, 1);
                DateOnly nextMonth = monthStart.AddMonths(1);
                int monthlyTagCount = await _context.InventoryCountTagSnapshots
                    .Where(item =>
                        item.Batch.CutoffDate >= monthStart &&
                        item.Batch.CutoffDate < nextMonth &&
                        item.Batch.WarehouseId == warehouse)
                    .CountAsync();

                var batch = new InventoryCountBatch
                {
                    BatchCode = batchCode,
                    CutoffDate = cutoffDate,
                    WarehouseId = warehouse,
                    GenerationMode = mode,
                    RackLocation = rack,
                    Revision = revision,
                    BatchSequence = batchSequence,
                    CreatedAt = DateTime.Now,
                    CreatedBy = generatedBy ?? string.Empty,
                    PrefillSystemCount = prefillSystemCount,
                    TagCount = tags.Count,
                    Status = "FINALIZED"
                };

                for (int index = 0; index < tags.Count; index++)
                {
                    InventoryCheckTagData tag = tags[index];
                    tag.BatchCode = batchCode;
                    tag.TagNumber =
                        $"{cutoffDate:yyMM}{warehouseSequence}{monthlyTagCount + index + 1:D4}";
                    batch.Tags.Add(new InventoryCountTagSnapshot
                    {
                        SequenceNumber = index + 1,
                        TagNumber = tag.TagNumber,
                        ErpLocation = tag.ErpLocation,
                        RackLocation = tag.RackLocation,
                        LocationDescription = tag.LocationDescription,
                        PartNumber = tag.PartNumber,
                        ProductionVersion = tag.ProductionVersion,
                        MaterialName = tag.MaterialName,
                        Unit = tag.Unit,
                        SystemBoxes = tag.SystemBoxes,
                        SystemQuantity = tag.SystemQuantity
                    });
                }

                _context.InventoryCountBatches.Add(batch);
                await _context.SaveChangesAsync();
                if (databaseTransaction != null)
                {
                    await databaseTransaction.CommitAsync();
                }

                return new InventoryCheckTagBatchData
                {
                    BatchId = batch.Id,
                    BatchCode = batch.BatchCode,
                    Revision = batch.Revision,
                    CreatedAt = batch.CreatedAt,
                    CreatedBy = batch.CreatedBy,
                    Tags = tags
                };
            }
            catch
            {
                if (databaseTransaction != null)
                {
                    await databaseTransaction.RollbackAsync();
                }

                throw;
            }
            finally
            {
                if (databaseTransaction != null)
                {
                    await databaseTransaction.DisposeAsync();
                }
            }
        }

        public async Task<InventoryCheckTagBatchData> LoadBatchAsync(int batchId)
        {
            await EnsureSnapshotSchemaAsync();
            var batch = await _context.InventoryCountBatches
                .AsNoTracking()
                .Include(item => item.Tags)
                .SingleOrDefaultAsync(item => item.Id == batchId);

            if (batch == null)
            {
                throw new InvalidOperationException("The saved inventory check-tag batch was not found.");
            }

            var tags = batch.Tags
                .OrderBy(item => item.SequenceNumber)
                .Select(item => new InventoryCheckTagData
                {
                    BatchCode = batch.BatchCode,
                    TagNumber = item.TagNumber,
                    CutoffDate = batch.CutoffDate,
                    WarehouseId = batch.WarehouseId,
                    ErpLocation = item.ErpLocation,
                    RackLocation = item.RackLocation,
                    LocationDescription = item.LocationDescription,
                    PartNumber = item.PartNumber,
                    ProductionVersion = item.ProductionVersion,
                    MaterialName = item.MaterialName,
                    Unit = item.Unit,
                    SystemBoxes = item.SystemBoxes,
                    SystemQuantity = item.SystemQuantity,
                    PrefillSystemCount = batch.PrefillSystemCount,
                    GeneratedBy = batch.CreatedBy
                })
                .ToList();

            return new InventoryCheckTagBatchData
            {
                BatchId = batch.Id,
                BatchCode = batch.BatchCode,
                Revision = batch.Revision,
                CreatedAt = batch.CreatedAt,
                CreatedBy = batch.CreatedBy,
                Tags = tags
            };
        }

        private static string Normalize(string value) =>
            (value ?? string.Empty).Trim().ToUpperInvariant();

        private static InventoryCheckTagBatchSummary ToSummary(InventoryCountBatch batch) => new()
        {
            BatchId = batch.Id,
            BatchCode = batch.BatchCode,
            CutoffDate = batch.CutoffDate,
            Warehouse = batch.WarehouseId,
            Scope = batch.GenerationMode == "RACK"
                ? "Rack: " + batch.RackLocation
                : "All racks",
            Revision = batch.Revision,
            TagCount = batch.TagCount,
            CreatedAt = batch.CreatedAt,
            CreatedBy = batch.CreatedBy
        };

        public async Task<List<InventoryCheckTagData>> GenerateAsync(
            DateOnly cutoffDate,
            string warehouseId,
            string rackFilter,
            string partNumberFilter,
            bool prefillSystemCount,
            string generatedBy,
            bool exactRackMatch = false)
        {
            string warehouse = (warehouseId ?? string.Empty).Trim().ToUpperInvariant();
            string rack = (rackFilter ?? string.Empty).Trim().ToUpperInvariant();
            string partNumber = (partNumberFilter ?? string.Empty).Trim().ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(warehouse))
            {
                throw new ArgumentException("Please select a warehouse.", nameof(warehouseId));
            }

            DateTime cutoffExclusive = cutoffDate
                .ToDateTime(TimeOnly.MinValue)
                .AddDays(1);

            var transactionQuery = _context.TransactionHistories
                .AsNoTracking()
                .Where(transaction =>
                    transaction.WhId == warehouse &&
                    transaction.EntryDate < cutoffExclusive &&
                    transaction.Quantity > 0 &&
                    (transaction.TransactionType == "IN" || transaction.TransactionType == "OUT"));

            var balances = await transactionQuery
                .Select(transaction => new
                {
                    transaction.Partnumber,
                    transaction.ProdVer,
                    transaction.CustomerId,
                    transaction.Location,
                    transaction.StorageLocation,
                    transaction.ProdDate,
                    transaction.Quantity,
                    transaction.TransactionType,
                    Boxes = transaction.Remarks == "BPPS" ||
                            transaction.Remarks == "FG" ||
                            transaction.ControlNumber.StartsWith("SHIPID-") ||
                            transaction.ControlNumber.StartsWith("AS-")
                        ? 1
                        : transaction.Box ?? 0
                })
                .GroupBy(transaction => new
                {
                    transaction.Partnumber,
                    transaction.ProdVer,
                    transaction.CustomerId,
                    transaction.Location,
                    transaction.StorageLocation,
                    transaction.ProdDate
                })
                .Select(group => new
                {
                    group.Key.Partnumber,
                    group.Key.ProdVer,
                    group.Key.CustomerId,
                    group.Key.Location,
                    group.Key.StorageLocation,
                    group.Key.ProdDate,
                    Quantity = group.Sum(transaction =>
                        transaction.TransactionType == "IN"
                            ? transaction.Quantity
                            : -transaction.Quantity),
                    Boxes = group.Sum(transaction =>
                        transaction.TransactionType == "IN"
                            ? transaction.Boxes
                            : -transaction.Boxes)
                })
                .Where(balance => balance.Quantity > 0)
                .ToListAsync();

            var requestedPartNumbers = balances
                .Select(balance => balance.Partnumber)
                .Distinct()
                .ToList();

            var products = await _context.Products
                .AsNoTracking()
                .Where(product => requestedPartNumbers.Contains(product.Partnumber))
                .ToDictionaryAsync(product => product.Partnumber);

            var groupedTags = balances
                .Select(balance =>
                {
                    products.TryGetValue(balance.Partnumber, out Product product);
                    int boxes = Math.Max(balance.Boxes, 0);
                    int pps = product?.Pps ?? 0;
                    string ppsType = InventoryPpsUtility.GetPpsType(
                        balance.Quantity,
                        boxes,
                        pps,
                        string.Empty);

                    return new
                    {
                        balance.Partnumber,
                        balance.ProdVer,
                        balance.CustomerId,
                        balance.Location,
                        balance.StorageLocation,
                        ProductName = product?.Partname ?? string.Empty,
                        PpsType = string.IsNullOrWhiteSpace(ppsType)
                            ? InventoryPpsUtility.Bpps
                            : ppsType,
                        balance.Quantity,
                        Boxes = boxes
                    };
                })
                .GroupBy(balance => new
                {
                    balance.Partnumber,
                    balance.ProdVer,
                    balance.CustomerId,
                    balance.Location,
                    balance.StorageLocation,
                    balance.ProductName,
                    balance.PpsType
                })
                .Select(group => new InventoryCheckTagData
                {
                    CutoffDate = cutoffDate,
                    WarehouseId = warehouse,
                    ErpLocation = group.Key.StorageLocation ?? string.Empty,
                    RackLocation = group.Key.Location ?? string.Empty,
                    LocationDescription = group.Key.PpsType,
                    PartNumber = group.Key.Partnumber ?? string.Empty,
                    ProductionVersion = group.Key.ProdVer ?? string.Empty,
                    MaterialName = group.Key.ProductName,
                    SystemBoxes = Math.Max(group.Sum(item => item.Boxes), 0),
                    SystemQuantity = group.Sum(item => item.Quantity),
                    PrefillSystemCount = prefillSystemCount,
                    GeneratedBy = generatedBy ?? string.Empty
                })
                .OrderBy(tag => tag.RackLocation)
                .ThenBy(tag => tag.ErpLocation)
                .ThenBy(tag => tag.PartNumber)
                .ThenBy(tag => tag.ProductionVersion)
                .ThenBy(tag => tag.LocationDescription)
                .ToList();

            string warehouseSequence = warehouse.Length > 0
                ? warehouse[warehouse.Length - 1].ToString()
                : "0";
            for (int index = 0; index < groupedTags.Count; index++)
            {
                groupedTags[index].TagNumber =
                    $"{cutoffDate:yyMM}{warehouseSequence}{index + 1:D3}";
            }

            return groupedTags
                .Where(tag => string.IsNullOrWhiteSpace(rack) ||
                              (exactRackMatch
                                  ? tag.RackLocation.Equals(rack, StringComparison.OrdinalIgnoreCase)
                                  : tag.RackLocation.Contains(rack, StringComparison.OrdinalIgnoreCase)))
                .Where(tag => string.IsNullOrWhiteSpace(partNumber) ||
                              tag.PartNumber.Contains(partNumber, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
