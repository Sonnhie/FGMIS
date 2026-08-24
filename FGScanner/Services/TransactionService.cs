using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace FGScanner.Services
{
    public class TransactionService
    {
        private readonly Queries _queries;
        public TransactionService(Queries queries)
        {
            _queries = queries;
        }

        public async Task<List<string>> GetRackLocationsAsync(string warehouseId)
        {
            return await _queries.GetRackLocations(warehouseId);
        }

        public async Task<List<ActualInventory>> GetActualInventories(string warehouseid, string location)
        {
            return await _queries.GetActualInventory(warehouseid, location);
        }

        public async Task<(bool isSuccess, string Message)> SaveScannedItemsAsync(List<ActualInventory> scannedItems, string currentLocation, string newLocation, string warehouseId, string location, string userId)
        {
            try
            {
                var (isSuccess, Message) = await _queries.TransferInventoryAsync(warehouseId, currentLocation, newLocation, scannedItems, userId);
                if (isSuccess)
                {
                    return (true, Message);
                }
                else
                {
                    return (false, Message);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Error: " + ex.InnerException.Message;
                }

                return (false, $"Crash Details:\n{errorMessage}");
            }
        }

        public async Task<(bool isSuccess, string Message)> InsertBPPS(List<ScannedData> ScanItem, string warehouseid, string userid)
        {
            try
            {
                foreach (var data in ScanItem)
                {
                    var isExist = await _queries.GetProductInfo(data.PartNumber);
                    if (isExist == null)
                    {
                        return (false, "Partnumber not exist in database.");
                    }
                }
                
                return await _queries.InsertBPPSItems(ScanItem, warehouseid, userid);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return (false, $"Error: {errorMessage}");
            }
        }
        public async Task<(bool isSuccess, string Message)> InsertFG(List<ScannedData> ScanItem, string warehouseid, string transaction_type, string userid)
        {
            try
            {
                var partnumbers  = ScanItem.Select(x => x.PartNumber).Distinct().ToList();
                var productdict = await _queries.GetProductsByPartNumbersAsync(partnumbers);

                foreach (var data in ScanItem)
                {
                    if (!productdict.TryGetValue(data.PartNumber, out var productInfo))
                    {
                        return (false, $"Partnumber '{data.PartNumber}' does not exist in database.");
                    }

                    if (data.Quantity <= 0)
                    {
                        return (false, $"Quantity for {data.PartNumber} must be greater than zero.");
                    }
                }

                return await _queries.InsertFGItems(ScanItem, warehouseid, transaction_type, userid);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return (false, $"Error: {errorMessage}");
            }
        }

        public async Task<(bool isSuccess, string Message)> 
            InsertFGOutgoing(
            List<ScannedData> ScanItem, 
            string warehouseid, 
            string id, 
            string transaction_type, 
            string userid,
            string marketcode,
            string remarks = "FG"
            )
        {
            try
            {
                foreach (var data in ScanItem)
                {
                    var isExist = await _queries.GetProductInfo(data.PartNumber);
                    if (isExist == null)
                    {
                        return (false, "Partnumber not exist in database.");
                    }

                    //if (isExist.Pps != data.Quantity)
                    //{
                    //    return (false, "Invalid PPS Quantity");
                    //}
                }

                return await _queries.InsertFGOutgoingItems(ScanItem, warehouseid, id, transaction_type, userid, marketcode, remarks);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return (false, $"Error: {errorMessage}");
            }
        }


        public async Task<(bool isSuccess, string Message, List<ScannedData> ValidItems, List<string> OverflowWarnings)> InsertReturns(
          List<ScannedData> scanItems,
          string warehouseId,
          string id,
          string transactionType,
          string userId,
          string remarks,
          string location)
        {
            try
            {
                if (scanItems == null || !scanItems.Any())
                {
                    return (false, "No scanned items provided.", new List<ScannedData>(), new List<string>());
                }

                // 1. Batch Product Check
                var distinctParts = scanItems
                    .Select(x => x.PartNumber)
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Distinct()
                    .ToList();

                // FIX: If GetProductsByPartNumbers returns Dictionary<string, Product> or similar
                var masterProducts = await _queries.GetProductsByPartNumbersAsync(distinctParts);

                foreach (var item in scanItems)
                {
                    if (string.IsNullOrWhiteSpace(item.PartNumber)) continue;
                    if (!masterProducts.TryGetValue(item.PartNumber, out var product) || product == null)
                    {
                        return (false, $"Partnumber '{item.PartNumber}' does not exist in database.", new List<ScannedData>(), new List<string>());
                    }
                }

                // 2. Query Stocks & Validate Limits
                var stockDict = await _queries.GetStocks(scanItems);
                var runningTotals = new Dictionary<string, int>();
                var validItems = new List<ScannedData>();
                var overflowWarnings = new List<string>();

                string GetCompositeKey(string partNo, DateOnly prodDate, string prodVer, string wh, string loc)
                        => $"{partNo?.Trim().ToUpper()}|{prodDate:yyyy-MM-dd}|{prodVer?.Trim().ToUpper()}|{wh?.Trim().ToUpper()}|{loc?.Trim().ToUpper()}";

                foreach (var item in scanItems)
                {
                    if (string.IsNullOrWhiteSpace(item.PartNumber)) continue;

                    string compositeKey = GetCompositeKey(
                        item.PartNumber,
                        item.ProductionDate,
                        item.ProductionVersion,
                        warehouseId,
                        item.Location
                    );

                    stockDict.TryGetValue(compositeKey, out int totalAvailableStock);
                    runningTotals.TryGetValue(compositeKey, out int currentScanTotal);

                    int projectedQty = currentScanTotal + item.Quantity;

                    if (projectedQty > totalAvailableStock)
                    {
                        overflowWarnings.Add(
                            $"{item.PartNumber} (Attempted: {projectedQty}, Stock: {totalAvailableStock}, Production: {item.ProductionDate:yyyy-MM-dd}, Rack: {item.Location})"
                        );
                        continue;
                    }

                    validItems.Add(item);
                    runningTotals[compositeKey] = projectedQty;
                }

                // All-or-Nothing check: If any item exceeds available stock, abort the entire transaction
                if (overflowWarnings.Count > 0)
                {
                    string warningList = string.Join("\n- ", overflowWarnings.Take(10));
                    if (overflowWarnings.Count > 10)
                    {
                        warningList += "\n...and " + (overflowWarnings.Count - 10) + " more.";
                    }
                    return (false, $"Validation Failed. No items were uploaded due to stock overflow limits:\n\n- {warningList}", new List<ScannedData>(), overflowWarnings);
                }

                if (validItems.Count == 0)
                {
                    return (false, "No valid items to return.", validItems, overflowWarnings);
                }

                // 3. Save Valid Items to Database
                var (isSuccess, Message) = await _queries.InsertReturnItems(validItems, warehouseId, id, transactionType, userId, remarks, location);

                if (!isSuccess)
                {
                    return (false, Message, validItems, overflowWarnings);
                }

                return (true, Message, validItems, overflowWarnings);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new List<ScannedData>(), new List<string>());
            }
        }

        public async Task<List<TransactionHistory>> getItemsByReturns(string docnum)
        {
            return await _queries.GetItemByReturn(docnum);
        }

        public async Task<List<TransactionHistory>> GetShipmentList(string shipmentID = null, DateTime? start = null, DateTime? end = null)
        {
            var result = await _queries.GetFilteredShipment(shipmentID, start, end);

            return result;
        }

        public async Task<List<TransactionHistory>> LoadShipmentItems(string controlnumber)
        {
            var result = await _queries.GetShipmentItems(controlnumber);
            return result;
        }

        public async Task<(bool isSuccess, string Message)> CancelShipment(string controlnumber, string userid)
        {
            var result = await _queries.CancelShipment(controlnumber, userid);
            return result;
        }

        public async Task<List<ReturnTable>> GetReturnList(string location, DateTime? start = null, DateTime? end = null)
        {
            var result = await _queries.GetFilteredReturn(location, start, end);

            return result;
        }

        public async Task<List<TransactionHistory>> LoadReturnItems(string controlnumber)
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
