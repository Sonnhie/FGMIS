using FGScanner.Database;
using FGScanner.DTOs;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FGScanner.Services.Classes
{
    public class TransactionService : ITransactionInterface
    {
        private readonly InventoryDbDevContext context;

        public TransactionService(InventoryDbDevContext _context)
        {
            this.context = _context;
        }


        public async Task<List<string>> GetPartNumberList()
        {
            try
            {
                var partnumbers = await context.Products.Select(p => p.Partnumber).ToListAsync();
                return partnumbers;
            }
            catch
            {
                return [];
            }
        }
        public async Task<Dictionary<string, int?>> GetPPS()
        {
            try
            {
                var PPSList = await context.Products
                                .Select(p => new
                                {
                                    p.Partnumber,
                                    p.Pps
                                })
                                .ToDictionaryAsync(p => p.Partnumber, p => p.Pps);
                return PPSList;
            }
            catch
            {
                return [];
            }
        }

        public async Task<List<string>> GetShipmentControlNumber()
        {
            try
            {
                var shipmentIds = await context.ShipmentTables
                                   .Select(s => s.TransactionId).ToListAsync();
                return shipmentIds;
            }
            catch
            {
                return [];
            }
        }

        public async Task<List<string>> GetRackLocationsAsync(string warehouseId)
        {
            try
            {
                var RackLocations = await context.RackTables.Where(r => r.WhId == warehouseId).Select(r => r.RackNo).ToListAsync();
                return RackLocations;
            }
            catch
            {
                return [];
            }

            //return await _queries.GetRackLocations(warehouseId);
        }

        public async Task<List<InventoryDto>> GetActualInventories(string warehouseid, string location)
        {
            try
            {
                var ActualInventory = await context.ActualInventories
                                            .Where(a => a.WhId.Contains(warehouseid) && a.Location == location)
                                            .Select(a => new InventoryDto
                                            {
                                                Partnumber = a.Partnumber,
                                                CustomerId = a.Customer,
                                                ProductionDate = a.ProdDate,
                                                ProductionVersion = a.ProdVer,
                                                Box = a.TotalBox,
                                                Quantity = a.Quantity
                                            })
                                            .ToListAsync();
                return ActualInventory;
            }
            catch
            {
                return [];
            }
        }

        public async Task<List<InventoryDto>> GetStocks(string warehouseid)
        {
            try
            {
                var ActualInventory = await context.ActualInventories
                                            .Where(a => a.WhId.Contains(warehouseid))
                                            .Select(a => new InventoryDto
                                            {
                                                Partnumber = a.Partnumber,
                                                CustomerId = a.Customer,
                                                ProductionDate = a.ProdDate,
                                                ProductionVersion = a.ProdVer,
                                                Box = a.TotalBox,
                                                Quantity = a.Quantity
                                            })
                                            .ToListAsync();
                return ActualInventory;
            }
            catch
            {
                return [];
            }
        }

        public async Task<ServiceResponseDto> SaveScannedItemsAsync(List<ActualInventory> scannedItems, string currentLocation, string newLocation, string warehouseId, string location, string userId)
        {
            try
            {
                using var Transaction = await context.Database.BeginTransactionAsync();
                if (scannedItems == null || scannedItems.Count == 0)
                {
                    return new ServiceResponseDto
                    {
                        Success = false,
                        Message = "No items to transfer."
                    };
                }

                var scannedPartnumber = scannedItems.Select(p => p.Partnumber).ToList();
                var inventoryExist = await context.ActualInventories
                                     .Where(p => scannedPartnumber.Contains(p.Partnumber) && p.Location == currentLocation && p.WhId == warehouseId)
                                     .ToListAsync();

                foreach (var item in scannedItems)
                {
                    var existingInventory = inventoryExist
                        .FirstOrDefault(x => x.Partnumber == item.Partnumber && x.ProdDate == item.ProdDate);

                    if (existingInventory == null)
                    {
                        return new ServiceResponseDto
                        {
                            Success = false,
                            Message = $"Item {item.Partnumber} does not exist in the current location {currentLocation}."
                        };
                    }

                    if (existingInventory.Quantity < item.Quantity)
                    {
                        return new ServiceResponseDto
                        {
                            Success = false,
                            Message = $"Transfer quantity: {item.Quantity} is greater than the actual inventory quantity: {existingInventory.Quantity}."
                        };
                    }

                    existingInventory.Quantity -= item.Quantity;

                    var Pullout = new TransactionHistory
                    {
                        Partnumber = item.Partnumber,
                        ProdDate = item.ProdDate,
                        CustomerId = item.Customer,
                        Quantity = item.Quantity,
                        ProdVer = item.ProdVer,
                        Box = item.TotalBox,
                        EntryDate = DateTime.Now,
                        Location = currentLocation.ToUpper(),
                        WhId = warehouseId,
                        Remarks = "Transfer to " + newLocation,
                        Status = "Active",
                        StorageLocation = existingInventory.StorageLocation,
                        TransactionType = "OUT",
                        IsSynced = false, // (Using your model's spelling)
                        TransactionId = Guid.NewGuid(),
                        InCharge = userId,
                        ControlNumber = "--"
                    };
                    context.TransactionHistories.Add(Pullout);

                    var PushIn = new TransactionHistory
                    {
                        Partnumber = item.Partnumber,
                        ProdDate = item.ProdDate,
                        CustomerId = item.Customer,
                        Quantity = item.Quantity,
                        ProdVer = item.ProdVer,
                        Box = item.TotalBox,
                        EntryDate = DateTime.Now,
                        Location = currentLocation.ToUpper(),
                        WhId = warehouseId,
                        Remarks = "Transfer from " + currentLocation,
                        Status = "Active",
                        StorageLocation = existingInventory.StorageLocation,
                        TransactionType = "IN",
                        IsSynced = false, // (Using your model's spelling)
                        TransactionId = Guid.NewGuid(),
                        InCharge = userId,
                        ControlNumber = "--"
                    };
                    context.TransactionHistories.Add(PushIn);

                }


                await context.SaveChangesAsync();
                await Transaction.CommitAsync();
               
                return new ServiceResponseDto
                {
                    Success = true,
                    Message = "Items trans successfully."
                };
            }
            catch(Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Error: " + ex.InnerException.Message;
                }

                return new ServiceResponseDto
                {
                    Success = false,
                    Message = $"Crash Details:\n{errorMessage}
                };
            }
        }

        public async Task<ServiceResponseDto> InsertBPPS(List<ScannedItemDto> ScanItem, string warehouseid, string userid)
        {
            try
            {
                if (ScanItem == null || ScanItem.Count == 0)
                {
                    return new ServiceResponseDto
                    {
                        Success = false,
                        Message = "No Scanned items."
                    };
                }

                var AllpartnumberExist = new HashSet<string>(await GetPartNumberList());
                HashSet<string> ErrorPartnumber = [];

                foreach (var item in ScanItem)
                {
                    var isExist = AllpartnumberExist.Contains(item.PartNumber);
                    if (!isExist)
                    {
                        ErrorPartnumber.Add(item.PartNumber);
                        continue;
                    }

                    var BppsItem = new TransactionHistory
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.UtcNow,
                        TransactionType = "IN",
                        Location = item.Location,
                        Remarks = item.Remarks ?? "BPPS",
                        Status = "",
                        StorageLocation = "9151",
                        ControlNumber = "",
                        WhId = warehouseid,
                        TransactionId = Guid.NewGuid()
                    };

                   context.TransactionHistories.Add(BppsItem);
                }

                if (ErrorPartnumber.Count > 0)
                {
                    string badParts = string.Join(", ", ErrorPartnumber);
                    return new ServiceResponseDto
                    {
                        Success = false,
                        Message = $"Invalid parts scanned: {badParts}"
                    };
                }

                await context.SaveChangesAsync();
                return new ServiceResponseDto
                {
                    Success = true,
                    Message = "BPPS Items successfully inserted."
                };

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Error: " + ex.InnerException.Message;
                }

                return new ServiceResponseDto
                {
                    Success = false,
                    Message = $"Crash Details:\n{errorMessage}"
                };
            }
        }

        public async Task<ServiceResponseDto> InsertFG(List<ScannedItemDto> ScanItem, string warehouseid, string transaction_type, string userid)
        {

            try
            {
                if (ScanItem == null || ScanItem.Count == 0)
                {
                    return new ServiceResponseDto
                    {
                        Success = false,
                        Message = "No Scanned items."
                    };
                }

                HashSet<string> ErrorPartnumber = [];
                HashSet<string> ErrorPPSPartnumber = [];
                var referenceList = await GetPPS();


                foreach (var item in ScanItem)
                {
                    if (!referenceList.TryGetValue(item.PartNumber, out var PPS))
                    {
                        ErrorPartnumber.Add(item.PartNumber);
                        continue;
                    }


                    if (item.Quantity != PPS)
                    {
                        ErrorPPSPartnumber.Add(item.PartNumber);
                        continue;
                    }


                    var FGitem = new TransactionHistory
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.UtcNow,
                        TransactionType = "IN",
                        Location = item.Location,
                        Remarks = item.Remarks ?? "FG",
                        Status = "",
                        StorageLocation = "9151",
                        ControlNumber = "",
                        WhId = warehouseid,
                        TransactionId = Guid.NewGuid()
                    };

                    context.TransactionHistories.Add(FGitem);
                }

                if (ErrorPartnumber.Count > 0 || ErrorPPSPartnumber.Count > 0)
                {
                    var errorMessage = "";

                    if (ErrorPartnumber.Count > 0)
                        errorMessage += $"Not exist on the Masterlist: {string.Join(", ", ErrorPartnumber)}. ";

                    if (ErrorPPSPartnumber.Count > 0)
                        errorMessage += $"Invalid PPS: {string.Join(", ", ErrorPPSPartnumber)}.";

                    return new ServiceResponseDto
                    {
                        Success = false,
                        Message = errorMessage.Trim()
                    };
                }

                await context.SaveChangesAsync();
                return new ServiceResponseDto
                {
                    Success = true,
                    Message = "FG Items successfully inserted."
                };

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Error: " + ex.InnerException.Message;
                }

                return new ServiceResponseDto
                {
                    Success = false,
                    Message = $"Crash Details:\n{errorMessage}"
                };
            }
        }

        public async Task<ServiceResponseDto> InsertFGOutgoing(List<ScannedItemDto> ScanItem, string warehouseid, string customer, string id, string transaction_type, string userid, string remarks = "FG")
        { 

            try
            {
                if (ScanItem == null || ScanItem.Count == 0)
                {
                    return new ServiceResponseDto
                    {
                        Success = false,
                        Message = "No Scanned items."
                    };
                }

                var AllpartnumberExist = new HashSet<string>(await GetPartNumberList());
                var AllShipmentId = new HashSet<string>(await GetShipmentControlNumber());
                HashSet<string> ErrorPartnumber = [];

                if (AllShipmentId.Contains(id))
                {
                    return new ServiceResponseDto
                    {
                        Success = false,
                        Message = "Duplicate shipment id."
                    };
                }

                var ShipmentRecord = new ShipmentTable
                {
                    TransactionId = id,
                    EntryDate = DateTime.Now,
                    WhId = warehouseid,
                    Status = "",
                    ShipmentId = Guid.NewGuid(),
                    IsSynced = false,
                    SyncStatus = 0
                };

                context.ShipmentTables.Add(ShipmentRecord);

                foreach (var item in ScanItem)
                {
                    var isExist = AllpartnumberExist.Contains(item.PartNumber);
                    if (!isExist)
                    {
                        ErrorPartnumber.Add(item.PartNumber);
                        continue;
                    }

                    var BppsItem = new TransactionHistory
                    {
                        Partnumber = item.PartNumber,
                        ProdDate = item.ProductionDate,
                        CustomerId = item.CustomerId,
                        Quantity = item.Quantity,
                        ProdVer = item.ProductionVersion,
                        EntryDate = DateTime.UtcNow,
                        TransactionType = "OUT",
                        Location = item.Location,
                        Remarks = item.Remarks ?? "BPPS",
                        Status = "",
                        StorageLocation = "9151",
                        ControlNumber = id,
                        WhId = warehouseid,
                        TransactionId = Guid.NewGuid()
                    };

                    context.TransactionHistories.Add(BppsItem);
                }

                await context.SaveChangesAsync();
                return new ServiceResponseDto
                {
                    Success = true,
                    Message = "FG Items successfully inserted."
                };
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Error: " + ex.InnerException.Message;
                }

                return new ServiceResponseDto
                {
                    Success = false,
                    Message = $"Crash Details:\n{errorMessage}"
                };
            }
        }

        private static Dictionary<string, int> DPIReference(List<DPIList> list)
        {
            try
            {
                var DPIreference = list
                                   .GroupBy(d => d.Partnumber)
                                   .ToDictionary(x => x.Key, x => x.Sum(x => x.Quantity));
                return DPIreference;
            }
            catch
            {
                return [];
            }
        }

        public async Task<(ServiceResponseDto, List<ScannedItemDto>)> ProcessFGValidation(List<DPIList> dPILists, List<ScannedItemDto> list, string warehouseid, string location)
        {
            try
            {
                HashSet<string> missingDPIPartnumber = [];
                Dictionary<string, string> ExcessItems = [];
                Dictionary<string, string> stockOverflowItems = [];
                HashSet<string> missingStockPartnumber = [];
                Dictionary<string, int> runningDpiTotals = [];
                Dictionary<string, int> runningStockTotals = [];
                List<ScannedItemDto> validItems = [];

                var currentStock = await GetStocks(warehouseid);

                var DPIDICT = DPIReference(dPILists);
                var ScanDICT = list
                                   .GroupBy(x => $"{x.PartNumber}_{x.ProductionDate}_{x.ProductionVersion}_{x.Location}")
                                   .ToDictionary(x => x.Key, x => x.Sum(x => x.Quantity));
                var stockDICT = currentStock
                    .GroupBy(x => new { x.Partnumber, x.ProductionVersion, x.ProductionDate, x.Location })
                    .ToDictionary(
                        g => $"{g.Key.Partnumber}_{(g.Key.ProductionDate.HasValue ? g.Key.ProductionDate.Value.ToString("dd-MM-yy") : string.Empty)}_{g.Key.ProductionVersion}_{g.Key.Location}",
                        g => g.Sum(x => x.Quantity)
                    );

                foreach(var items in list)
                {
                    string itemKey = $"{items.PartNumber}_{items.ProductionDate}_{items.ProductionVersion}_{items.Location}";
                    ScanDICT.TryGetValue(itemKey, out var currentScan);

                    if (!stockDICT.TryGetValue(itemKey, out var maxAllowed))
                    {
                        missingStockPartnumber.Add(items.PartNumber);
                        continue;
                    }

                    if (!DPIDICT.TryGetValue(items.PartNumber, out var dpiReference))
                    {
                        missingDPIPartnumber.Add(items.PartNumber);
                        continue;
                    }

                    runningStockTotals.TryGetValue(itemKey, out var currentStockScanned);
                    runningDpiTotals.TryGetValue(items.PartNumber, out var currentDpiScanned);

                    var projectedStockQty = currentStockScanned + items.Quantity;
                    var projectedDpiQty = currentDpiScanned + items.Quantity;

                    
                    if(projectedDpiQty > dpiReference)
                    {
                        ExcessItems[items.PartNumber] = $"- {items.PartNumber} (Attempted: {projectedDpiQty}, Limit: {dpiReference}, Production: {items.ProductionDate}, Rack: {items.Location})";
                        continue;
                    }


                    if (projectedStockQty > maxAllowed)
                    {
                        stockOverflowItems[items.PartNumber] = $"- {items.PartNumber} (Attempted: {projectedStockQty}, Stock: {maxAllowed}, Production: {items.ProductionDate}, Rack: {items.Location})";
                        continue;
                    }

                    validItems.Add(items);
                    runningDpiTotals[items.PartNumber] = projectedDpiQty;
                    runningStockTotals[itemKey] = projectedStockQty;
                }


                if (missingStockPartnumber.Count > 0 || missingDPIPartnumber.Count > 0 || ExcessItems.Count > 0 || stockOverflowItems.Count > 0)
                {
                    string warningMessage = "Upload finished, but some items were skipped:\n\n";

                    if (missingStockPartnumber.Count > 0)
                    {
                        warningMessage += $"Invalid stock: {missingStockPartnumber.Count}";
                    }

                    if (missingDPIPartnumber.Count > 0)
                        warningMessage += $"Missing from DPI Plan: {missingDPIPartnumber.Count} items.\n";

                    if (ExcessItems.Count > 0)
                    {
                        var excessToDisplay = ExcessItems.Values;
                        warningMessage += $"Exceeded DPI Limits:\n{string.Join("\n", excessToDisplay)}";
                        //if (ExcessItems.Count > 10)
                        //    warningMessage += $"\n...and {ExcessItems.Count - 10} more.";
                    }

                    if (stockOverflowItems.Count > 0)
                    {
                        var overflowToDisplay = stockOverflowItems.Values;
                        warningMessage += $"Stock Overflows:\n- {string.Join("\n- ", overflowToDisplay)}";
                    }

                    return (new ServiceResponseDto 
                    {
                        Success = false,
                        Message = warningMessage
                    }, []);
                }

                return (new ServiceResponseDto
                {
                    Success = true,
                    Message = "Successfully uploaded."
                }, validItems);
            }
            catch(Exception ex) 
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Error: " + ex.InnerException.Message;
                }

                return (new ServiceResponseDto
                {
                    Success = false,
                    Message = $"Crash Details:\n{errorMessage}"
                }, []);
            }
        }

        
        public async Task<ServiceResponseDto> InsertReturns(List<ScannedItemDto> ScanItem, string warehouseid, string id, string transaction_type, string userid, string remarks, string location)
        {
            //try
            //{
            //    foreach (var data in ScanItem)
            //    {
            //        var isExist = await _queries.GetProductInfo(data.PartNumber);
            //        if (isExist == null)
            //        {
            //            return (false, "Partnumber not exist in database.");
            //        }

            //        if (isExist.PPS != data.Quantity)
            //        {
            //            return (false, "Invalid PPS Quantity");
            //        }
            //    }

            //    return await _queries.InsertReturnItems(ScanItem, warehouseid, id, transaction_type, userid, remarks, location);
            //}
            //catch (Exception ex)
            //{
            //    string errorMessage = ex.Message;
            //    return (false, $"Error: {errorMessage}");
            //}
        }

        public async Task<List<Transaction>> getItemsByReturns(string docnum)
        {
            return await _queries.GetItemByReturn(docnum);
        }

        public async Task<List<Transaction>> GetShipmentList(string shipmentID = null, DateTime? start = null, DateTime? end = null)
        {
            var result = await _queries.GetFilteredShipment(shipmentID, start, end);

            return result;
        }

        public async Task<List<Transaction>> LoadShipmentItems(string controlnumber)
        {
            var result = await _queries.GetShipmentItems(controlnumber);
            return result;
        }

        public async Task<(bool isSuccess, string Message)> CancelShipment(string controlnumber, string userid)
        {
            var result = await _queries.CancelShipment(controlnumber, userid);
            return result;
        }

        public async Task<List<Return>> GetReturnList(string location, DateTime? start = null, DateTime? end = null)
        {
            var result = await _queries.GetFilteredReturn(location, start, end);

            return result;
        }

        public async Task<List<Transaction>> LoadReturnItems(string controlnumber)
        {
            var result = await _queries.GetReturnItems(controlnumber);
            return result;
        }

        public async Task<(bool isSuccess, string Message)> CancelReturn(string controlnumber, string userid)
        {
            var result = await _queries.CancelReturn(controlnumber, userid);
            return result;
        }
    }
}
