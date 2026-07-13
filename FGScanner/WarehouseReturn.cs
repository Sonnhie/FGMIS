using FGScanner.Model;
using FGScanner.Util;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGScanner.Database;

namespace FGScanner
{
    public partial class WarehouseReturn : Form
    {
        private readonly Util.db_connection _Connection;
        private readonly string user_id;
        private List<TransferSlipData> data = new List<TransferSlipData>();
        private HashSet<string> warnedPartNumbers = new HashSet<string>();
        public WarehouseReturn(string user)
        {
            InitializeComponent();
            _Connection = new Util.db_connection();
            user_id = user;
            toolStripStatusLabel1.Visible = false;
        }

        private readonly BindingList<ScannedModel> ShippingItems = new BindingList<ScannedModel>();
        
       
        private bool OnScanProcess(string QRCode, string location)
        {
            var Process = new ScannerUtility();
            var Insert = new TransactionRepo();


            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Invalid location or empty.");
                return false;
            }

            if (string.IsNullOrEmpty(QRCode))
            {
                MessageBox.Show("QR Code Error or empty!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(remarktext.Text))
            {
                MessageBox.Show($"Remarks is required", "Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Process.ProcessQRData(QRCode, out var itemModel, out var error))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(itemModel.PartNumber))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            int currentScannedQty = ShippingItems
                        .Where(x => x.PartNumber == itemModel.PartNumber && x.ProductionDate == itemModel.ProductionDate)
                        .Sum(x => x.Quantity);
            var newTotal = currentScannedQty + itemModel.Quantity;
            int stockAvailable = Insert.CheckStock(itemModel.PartNumber, itemModel.ProductionDate, location);


            if (newTotal > stockAvailable)
            {
                if (!warnedPartNumbers.Contains(itemModel.PartNumber))
                {
                    // 3. REJECT: Show exact numbers to the user
                    MessageBox.Show(
                    $"Cannot add this item. Stock would be exceeded.\n\n" +
                    $"Stock Available: {stockAvailable}\n" +
                    $"Already Scanned: {currentScannedQty}\n" +
                    $"This Scan: {itemModel.Quantity}\n" +
                    $"Shortfall: {newTotal - stockAvailable}",
                    "Stock Overflow",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                    warnedPartNumbers.Add(itemModel.PartNumber);
                }
                return false;
            }

            ShippingItems.Add(new ScannedModel
            {
                TransactionDate = DateTime.Now,
                Customer = Insert.GetCustomer(itemModel.PartNumber),
                PartNumber = itemModel.PartNumber,
                ProductionDate = itemModel.ProductionDate,
                ProductionVersion = itemModel.ProductionVer,
                Quantity = itemModel.Quantity,
                TransactionType = "OUT",
                Location = location.ToUpper(),
                Storage_location = cmbfrom.Text,
                New_Location = cmbto.Text,
                Remarks = remarktext.Text,
                TransactionId = controlnumberLabel.Text,
                Whid = CmbWHid.Text
            });

            UpdateReturnlogs();
            return true;
        }

        private void UpdateReturnlogs()
        {
            var data = ShippingItems
             .GroupBy(x => new { x.ProductionDate, x.PartNumber, x.TransactionId })
             .Select(g => new
             {
                 TransactionID = g.Key.TransactionId,
                 Partnumber = g.Key.PartNumber,
                 LotDate = g.Key.ProductionDate,
                 Box = g.Count(),
                 Quantity = g.Sum(x => x.Quantity),
                 PostingDate = g.FirstOrDefault()?.TransactionDate
             }).ToList();
            BindingSource source = new BindingSource
            {
                DataSource = data
            };

            returnlogs.DataSource = source;
        }

        private async Task<bool> UploadData()
        {
            if (ShippingItems.Count == 0)
            {
                MessageBox.Show("No items to upload!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var Repo = new TransactionRepo();

            using (SqlConnection conn = _Connection.Getconnection())
            {
                await conn.OpenAsync();

                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        foreach(var item in ShippingItems)
                        {
                            await Repo.InsertSingleTransaction(new InventoryTransactionModel
                            {
                                PartNumber = item.PartNumber,
                                ProductionDate = item.ProductionDate,
                                Customer = item.Customer,
                                Quantity = item.Quantity,
                                TransactionType = item.TransactionType,
                                TransactionDate = item.TransactionDate,
                                ProductionVersion = item.ProductionVersion,
                                Location = item.Location,
                                Remarks = item.Remarks,
                                Storage_location = item.Storage_location,
                                TransactionId = item.TransactionId,
                                WhId = CmbWHid.Text,
                                User = user_id
                            }, conn, tx);

                            await Repo.InsertReturnTransaction(new InventoryTransactionModel
                            {
                                PartNumber = item.PartNumber,
                                ProductionDate = item.ProductionDate,
                                Customer = item.Customer,
                                Quantity = item.Quantity,
                                TransactionDate = item.TransactionDate,
                                ProductionVersion = item.ProductionVersion,
                                Location = item.Location,
                                FromStorageLocation = cmbfrom.Text,
                                ToStorageLocation = cmbto.Text,
                                Remarks = item.Remarks,
                                TransactionId = item.TransactionId,
                                WhId = CmbWHid.Text
                            },conn,tx);
                        }
                        tx.Commit();
                        MessageBox.Show("Data uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show($"Error uploading data: {ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        private void WarehouseReturn_Load(object sender, EventArgs e)
        {
            controlnumberLabel.Text = GenerateTransactionNumber();
        }

        private string GenerateTransactionNumber()
        {
            var Method = new TransactionRepo();
            int seq = Method.GetLatestReturnId();
            return $"AS-{DateTime.Now:yyyyMMdd}-{seq:D2}";
        }

        private async Task AutoFillTemplate(List<OrdersSummaryExtend> whreturn, string Filepath, IProgress<int> Progress)
        {
            if(whreturn == null || whreturn.Count == 0)
                { return; }

            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TransferSlip.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
            {
                var ws = package.Workbook.Worksheets["Sheet1"];
                var startrow = 11;
                var copy_startrow = 35;

                DateTime today = DateTime.Now;
                string date = today.ToString("MM/dd/yyy");


                ws.Cells["B6"].Value = date;
                ws.Cells["E6"].Value = whreturn[0].From;
                ws.Cells["F6"].Value = whreturn[0].To;
                ws.Cells["H5"].Value = user_id;
                ws.Cells["M3"].Value = whreturn[0].TransactionId;

                int current = 0;
                var method = new TransactionRepo();
                foreach (var item in whreturn)
                {
                    current++;
  
                    string partname = method.GetPartname(item.Partnumber);
                    int PPS = item.Quantity / item.Box;

                    ws.Cells[startrow, 2].Value = item.Partnumber;
                    ws.Cells[startrow, 5].Value = partname;
                    ws.Cells[startrow, 9].Value = item.ProductionDate.ToString("MM/dd/yyyy"); 
                    ws.Cells[startrow, 10].Value = item.Box;
                    ws.Cells[startrow, 11].Value = PPS;
                    ws.Cells[startrow, 12].Value = item.Quantity;
                    ws.Cells[startrow, 13].Value = item.Remarks;

                    ws.Cells[copy_startrow, 2].Value = item.Partnumber;
                    ws.Cells[copy_startrow, 5].Value = partname;
                    ws.Cells[copy_startrow, 9].Value = item.ProductionDate.ToString("MM/dd/yyyy");
                    ws.Cells[copy_startrow, 10].Value = item.Box;
                    ws.Cells[copy_startrow, 11].Value = PPS;
                    ws.Cells[copy_startrow, 12].Value = item.Quantity;
                    ws.Cells[copy_startrow, 13].Value = item.Remarks;

                    startrow++;
                    copy_startrow++;

                    int percent = (int)((double)current / (double)whreturn.Count * 100);
                    percent = Math.Min(percent, 100);
                    Progress?.Report(percent);
                    await Task.Delay(100);
                }

                package.SaveAs(new FileInfo(Filepath));
                Progress?.Report(100);
            }
        }

        private int _transferRowIndex = 0; // Tracks data rows across pages
 
        private int DrawTransferSlip(Graphics g, int width, int height, int startX, int startY, TransferSlipData data, string label)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // --- PENS & FONTS ---
            Pen pen = new Pen(Color.Black, 1);
            Pen dashedPen = new Pen(Color.Black, 1) { DashPattern = new float[] { 4, 4 } };
            Font titleFont = new Font("Arial", 22, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 10, FontStyle.Regular);
            Font smallFont = new Font("Arial", 8, FontStyle.Regular);
            Font labelFont = new Font("Arial", 10, FontStyle.Italic | FontStyle.Bold); // For the "COPY" label

            StringFormat centerFmt = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat leftFmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            StringFormat rightFmt = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

            int y = startY;
            int docNoWidth = (int)(width * 0.15f);

            // --- 0. DRAW THE LABEL (TOP RIGHT) ---
            g.DrawString(label, labelFont, Brushes.DimGray, new Rectangle(startX, y - 15, width, 15), rightFmt);

            // --- 1. TITLE & DOCUMENT NO ROW ---
            int titleH = 40;
            g.DrawRectangle(pen, startX, y, width, titleH);
            g.DrawString("TRANSFER SLIP", titleFont, Brushes.Black, new Rectangle(startX, y, width - docNoWidth, titleH), centerFmt);
            g.DrawLine(pen, startX + width - docNoWidth, y, startX + width - docNoWidth, y + titleH + 40);
            g.DrawString("Document No.", headerFont, Brushes.Black, startX + width - docNoWidth + 5, y + 5);
            g.DrawString(data.DocumentNo, bodyFont, Brushes.Black, new Rectangle(startX + width - docNoWidth, y + 15, docNoWidth, titleH - 15), centerFmt);
            y += titleH;

            // --- 2. SHIFT ROW ---
            int shiftH = 30;
            g.DrawRectangle(pen, startX, y, width, shiftH);
            g.DrawString("Shift:", headerFont, Brushes.Black, startX + 5, y + 7);
            g.DrawLine(pen, startX + 60, y, startX + 60, y + shiftH);
            g.DrawLine(pen, startX + 100, y, startX + 100, y + shiftH);
            g.DrawString(data.Shift, bodyFont, Brushes.Black, new Rectangle(startX + 60, y, 40, shiftH), centerFmt);
            y += shiftH;

            // --- 3. ISSUE DATE & PERSONNEL ROW ---
            int persH = 50;
            g.DrawRectangle(pen, startX, y, width, persH);
            float[] topCols = { width * 0.15f, width * 0.25f, width * 0.15f, width * 0.15f, width * 0.15f, width * 0.15f };
            float cx = startX;
            string[] topLabels = { "Issue Date", "Location", "Prepared by:", "Checked by:", "Received by:", "Encoded by:" };
            string[] topValues = { data.IssueDate, "", data.PreparedBy, data.CheckedBy, data.ReceivedBy, data.EncodedBy };

            for (int i = 0; i < topCols.Length; i++)
            {
                if (i > 0) g.DrawLine(pen, cx, y, cx, y + persH);
                if (i == 1)
                {
                    g.DrawString(topLabels[i], headerFont, Brushes.Black, new RectangleF(cx, y, topCols[i], persH / 2), centerFmt);
                    g.DrawLine(pen, cx, y + persH / 2, cx + topCols[i], y + persH / 2);
                    float halfLoc = topCols[i] / 2;
                    g.DrawLine(pen, cx + halfLoc, y + persH / 2, cx + halfLoc, y + persH);
                    g.DrawString("From", headerFont, Brushes.Black, new RectangleF(cx, y + persH / 2, halfLoc, persH / 2), centerFmt);
                    g.DrawString("To", headerFont, Brushes.Black, new RectangleF(cx + halfLoc, y + persH / 2, halfLoc, persH / 2), centerFmt);
                }
                else
                {
                    g.DrawString(topLabels[i], headerFont, Brushes.Black, new RectangleF(cx + 2, y + 2, topCols[i], 20), leftFmt);
                    g.DrawString(topValues[i], bodyFont, Brushes.Black, new RectangleF(cx, y + 20, topCols[i], persH - 20), centerFmt);
                }
                cx += topCols[i];
            }
            y += persH;

            // --- 4. MAIN TABLE HEADERS ---
            int headH = 50;
            g.DrawRectangle(pen, startX, y, width, headH);
            float[] colW = { width * 0.15f, width * 0.21f, width * 0.05f, width * 0.10f, width * 0.10f, width * 0.19f, width * 0.20f };
            string[] headers = { "Material Code", "Material Name", "Rev.\nNo", "Inspection\nDate", "Production\nDate", "Quantity", "Remarks" };

            cx = startX;
            for (int i = 0; i < colW.Length; i++)
            {
                if (i > 0) g.DrawLine(pen, cx, y, cx, y + headH);
                if (i == 5)
                {
                    g.DrawString(headers[i], headerFont, Brushes.Black, new RectangleF(cx, y, colW[i], headH / 2), centerFmt);
                    g.DrawLine(pen, cx, y + headH / 2, cx + colW[i], y + headH / 2);
                    float qW = colW[i] / 3;
                    g.DrawLine(pen, cx + qW, y + headH / 2, cx + qW, y + headH);
                    g.DrawLine(pen, cx + (qW * 2), y + headH / 2, cx + (qW * 2), y + headH);
                    g.DrawString("No.Box", smallFont, Brushes.Black, new RectangleF(cx, y + headH / 2, qW, headH / 2), centerFmt);
                    g.DrawString("PPS", smallFont, Brushes.Black, new RectangleF(cx + qW, y + headH / 2, qW, headH / 2), centerFmt);
                    g.DrawString("Pcs", smallFont, Brushes.Black, new RectangleF(cx + (qW * 2), y + headH / 2, qW, headH / 2), centerFmt);
                }
                else
                {
                    g.DrawString(headers[i], headerFont, Brushes.Black, new RectangleF(cx, y, colW[i], headH), centerFmt);
                }
                cx += colW[i];
            }
            y += headH;

            // --- 5. MAIN TABLE DATA ROWS ---
            int rowH = 30;
            int numRowsPerPage = 10;
            float[] dataColW = { colW[0], colW[1], colW[2], colW[3], colW[4], colW[5] / 3, colW[5] / 3, colW[5] / 3, colW[6] };

            for (int r = 0; r < numRowsPerPage; r++)
            {
                int currentRowIndex = _transferRowIndex + r;
                cx = startX;
                for (int c = 0; c < dataColW.Length; c++)
                {
                    if (c > 0) g.DrawLine(pen, cx, y, cx, y + rowH);
                    if (data != null && currentRowIndex < data.Rows.Count)
                    {
                        var dRow = data.Rows[currentRowIndex];
                        string cellData = GetCellData(c, dRow);
                        g.DrawString(cellData, bodyFont, Brushes.Black, new RectangleF(cx, y, dataColW[c], rowH), centerFmt);
                    }
                    cx += dataColW[c];
                }
                g.DrawLine(pen, startX, y + rowH, startX + width, y + rowH);
                g.DrawLine(pen, startX, y, startX, y + rowH);
                g.DrawLine(pen, startX + width, y, startX + width, y + rowH);
                y += rowH;
            }

            // --- 6. FOOTER ---
            g.DrawString("CF-140 (Rev.00)", smallFont, Brushes.Black, startX, y + 5);
            y += 25;
            g.DrawLine(dashedPen, startX, y, startX + width, y);

            return y + 25; // Return position for the next copy
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            TransferSlipData myData = data.FirstOrDefault();
            if (myData == null) return;

            // --- DRAW TOP COPY (Original) ---
            int middleOfPage = DrawTransferSlip(
                e.Graphics,
                e.MarginBounds.Width,
                e.MarginBounds.Height,
                e.MarginBounds.Left,
                e.MarginBounds.Top,
                myData,
                "ORIGINAL COPY"
            );

            // --- DRAW BOTTOM COPY (Duplicate) ---
            // This will print the same Rows as the top because _transferRowIndex hasn't changed yet!
            DrawTransferSlip(
                e.Graphics,
                e.MarginBounds.Width,
                e.MarginBounds.Height,
                e.MarginBounds.Left,
                middleOfPage + 20, // Add a little extra gap for the cut line
                myData,
                "DUPLICATE COPY"
            );

            // --- NOW UPDATE THE INDEX FOR THE NEXT SHEET ---
            _transferRowIndex += 10;

            // Handle Pagination
            if (_transferRowIndex < myData.Rows.Count)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                _transferRowIndex = 0; // Reset for the next time you print
            }
        }

        private string GetCellData(int column, TransferRow dRow)
        {
            switch (column)
            {
                case 0: return dRow.MaterialCode;
                case 1: return dRow.MaterialName;
                case 2: return dRow.RevNo;
                case 3: return dRow.InspectionDate;
                case 4: return dRow.ProductionDate;
                case 5: return dRow.NoBox.ToString();
                case 6: return dRow.PPS.ToString();
                case 7: return dRow.Pcs;
                case 8: return dRow.Remarks;
                default: return "";
            }
        }
   
        
        private void CmbWHid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }
        
        private async void button2_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(remarktext.Text))
            {
                await UploadData();
            }
            else
            {
                MessageBox.Show($"Error uploading data: Remarks is required", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void GBtn_Click_1(object sender, EventArgs e)
        {
            string docNo = controlnumberLabel.Text;

            if (string.IsNullOrWhiteSpace(docNo))
            {
                MessageBox.Show("Invalid control Number");
                return;
            }

            var Repo = new TransactionRepo();
            data = Repo.GetTransferSlipData(docNo);

            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

            printDocument1.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintPreviewDialog printPreviewDialog = new()
            {
                Document = printDocument1,
                Width = 800,
                Height = 800
            };

            printPreviewDialog.PrintPreviewControl.Columns = 1;
            printPreviewDialog.ShowDialog();
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            string TransactionID = controlnumberLabel.Text;
            string Filename = $@"TransferSlip_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            var method = new TransactionRepo();
            List<OrdersSummaryExtend> order = method.GetWarehouseReturn(TransactionID);

            if (order.Count == 0)
            {
                MessageBox.Show("No items to generate!", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Excel Files|*.xlsx";
                sf.Title = "Save Transfer Slip";
                sf.DefaultExt = "xlsx";
                sf.FileName = Filename;

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string filepath = sf.FileName;

                    if (order.Count == 0)
                    {
                        MessageBox.Show("No Data Found.");
                        return;
                    }

                    progressBar.Value = 0;
                    progressBar.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Generating Transfer Slip...";

                    var progress = new Progress<int>(value =>
                    {
                        progressBar.Value = value;
                        toolStripStatusLabel1.Text = $"Generating Transfer Slip... {value}%";
                    });

                    try
                    {
                        await AutoFillTemplate(order, filepath, progress);
                        progressBar.Value = 100;
                        toolStripStatusLabel1.Text = "Generating completed successfully!";
                        MessageBox.Show("Export completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = "Export failed!";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        progressBar.Value = 0;
                        progressBar.Visible = false;
                        toolStripStatusLabel1.Text = "";
                        controlnumberLabel.Text = GenerateTransactionNumber();
                        ShippingItems.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Generation canceled.");
                }
            }
        }

        private async Task ProcessUpload(FileInfo fileInfo, IProgress<int> progress)
        {
            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                int startRow = 1;
                int rowCount = ws.Dimension.Rows;
                int totalRows = rowCount - startRow + 1;

                int current = 0;
                string qrcodedata = null;
                string location = null;

                for (int row = startRow; row <= rowCount; row++)
                {
                    current++;
                    qrcodedata = ws.Cells[row, 1].Value.ToString().ToUpper();
                    location = ws.Cells[row, 2].Value.ToString();
                    if (qrcodedata != null)
                    {
                        OnScanProcess(qrcodedata, location);
                    }

                    int percent = (int)((double)current / totalRows * 100);
                    percent = Math.Min(percent, 100);
                    if (current % 10 == 0)
                        progress?.Report(percent);
                    progress?.Report(percent);
                }
                await Task.Delay(100);
            }

            progress?.Report(100);
        }

        private async void UploadScanDataBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CmbWHid.Text))
            {
                MessageBox.Show("Select warehouse first.");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                openFileDialog.Title = "Select an Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filepath = openFileDialog.FileName;

                    FileInfo fileInfo = new FileInfo(filepath);

                    var progress = new Progress<int>(value =>
                    {
                        progressBar.Value = value;
                        toolStripStatusLabel1.Text = $"Processing... {value}%";
                    });

                    try
                    {
                        progressBar.Visible = true;
                        toolStripStatusLabel1.Text = "Processing...";
                        await ProcessUpload(fileInfo, progress);
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = "Processing failed!";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        UpdateReturnlogs();
                        progressBar.Visible = false;
                        toolStripStatusLabel1.Text = "Processing completed!";
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShippingItems.Clear();
            UpdateReturnlogs();
        }
    }
}
