using FGScanner.Database;
using FGScanner.Forms.DataEntry;
using FGScanner.Models;
using FGScanner.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FGScanner.Repositories
{
    public class Queries
    {
        private readonly InventoryDbContext _context;
        public Queries(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> UpdateMovementClassification()
        {
            string Query = "EXEC sp_UpdateInventoryClassification_clean";
            return await _context.Database.ExecuteSqlRawAsync(Query);
        }

        public async Task<Product> GetProductInfo(string partnumber)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Partnumber == partnumber);
        }

        public async Task<ActualInventory> GetStockInfo(string partnumber, DateOnly proddate, string prodversion, string location, string whid)
        {
            try
            {
                var inventory = await _context.ActualInventories
                 .Where(x => x.Partnumber == partnumber && x.ProdDate == proddate && x.ProdVer == prodversion && x.Location == location && x.WhId == whid)
                 .FirstOrDefaultAsync();
                return inventory;
            }
            catch
            {
                return new();
            }
        }
        public int GetProductPPS(string partnumber)
        {
            var productPPS = _context.Products.FirstOrDefault(x => x.Partnumber == partnumber);
            return productPPS?.Pps ?? 0;
        }
        public int GetProductID(string partnumber)
        {
            var productID = _context.Products.FirstOrDefault(x => x.Partnumber == partnumber);
            return productID?.Id ?? 0;
        }

        public string GetProductPartName(string partnumber)
        {
            var productPartName = _context.Products.FirstOrDefault(x => x.Partnumber == partnumber);
            return productPartName?.Partname ?? string.Empty;
        }
        public string GetProductCustomer(string partnumber)
        {
            var productCustomer = _context.Products.FirstOrDefault(x => x.Partnumber == partnumber);
            return productCustomer?.CustomerId ?? string.Empty;
        }

        public async Task<List<string>> GetRackLocations(string warehouseid)
        {
            return await _context.RackTables
                .Where(r => r.WhId == warehouseid)
                .Select(r => r.RackNo)
                .ToListAsync();
        }

        public async Task<List<ActualInventory>> GetActualInventory(string warehouseid, string location)
        {
            return await _context.ActualInventories
                     .Where(a => a.WhId == warehouseid && a.Location == location)
                     .Select(a => new ActualInventory
                     {
                         Partnumber = a.Partnumber,
                         ProdDate = a.ProdDate,
                         Customer = a.Customer,
                         Quantity = a.Quantity,
                         ProdVer = a.ProdVer,
                         TotalBox = a.TotalBox,
                     })
                     .ToListAsync();
        }

        public async Task<(bool isSuccess, string Message)> TransferInventoryAsync(
          string warehouseId,
          string currLocation,
          string newLocation,
          List<ActualInventory> inventories,
          string userId)
        {
            // 1. INPUT VALIDATION & CASE STANDARDIZATION
            if (inventories == null || !inventories.Any())
            {
                return (false, "Data is null or empty");
            }

            currLocation = currLocation.Trim().ToUpper();
            newLocation = newLocation.Trim().ToUpper();

            if (currLocation == newLocation)
            {
                return (false, "Source and destination locations cannot be the same.");
            }

            // 2. DEDUPLICATE & AGGREGATE PAYLOAD 
            var aggregatedRequests = inventories
                .GroupBy(i => new { i.Partnumber, i.ProdDate, i.ProdVer, Customer = i.Customer ?? string.Empty })
                .Select(g => new
                {
                    g.Key.Partnumber,
                    g.Key.ProdDate,
                    g.Key.ProdVer,
                    g.Key.Customer,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalBox = g.Sum(x => x.TotalBox)
                })
                .ToList();

            var partNumbers = aggregatedRequests.Select(r => r.Partnumber).Distinct().ToList();

            // 3. BATCH MASTER PRODUCT CHECK
            var validPartNumbers = await _context.Products
                .Where(p => partNumbers.Contains(p.Partnumber))
                .Select(p => p.Partnumber)
                .ToListAsync();

            var missingParts = partNumbers.Except(validPartNumbers).ToList();
            if (missingParts.Any())
            {
                return (false, $"Item(s) do not exist in master product records: {string.Join(", ", missingParts)}");
            }

            // 4. ATOMIC DATABASE TRANSACTION
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Query source inventory ONLY to validate available balance (AsNoTracking avoids EF tracking issues)
                var sourceInventories = await _context.ActualInventories
                    .AsNoTracking()
                    .Where(x => partNumbers.Contains(x.Partnumber) && x.Location == currLocation)
                    .ToListAsync();

                var now = DateTime.Now;

                foreach (var req in aggregatedRequests)
                {
                    var sourceItem = sourceInventories
                        .FirstOrDefault(x => x.Partnumber == req.Partnumber && x.ProdDate == req.ProdDate);

                    // Check existence in source location
                    if (sourceItem == null)
                    {
                        await transaction.RollbackAsync();
                        return (false, $"Item {req.Partnumber} (ProdDate: {req.ProdDate:yyyy-MM-dd}) does not exist in location {currLocation}.");
                    }

                    // Check available quantity limit
                    if (sourceItem.Quantity < req.TotalQuantity)
                    {
                        await transaction.RollbackAsync();
                        return (false, $"Transfer quantity ({req.TotalQuantity}) exceeds available stock ({sourceItem.Quantity}) for item {req.Partnumber}.");
                    }

                    // 5. CREATE AUDIT HISTORIES (SQL Trigger will automatically update ActualInventory on Save)

                    // OUT Transaction from Current Location
                    _context.TransactionHistories.Add(new TransactionHistory
                    {
                        TransactionId = Guid.NewGuid(),
                        Partnumber = req.Partnumber,
                        ProdDate = req.ProdDate,
                        CustomerId = req.Customer,
                        Quantity = req.TotalQuantity,
                        ProdVer = req.ProdVer,
                        Box = req.TotalBox,
                        EntryDate = now,
                        Location = currLocation,
                        WhId = warehouseId,
                        Remarks = $"Transfer to {newLocation}",
                        Status = "Active",
                        StorageLocation = sourceItem.StorageLocation,
                        TransactionType = "OUT",
                        IsSynced = false,
                        InCharge = userId
                    });

                    // IN Transaction to New Location
                    _context.TransactionHistories.Add(new TransactionHistory
                    {
                        TransactionId = Guid.NewGuid(),
                        Partnumber = req.Partnumber,
                        ProdDate = req.ProdDate,
                        CustomerId = req.Customer,
                        Quantity = req.TotalQuantity,
                        ProdVer = req.ProdVer,
                        Box = req.TotalBox,
                        EntryDate = now,
                        Location = newLocation,
                        WhId = warehouseId,
                        Remarks = $"Transfer from {currLocation}",
                        Status = "Active",
                        StorageLocation = sourceItem.StorageLocation,
                        TransactionType = "IN",
                        IsSynced = false,
                        InCharge = userId
                    });
                }

                // 6. SAVE CHANGES -> THIS FIRES YOUR SQL TRIGGER TO RECALCULATE ACTUAL INVENTORY
                await _context.SaveChangesAsync();

                // Run movement classification inside transaction
                await UpdateMovementClassification();

                await transaction.CommitAsync();
                return (true, "Transfer completed successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                string innerErr = ex.InnerException != null ? $" -> {ex.InnerException.Message}" : string.Empty;
                return (false, $"SQL Error: {ex.Message}{innerErr}");
            }
        }

        public async Task<(bool isSuccess, string Message)> InsertBPPSItems(List<ScannedData> Items, string warehouseId, string userid)
        {
            try
            {
                var TransactionItems = new List<TransactionHistory>();

                foreach (var item in Items)
                {
                    TransactionItems.Add(new TransactionHistory
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        Box = 1,
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.Now,
                        TransactionType = "IN",
                        Location = item.Location,
                        Remarks = "BPPS",
                        StorageLocation = item.StorageLocation ?? "9151",
                        Status = "",
                        WhId = warehouseId,
                        TransactionId = Guid.NewGuid(),
                        IsSynced = false,
                        InCharge = userid
                    });
                }

                _context.TransactionHistories.AddRange(TransactionItems);
                await _context.SaveChangesAsync();
                await UpdateMovementClassification();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                return (false, $"SQL Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<Dictionary<string, Product>> GetProductsByPartNumbersAsync(List<string> partNumbers)
        {
            return await _context.Products
                .Where(p => partNumbers.Contains(p.Partnumber))
                .ToDictionaryAsync(p => p.Partnumber);
        }

        public async Task<(bool isSuccess, string Message)> InsertFGItems(List<ScannedData> Items, string warehouseId, string transaction_type, string userid)
        {
            try
            {
                var TransactionItems = new List<TransactionHistory>();
                var scannedPartNumbers = Items.Select(x => x.PartNumber).Distinct().ToList();
                var productDict = await _context.Products
                              .Where(p => scannedPartNumbers.Contains(p.Partnumber))
                              .ToDictionaryAsync(p => p.Partnumber, p => p.Pps);
                 
                foreach (var item in Items)
                {
                    int pps = 1;
                    if (productDict.TryGetValue(item.PartNumber, out int dbPps) && dbPps > 0)
                    {
                        pps = dbPps;
                    }

                    TransactionItems.Add(new TransactionHistory
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        Box = (int)Math.Ceiling((double)item.Quantity / pps),
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.Now,
                        TransactionType = transaction_type,
                        Location = item.Location,
                        Remarks = "FG",
                        StorageLocation = item.StorageLocation ?? "9151",
                        Status = "",
                        WhId = warehouseId,
                        TransactionId = Guid.NewGuid(),
                        IsSynced = false,
                        InCharge = userid
                    });
                }
                _context.TransactionHistories.AddRange(TransactionItems);
                await _context.SaveChangesAsync();
                await UpdateMovementClassification();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                return (false, $"SQL Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<ActualInventory> CheckIfExist(string partnumber, string location, DateOnly proddate)
        {
            return await _context.ActualInventories.Where(x => x.Partnumber == partnumber && x.Location == location && x.ProdDate == proddate).FirstOrDefaultAsync();
        }

        public async Task<(bool isSuccess, string Message)> InsertFGOutgoingItems(List<ScannedData> items, string warehouseId, string id, string transactionType, string userId, string marketcode, string remarks)
        {
            if (items == null || !items.Any())
            {
                return (false, "No shipment items provided.");
            }

            if (items.Any(item => string.IsNullOrWhiteSpace(item.PartNumber) || item.Quantity <= 0 ||
                                  string.IsNullOrWhiteSpace(item.Location) || string.IsNullOrWhiteSpace(item.ProductionVersion)))
            {
                return (false, "Shipment items must have a part number, production version, location, and a quantity greater than zero.");
            }

            static string GetStockKey(string partNo, DateOnly prodDate, string prodVer, string wh, string loc)
                => $"{partNo.Trim().ToUpperInvariant()}|{prodDate:yyyy-MM-dd}|{prodVer.Trim().ToUpperInvariant()}|{wh.Trim().ToUpperInvariant()}|{loc.Trim().ToUpperInvariant()}";

            string normalizedWarehouseId = warehouseId?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(normalizedWarehouseId))
            {
                return (false, "Please select a warehouse.");
            }

            var requestedQuantities = items
                .GroupBy(item => GetStockKey(item.PartNumber, item.ProductionDate, item.ProductionVersion, normalizedWarehouseId, item.Location))
                .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));

            try
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

                var partNumbers = items.Select(item => item.PartNumber.Trim()).Distinct().ToList();
                var inventory = await _context.ActualInventories
                    .Where(item => partNumbers.Contains(item.Partnumber) && item.WhId == normalizedWarehouseId)
                    .ToListAsync();

                var availableQuantities = inventory
                    .GroupBy(item => GetStockKey(item.Partnumber, item.ProdDate, item.ProdVer, item.WhId, item.Location))
                    .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));

                foreach (var request in requestedQuantities)
                {
                    availableQuantities.TryGetValue(request.Key, out int availableQuantity);
                    if (request.Value > availableQuantity)
                    {
                        await transaction.RollbackAsync();
                        return (false, $"Shipment quantity ({request.Value}) exceeds available stock ({availableQuantity}) for {request.Key}.");
                    }
                }

                var TransactionItems = new List<TransactionHistory>();

                var newShipment = new ShipmentTable
                {
                    TransactionId = id,
                    EntryDate = DateTime.Now,
                    WhId = normalizedWarehouseId,
                    Status = "",
                    Customer = marketcode,
                    ShipmentId = Guid.NewGuid(),
                    IsSynced = false,
                    SyncStatus = 0
                };
                _context.ShipmentTables.Add(newShipment);

                foreach (var item in items)
                {
                    TransactionItems.Add(new TransactionHistory
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        Box = 1,
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.Now,
                        TransactionType = transactionType,
                        Location = item.Location.ToUpper(),
                        Remarks = remarks,
                        StorageLocation = item.StorageLocation ?? "9151",
                        Status = "",
                        WhId = normalizedWarehouseId,
                        ControlNumber = id,
                        TransactionId = Guid.NewGuid(),
                        IsSynced = false,
                        InCharge = userId
                    });
                }
                _context.TransactionHistories.AddRange(TransactionItems);
                await _context.SaveChangesAsync();
                await UpdateMovementClassification();
                await transaction.CommitAsync();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                return (false, $"SQL Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        public async Task<(bool isSuccess, string Message)> InsertReturnItems(
            List<ScannedData> items,
            string warehouseId,
            string id,
            string transactionType,
            string userId,
            string remarks,
            string location)
        {
            if (items == null || !items.Any())
            {
                return (false, "No scanned items provided to insert.");
            }

            if (items.Any(item => string.IsNullOrWhiteSpace(item.PartNumber) || item.Quantity <= 0 ||
                                  string.IsNullOrWhiteSpace(item.Location) || string.IsNullOrWhiteSpace(item.ProductionVersion)))
            {
                return (false, "Return items must have a part number, production version, location, and a quantity greater than zero.");
            }

            string normalizedWarehouseId = warehouseId?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(normalizedWarehouseId))
            {
                return (false, "Please select a warehouse.");
            }

            static string GetStockKey(string partNo, DateOnly prodDate, string prodVer, string wh, string loc)
                => $"{partNo.Trim().ToUpperInvariant()}|{prodDate:yyyy-MM-dd}|{prodVer.Trim().ToUpperInvariant()}|{wh.Trim().ToUpperInvariant()}|{loc.Trim().ToUpperInvariant()}";

            var requestedQuantities = items
                .GroupBy(item => GetStockKey(item.PartNumber, item.ProductionDate, item.ProductionVersion, normalizedWarehouseId, item.Location))
                .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));

            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            try
            {
                var partNumbers = items.Select(item => item.PartNumber.Trim()).Distinct().ToList();
                var inventory = await _context.ActualInventories
                    .Where(item => partNumbers.Contains(item.Partnumber) && item.WhId == normalizedWarehouseId)
                    .ToListAsync();

                var availableQuantities = inventory
                    .GroupBy(item => GetStockKey(item.Partnumber, item.ProdDate, item.ProdVer, item.WhId, item.Location))
                    .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));

                foreach (var request in requestedQuantities)
                {
                    availableQuantities.TryGetValue(request.Key, out int availableQuantity);
                    if (request.Value > availableQuantity)
                    {
                        await transaction.RollbackAsync();
                        return (false, $"Return quantity ({request.Value}) exceeds available stock ({availableQuantity}) for item: {request.Key}.");
                    }
                }

                var productDict = await _context.Products
                    .Where(p => partNumbers.Contains(p.Partnumber))
                    .ToDictionaryAsync(p => p.Partnumber, p => p.Pps);

                var newReturn = new ReturnTable
                {
                    TransactionId = id,
                    EntryDate = DateTime.Now,
                    WhId = normalizedWarehouseId,
                    FromLocation = "9151",
                    ToLocation = (location ?? string.Empty).Trim().ToUpper(),
                    ReturnId = Guid.NewGuid(),
                    Remarks = remarks,
                    IsSynced = false,
                    SyncStatus = 0
                };
                _context.ReturnTables.Add(newReturn);

                var transactionItems = new List<TransactionHistory>();
                var now = DateTime.Now;

                foreach (var item in items)
                {
                    int pps = 1;
                    if (productDict.TryGetValue(item.PartNumber, out int dbPps) && dbPps > 0)
                    {
                        pps = dbPps;
                    }

                    transactionItems.Add(new TransactionHistory
                    {
                        TransactionId = Guid.NewGuid(),
                        ControlNumber = id,
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        Box = (int)Math.Ceiling((double)item.Quantity / pps),
                        ProdVer = item.ProductionVersion,
                        EntryDate = now,
                        TransactionType = transactionType,
                        Location = (item.Location ?? string.Empty).Trim().ToUpper(),
                        Remarks = remarks,
                        StorageLocation = item.StorageLocation ?? "9151",
                        Status = "Active",
                        WhId = normalizedWarehouseId,
                        IsSynced = false,
                        InCharge = userId
                    });
                }

                _context.TransactionHistories.AddRange(transactionItems);

                await _context.SaveChangesAsync();

                await UpdateMovementClassification();

                await transaction.CommitAsync();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                string innerErr = ex.InnerException != null ? $" -> {ex.InnerException.Message}" : string.Empty;
                return (false, $"SQL Error: {ex.Message}{innerErr}");
            }
        }

        public async Task<List<TransactionHistory>> GetItemByShipment(string shipmentID)
        {
            var items = await _context.TransactionHistories
                       .Where(x => x.ControlNumber.Contains(shipmentID))
                       .ToListAsync();
            return items;
        }

        public async Task<List<TransactionHistory>> GetItemByReturn(string ReturnID)
        {
            var items = await _context.TransactionHistories
                        .Include(t => t.ReturnTable)
                        .Where(x => x.ControlNumber.Contains(ReturnID))
                        .ToListAsync();
            return items;
        }

        public async Task<ReturnTable> CheckReturnIdDuplicate(string returnid)
        {
            var item = await _context.ReturnTables.FirstOrDefaultAsync(x => x.TransactionId == returnid);
            return item;
        }

        public async Task<ShipmentTable> CheckShipmentIdDuplicate(string shipmentid)
        {
            var item = await _context.ShipmentTables.FirstOrDefaultAsync(x => x.TransactionId == shipmentid);
            return item;
        }

        public async Task<List<TransactionHistory>> GetFilteredShipment(string shipmentID = null, DateTime? start = null, DateTime? end = null)
        {
            IQueryable<TransactionHistory> query = _context.TransactionHistories.AsQueryable();

            query = query.Where(x => x.ControlNumber.Contains("SHIPID-"));

            if (!string.IsNullOrEmpty(shipmentID))
            {
                query = query.Where(x => x.ControlNumber == shipmentID);
            }
            if (start.HasValue)
            {
                query = query.Where(x => x.EntryDate >= start.Value.Date);
            }

            if (end.HasValue)
            {
                var nextDay = end.Value.Date.AddDays(1);
                query = query.Where(x => x.EntryDate < nextDay);
            }

            var result = await query
                         .GroupBy(x =>  x.ControlNumber)
                         .Select(x => new TransactionHistory
                         {
                             ControlNumber = x.Key,
                             EntryDate = x.Max(x => x.EntryDate),
                             Quantity = x.Sum(x => x.Quantity),
                             Box = x.Sum(x => x.Box),
                             Remarks = x.First().Remarks
                         })
                         .ToListAsync();
            return result;
        }

        public async Task<List<TransactionHistory>> GetShipmentItems(string controlnumber)
        {
            var result = await _context.TransactionHistories
                   .Where(x => x.ControlNumber == controlnumber)
                   .GroupBy(x => new { x.Partnumber, x.ProdDate, x.ProdVer })
                   .Select(x => new TransactionHistory
                   {
                       Partnumber = x.Key.Partnumber,
                       ProdDate = x.Key.ProdDate,
                       ProdVer = x.Key.ProdVer,
                       Quantity = x.Sum(x => x.Quantity),
                       Box = x.Sum(x => x.Box),
                       Remarks = x.First().Remarks
                   })
                   .ToListAsync();
            return result;
        }

        public async Task<(bool isSuccess, string Message)> CancelShipment(string controlnumber, string userid)
        {
            await using var dbTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var shipment = await _context.ShipmentTables.SingleOrDefaultAsync(x => x.TransactionId == controlnumber);
                if (shipment == null)
                {
                    return (false, "Shipment does not exist.");
                }

                if(string.Equals(shipment.Status, "Cancelled",StringComparison.OrdinalIgnoreCase))
                {
                    return (false, "Shipment has already been cancelled.");
                }

                var originalRows = await _context.TransactionHistories
                                        .Where(x => x.ControlNumber == controlnumber)
                                        .ToListAsync();
                if(originalRows.Count == 0)
                {
                    return (false, "No Transaction history found.");
                }

                foreach (var original in originalRows)
                {
                    _context.TransactionHistories.Add(new TransactionHistory
                    {
                        TransactionId = Guid.NewGuid(),
                        Partnumber = original.Partnumber,
                        ProdDate = original.ProdDate,
                        ProdVer = original.ProdVer,
                        CustomerId = original.CustomerId,
                        Quantity = original.Quantity,
                        Box = original.Box,

                        // Copy the original inventory identity
                        WhId = original.WhId,
                        Location = original.Location,
                        StorageLocation = original.StorageLocation,

                        TransactionType =
                            original.TransactionType == "OUT" ? "IN" : "OUT",

                        EntryDate = DateTime.Now,
                        Remarks = "Cancelled Shipment",
                        Status = "Active",
                        InCharge = userid,
                        IsSynced = false,
                        SyncStatus = 0
                    });
                }

                shipment.Status = "Cancelled";
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return (true, "Shipment cancelled successfully.");
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                return (false, $"Cancellation failed: {ex.Message}");
            }

            //try
            //{
            //    var isExist = await _context.ShipmentTables.FirstOrDefaultAsync(x => x.TransactionId == controlnumber);
            //    if (isExist == null)
            //    {
            //        return (false, "Shipment ID is not exist on the record.");
            //    }

            //    isExist.Status = "Cancelled";
            //    var result = await _context.TransactionHistories.Where(x => x.ControlNumber.Equals(controlnumber)).ToListAsync();
            //    if (result.Count == 0)
            //    {
            //        return (false, "No transaction history.");
            //    }

            //    foreach(var transaction in result)
            //    {
            //        var ShipmentItems = new TransactionHistory
            //        {
            //            Partnumber = transaction.Partnumber,
            //            ProdDate = transaction.ProdDate,
            //            CustomerId = transaction.CustomerId,
            //            Quantity = transaction.Quantity,
            //            Box = transaction.Box,
            //            ProdVer = transaction.ProdVer,
            //            EntryDate = DateTime.Now,
            //            Location = transaction.Location,
            //            TransactionType = "IN",
            //            Remarks = "Cancelled Shipment",
            //            Status = "",
            //            StorageLocation = "9151",
            //            ControlNumber = "",
            //            WhId = "WH1",
            //            InCharge = userid,
            //            TransactionId = Guid.NewGuid(),
            //            IsSynced = false,
            //            SyncStatus = 0
            //        };

            //        _context.TransactionHistories.Add(ShipmentItems);
            //    }
            //    await _context.SaveChangesAsync();
            //    return (true, $"Shipment cancelled successfully, Shipment ID: {controlnumber}");
            //}
            //catch(Exception ex)
            //{
            //    return(false, ex.Message);
            //}
        }

        public async Task<List<ReturnTable>> GetFilteredReturn(string location, DateTime? start = null, DateTime? end = null)
        {
            IQueryable<ReturnTable> query = _context.ReturnTables.AsQueryable();

            query = query.Where(x => x.TransactionId.Contains("AS-"));

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(x => x.ToLocation == location);
            }

            if (start.HasValue)
            {
                query = query.Where(x => x.EntryDate >= start.Value.Date);
            }

            if (end.HasValue)
            {
                var nextDay = end.Value.Date.AddDays(1);
                query = query.Where(x => x.EntryDate < nextDay);
            }

            var result = await query
                         .Include(x => x.TransactionHistories)
                         .Select(x => new ReturnTable
                         {
                             TransactionId = x.TransactionId,
                             EntryDate = x.EntryDate,
                             Quantity = x.TransactionHistories.Sum(x => x.Quantity),
                             Box = x.TransactionHistories.Sum(x => x.Box) ?? 0,
                             Remarks = x.Remarks,
                             ToLocation = x.ToLocation,
                             Status = x.Status
                         })
                         .ToListAsync();
            return result;
        }

        public async Task<List<TransactionHistory>> GetReturnItems(string controlnumber)
        {
            var result = await _context.TransactionHistories
                        .Where(x => x.ControlNumber == controlnumber)
                        .GroupBy(x => new { x.Partnumber, x.ProdDate, x.ProdVer })
                        .Select(x => new TransactionHistory
                        {
                            Partnumber = x.Key.Partnumber,
                            ProdDate = x.Key.ProdDate,
                            ProdVer = x.Key.ProdVer,
                            Quantity = x.Sum(x => x.Quantity),
                            Box = x.Sum(x => x.Box),
                            Remarks = x.First().Remarks
                        })
                        .ToListAsync();
            return result;
        }

        public async Task<(bool isSuccess, string Message)> CancelReturn(string controlnumber, string userid)
        {
            await using var dbTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var returns = await _context.ReturnTables.SingleOrDefaultAsync(x => x.TransactionId == controlnumber);
                if (returns == null)
                {
                    return (false, "Warehouse Return does not exist.");
                }

                if (string.Equals(returns.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    return (false, "Warehouse Return has already been cancelled.");
                }

                var originalRows = await _context.TransactionHistories
                                        .Where(x => x.ControlNumber == controlnumber)
                                        .ToListAsync();
                if (originalRows.Count == 0)
                {
                    return (false, "No Transaction history found.");
                }

                foreach (var original in originalRows)
                {
                    _context.TransactionHistories.Add(new TransactionHistory
                    {
                        TransactionId = Guid.NewGuid(),
                        Partnumber = original.Partnumber,
                        ProdDate = original.ProdDate,
                        ProdVer = original.ProdVer,
                        CustomerId = original.CustomerId,
                        Quantity = original.Quantity,
                        Box = original.Box,

                        // Copy the original inventory identity
                        WhId = original.WhId,
                        Location = original.Location,
                        StorageLocation = original.StorageLocation,

                        TransactionType =
                            original.TransactionType == "OUT" ? "IN" : "OUT",

                        EntryDate = DateTime.Now,
                        Remarks = "Cancelled Return",
                        Status = "Active",
                        InCharge = userid,
                        IsSynced = false,
                        SyncStatus = 0
                    });
                }

                returns.Status = "Cancelled";
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return (true, "Warehouse Return cancelled successfully.");
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                return (false, $"Cancellation failed: {ex.Message}");
            }


            //try
            //{
            //    var isExist = await _context.ReturnTables.FirstOrDefaultAsync(x => x.TransactionId == controlnumber);
            //    if (isExist == null)
            //    {
            //        return (false, "Return ID is not exist on the record.");
            //    }

            //    isExist.Status = "Cancelled";

            //    var result = await _context.TransactionHistories.Where(x => x.ControlNumber.Equals(controlnumber)).ToListAsync();

            //    if (result.Count == 0)
            //    {
            //        return (false, "No transaction history.");
            //    }

            //    foreach (var transaction in result)
            //    {
            //        var returnItems = new TransactionHistory
            //        {
            //            Partnumber = transaction.Partnumber,
            //            ProdDate = transaction.ProdDate,
            //            CustomerId = transaction.CustomerId,
            //            Quantity = transaction.Quantity,
            //            Box = transaction.Box,
            //            ProdVer = transaction.ProdVer,
            //            EntryDate = DateTime.Now,
            //            Location = transaction.Location,
            //            TransactionType = "IN",
            //            Remarks = "Cancelled Returns",
            //            Status = "",
            //            StorageLocation = "9151",
            //            ControlNumber = "",
            //            WhId = "WH1",
            //            InCharge = userid,
            //            TransactionId = Guid.NewGuid(),
            //            IsSynced = false,
            //            SyncStatus = 0
            //        };

            //        _context.TransactionHistories.Add(returnItems);
            //    }

            //    await _context.SaveChangesAsync();
            //    return (true, $"Return cancelled successfully, Return ID: {controlnumber}");
            //}
            //catch (Exception ex)
            //{
            //    return (false, ex.Message);
            //}
        }

        public async Task<List<ActualInventory>> GetRackQuantity(string warehouseid)
        {
            var result = await _context.ActualInventories
                        .Where(x => x.WhId == warehouseid)
                        .GroupBy(x => x.Location)
                        .Select(x => new ActualInventory
                        {
                            Location = x.Key,
                            Quantity = x.Sum(x => x.Quantity)
                        })
                        .ToListAsync();
            return result;
        }

        public async Task<string> GetRackCustomer(string location, string warehouseid)
        {
            var result = await _context.ActualInventories.FirstOrDefaultAsync(x => x.Location == location && x.WhId == warehouseid);
            return result?.Customer ?? "";
        }

        public async Task<Dictionary<string, int>> GetRackIds(string warehouseid)
        {
            var rawData = await _context.TransactionHistories
                .Where(x => x.WhId == warehouseid && x.Location != null)
                .Select(x => new { x.Location, x.Id })
                .ToListAsync();

            // 2. Build the dictionary safely in C#
            var result = rawData
                // Trim spaces and force uppercase so "2F1-09 " and "2f1-09" become identical
                .GroupBy(x => x.Location.Trim().ToUpper())
                .ToDictionary(
                    group => group.Key,
                    group => group.Max(x => x.Id)
                );

            return result;
        }

        public async Task<int> GetRackQty(string location, string warehouseid)
        {
            var result = await _context.ActualInventories
                         .Where(x => x.Location == location && x.WhId == warehouseid)
                         .SumAsync(x => x.Quantity);
            return result;
        }

        public async Task<List<ActualInventory>> GetItemByPartnumber(string partnumber, string warehouseid)
        {
            var MappedList = await _context.ActualInventories
                            .Where(x => x.Partnumber == partnumber && x.WhId == warehouseid)
                            .GroupBy(x => x.Location )
                            .Select(x => new ActualInventory
                            {
                                Location = x.Key,
                                Quantity = x.Sum(x => x.Quantity),
                                TotalBox = x.Sum(x => x.TotalBox)
                            })
                            .ToListAsync();

            return MappedList;
        }

        public async Task<List<ActualInventory>> GetItemByLocation(string location, string warehouseid)
        {
            var transactions = await _context.TransactionHistories
                .AsNoTracking()
                .Where(transaction =>
                    transaction.Location == location &&
                    transaction.WhId == warehouseid &&
                    transaction.Quantity > 0 &&
                    (transaction.TransactionType == "IN" || transaction.TransactionType == "OUT"))
                .ToListAsync();

            var partnumbers = transactions
                .Select(transaction => transaction.Partnumber)
                .Distinct()
                .ToList();
            var productPps = await _context.Products
                .AsNoTracking()
                .Where(product => partnumbers.Contains(product.Partnumber))
                .ToDictionaryAsync(product => product.Partnumber, product => product.Pps);

            var movements = transactions.Select(transaction =>
            {
                productPps.TryGetValue(transaction.Partnumber, out int pps);
                string movementCategory = GetLedgerCategory(
                    transaction.TransactionType,
                    transaction.ControlNumber,
                    transaction.Remarks);
                int boxes = InventoryPpsUtility.GetLedgerBoxQuantity(movementCategory, transaction.Box);
                string ppsType = InventoryPpsUtility.GetPpsType(
                    transaction.Quantity,
                    boxes,
                    pps,
                    transaction.Remarks);
                int direction = transaction.TransactionType == "IN" ? 1 : -1;

                return new
                {
                    transaction.Partnumber,
                    transaction.ProdDate,
                    transaction.ProdVer,
                    transaction.CustomerId,
                    transaction.WhId,
                    PpsType = string.IsNullOrWhiteSpace(ppsType) ? "BPPS" : ppsType,
                    Quantity = direction * transaction.Quantity,
                    Boxes = direction * boxes
                };
            });

            return movements
                .GroupBy(movement => new
                {
                    movement.Partnumber,
                    movement.ProdDate,
                    movement.ProdVer,
                    movement.CustomerId,
                    movement.WhId,
                    movement.PpsType
                })
                .Select(group => new ActualInventory
                {
                    Partnumber = group.Key.Partnumber,
                    ProdDate = group.Key.ProdDate,
                    ProdVer = group.Key.ProdVer,
                    Customer = group.Key.CustomerId,
                    Quantity = group.Sum(movement => movement.Quantity),
                    TotalBox = Math.Max(group.Sum(movement => movement.Boxes), 0),
                    Remarks = group.Key.PpsType,
                    WhId = group.Key.WhId
                })
                .Where(item => item.Quantity > 0)
                .OrderBy(item => item.ProdDate)
                .ThenBy(item => item.Partnumber)
                .ThenBy(item => item.Remarks)
                .ToList();
        }

        private string GenerateTransactionNumber()
        {
            var Method = new TransactionRepo();
            int seq = Method.GetNextDocumentId();
            return $"{DateTime.Now:yyMMdd}{seq:D2}";
        }

        public async Task<List<InventoryCardData>> GetInventoryCardDataByLocation(string location, string warehouseid, string userid)
        {
            string CtrlNumber = GenerateTransactionNumber();
            var rawData = await _context.TransactionHistories
                .AsNoTracking()
                .Where(transaction =>
                    transaction.Location == location &&
                    transaction.WhId == warehouseid &&
                    transaction.Quantity > 0 &&
                    (transaction.TransactionType == "IN" || transaction.TransactionType == "OUT"))
                .OrderBy(transaction => transaction.Partnumber)
                .ThenBy(transaction => transaction.ProdDate)
                .ThenBy(transaction => transaction.EntryDate)
                .ToListAsync();

            var partnumbers = rawData
                .Select(transaction => transaction.Partnumber)
                .Distinct()
                .ToList();
            var productInfo = await _context.Products
                .AsNoTracking()
                .Where(product => partnumbers.Contains(product.Partnumber))
                .ToDictionaryAsync(product => product.Partnumber);

            var categorizedMovements = rawData.Select(transaction =>
            {
                productInfo.TryGetValue(transaction.Partnumber, out var product);
                int pps = product?.Pps ?? 0;
                string movementCategory = GetLedgerCategory(
                    transaction.TransactionType,
                    transaction.ControlNumber,
                    transaction.Remarks);
                int boxes = InventoryPpsUtility.GetLedgerBoxQuantity(movementCategory, transaction.Box);
                string ppsType = InventoryPpsUtility.GetPpsType(
                    transaction.Quantity,
                    boxes,
                    pps,
                    transaction.Remarks);
                int direction = transaction.TransactionType == "IN" ? 1 : -1;

                return new
                {
                    transaction.Partnumber,
                    transaction.ProdDate,
                    transaction.ProdVer,
                    transaction.StorageLocation,
                    Pps = pps,
                    ProductId = product?.Id ?? 0,
                    Category = string.IsNullOrWhiteSpace(ppsType) ? "BPPS" : ppsType,
                    Boxes = direction * boxes,
                    Quantity = direction * transaction.Quantity
                };
            }).ToList();

            var categorizedInventory = categorizedMovements
                .GroupBy(movement => new
                {
                    movement.Partnumber,
                    movement.ProdDate,
                    movement.ProdVer,
                    movement.StorageLocation,
                    movement.Pps,
                    movement.ProductId,
                    movement.Category
                })
                .Select(group => new
                {
                    group.Key.Partnumber,
                    group.Key.ProdDate,
                    group.Key.ProdVer,
                    group.Key.StorageLocation,
                    group.Key.Pps,
                    group.Key.ProductId,
                    group.Key.Category,
                    TotalBox = group.Sum(movement => movement.Boxes),
                    Quantity = group.Sum(movement => movement.Quantity)
                })
                .Where(item => item.Quantity > 0)
                .ToList();

            var allCards = categorizedInventory
                           .GroupBy(item => new { item.Partnumber, item.Category })
                           .Select(group =>
                           {
                               var cardRows = group.Select(row => new InventoryRow
                               {
                                   LotNo = row.ProdDate.ToString("MM-dd-yy"),
                                   Boxes = Math.Max(row.TotalBox, 0),
                                   Quantity = row.Category == "Exact PPS" && row.Pps > 0
                                       ? row.Pps
                                       : row.TotalBox > 0
                                           ? row.Quantity / row.TotalBox
                                           : row.Quantity,
                                   CalculatedTotalQuantity = row.Category == "Exact PPS"
                                       ? Math.Max(row.TotalBox, 0) * row.Pps
                                       : row.Quantity
                               }).ToList();

                               int totalBox = cardRows.Sum(x => x.Boxes);
                               int totalQuantity = cardRows.Sum(x => x.TotalQty);
                               int pps = group.First().Pps;
                               int id = group.First().ProductId;
                               


                               return new InventoryCardData
                               {
                                   id = id,
                                   PartNo = group.Key.Partnumber,
                                   Category = group.Key.Category,
                                   ErpLocation = group.First().StorageLocation ?? string.Empty,
                                   MonthYear = DateTime.Now.ToString("yyyy MMMM").ToUpper(),
                                   location = location,
                                   Rows = cardRows,
                                   GrandTotalBoxes = totalBox,
                                   GrandTotalQuantity = totalQuantity,
                                   PPS = pps,
                                   PreparedBy = userid,
                                   ControlNumber = CtrlNumber
                               };
                           })
                           .ToList();
            return allCards;
        }

        public async Task<Dictionary<string, int>> GetStocks(List<ScannedData> data)
        {
            try
            {
                if (data == null || !data.Any())
                    return new Dictionary<string, int>();

                var scanPartnumbers = data
                    .Select(x => x.PartNumber)
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Distinct()
                    .ToList();

                var inventoryList = await _context.ActualInventories
                    .AsNoTracking()
                    .Where(x => scanPartnumbers.Contains(x.Partnumber))
                    .ToListAsync();

                // Standardize key string formatting: ISO Date (yyyy-MM-dd) + Uppercase strings
                var result = inventoryList
                    .GroupBy(x =>
                        $"{x.Partnumber?.Trim().ToUpper()}|" +
                        $"{x.ProdDate:yyyy-MM-dd}|" +
                        $"{x.ProdVer?.Trim().ToUpper()}|" +
                        $"{x.WhId?.Trim().ToUpper()}|" +
                        $"{x.Location?.Trim().ToUpper()}")
                    .ToDictionary(
                        g => g.Key,
                        g => Convert.ToInt32(g.Sum(x => x.Quantity))
                    );

                return result;
            }
            catch (Exception ex)
            {
                // Log ex here if you have a logger instance
                throw new Exception($"Failed to retrieve stock inventory levels: {ex.Message}", ex);
            }
        }

        public async Task<PagedResult<ActualInventory>> GetFilteredInventory(string partnumber = null, int pageNumber = 1, int pageSize = 50)
        {
            var transactionQuery = _context.TransactionHistories
                .AsNoTracking()
                .Where(transaction =>
                    transaction.Quantity > 0 &&
                    (transaction.TransactionType == "IN" || transaction.TransactionType == "OUT"));

            if (!string.IsNullOrWhiteSpace(partnumber))
            {
                string filter = partnumber.Trim();
                transactionQuery = transactionQuery.Where(transaction => transaction.Partnumber.Contains(filter));
            }

            var movements =
                from transaction in transactionQuery
                join product in _context.Products.AsNoTracking()
                    on transaction.Partnumber equals product.Partnumber into products
                from product in products.DefaultIfEmpty()
                let isScanBased = transaction.Remarks == "BPPS" ||
                                  transaction.Remarks == "FG" ||
                                  transaction.ControlNumber.StartsWith("SHIPID-") ||
                                  transaction.ControlNumber.StartsWith("AS-")
                let boxes = isScanBased ? 1 : transaction.Box ?? 0
                let ppsType = transaction.Remarks == "BPPS"
                    ? "BPPS"
                    : product != null && product.Pps > 0 && transaction.Quantity == boxes * product.Pps
                        ? "Exact PPS"
                        : "BPPS"
                let direction = transaction.TransactionType == "IN" ? 1 : -1
                select new
                {
                    transaction.Partnumber,
                    transaction.CustomerId,
                    transaction.ProdDate,
                    transaction.ProdVer,
                    transaction.Location,
                    transaction.StorageLocation,
                    transaction.WhId,
                    PpsType = ppsType,
                    Quantity = direction * transaction.Quantity,
                    Boxes = direction * boxes
                };

            var balances = movements
                .GroupBy(movement => new
                {
                    movement.Partnumber,
                    movement.CustomerId,
                    movement.ProdDate,
                    movement.ProdVer,
                    movement.Location,
                    movement.StorageLocation,
                    movement.WhId,
                    movement.PpsType
                })
                .Select(group => new
                {
                    group.Key.Partnumber,
                    group.Key.CustomerId,
                    group.Key.ProdDate,
                    group.Key.ProdVer,
                    group.Key.Location,
                    group.Key.StorageLocation,
                    group.Key.WhId,
                    group.Key.PpsType,
                    Quantity = group.Sum(movement => movement.Quantity),
                    TotalBox = group.Sum(movement => movement.Boxes)
                })
                .Where(item =>
                    item.Quantity > 0 &&
                    _context.ActualInventories.Any(inventory =>
                        inventory.Quantity > 0 &&
                        inventory.Partnumber == item.Partnumber &&
                        inventory.ProdDate == item.ProdDate &&
                        inventory.ProdVer == item.ProdVer &&
                        inventory.Location == item.Location &&
                        inventory.WhId == item.WhId));

            int totalCount = await balances.CountAsync();

            var pageRows = await balances
                .OrderBy(item => item.Partnumber)
                .ThenBy(item => item.ProdDate)
                .ThenBy(item => item.Location)
                .ThenBy(item => item.PpsType)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagePartnumbers = pageRows.Select(item => item.Partnumber).Distinct().ToList();
            var inventoryMetadata = await _context.ActualInventories
                .AsNoTracking()
                .Where(item => pagePartnumbers.Contains(item.Partnumber))
                .ToListAsync();

            var items = pageRows.Select(row =>
            {
                var metadata = inventoryMetadata.FirstOrDefault(item =>
                    item.Partnumber == row.Partnumber &&
                    item.ProdDate == row.ProdDate &&
                    item.ProdVer == row.ProdVer &&
                    item.Location == row.Location &&
                    item.WhId == row.WhId);

                return new ActualInventory
                {
                    Partnumber = row.Partnumber,
                    Customer = row.CustomerId,
                    ProdDate = row.ProdDate,
                    ProdVer = row.ProdVer,
                    Quantity = row.Quantity,
                    TotalBox = Math.Max(row.TotalBox, 0),
                    Location = row.Location,
                    StorageLocation = row.StorageLocation,
                    WhId = row.WhId,
                    Remarks = row.PpsType,
                    UpdatedDate = metadata?.UpdatedDate,
                    MovementClassification = metadata?.MovementClassification
                };
            }).ToList();

            var result = new PagedResult<ActualInventory>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize
            };

            return result;
        }

        public async Task<PagedResult<ActualInventory>> GetFilteredSlowMovingInventory(string partnumber = null, int pageNumber = 1, int pageSize = 50)
        {
            IQueryable<ActualInventory> query = _context.ActualInventories.AsNoTracking();

            if (!string.IsNullOrEmpty(partnumber))
            {
                query = query.Where(x => x.Partnumber == partnumber);
            }

            int totalCount = await query.Where(x => x.MovementClassification == "SLOW").CountAsync();

            var items = await query
                        .Where(x => x.MovementClassification == "SLOW")
                        .OrderBy(x => x.Partnumber)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            var result = new PagedResult<ActualInventory>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize
            };

            return result;
        }

        public async Task<(bool isSuccess, string Message)> ManualDeduction(TransactionHistory transaction)
        {
            if (transaction == null)
                return (false, "No inventory was supplied.");

            if (transaction.Quantity <= 0)
                return (false, "Deduction quantity must be greater than zero.");

            await using var dbTransaction =
                        await _context.Database.BeginTransactionAsync(
                            IsolationLevel.Serializable);

            try
            {
                var inventory = await _context.ActualInventories
                                        .SingleOrDefaultAsync(x =>
                                        x.Partnumber == transaction.Partnumber &&
                                        x.ProdDate == transaction.ProdDate &&
                                        x.ProdVer == transaction.ProdVer &&
                                        x.Location == transaction.Location &&
                                        x.WhId == transaction.WhId);
                if (inventory == null) return (false, "The inventory record no longer exists.");

                if(transaction.Quantity > inventory.Quantity)
                {
                    return (false, $"Only {inventory.Quantity} pieces are available.");
                }

                var newItem = new TransactionHistory
                {
                    Partnumber = transaction.Partnumber,
                    Location = transaction.Location,
                    ProdDate = transaction.ProdDate,
                    ProdVer = transaction.ProdVer,
                    CustomerId = transaction.CustomerId,
                    Box = transaction.Box,
                    Quantity = transaction.Quantity,
                    StorageLocation = "9151",
                    WhId = transaction.WhId ?? "WH1",
                    TransactionType = "OUT",
                    EntryDate = DateTime.Now,
                    Remarks = transaction.Remarks,
                    Status = "",
                    ControlNumber = "",
                    TransactionId = Guid.NewGuid(),
                    IsSynced = false,
                    InCharge = transaction.InCharge,
                    SyncStatus = 0
                };

                _context.TransactionHistories.Add(newItem);
                await _context.SaveChangesAsync();
                return (true, "Items successfully deducted.");
            }
            catch(Exception e)
            {
                return (false, $"Error: {e.Message}");
            }
        }

        public async Task<List<InventoryReport>> GetInventoryDataAsync()
        {
            try
            {
                var result = await _context.ActualInventories
                            .Where(x => x.Quantity > 0)
                            .GroupBy(x => new { x.Partnumber, x.ProdDate, x.Location, x.ProdVer })
                            .Select(x => new InventoryReport
                            {
                                partnumber = x.Key.Partnumber,
                                customer = x.Max(x => x.Customer).ToString(),
                                proddate = x.Key.ProdDate,
                                prodver = x.Key.ProdVer,
                                location = x.Key.Location,
                                quantity = x.Sum(x => x.Quantity),
                                box = x.Sum(x => x.TotalBox),
                                storagelocation = x.Max(x => x.StorageLocation),
                                updatedInventory = x.Max(x => x.UpdatedDate),
                                classification = x.Max(x => x.MovementClassification)
                            })
                            .ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Models.ShipmentReport>> GetShipmentData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                //var result = await _context.TransactionHistories
                //            .Where(s => s.ControlNumber.Contains("SHIPID") && s.Remarks != "Cancelled Shipment" && s.Status != "Cancelled" && s.EntryDate >= startDate && s.EntryDate <= endDate)
                //            .GroupBy(s => new { s.Partnumber, s.ControlNumber, s.ProdDate, s.ProdVer, s.CustomerId, s.EntryDate})
                //            .Select(async s => new ShipmentReport
                //            {
                //                ControlNumber = s.Key.ControlNumber,
                //                Partnumber = s.Key.Partnumber,
                //                Customer = await _context.ShipmentTables.Where(t => t.TransactionId.Contains(s.Key.ControlNumber)).Select(t => t.Customer).FirstOrDefaultAsync(),
                //                ProdDate = s.Key.ProdDate,
                //                ProdVersion = s.Key.ProdVer,
                //                Quantity = s.Sum(x => x.Quantity),
                //                Box  = s.Sum(x => x.Box) ?? 0,
                //                EntryDate = s.Key.EntryDate,
                //            })
                //            .ToListAsync();

                var groupedData = await _context.TransactionHistories
                                .Where(s => s.ControlNumber.Contains("SHIPID")
                                         && s.Remarks != "Cancelled Shipment"
                                         && s.Status != "Cancelled"
                                         && s.EntryDate >= startDate
                                         && s.EntryDate <= endDate)
                                .GroupBy(s => new { s.Partnumber, s.ControlNumber, s.ProdDate, s.ProdVer, s.CustomerId, s.EntryDate })
                                .Select(s => new
                                {
                                    s.Key.ControlNumber,
                                    s.Key.Partnumber,
                                    s.Key.ProdDate,
                                    ProdVersion = s.Key.ProdVer,
                                    Quantity = s.Sum(x => x.Quantity),
                                    Box = s.Sum(x => x.Box) ?? 0,
                                    s.Key.EntryDate,
                                })
                                .ToListAsync();

                                            // 2. Fetch customers sequentially or perform an in-memory lookup
                                            var result = new List<ShipmentReport>();

                                            foreach (var item in groupedData)
                                            {
                                                var customer = await _context.ShipmentTables
                                                    .Where(t => t.TransactionId.Contains(item.ControlNumber))
                                                    .Select(t => t.Customer)
                                                    .FirstOrDefaultAsync();

                                                result.Add(new ShipmentReport
                                                {
                                                    ControlNumber = item.ControlNumber,
                                                    Partnumber = item.Partnumber,
                                                    Customer = customer,
                                                    ProdDate = item.ProdDate,
                                                    ProdVersion = item.ProdVersion,
                                                    Quantity = item.Quantity,
                                                    Box = item.Box,
                                                    EntryDate = item.EntryDate
                                                });
                                            }

               return result;
            }catch 
            {
                return [];
            }
        }

        public async Task<List<SlowMovingReport>> GetSlowMovingDataAsync()
        {
            try
            {
                var result = await _context.ActualInventories
                            .Where(x => x.Quantity > 0 && x.MovementClassification == "SLOW")
                            .GroupBy(x => new { x.Partnumber, x.ProdDate, x.Location, x.ProdVer })
                            .Select(x => new SlowMovingReport
                            {
                                partnumber = x.Key.Partnumber,
                                customer = x.Max(x => x.Customer).ToString(),
                                proddate = x.Key.ProdDate,
                                prodver = x.Key.ProdVer,
                                location = x.Key.Location,
                                quantity = x.Sum(x => x.Quantity),
                                box = x.Sum(x => x.TotalBox),
                                storagelocation = x.Max(x => x.StorageLocation),
                                updatedInventory = x.Max(x => x.LastOutDate)
                            })
                            .ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }

      
        public Task<StockCardHeader> GetStockLedger(
            string partnumber,
            DateTime startDate,
            DateTime endDate,
            string prodver,
            string warehouseid)
        {
            return GetStockLedgerInternal(partnumber, startDate, endDate, prodver, warehouseid, detailed: false);
        }

        public Task<StockCardHeader> GetDetailedStockLedger(
            string partnumber,
            DateTime startDate,
            DateTime endDate,
            string prodver,
            string warehouseid)
        {
            return GetStockLedgerInternal(partnumber, startDate, endDate, prodver, warehouseid, detailed: true);
        }

        private async Task<StockCardHeader> GetStockLedgerInternal(
            string partnumber,
            DateTime startDate,
            DateTime endDate,
            string prodver,
            string warehouseid,
            bool detailed)
        {
            if (string.IsNullOrWhiteSpace(partnumber) ||
                string.IsNullOrWhiteSpace(prodver) ||
                string.IsNullOrWhiteSpace(warehouseid))
            {
                return null;
            }

            DateTime periodStart = startDate.Date;
            DateTime periodEnd = endDate.Date;
            if (periodEnd < periodStart)
            {
                return null;
            }

            DateTime periodEndExclusive = periodEnd.AddDays(1);
            string normalizedPartnumber = partnumber.Trim();
            string normalizedProdver = prodver.Trim();
            string normalizedWarehouse = warehouseid.Trim();

            try
            {
                var filteredTransactions = _context.TransactionHistories
                    .AsNoTracking()
                    .Where(transaction =>
                        transaction.Partnumber == normalizedPartnumber &&
                        transaction.ProdVer == normalizedProdver &&
                        transaction.WhId == normalizedWarehouse &&
                        transaction.Quantity > 0 &&
                        (transaction.TransactionType == "IN" || transaction.TransactionType == "OUT"));

                int beginningBalance = await filteredTransactions
                    .Where(transaction => transaction.EntryDate < periodStart)
                    .SumAsync(transaction => transaction.TransactionType == "IN"
                        ? transaction.Quantity
                        : -transaction.Quantity);

                int beginningBoxBalance = await filteredTransactions
                    .Where(transaction => transaction.EntryDate < periodStart)
                    .SumAsync(transaction =>
                        transaction.TransactionType == "IN"
                            ? transaction.Remarks == "BPPS" ||
                              transaction.Remarks == "FG" ||
                              transaction.ControlNumber.StartsWith("SHIPID-") ||
                              transaction.ControlNumber.StartsWith("AS-")
                                ? 1
                                : transaction.Box ?? 0
                            : -(transaction.Remarks == "BPPS" ||
                                transaction.Remarks == "FG" ||
                                transaction.ControlNumber.StartsWith("SHIPID-") ||
                                transaction.ControlNumber.StartsWith("AS-")
                                    ? 1
                                    : transaction.Box ?? 0));

                int productPps = await _context.Products
                    .AsNoTracking()
                    .Where(product => product.Partnumber == normalizedPartnumber)
                    .Select(product => product.Pps)
                    .FirstOrDefaultAsync();

                var transactions = await filteredTransactions
                    .Where(transaction =>
                        transaction.EntryDate >= periodStart &&
                        transaction.EntryDate < periodEndExclusive)
                    .OrderBy(transaction => transaction.EntryDate)
                    .ThenBy(transaction => transaction.Id)
                    .Select(transaction => new
                    {
                        transaction.Id,
                        transaction.EntryDate,
                        transaction.TransactionType,
                        transaction.Quantity,
                        transaction.Box,
                        transaction.ControlNumber,
                        transaction.Remarks,
                        transaction.InCharge
                    })
                    .ToListAsync();

                var stockCard = new StockCardHeader
                {
                    PartNumber = normalizedPartnumber,
                    PartName = GetProductPartName(normalizedPartnumber),
                    Customer = GetProductCustomer(normalizedPartnumber)
                };

                var ledgerRows = transactions.Select(transaction =>
                {
                    string category = GetLedgerCategory(
                        transaction.TransactionType,
                        transaction.ControlNumber,
                        transaction.Remarks);
                    int ledgerBoxes = InventoryPpsUtility.GetLedgerBoxQuantity(category, transaction.Box);

                    return new
                    {
                        transaction.Id,
                        transaction.EntryDate,
                        transaction.TransactionType,
                        transaction.Quantity,
                        Boxes = ledgerBoxes,
                        ControlNumber = transaction.ControlNumber ?? string.Empty,
                        Remarks = transaction.Remarks ?? string.Empty,
                        InCharge = transaction.InCharge ?? string.Empty,
                        PpsType = InventoryPpsUtility.GetPpsType(
                            transaction.Quantity,
                            ledgerBoxes,
                            productPps,
                            transaction.Remarks),
                        Category = category
                    };
                });

                var rows = detailed
                    ? ledgerRows.Select(transaction => new
                    {
                        InventoryDate = transaction.EntryDate,
                        ExactTime = transaction.EntryDate,
                        transaction.Id,
                        transaction.Category,
                        transaction.ControlNumber,
                        transaction.Remarks,
                        transaction.InCharge,
                        In = transaction.TransactionType == "IN" ? transaction.Quantity : 0,
                        Out = transaction.TransactionType == "OUT" ? transaction.Quantity : 0,
                        BoxIn = transaction.TransactionType == "IN" ? transaction.Boxes : 0,
                        BoxOut = transaction.TransactionType == "OUT" ? transaction.Boxes : 0,
                        transaction.PpsType
                    })
                    : ledgerRows
                        .GroupBy(transaction => new
                        {
                            TransactionDay = transaction.EntryDate.Date,
                            transaction.Category,
                            transaction.InCharge
                        })
                        .Select(group => new
                        {
                            InventoryDate = group.Key.TransactionDay,
                            ExactTime = group.Min(transaction => transaction.EntryDate),
                            Id = group.Min(transaction => transaction.Id),
                            group.Key.Category,
                            ControlNumber = string.Join(", ", group
                                .Select(transaction => transaction.ControlNumber)
                                .Where(value => !string.IsNullOrWhiteSpace(value))
                                .Distinct(StringComparer.OrdinalIgnoreCase)),
                            Remarks = string.Join(", ", group
                                .Select(transaction => transaction.Remarks)
                                .Where(value => !string.IsNullOrWhiteSpace(value))
                                .Distinct(StringComparer.OrdinalIgnoreCase)),
                            group.Key.InCharge,
                            In = group.Where(transaction => transaction.TransactionType == "IN").Sum(transaction => transaction.Quantity),
                            Out = group.Where(transaction => transaction.TransactionType == "OUT").Sum(transaction => transaction.Quantity),
                            BoxIn = group.Where(transaction => transaction.TransactionType == "IN").Sum(transaction => transaction.Boxes),
                            BoxOut = group.Where(transaction => transaction.TransactionType == "OUT").Sum(transaction => transaction.Boxes),
                            PpsType = InventoryPpsUtility.GetGroupedPpsType(group.Select(transaction => transaction.PpsType))
                        });

                int currentStock = beginningBalance;
                int currentBoxes = beginningBoxBalance;
                foreach (var row in rows.OrderBy(row => row.ExactTime).ThenBy(row => row.Id))
                {
                    int rowBeginningStock = currentStock;
                    int rowBeginningBoxes = currentBoxes;
                    currentStock += row.In - row.Out;
                    currentBoxes += row.BoxIn - row.BoxOut;

                    stockCard.Ledgers.Add(new StockLedger
                    {
                        InventoryDate = row.InventoryDate,
                        BeginningStock = rowBeginningStock,
                        In = row.In,
                        Out = row.Out,
                        RunningStock = currentStock,
                        BoxIn = row.BoxIn,
                        BoxOut = row.BoxOut,
                        BeginningBoxes = rowBeginningBoxes,
                        RunningBoxes = currentBoxes,
                        PpsType = row.PpsType,
                        Category = row.Category,
                        ControlNumber = row.ControlNumber,
                        Incharge = row.InCharge,
                        Remarks = row.Remarks
                    });
                }

                stockCard.EndingStock = currentStock;
                stockCard.EndingBoxes = currentBoxes;
                return stockCard;
            }
            catch
            {
                return null;
            }
        }

        private static string GetLedgerCategory(string transactionType, string controlNumber, string remarks)
        {
            string normalizedControlNumber = controlNumber ?? string.Empty;
            string normalizedRemarks = remarks ?? string.Empty;

            if (normalizedRemarks.StartsWith("Transfer", StringComparison.OrdinalIgnoreCase))
                return "Transfer";

            if (normalizedRemarks.StartsWith("Cancelled", StringComparison.OrdinalIgnoreCase))
                return "Cancellation";

            if (normalizedRemarks.StartsWith("Manual Deduction", StringComparison.OrdinalIgnoreCase) ||
                normalizedRemarks.Equals("Quality Control Testing - OUT", StringComparison.OrdinalIgnoreCase))
                return "Stock Adjustment";

            if (normalizedControlNumber.StartsWith("SHIPID-", StringComparison.OrdinalIgnoreCase))
                return "Shipment";

            if (normalizedControlNumber.StartsWith("AS-", StringComparison.OrdinalIgnoreCase))
                return "Warehouse Return";

            if (normalizedRemarks.Equals("BPPS", StringComparison.OrdinalIgnoreCase))
                return "BPPS";

            if (normalizedRemarks.Equals("FG", StringComparison.OrdinalIgnoreCase))
                return "Finished Goods";

            return transactionType == "IN" ? "Other IN" : "Other OUT";
        }

        public async Task<PagedResult<Product>> GetFilteredProductList(string partnumber = null, int pageNumber = 1, int pageSize = 50)
        {
            IQueryable<Product> query = _context.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(partnumber))
            {
                query = query.Where(x => x.Partnumber == partnumber);
            }

            int totalCount = await query.CountAsync();

            var items = await query
                        .OrderBy(x => x.Partnumber)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            var result = new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize
            };

            return result;
        }
        
        public async Task<List<int>> GetYear()
        {
            try
            {
                var result = await _context.TransactionHistories
                                   .GroupBy(x => x.EntryDate.Year)
                                   .Select(x => x.Key)
                                   .ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<MonthlyInventorySummary>> GetMonthlySummary(int year)
        {
            var report = new List<MonthlyInventorySummary>();
            using var dbContext = new InventoryDbContext();
            try
            {
                var previousStock = await dbContext.TransactionHistories
                    .Where(x => x.EntryDate.Year < year)
                    .SumAsync(x => x.TransactionType == "IN" ? x.Quantity :
                                   x.TransactionType == "OUT" ? -x.Quantity : 0);

                int runningStock = previousStock;

                var currentStockBalance = await dbContext.TransactionHistories
                    .Where(x => x.EntryDate.Year == year)
                    .GroupBy(x => x.EntryDate.Month)
                    .Select(x => new
                    {
                        Month = x.Key,
                        In = x.Where(t => t.TransactionType == "IN").Sum(t => (int?)t.Quantity) ?? 0,
                        Out = x.Where(t => t.TransactionType == "OUT").Sum(t => (int?)t.Quantity) ?? 0
                    })
                    .ToListAsync();

                for (int i = 1; i <= 12; i++)
                {
                    var monthData = currentStockBalance.FirstOrDefault(d => d.Month == i);

                    int totalInForMonth = monthData?.In ?? 0;
                    int totalOutForMonth = monthData?.Out ?? 0;


                    runningStock += totalInForMonth - totalOutForMonth;

                    report.Add(new MonthlyInventorySummary
                    {
                        Month = i,                 // The current month (1-12)
                        In = totalInForMonth,
                        Out = totalOutForMonth,
                        EndingStock = runningStock // The cumulative balance
                    });
                }

                return report;
            }
            catch
            {
                return [];
            }
        }

        public async Task<List<CustomerStock>> GetCustomerStocksAsync()
        {
            var totals = await _context.ActualInventories
                .AsNoTracking()
                .Where(item => item.Customer != null && item.Customer.Trim() != string.Empty)
                .GroupBy(item => item.Customer.Trim())
                .Select(group => new
                {
                    Customer = group.Key,
                    Stock = group.Sum(item => (long)item.Quantity)
                })
                .Where(item => item.Stock > 0)
                .OrderByDescending(item => item.Stock)
                .ToListAsync();

            return totals.Select(item => new CustomerStock
            {
                Customer = item.Customer,
                Stock = item.Stock
            }).ToList();
        }

        public async Task<List<MonthlyShipments>> GetMonthlyShipment(int year)
        {
            var report = new List<MonthlyShipments>();
            using var dbContext = new InventoryDbContext();
            try
            {
                var previousStock = await dbContext.TransactionHistories
                    .Where(x => x.EntryDate.Year < year && x.ControlNumber.Contains("SHIPID-"))
                    .SumAsync(x => x.Quantity);

                int runningStock = previousStock;

                var currentStockBalance = await dbContext.TransactionHistories
                    .Where(x => x.EntryDate.Year == year && x.ControlNumber.Contains("SHIPID-"))
                    .GroupBy(x => x.EntryDate.Month)
                    .Select(x =>  new
                    {
                        Month = x.Key,
                        Quantity = x.Sum(x => x.Quantity)
                    })
                    .ToListAsync();
                int previousMonthQuantity = 0;

                for (int i = 1; i <= 12; i++)
                {
                    var monthData = currentStockBalance.FirstOrDefault(d => d.Month == i);

                    int current = monthData?.Quantity ?? 0;

                   
                    runningStock += current;
                    int change = current - previousMonthQuantity;
                    double changePercent = previousMonthQuantity == 0
                                   ? 0
                                   : (change * 100.0 / previousMonthQuantity);

                    report.Add(new MonthlyShipments
                    {
                        Month = i,                 // The current month (1-12)
                        Out = current,
                        Change = change,
                        ChangePercent = changePercent
                    });
                }

                return report;
            }
            catch
            {
                return [];
            }
        }

        public async Task<List<MonthlyReturns>> GetMonthlyReturns(int year)
        {
            var report = new List<MonthlyReturns>();
            using var dbContext = new InventoryDbContext();
            try
            {
                var previousStock = await dbContext.TransactionHistories
                    .Where(x => x.EntryDate.Year < year && x.ControlNumber.Contains("AS-"))
                    .SumAsync(x => x.Quantity);

                int runningStock = previousStock;

                var currentStockBalance = await dbContext.TransactionHistories
                    .Where(x => x.EntryDate.Year == year && x.ControlNumber.Contains("AS-"))
                    .GroupBy(x => x.EntryDate.Month)
                    .Select(x => new
                    {
                        Month = x.Key,
                        Quantity = x.Sum(x => x.Quantity)
                    })
                    .ToListAsync();
                int previousMonthQuantity = 0;

                for (int i = 1; i <= 12; i++)
                {
                    var monthData = currentStockBalance.FirstOrDefault(d => d.Month == i);

                    int current = monthData?.Quantity ?? 0;


                    runningStock += current;
                    int change = current - previousMonthQuantity;
                    double changePercent = previousMonthQuantity == 0
                                   ? 0
                                   : (change * 100.0 / previousMonthQuantity);

                    report.Add(new MonthlyReturns
                    {
                        Month = i,                 // The current month (1-12)
                        Out = current,
                        Change = change,
                        ChangePercent = changePercent
                    });
                }

                return report;
            }
            catch
            {
                return [];
            }
        }

        public async Task<int> GetSlowMovingItem()
        {
            using var dbContext = new InventoryDbContext();
           
            try
            {
                int count = 0;
                var result = await dbContext.ActualInventories
                            .Where(x => x.MovementClassification == "SLOW" && x.Quantity > 0)
                            .GroupBy(x => x.Partnumber)
                            .Select(x => x.Key)
                            .ToListAsync();
                count = result.Count;

                return count;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<(bool isSuccess, string Message)> DeletePartnumber(int id)
        {
            using var dbcontext = new InventoryDbContext();
            try
            {
                var productinfo = await dbcontext.Products.FindAsync(id);
                if (productinfo == null)
                {
                    return (false, "Partnumber not exist.");
                }

                dbcontext.Remove(productinfo);
                await dbcontext.SaveChangesAsync();
                return (true, "Successfully deleted.");
            }catch (Exception ex)
            {
                string errormessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errormessage += "\nInner Error: " + ex.InnerException.Message;
                }

                return (false, $"Crash Details:\n{errormessage}");
            }
        }
    }
}
