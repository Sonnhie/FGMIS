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
                foreach (var data in ScanItem)
                {
                    var isExist = await _queries.GetProductInfo(data.PartNumber);
                    if (isExist == null)
                    {
                        return (false, "Partnumber not exist in database.");
                    }

                    if (isExist.PPS != data.Quantity)
                    {
                        return (false, "Invalid PPS Quantity");
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

        public async Task<(bool isSuccess, string Message)> InsertFGOutgoing(List<ScannedData> ScanItem, string warehouseid, string id, string transaction_type, string userid, string remarks = "FG")
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

                    if (isExist.PPS != data.Quantity)
                    {
                        return (false, "Invalid PPS Quantity");
                    }
                }

                return await _queries.InsertFGOutgoingItems(ScanItem, warehouseid, id, transaction_type, userid, remarks);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return (false, $"Error: {errorMessage}");
            }
        }

        public async Task<(bool isSuccess, string Message)> InsertReturns(List<ScannedData> ScanItem, string warehouseid, string id, string transaction_type, string userid, string remarks, string location)
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

                    if (isExist.PPS != data.Quantity)
                    {
                        return (false, "Invalid PPS Quantity");
                    }
                }

                return await _queries.InsertReturnItems(ScanItem, warehouseid, id, transaction_type, userid, remarks, location);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return (false, $"Error: {errorMessage}");
            }
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
