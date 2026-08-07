using FGScanner.Database;
using FGScanner.Forms.DataEntry;
using FGScanner.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly Dbcontext _dbContext;
        public Queries(Dbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> UpdateMovementClassification()
        {
            string Query = "EXEC sp_UpdateInventoryClassification_clean";
            return await _dbContext.Database.ExecuteSqlRawAsync(Query);
        }

        public async Task<Products> GetProductInfo(string partnumber)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.PartNumber == partnumber);
        }

        public async Task<ActualInventory> GetStockInfo(string partnumber, DateTime proddate, string prodversion, string location, string whid)
        {
            try
            {
                var inventory = await _dbContext.ActualInventories
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
            var productPPS = _dbContext.Products.FirstOrDefault(x => x.PartNumber == partnumber);
            return productPPS?.PPS ?? 0;
        }
        public int GetProductID(string partnumber)
        {
            var productID = _dbContext.Products.FirstOrDefault(x => x.PartNumber == partnumber);
            return productID?.Id ?? 0;
        }

        public string GetProductPartName(string partnumber)
        {
            var productPartName = _dbContext.Products.FirstOrDefault(x => x.PartNumber == partnumber);
            return productPartName?.PartName ?? string.Empty;
        }
        public string GetProductCustomer(string partnumber)
        {
            var productCustomer = _dbContext.Products.FirstOrDefault(x => x.PartNumber == partnumber);
            return productCustomer?.CustomerId ?? string.Empty;
        }

        public async Task<List<string>> GetRackLocations(string warehouseid)
        {
            return await _dbContext.RackLocations
                .Where(r => r.WhId == warehouseid)
                .Select(r => r.RackNo)
                .ToListAsync();
        }

        public async Task<List<ActualInventory>> GetActualInventory(string warehouseid, string location)
        {
            return await _dbContext.ActualInventories
                .Where(a => a.WhId == warehouseid && a.Location == location)
                .Select(a => new ActualInventory
                {
                    Partnumber = a.Partnumber,
                    ProdDate = a.ProdDate,
                    CustomerId = a.CustomerId,
                    Quantity = a.Quantity,
                    ProdVer = a.ProdVer,
                    TotalBox = a.TotalBox,
                })
                .ToListAsync();
        }

        public async Task<(bool isSuccess, string Message)> TransferInventoryAsync(string warehouseId, string currLocation, string newLocation, List<ActualInventory> inventories, string userId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (inventories == null || inventories.Count == 0)
                {
                    return (false, "Data is null or empty");
                }

                var partNumbers = inventories.Select(i => i.Partnumber).ToList();
                var allExistingInventory = await _dbContext.ActualInventories
                    .Where(x => partNumbers.Contains(x.Partnumber) && x.Location == currLocation)
                    .ToListAsync();

                foreach (var inventory in inventories)
                {
                    var existingInventory = allExistingInventory
                                            .FirstOrDefault(x => x.Partnumber == inventory.Partnumber && x.ProdDate == inventory.ProdDate);

                    var isExist = await GetProductInfo(inventory.Partnumber);

                    if (isExist == null)
                    {
                        return (false, $"Item {inventory.Partnumber} does not exist in the database.");
                    }

                    if (existingInventory == null)
                    {
                        return (false, $"Item {inventory.Partnumber} does not exist in the current location {currLocation}.");
                    }

                    if (existingInventory.Quantity < inventory.Quantity)
                    {
                        return (false, $"Transfer quantity: {inventory.Quantity} is greater than the actual inventory quantity: {existingInventory.Quantity}.");
                    }

                    var Pullout = new Transaction
                    {
                        Partnumber = inventory.Partnumber,
                        ProdDate = inventory.ProdDate,
                        CustomerId = inventory.CustomerId,
                        Quantity = inventory.Quantity,
                        ProdVer = inventory.ProdVer,
                        Box = inventory.TotalBox,
                        EntryDate = DateTime.Now,
                        Location = currLocation.ToUpper(),
                        WhId = warehouseId,
                        Remarks = "Transfer to " + newLocation,
                        Status = "Active",
                        StorageLocation = existingInventory.StorageLocation,
                        TransactionType = "OUT",
                        IsSynced = false, // (Using your model's spelling)
                        TransactionID = Guid.NewGuid(),
                        InCharge = userId
                    };
                    _dbContext.Transactions.Add(Pullout);


                    var TransferIn = new Transaction
                    {
                        Partnumber = inventory.Partnumber,
                        ProdDate = inventory.ProdDate,
                        CustomerId = inventory.CustomerId,
                        Quantity = inventory.Quantity,
                        ProdVer = inventory.ProdVer,
                        Box = inventory.TotalBox,
                        EntryDate = DateTime.Now,
                        Location = newLocation,
                        WhId = warehouseId,
                        Remarks = "Transfer from " + currLocation,
                        Status = "Active",
                        StorageLocation = existingInventory.StorageLocation,
                        TransactionType = "IN",
                        IsSynced = false,
                        TransactionID = Guid.NewGuid(),
                        InCharge = userId
                    };
                    _dbContext.Transactions.Add(TransferIn);


                } 
                // 2. ONLY AFTER ALL ITEMS ARE PROCESSED, Save and Commit!
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                await UpdateMovementClassification();
                // 3. Finally, exit the method
                return (true, "Transfer successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"SQL Error: {ex.Message}");
            }
        }
        
        public async Task<(bool isSuccess, string Message)> InsertBPPSItems(List<ScannedData> Items, string warehouseId, string userid)
        {
            try
            {
                var TransactionItems = new List<Transaction>();

                foreach (var item in Items)
                {
                    TransactionItems.Add(new Transaction
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
                        TransactionID = Guid.NewGuid(),
                        IsSynced = false,
                        InCharge = userid
                    });
                }

                _dbContext.Transactions.AddRange(TransactionItems);
                await _dbContext.SaveChangesAsync();
                await UpdateMovementClassification();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                return (false, $"SQL Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<Dictionary<string, Products>> GetProductsByPartNumbersAsync(List<string> partNumbers)
        {
            return await _dbContext.Products
                .Where(p => partNumbers.Contains(p.PartNumber))
                .ToDictionaryAsync(p => p.PartNumber);
        }

        public async Task<(bool isSuccess, string Message)> InsertFGItems(List<ScannedData> Items, string warehouseId, string transaction_type, string userid)
        {
            try
            {
                var TransactionItems = new List<Transaction>();
                var scannedPartNumbers = Items.Select(x => x.PartNumber).Distinct().ToList();
                var productDict =  await _dbContext.Products
                                   .Where(p => scannedPartNumbers.Contains(p.PartNumber))
                                   .ToDictionaryAsync(p => p.PartNumber, p => p.PPS);



                foreach (var item in Items)
                {
                    int pps = 1;
                    if (productDict.TryGetValue(item.PartNumber, out int dbPps) && dbPps > 0)
                    {
                        pps = dbPps;
                    }

                    TransactionItems.Add(new Transaction
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
                        TransactionID = Guid.NewGuid(),
                        IsSynced = false,
                        InCharge = userid
                    });
                }

                _dbContext.Transactions.AddRange(TransactionItems);
                await _dbContext.SaveChangesAsync();
                await UpdateMovementClassification();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                return (false, $"SQL Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<ActualInventory> CheckIfExist(string partnumber, string location, DateTime proddate)
        {
            return await _dbContext.ActualInventories.Where(x => x.Partnumber ==  partnumber && x.Location == location && x.ProdDate == proddate).FirstOrDefaultAsync();
        }

        public async Task<(bool isSuccess, string Message)> InsertFGOutgoingItems(List<ScannedData> Items, string warehouseId, string id, string transaction_type, string userid, string remarks)
        {
            try
            {
                var TransactionItems = new List<Transaction>();

                var newShipment = new Shipment
                {
                    TransactionID = id,
                    EntryDate = DateTime.Now,
                    WhId = warehouseId,
                    Status = "",
                    ShipmentID = Guid.NewGuid(),
                    IsSynced = false,
                    SyncStatus = 0
                };

                 _dbContext.Shipments.Add(newShipment);

                foreach (var item in Items)
                {
                    TransactionItems.Add(new Transaction
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        Box = 1,
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.Now,
                        TransactionType = transaction_type,
                        Location = item.Location.ToUpper(),
                        Remarks = remarks,
                        StorageLocation = item.StorageLocation ?? "9151",
                        Status = "",
                        WhId = warehouseId,
                        controlNumber = id,
                        TransactionID = Guid.NewGuid(),
                        IsSynced = false,
                        InCharge = userid
                    });
                }

                _dbContext.Transactions.AddRange(TransactionItems);
                await _dbContext.SaveChangesAsync();
                await UpdateMovementClassification();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                return (false, $"SQL Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        public async Task<(bool isSuccess, string Message)> InsertReturnItems(List<ScannedData> Items, string warehouseId, string id, string transaction_type, string userid, string remarks, string location)
        {
            try
            {
                var TransactionItems = new List<Transaction>();

                var newReturn = new Return
                {
                    TransactionID = id,
                    EntryDate = DateTime.Now,
                    WhId = warehouseId,
                    From = "9151",
                    To = location.ToUpper(),
                    ReturnID = Guid.NewGuid(),
                    Remarks = remarks,
                    IsSynced = false,
                    SyncStatus = 0
                };

                _dbContext.Returns.Add(newReturn);

                foreach (var item in Items)
                {
                    TransactionItems.Add(new Transaction
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        Box = 1,
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.Now,
                        TransactionType = transaction_type,
                        Location = item.Location.ToUpper(),
                        Remarks = remarks,
                        StorageLocation = location ?? item.StorageLocation,
                        Status = "",
                        WhId = warehouseId,
                        controlNumber = id,
                        TransactionID = Guid.NewGuid(),
                        IsSynced = false,
                        InCharge = userid
                    });
                }

                _dbContext.Transactions.AddRange(TransactionItems);
                await _dbContext.SaveChangesAsync();
                await UpdateMovementClassification();
                return (true, "Items successfully inserted to database.");
            }
            catch (Exception ex)
            {
                return (false, $"SQL Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<List<Transaction>> GetItemByShipment(string shipmentID)
        {
            var items = await _dbContext.Transactions
                        .Where(x => x.controlNumber.Contains(shipmentID))
                        .ToListAsync();
            return items;
        }

        public async Task<List<Transaction>> GetItemByReturn(string ReturnID)
        {
            var items = await _dbContext.Transactions
                        .Include(x => x.Returns)
                        .Where(x => x.controlNumber == ReturnID)
                        .ToListAsync();
            return items;
        }

        public async Task<Return> CheckReturnIdDuplicate(string returnid)
        {
            var item = await _dbContext.Returns.FirstOrDefaultAsync(x => x.TransactionID == returnid);
            return item;
        }

        public async Task<Shipment> CheckShipmentIdDuplicate(string shipmentid)
        {
            var item = await _dbContext.Shipments.FirstOrDefaultAsync(x => x.TransactionID == shipmentid);
            return item;
        }

        public async Task<List<Transaction>> GetFilteredShipment(string shipmentID = null, DateTime? start = null, DateTime? end = null)
        {
            IQueryable<Transaction> query = _dbContext.Transactions.AsQueryable();

            query = query.Where(x => x.controlNumber.Contains("SHIPID-"));

            if (!string.IsNullOrEmpty(shipmentID))
            {
                query = query.Where(x => x.controlNumber == shipmentID);
            }
            if (start.HasValue)
            {
                query = query.Where(x => x.EntryDate >= start.Value);
            }

            if (end.HasValue)
            {
                query = query.Where(x => x.EntryDate <= end.Value);
            }

            var result = await query
                         .GroupBy(x =>  x.controlNumber)
                         .Select(x => new Transaction
                         {
                             controlNumber = x.Key,
                             EntryDate = x.Max(x => x.EntryDate),
                             Quantity = x.Sum(x => x.Quantity),
                             Box = x.Sum(x => x.Box),
                             Remarks = x.First().Remarks
                         })
                         .ToListAsync();
            return result;
        }

        public async Task<List<Transaction>> GetShipmentItems(string controlnumber)
        {
            var result = await _dbContext.Transactions
                        .Where(x => x.controlNumber == controlnumber)
                        .GroupBy(x => new { x.Partnumber, x.ProdDate, x.ProdVer })
                        .Select(x => new Transaction
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
            try
            {
                var isExist = await _dbContext.Shipments.FirstOrDefaultAsync(x => x.TransactionID == controlnumber);
                if (isExist == null)
                {
                    return (false, "Shipment ID is not exist on the record.");
                }

                isExist.Status = "Cancelled";

                var result = await _dbContext.Transactions.Where(x => x.controlNumber.Equals(controlnumber)).ToListAsync();

                if(result.Count == 0)
                {
                    return (false, "No transaction history.");
                }

                foreach(var transaction in result)
                {
                    var ShipmentItems = new Transaction
                    {
                        Partnumber = transaction.Partnumber,
                        ProdDate = transaction.ProdDate,
                        CustomerId = transaction.CustomerId,
                        Quantity = transaction.Quantity,
                        Box = transaction.Box,
                        ProdVer = transaction.ProdVer,
                        EntryDate = DateTime.Now,
                        Location = transaction.Location,
                        TransactionType = "IN",
                        Remarks = "Cancelled Shipment",
                        Status = "",
                        StorageLocation = "9151",
                        controlNumber = "",
                        WhId = "WH1",
                        InCharge = userid,
                        TransactionID = Guid.NewGuid(),
                        IsSynced = false,
                        SyncStatus = 0
                    };

                    _dbContext.Transactions.Add(ShipmentItems);
                }
                await _dbContext.SaveChangesAsync();
                return (true, $"Shipment cancelled successfully, Shipment ID: {controlnumber}");
            }
            catch(Exception ex)
            {
                return(false, ex.Message);
            }
        }

        public async Task<List<Return>> GetFilteredReturn(string location, DateTime? start = null, DateTime? end = null)
        {
            IQueryable<Return> query = _dbContext.Returns.AsQueryable();

            query = query.Where(x => x.TransactionID.Contains("AS-"));

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(x => x.To == location);
            }

            if (start.HasValue)
            {
                query = query.Where(x => x.EntryDate >= start.Value);
            }

            if (end.HasValue)
            {
                query = query.Where(x => x.EntryDate <= end.Value);
            }

            var result = await query
                         .Include(x => x.Transactions)
                         .Select(x => new Return
                         {
                             TransactionID = x.TransactionID,
                             EntryDate = x.EntryDate,
                             Quantity = x.Transactions.Sum(x => x.Quantity),
                             Box = x.Transactions.Sum(x => x.Box),
                             Remarks = x.Remarks,
                             To = x.To,
                             Status = x.Status
                         })
                         .ToListAsync();
            return result;
        }

        public async Task<List<Transaction>> GetReturnItems(string controlnumber)
        {
            var result = await _dbContext.Transactions
                        .Where(x => x.controlNumber == controlnumber)
                        .GroupBy(x => new { x.Partnumber, x.ProdDate, x.ProdVer })
                        .Select(x => new Transaction
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
            try
            {
                var isExist = await _dbContext.Returns.FirstOrDefaultAsync(x => x.TransactionID == controlnumber);
                if (isExist == null)
                {
                    return (false, "Return ID is not exist on the record.");
                }

                isExist.Status = "Cancelled";

                var result = await _dbContext.Transactions.Where(x => x.controlNumber.Equals(controlnumber)).ToListAsync();

                if (result.Count == 0)
                {
                    return (false, "No transaction history.");
                }

                foreach (var transaction in result)
                {
                    var returnItems = new Transaction
                    {
                        Partnumber = transaction.Partnumber,
                        ProdDate = transaction.ProdDate,
                        CustomerId = transaction.CustomerId,
                        Quantity = transaction.Quantity,
                        Box = transaction.Box,
                        ProdVer = transaction.ProdVer,
                        EntryDate = DateTime.Now,
                        Location = transaction.Location,
                        TransactionType = "IN",
                        Remarks = "Cancelled Returns",
                        Status = "",
                        StorageLocation = "9151",
                        controlNumber = "",
                        WhId = "WH1",
                        InCharge = userid,
                        TransactionID = Guid.NewGuid(),
                        IsSynced = false,
                        SyncStatus = 0
                    };

                    _dbContext.Transactions.Add(returnItems);
                }

                await _dbContext.SaveChangesAsync();
                return (true, $"Return cancelled successfully, Return ID: {controlnumber}");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<ActualInventory>> GetRackQuantity(string warehouseid)
        {
            var result = await _dbContext.ActualInventories
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
            var result = await _dbContext.ActualInventories.FirstOrDefaultAsync(x => x.Location == location && x.WhId == warehouseid);
            return result?.CustomerId ?? "";
        }

        public async Task<Dictionary<string, int>> GetRackIds(string warehouseid)
        {
            var rawData = await _dbContext.Transactions
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
            var result = await _dbContext.ActualInventories
                         .Where(x => x.Location == location && x.WhId == warehouseid)
                         .SumAsync(x => x.Quantity);
            return result;
        }

        public async Task<List<ActualInventory>> GetItemByPartnumber(string partnumber, string warehouseid)
        {
            var MappedList = await _dbContext.ActualInventories
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
            var MappedList = await _dbContext.ActualInventories
                            .Where(x => x.Location == location && x.WhId == warehouseid)
                            .GroupBy(x => new { x.Partnumber, x.ProdDate, x.ProdVer })
                            .Select(x => new ActualInventory
                            {
                                Partnumber = x.Key.Partnumber,
                                ProdDate = x.Key.ProdDate,
                                ProdVer = x.Key.ProdVer,
                                CustomerId = x.First().CustomerId,
                                Quantity = x.Sum(x => x.Quantity),
                                TotalBox = x.Sum(x => x.TotalBox),
                                WhId = x.Max(x => x.WhId)
                            })
                            .OrderBy(x => x.ProdDate)
                            .ToListAsync();

            return MappedList;
        }

        public async Task<List<InventoryCardData>> GetInventoryCardDataByLocation(string location, string warehouseid, string userid)
        {
            var rawData = await _dbContext.ActualInventories
                            .Where(x => x.Location == location && x.WhId == warehouseid)
                            .OrderBy(x => x.Partnumber)
                            .ToListAsync();
            var allCards = rawData
                           .GroupBy(item => item.Partnumber)
                           .Select(group =>
                           {
                               var cardRows = group.Select(row => new InventoryRow
                               {
                                   LotNo = row.ProdDate.ToString("MM-dd-yy"),
                                   Boxes = row.TotalBox,
                                   Quantity = GetProductPPS(row.Partnumber)
                               }).ToList();

                               int totalBox = cardRows.Sum(x => x.Boxes);
                               int totalQuantity = cardRows.Sum(x => x.TotalQty);
                               int pps = GetProductPPS(group.Key);
                               int id = GetProductID(group.Key);

                               return new InventoryCardData
                               {
                                   id = id,
                                   PartNo = group.Key,
                                   ErpLocation = group.First().StorageLocation ?? string.Empty,
                                   MonthYear = DateTime.Now.ToString("yyyy MMMM").ToUpper(),
                                   location = location,
                                   Rows = cardRows,
                                   GrandTotalBoxes = totalBox,
                                   GrandTotalQuantity = totalQuantity,
                                   PPS = pps,
                                   PreparedBy = userid
                               };
                           })
                           .ToList();
            return allCards;
        }

        public async Task<Dictionary<string, int>> GetStocks(List<ScannedData> data)
        {
            try
            {
                var scanPartnumbers = data.Select(x => x.PartNumber).Distinct().ToList();

                var inventoryList = await _dbContext.ActualInventories
                                 .Where(x => scanPartnumbers.Contains(x.Partnumber))
                                 .ToListAsync();
                var result = inventoryList
                            .GroupBy(x => $"{x.Partnumber}|{x.ProdDate}|{x.ProdVer}|{x.WhId}|{x.Location}")
                            .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
                return result;
            }
            catch
            {
                return new Dictionary<string, int>();
            }
        }

        public async Task<PagedResult<ActualInventory>> GetFilteredInventory(string partnumber = null, int pageNumber = 1, int pageSize = 50)
        {
            IQueryable<ActualInventory> query = _dbContext.ActualInventories.AsNoTracking();

            if (!string.IsNullOrEmpty(partnumber))
            {
                query = query.Where(x => x.Partnumber.Contains(partnumber));
            }

            int totalCount = await query.CountAsync();

            var items = await query
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

        public async Task<PagedResult<ActualInventory>> GetFilteredSlowMovingInventory(string partnumber = null, int pageNumber = 1, int pageSize = 50)
        {
            IQueryable<ActualInventory> query = _dbContext.ActualInventories.AsNoTracking();

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

        public async Task<(bool isSuccess, string Message)> ManualDeduction(Transaction transaction)
        {
            try
            {
                if(transaction ==  null)
                {
                    return (false, "No invntory to deduct");
                }

                var newItem = new Transaction
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
                    controlNumber = "",
                    TransactionID = Guid.NewGuid(),
                    IsSynced = false,
                    InCharge = transaction.InCharge,
                    SyncStatus = 0
                };

                _dbContext.Transactions.Add(newItem);
                await _dbContext.SaveChangesAsync();
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
                var result = await _dbContext.ActualInventories
                            .Where(x => x.Quantity > 0)
                            .GroupBy(x => new { x.Partnumber, x.ProdDate, x.Location, x.ProdVer })
                            .Select(x => new InventoryReport
                            {
                                partnumber = x.Key.Partnumber,
                                customer = x.Max(x => x.CustomerId).ToString(),
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

        public async Task<List<ShipmentReport>> GetShipmentData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var result = await _dbContext.Transactions
                            .Where(s => s.controlNumber.Contains("SHIPID") && s.Remarks != "Cancelled Shipment" && s.Status != "Cancelled" && s.EntryDate >= startDate && s.EntryDate <= endDate)
                            .GroupBy(s => new { s.Partnumber, s.controlNumber, s.ProdDate, s.ProdVer, s.CustomerId, s.EntryDate })
                            .Select(s => new ShipmentReport
                            {
                                ControlNumber = s.Key.controlNumber,
                                Partnumber = s.Key.Partnumber,
                                Customer = s.Key.CustomerId,
                                ProdDate = s.Key.ProdDate,
                                ProdVersion = s.Key.ProdVer,
                                Quantity = s.Sum(x => x.Quantity),
                                Box  = s.Sum(x => x.Box),
                                EntryDate = s.Key.EntryDate,
                            })
                            .ToListAsync();
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
                var result = await _dbContext.ActualInventories
                            .Where(x => x.Quantity > 0 && x.MovementClassification == "SLOW")
                            .GroupBy(x => new { x.Partnumber, x.ProdDate, x.Location, x.ProdVer })
                            .Select(x => new SlowMovingReport
                            {
                                partnumber = x.Key.Partnumber,
                                customer = x.Max(x => x.CustomerId).ToString(),
                                proddate = x.Key.ProdDate,
                                prodver = x.Key.ProdVer,
                                location = x.Key.Location,
                                quantity = x.Sum(x => x.Quantity),
                                box = x.Sum(x => x.TotalBox),
                                storagelocation = x.Max(x => x.StorageLocation),
                                updatedInventory = x.Max(x => x.Last_Out_Date)
                            })
                            .ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }

      
        public async Task<StockCardHeader> GetStockLedger(string partnumber, DateTime startDate, DateTime endDate, string prodver, string warehouseid)
        {
            try
            {
                var beginningBalance = await _dbContext.Transactions
                    .Where(t => t.Partnumber == partnumber && t.EntryDate < startDate && t.ProdVer == prodver && t.WhId == warehouseid)
                    .SumAsync(t => t.TransactionType == "IN" ? t.Quantity : t.TransactionType == "OUT" ? -t.Quantity : 0);

                var dailyTransaction = await _dbContext.Transactions
                    .Where(t => t.Partnumber == partnumber
                             && t.ProdVer == prodver
                             && t.EntryDate >= startDate
                             && t.EntryDate < endDate.AddDays(1)
                             && t.Quantity > 0
                             && t.WhId == warehouseid)
                    .GroupBy(t => new { TransactionDate = t.EntryDate.Date, t.InCharge, t.Remarks })
                    .Select(t => new
                    {
                        TransactionDay = t.Key.TransactionDate,
                        ExactTime = t.Max(x => x.EntryDate),
                        Incharge = t.Key.InCharge,
                        TotalIN = t.Where(x => x.TransactionType == "IN").Sum(x => x.Quantity),
                        TotalOUT = t.Where(x => x.TransactionType == "OUT" && (
                                   x.controlNumber.Contains("AS-") ||
                                   x.controlNumber.Contains("SHIP-") ||
                                   x.Remarks.Contains("Transfer to") ||
                                   x.Remarks.Contains("Transfer from") ||
                                   x.Remarks.Contains("Cancelled") ||
                                   x.Remarks == "Manual Deduction - Excess Scan" ||
                                   x.Remarks == "Manual Deduction - Damaged Goods" ||
                                   x.Remarks == "Quality Control Testing - OUT" ||
                                   x.Remarks == "Manual Deduction - Cycle Count Adjustment"
                        )).Sum(x => x.Quantity),
                        OutTransaction = t.Where(x => x.TransactionType == "OUT").FirstOrDefault(),
                        InTransaction = t.Where(x => x.TransactionType == "IN").FirstOrDefault(),
                    })
                    .OrderBy(x => x.TransactionDay)
                    .ThenBy(x => x.ExactTime)
                    .ToListAsync();
                
                var stockCard = new StockCardHeader
                {
                    PartNumber = partnumber,
                    PartName = GetProductPartName(partnumber),
                    Customer = GetProductCustomer(partnumber),
                    Ledgers = new List<StockLedger>()
                };

                int currentStock = beginningBalance;

                foreach (var item in dailyTransaction)
                {
                    int startingStockForDay = currentStock;
                    currentStock += (item.TotalIN - item.TotalOUT);

                    string finalRemarks = "";

                    if (item.OutTransaction != null)
                    {
                        finalRemarks = item.OutTransaction.Remarks ?? string.Empty;
                    }

                    if (item.InTransaction != null)
                    {
                        string inRemarks = item.InTransaction.Remarks ?? string.Empty;

                        if (inRemarks.StartsWith("Transfer") ||
                            inRemarks.StartsWith("Cancelled Returns") ||
                            inRemarks.StartsWith("Cancelled Shipment"))
                        {
                            finalRemarks = inRemarks;
                        }
                    }

                  
                    stockCard.Ledgers.Add(new StockLedger
                    {
                        InventoryDate = item.TransactionDay,
                        BeginningStock = startingStockForDay, 
                        In = item.TotalIN,
                        Out = item.TotalOUT,
                        RunningStock = currentStock,
                        Incharge = item.Incharge ?? string.Empty,
                        Remarks = finalRemarks
                        
                    });
                }

               
                stockCard.EndingStock = currentStock;

               
                return stockCard;
            }
            catch
            {
                return null;
            }
        }

        public async Task<PagedResult<Products>> GetFilteredProductList(string partnumber = null, int pageNumber = 1, int pageSize = 50)
        {
            IQueryable<Products> query = _dbContext.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(partnumber))
            {
                query = query.Where(x => x.PartNumber == partnumber);
            }

            int totalCount = await query.CountAsync();

            var items = await query
                        .OrderBy(x => x.PartNumber)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            var result = new PagedResult<Products>
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
                var result = await _dbContext.Transactions
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
            using var dbContext = new Dbcontext();
            try
            {
                var previousStock = await dbContext.Transactions
                    .Where(x => x.EntryDate.Year < year)
                    .SumAsync(x => x.TransactionType == "IN" ? x.Quantity :
                                   x.TransactionType == "OUT" ? -x.Quantity : 0);

                int runningStock = previousStock;

                var currentStockBalance = await dbContext.Transactions
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
            try
            {
                using var dbContext = new Dbcontext();
                var stocks = new List<CustomerStock>();
                var result = await dbContext.Transactions
                                  .GroupBy(x => x.CustomerId)
                                  .Select(x => new
                                  {
                                      Customer = x.Key,
                                      TotalIn = x.Where(x => x.TransactionType == "IN").Sum(x => x.Quantity),
                                      TotalOUT = x.Where(x => x.TransactionType == "OUT").Sum(x => x.Quantity)
                                  })
                                  .ToListAsync();
                foreach(var item in result)
                {
                    int totalStock = item.TotalIn - item.TotalOUT;
                    stocks.Add(new CustomerStock
                    {
                        Customer = item.Customer,
                        Stock = totalStock
                    });
                }

                return stocks;
            }
            catch
            {
                return [];
            }
        }

        public async Task<List<MonthlyShipments>> GetMonthlyShipment(int year)
        {
            var report = new List<MonthlyShipments>();
            using var dbContext = new Dbcontext();
            try
            {
                var previousStock = await dbContext.Transactions
                    .Where(x => x.EntryDate.Year < year && x.controlNumber.Contains("SHIPID-"))
                    .SumAsync(x => x.Quantity);

                int runningStock = previousStock;

                var currentStockBalance = await dbContext.Transactions
                    .Where(x => x.EntryDate.Year == year && x.controlNumber.Contains("SHIPID-"))
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
            using var dbContext = new Dbcontext();
            try
            {
                var previousStock = await dbContext.Transactions
                    .Where(x => x.EntryDate.Year < year && x.controlNumber.Contains("AS-"))
                    .SumAsync(x => x.Quantity);

                int runningStock = previousStock;

                var currentStockBalance = await dbContext.Transactions
                    .Where(x => x.EntryDate.Year == year && x.controlNumber.Contains("AS-"))
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
            using var dbContext = new Dbcontext();
           
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
    }
}
