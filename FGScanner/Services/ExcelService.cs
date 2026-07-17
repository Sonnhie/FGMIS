using FGScanner.Database;
using FGScanner.Models;
using FGScanner.Repositories;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Superpower.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FGScanner.Services
{
    public class ExcelService
    {
        private readonly Queries _queries;
        private List<ScannedData> validScan = new List<ScannedData>();
        private BindingList<DPIList> DPIItems = new BindingList<DPIList>();
        public ExcelService(Queries queries)
        {
            _queries = queries;
        }

        public async Task<(bool isSuccess, string Message, List<ScannedData> ScanItem)> ProcessBPPSQRCodeData(string QRCode, string location)
        {
            //Initialize a variables to store the scanned items
            string partNumber = "";
            string productionVersion = "";
            string productionDate;
            string quantity = "";
            string initial = "";


            //Convert the QR code to uppercase and split it into parts
            var toUpperQRCode = QRCode.ToUpper();
            var SlashedPart = toUpperQRCode.Split('/');
            var leftPart = SlashedPart[0].Split('-');
            var rightPart = SlashedPart[1].Split('-');
            initial = rightPart[0].Substring(1);
            quantity = initial.Substring(0, initial.Length - 2);
            productionDate = rightPart[0].Substring(rightPart[0].Length - 2) + "-" + rightPart[1] + "-" + rightPart[2];


            //Check if the QR code format is valid
            if (SlashedPart.Length != 2)
            {
                return (false, "Invalid QR Code format.", null);
            }

            if (leftPart.Length < 2)
            {
                return (false, "Invalid QR Code format.", null);
            }

            if (rightPart.Length != 3)
            {
                return (false, "Invalid QR Code format.", null);
            }

            if (!rightPart[0].StartsWith('O'))
            {
                return (false, "Invalid QR Code format.", null);
            }


            //Parse the QR Code data of left part to get the part number and production version
            if (leftPart.Length == 2)
            {
                partNumber = leftPart[0];
                productionVersion = leftPart[1];
            }

            if (leftPart.Length == 3)
            {
                partNumber = leftPart[0] + "-" + leftPart[1];
                productionVersion = leftPart[2];
            }

            if (leftPart.Length == 4)
            {
                partNumber = leftPart[0] + "-" + leftPart[1] + "-" + leftPart[2];
                productionVersion = leftPart[3];
            }

            if (leftPart.Length == 5)
            {
                partNumber = leftPart[0] + "-" + leftPart[1] + "-" + leftPart[2] + "-" + leftPart[3];
                productionVersion = leftPart[4];
            }

            if (!DateTime.TryParseExact(productionDate, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var ProdDate))
            {
                return (false, "Invalid production date format.", null);
            }

            if (!int.TryParse(quantity, out int Quantity))
            {
                return (false, "Invalid quantity format.", null);
            }

            if (string.IsNullOrWhiteSpace(partNumber))
            {
                return (false, "Invalid part number.", null);
            }

            //Check if the part number exists in the database
            var product = await _queries.GetProductInfo(partNumber);

            if (product == null)
            {
                return (false, "Part number not exist on database.", null);
            }

            if (product.CustomerId == null)
            {
                return (false, "Customer ID not found for the part number.", null);
            }

            //Create a new ScannedModel object to store the scanned item
            var scannedItem = new ScannedData
            {
                PartNumber = partNumber,
                ProductionVersion = productionVersion,
                ProductionDate = ProdDate,
                Quantity = Quantity,
                Location = location,
                CustomerId = product.CustomerId
            };

            return (true, "QR Code processed successfully.", new List<ScannedData> { scannedItem });
        }
        public async Task<(bool isSuccess, string Message, ScannedData ScanItem)> ProcessFGQRCodeData(string QRCode, string location)
        {
            //Initialize a variables to store the scanned items
            string partNumber = "";
            string productionVersion = "";
            string productionDate;
            string quantity = "";
            string initial = "";


            //Convert the QR code to uppercase and split it into parts
            var toUpperQRCode = QRCode.ToUpper();
            var SlashedPart = toUpperQRCode.Split('/');
            var leftPart = SlashedPart[0].Split('-');
            var rightPart = SlashedPart[1].Split('-');
            initial = rightPart[0].Substring(1);
            quantity = initial.Substring(0, initial.Length - 2);
            productionDate = rightPart[0].Substring(rightPart[0].Length - 2) + "-" + rightPart[1] + "-" + rightPart[2];


            //Check if the QR code format is valid
            if (SlashedPart.Length != 2)
            {
                return (false, "Invalid QR Code format.", null);
            }

            if (leftPart.Length < 2)
            {
                return (false, "Invalid QR Code format.", null);
            }

            else if (rightPart.Length != 3)
            {
                return (false, "Invalid QR Code format.", null);
            }

            else if (!rightPart[0].StartsWith('O'))
            {
                return (false, "Invalid QR Code format.", null);
            }


            //Parse the QR Code data of left part to get the part number and production version
            if (leftPart.Length == 2)
            {
                partNumber = leftPart[0];
                productionVersion = leftPart[1];
            }

            else if (leftPart.Length == 3)
            {
                partNumber = leftPart[0] + "-" + leftPart[1];
                productionVersion = leftPart[2];
            }

            else if (leftPart.Length == 4)
            {
                partNumber = leftPart[0] + "-" + leftPart[1] + "-" + leftPart[2];
                productionVersion = leftPart[3];
            }

            else if (leftPart.Length == 5)
            {
                partNumber = leftPart[0] + "-" + leftPart[1] + "-" + leftPart[2] + "-" + leftPart[3];
                productionVersion = leftPart[4];
            }

            if (!DateTime.TryParseExact(productionDate, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var ProdDate))
            {
                return (false, "Invalid production date format.", null);
            }

            if (!int.TryParse(quantity, out int Quantity))
            {
                return (false, "Invalid quantity format.", null);
            }

            if (string.IsNullOrWhiteSpace(partNumber))
            {
                return (false, "Invalid part number.", null);
            }

            //Check if the part number exists in the database
            var product = await _queries.GetProductInfo(partNumber);

            if (product == null)
            {
                return (false, "Part number not exist on database.", null);
            }

            if (product.CustomerId == null)
            {
                return (false, "Customer ID not found for the part number.", null);
            }

            if (product.PPS != Quantity)
            {
                return (false, "Invalid PPS.", null);
            }


            //Create a new ScannedModel object to store the scanned item
            var scannedItem = new ScannedData
            {
                PartNumber = partNumber,
                ProductionVersion = productionVersion,
                ProductionDate = ProdDate,
                Quantity = Quantity,
                Location = location,
                CustomerId = product.CustomerId
            };

            return (true, "QR Code processed successfully.", scannedItem);
        }
        public async Task<(bool isSuccess, string Message, List<ScannedData> ScanItem)> ProcessBPPSUpload(FileInfo fileinfo, IProgress<int> progress, string warehouseId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(warehouseId))
                {
                    return (false, "Please select a warehouse.", null);
                }

                ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
                using (var package = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int startRow = 1;
                    int rowCount = ws.Dimension.Rows;
                    int totalRows = rowCount - startRow + 1;

                    int currentRow = 0;
                    string QRCode = string.Empty;
                    string location = string.Empty;
                    validScan.Clear();
                    for (int row = startRow; row <= rowCount; row++)
                    {
                        QRCode = ws.Cells[row, 1].Value?.ToString().Trim().ToUpper();
                        location = ws.Cells[row, 2].Value?.ToString().Trim().ToUpper();
                        if (!string.IsNullOrWhiteSpace(QRCode) && !string.IsNullOrWhiteSpace(location))
                        {
                            var result = await ProcessBPPSQRCodeData(QRCode, location);
                            if (result.isSuccess)
                            {
                                validScan.Add(result.ScanItem.FirstOrDefault());
                            }
                        }
                        currentRow++;
                        int progressPercentage = (int)((double)currentRow / totalRows * 100);
                        progressPercentage = Math.Min(progressPercentage, 100);
                        if (currentRow % 10 == 0)
                            progress?.Report(progressPercentage);
                        progress.Report(progressPercentage);
                    }
                    await Task.Delay(500);
                }
                progress?.Report(100);
                return (true, "File processed successfully.", validScan);
            }
            catch (Exception ex)
            {
                return (false, $"Error processing file: {ex.Message}", null);
            }
        }
        public async Task<(bool isSuccess, string Message, List<ScannedData> ScanItem)> ProcessFGUpload(FileInfo fileinfo, IProgress<int> progress, string warehouseId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(warehouseId))
                {
                    return (false, "Please select a warehouse.", null);
                }

                ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
                using (var package = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int startRow = 1;
                    int rowCount = ws.Dimension.Rows;
                    int totalRows = rowCount - startRow + 1;

                    int currentRow = 0;
                    string QRCode = string.Empty;
                    string location = string.Empty;
                    validScan.Clear();
                    for (int row = startRow; row <= rowCount; row++)
                    {
                        QRCode = ws.Cells[row, 1].Value?.ToString().Trim().ToUpper();
                        location = ws.Cells[row, 2].Value?.ToString().Trim().ToUpper();
                        if (!string.IsNullOrWhiteSpace(QRCode) && !string.IsNullOrWhiteSpace(location))
                        {
                            var (isSuccess, Message, ScanItem) = await ProcessFGQRCodeData(QRCode, location);
                            if (isSuccess)
                            {
                                validScan.Add(ScanItem);
                            }
                            else
                            {
                                return (false, $"Failed on row {row}: {Message}", null);
                            }
                        }
                        currentRow++;
                        int progressPercentage = (int)((double)currentRow / totalRows * 100);
                        progressPercentage = Math.Min(progressPercentage, 100);
                        if (currentRow % 10 == 0)
                            progress?.Report(progressPercentage);
                        progress.Report(progressPercentage);
                    }
                    await Task.Delay(500);
                }
                progress?.Report(100);
                return (true, "File processed successfully.", validScan);
            }
            catch (Exception ex)
            {
                return (false, $"Error processing file: {ex.Message}", null);
            }
        }
        public async Task<(bool isSuccess, string Message, List<ScannedData> ScanItem)> ProcessFgShipmentUpload(FileInfo fileinfo, IProgress<int> progress, string warehouseId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(warehouseId))
                {
                    return (false, "Please select a warehouse.", null);
                }

                ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
                using (var package = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int startRow = 1;
                    int rowCount = ws.Dimension.Rows;
                    int totalRows = rowCount - startRow + 1;

                    int currentRow = 0;
                    string QRCode = string.Empty;
                    string location = string.Empty;
                    validScan.Clear();
                    for (int row = startRow; row <= rowCount; row++)
                    {
                        QRCode = ws.Cells[row, 1].Value?.ToString().Trim().ToUpper();
                        location = ws.Cells[row, 2].Value?.ToString().Trim().ToUpper();
                        if (!string.IsNullOrWhiteSpace(QRCode) && !string.IsNullOrWhiteSpace(location))
                        {
                            var (isSuccess, Message, ScanItem) = await ProcessFGQRCodeData(QRCode, location);
                            if (isSuccess)
                            {
                                if (ScanItem == null)
                                {
                                    return (false, Message, null);
                                }

                                var reference = await _queries.CheckIfExist(ScanItem.PartNumber, ScanItem.Location, ScanItem.ProductionDate);

                                if (reference == null)
                                {
                                    return (false, $"{ScanItem.PartNumber} with Lot date: {ScanItem.ProductionDate} does not exist on location {ScanItem.Location}", null);
                                }

                                var currentScanQty = validScan.Where(x => x.PartNumber == reference.Partnumber && x.ProductionDate == reference.ProdDate).Sum(x => x.Quantity);
                                var projectedQty = currentScanQty + ScanItem.Quantity;
                                if (projectedQty <= reference.Quantity)
                                {
                                    validScan.Add(ScanItem);
                                }
                                else
                                {
                                    return (false,
                                             $"Stock overflow for {ScanItem.PartNumber}!\n" +
                                             $"Available Stock: {reference.Quantity} \n" +
                                             $"Currently Scanned: {currentScanQty} \n" +
                                             $"Attempted to add: {ScanItem.Quantity}", null);
                                }
                            }
                            else
                            {
                                // Stop the process and tell the user exactly why it failed!
                                return (false, $"Failed on row {row}: {Message}", null);
                            }
                        }
                        currentRow++;
                        int progressPercentage = (int)((double)currentRow / totalRows * 100);
                        progressPercentage = Math.Min(progressPercentage, 100);
                        if (currentRow % 10 == 0)
                        {
                            progress?.Report(progressPercentage);
                        }
                    }
                    await Task.Delay(500);
                }
                progress?.Report(100);
                return (true, "File processed successfully.", validScan);
            }
            catch (Exception ex)
            {
                return (false, $"Error processing file: {ex.Message}", null);
            }
        }
        public async Task<(bool isSuccess, string Message, BindingList<DPIList> Items)> ProcessDPIUpload(FileInfo fileinfo, IProgress<int> progress)
        {
            try
            {
                DPIItems.Clear();
                ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
                using (var package = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int startRow = 2;
                    int rowCount = ws.Dimension.Rows;
                    int totalRows = rowCount - startRow + 1;

                    int currentRow = 0;

                    for (int row = startRow; row <= rowCount; row++)
                    {
                        string partNumber = ws.Cells[row, 1].Text;
                        string qtyText = ws.Cells[row, 2].Text;
                        string ppsText = ws.Cells[row, 3].Text;
                        string boxText = ws.Cells[row, 4].Text;

                        if (string.IsNullOrWhiteSpace(partNumber) && string.IsNullOrWhiteSpace(qtyText))
                        {
                            currentRow++; // Still increment to keep progress bar accurate
                            continue;
                        }

                        int.TryParse(qtyText, out int quantity);
                        int.TryParse(ppsText, out int pps);
                        int.TryParse(boxText, out int box);

                        DPIItems.Add(new DPIList
                        {
                            Partnumber = partNumber,
                            Quantity = quantity,
                            PPS = pps,
                            Box = box
                        });


                        currentRow++;
                        int progressPercentage = (int)((double)currentRow / totalRows * 100);
                        progressPercentage = Math.Min(progressPercentage, 100);
                        if (currentRow % 10 == 0)
                            progress?.Report(progressPercentage);
                        progress.Report(progressPercentage);
                    }
                    await Task.Delay(500);
                }
                progress?.Report(100);
                return (true, "File processed successfully.", DPIItems);
            }
            catch (Exception ex)
            {
                return (false, $"Error processing file: {ex.Message}", null);
            }
        }
        public async Task<(bool isSuccess, string Message)> AutofillPackingListTemplate(string fileParth, string shipmentID, IProgress<int> progress)
        {
            var items = await _queries.GetItemByShipment(shipmentID);
            var itemsGroup = items
                            .GroupBy(x => new { x.Partnumber, x.ProdDate })
                            .Select(x => new
                            {
                                PartNumber = x.Key.Partnumber,
                                Proddate = x.Key.ProdDate,
                                Boxes = x.Sum(x => x.Box),
                                Quantity = x.Sum(x => x.Quantity)
                            })
                            .ToList();

            if (items == null)
            {
                return (false, "No data found on that shipment id.");
            }

            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "packinglist.xlsx");
            using var package = new ExcelPackage(templatePath);
            var ws = package.Workbook.Worksheets["WarehouseCopy"];
            var wscopy = package.Workbook.Worksheets["InvoiceCopy"];
            var summarizedItems = items
                                 .GroupBy(x => new { x.Partnumber })
                                 .Select(x => new
                                 {
                                     Partnumber = x.Key,
                                     Quantity = x.Sum(s => s.Quantity),
                                     Box = x.Sum(s => s.Box),
                                     Customer = x.First().CustomerId
                                 })
                                 .ToList();
            var TotalQuantity = summarizedItems.Sum(x => x.Quantity);
            var TotalBox = summarizedItems.Sum(x => x.Box);

            var startrow = 10;
            var copystartrow = 10;
            DateTime today = DateTime.Now;
            string date = today.ToString("MM/dd/yyyy");
            string time = today.ToString("HH:mm:ss");

            ws.Cells["C6"].Value = date;
            ws.Cells["C7"].Value = time;
            ws.Cells["J6"].Value = items.First().CustomerId;
            ws.Cells["J54"].Value = items.Max(x => x.controlNumber);
            ws.Cells["G53"].Value = items.Sum(x => x.Quantity);
            ws.Cells["E53"].Value = items.Sum(x => x.Box);

            wscopy.Cells["C6"].Value = date;
            wscopy.Cells["C7"].Value = time;
            wscopy.Cells["J6"].Value = items.First().CustomerId;
            wscopy.Cells["J54"].Value = items.Max(x => x.controlNumber);


            int current = 0;
            int initialRow = 10;
            int maxRowsInTemplate = 43;
            int maxRowLimit = initialRow + maxRowsInTemplate;
            int pageCount = 1;



            foreach (var subitems in summarizedItems)
            {
                int PPS = subitems.Quantity / subitems.Box;
                wscopy.Cells[copystartrow, 2].Value = subitems.Partnumber;
                wscopy.Cells[copystartrow, 5].Value = subitems.Box.ToString();
                wscopy.Cells[copystartrow, 6].Value = PPS.ToString();
                wscopy.Cells[copystartrow, 7].Value = subitems.Quantity.ToString();
                copystartrow++;
            }
            var Template = ws;

            foreach (var item in itemsGroup)
            {
                if (startrow >= maxRowLimit)
                {
                    pageCount++;
                    ws = package.Workbook.Worksheets.Add($"WarehouseCopy", Template);
                    startrow = initialRow;
                }

                current++;

                var info = await _queries.GetProductInfo(item.PartNumber);

                ws.Cells[startrow, 2].Value = item.PartNumber.ToString();
                ws.Cells[startrow, 4].Value = item.Proddate.ToString("MM/dd/yyyy");
                ws.Cells[startrow, 5].Value = item.Boxes.ToString();
                ws.Cells[startrow, 6].Value = info.PPS.ToString();
                ws.Cells[startrow, 7].Value = item.Quantity.ToString();
                startrow++;


                int percent = (int)((double)current / (double)item.Boxes * 100);
                percent = Math.Min(percent, 100);
                progress?.Report(percent);
                await Task.Delay(100);
            }


            package.SaveAs(new FileInfo(fileParth));
            progress?.Report(100);
            return (true, "Packing List successfully generated.");
        }
        public async Task<(bool isSuccess, string Message, List<ScannedData> ScanItem)> ProcessReturnUpload(FileInfo fileinfo, IProgress<int> progress, string warehouseId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(warehouseId))
                {
                    return (false, "Please select a warehouse.", null);
                }

                ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
                using (var package = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int startRow = 1;
                    int rowCount = ws.Dimension.Rows;
                    int totalRows = rowCount - startRow + 1;

                    int currentRow = 0;
                    string QRCode = string.Empty;
                    string location = string.Empty;
                    validScan.Clear();
                    for (int row = startRow; row <= rowCount; row++)
                    {
                        QRCode = ws.Cells[row, 1].Value?.ToString().Trim().ToUpper();
                        location = ws.Cells[row, 2].Value?.ToString().Trim().ToUpper();
                        if (!string.IsNullOrWhiteSpace(QRCode) && !string.IsNullOrWhiteSpace(location))
                        {
                            var (isSuccess, Message, ScanItem) = await ProcessFGQRCodeData(QRCode, location);
                            if (isSuccess)
                            {
                                if (ScanItem == null)
                                {
                                    return (false, Message, null);
                                }

                                var reference = await _queries.CheckIfExist(ScanItem.PartNumber, ScanItem.Location, ScanItem.ProductionDate);

                                if (reference == null)
                                {
                                    return (false, $"{ScanItem.PartNumber} with Lot date: {ScanItem.ProductionDate} does not exist on location {ScanItem.Location}", null);
                                }

                                var currentScanQty = validScan.Where(x => x.PartNumber == reference.Partnumber && x.ProductionDate == reference.ProdDate).Sum(x => x.Quantity);
                                var projectedQty = currentScanQty + ScanItem.Quantity;
                                if (projectedQty <= reference.Quantity)
                                {
                                    validScan.Add(ScanItem);
                                }
                                else
                                {
                                    return (false,
                                             $"Stock overflow for {ScanItem.PartNumber}!\n" +
                                             $"Available Stock: {reference.Quantity} \n" +
                                             $"Currently Scanned: {currentScanQty} \n" +
                                             $"Attempted to add: {ScanItem.Quantity}", null);
                                }
                            }
                            else
                            {
                                // Stop the process and tell the user exactly why it failed!
                                return (false, $"Failed on row {row}: {Message}", null);
                            }
                        }
                        currentRow++;
                        int progressPercentage = (int)((double)currentRow / totalRows * 100);
                        progressPercentage = Math.Min(progressPercentage, 100);
                        if (currentRow % 10 == 0)
                        {
                            progress?.Report(progressPercentage);
                        }
                    }
                    await Task.Delay(500);
                }
                progress?.Report(100);
                return (true, "File processed successfully.", validScan);
            }
            catch (Exception ex)
            {
                return (false, $"Error processing file: {ex.Message}", null);
            }
        }

        public async Task<(bool isSuccess, string Message)> GenerateReportExcel<T>(ReportGeneration<T> report, string filepath, IProgress<int> progress = null)
        {
            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("NIDEC");
                if (report.Columns == null)
                    return (false, "No Headers found.");

                if (report.Items == null || report.Items.Count == 0)
                    return (false, "No data to generate.");

                await Task.Run(() =>
                {
                    using var package = new ExcelPackage();
                    var worksheet = package.Workbook.Worksheets.Add(report.Title);
                    worksheet.Cells["A1"].Value = report.Title;
                    for (int i = 0; i < report.Columns.Length; i++)
                    {
                        worksheet.Cells[3, i + 1].Value = report.Columns[i];
                        worksheet.Cells[3, i + 1].Style.Font.Name = "Bahnschrift";
                        worksheet.Cells[3, i + 1].Style.Font.Size = 11;
                        worksheet.Cells[3, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }

                    int totalItem = report.Items.Count;
                    PropertyInfo[] properties = typeof(T).GetProperties();
                    for (int i = 0; i < totalItem; i++)
                    {
                        var items = report.Items[i];
                        int currentRow = i + 4;

                        for (int col = 0; col < properties.Length; col++)
                        {
                            var prop = properties[col];
                            var value = prop.GetValue(items);
                            var cell = worksheet.Cells[currentRow, col + 1];
                            cell.Value = value;
                            if (value is DateTime)
                            {
                                cell.Style.Numberformat.Format = "mm/dd/yyyy";
                            }

                            cell.Style.Font.Size = 10;
                            cell.Style.Font.Name = "Bahnschrift";
                        }

                        if (progress != null)
                        {
                            int percentage = (int)(((double)(i + 1) / totalItem) * 100);
                            if (percentage % 1 == 0)
                            {
                                progress.Report(percentage);
                            }
                        }
                    }
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    package.SaveAs(new FileInfo(filepath));
                });

                progress?.Report(100);
                return (true, $"{report.Title} successfully generated.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool isSuccess, string Message)> AutofillStockCardTemplate(StockCardHeader stock, string filePath, IProgress<int> progress)
        {
            try
            {
                if (stock == null || stock.Ledgers.Count == 0)
                    return (false, "No data to generate.");

                ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "SF-78-FG003_Rev.00_Stock Card.xlsx");

                using ExcelPackage package = new(new FileInfo(templatePath));
                var ws = package.Workbook.Worksheets["Sheet1"];
                var startrow = 7;


                ws.Cells["B3"].Value = stock.PartNumber.ToString() ?? string.Empty;
                ws.Cells["E4"].Value = stock.Ledgers.First().BeginningStock.ToString() ?? string.Empty;
                ws.Cells["B5"].Value = stock.PartName.ToString() ?? string.Empty;

                int current = 0;

                var Records = stock.Ledgers;

                foreach (var item in Records)
                {
                    current++;

                    ws.Cells[startrow, 1].Value = item.InventoryDate;
                    ws.Cells[startrow, 2].Value = item.In;
                    ws.Cells[startrow, 3].Value = item.Out;
                    ws.Cells[startrow, 4].Value = item.RunningStock;
                    ws.Cells[startrow, 5].Value = item.Incharge;
                    ws.Cells[startrow, 6].Value = item.Remarks;

                    startrow++;

                    int percent = (int)((double)current / (double)Records.Count * 100);
                    percent = Math.Min(percent, 100);
                    progress?.Report(percent);
                    await Task.Delay(100);
                }

                package.SaveAs(new FileInfo(filePath));
                progress?.Report(100);
                return (true, "Stock Card generated successfully.");
            }
            catch(Exception ex)
            {
                return(false, ex.Message);
            }


        }
    }
}
