using FGScanner.Model;
using FGScanner.Util;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class WarehouseReturn : Form
    {
        private readonly db_connection _Connection;
        private readonly string user_id;
        private List<TransferSlipData> data = new List<TransferSlipData>();

        public WarehouseReturn(string user)
        {
            InitializeComponent();
            _Connection = new db_connection();
            user_id = user;
            LoadAutosuggest();
            toolStripStatusLabel1.Visible = false;
        }

        private readonly BindingList<ScannedModel> ShippingItems = new BindingList<ScannedModel>();
        
        private void LoadAutosuggest()
        {
            string whid = CmbWHid.Text;
            var List = new TransactionRepo();
            var data = List.GetRackLocations(whid);

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            auto.AddRange(data.ToArray());

            TxtRackno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TxtRackno.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TxtRackno.AutoCompleteCustomSource = auto;
            //TxtRackno.DataSource = auto;
        }
       
        private bool OnScanProcess(string QRCode)
        {
            var Process = new ScannerUtility();
            var Insert = new TransactionRepo();

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

            int partcount = ShippingItems.Count(x => x.PartNumber == itemModel.PartNumber);
            int StockCount = Insert.CheckStock(itemModel.PartNumber, TxtRackno.Text);
            var lastIndex = ShippingItems.Count - 1;

            if (StockCount > partcount)
            {
                ShippingItems.Add(new ScannedModel
                {
                    TransactionDate = DateTime.Now,
                    Customer = Insert.GetCustomer(itemModel.PartNumber),
                    PartNumber = itemModel.PartNumber,
                    ProductionDate = itemModel.ProductionDate,
                    ProductionVersion = itemModel.ProductionVer,
                    Quantity = itemModel.Quantity,
                    TransactionType = "OUT",
                    Location = TxtRackno.Text,
                    Storage_location = cmbfrom.Text,
                    New_Location = cmbto.Text,
                    Remarks = remarktext.Text,
                    TransactionId = controlnumberLabel.Text,
                    Whid = CmbWHid.Text
                });
            }
            else
            {
                MessageBox.Show($"Scanned quantity for part number {itemModel.PartNumber} exceeds available stock in the selected rack. Current scanned quantity: {partcount}, Available stock: {StockCount}", "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            UpdateReturnlogs();
            return true;
        }

        private void UpdateReturnlogs()
        {
            BindingSource source = new BindingSource
            {
                DataSource = ShippingItems
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
                                WhId = CmbWHid.Text
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
                        //Repo.RunMovementClassification();
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
            TxtScanData.Focus();
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

        private int DrawTransferSlip(Graphics g, int width, int height, int startX, int startY, TransferSlipData data)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // --- PENS & FONTS ---
            Pen pen = new Pen(Color.Black, 1);
            Pen dashedPen = new Pen(Color.Black, 1) { DashPattern = new float[] { 4, 4 } };

            Font titleFont = new Font("Arial", 22, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 10, FontStyle.Regular);
            Font smallFont = new Font("Arial", 8, FontStyle.Regular);

            StringFormat centerFmt = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat leftFmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

            int y = startY;
            int docNoWidth = (int)(width * 0.15f); // 15% of the paper width
            // --- 1. TITLE & DOCUMENT NO ROW ---
            int titleH = 40;
            g.DrawRectangle(pen, startX, y, width, titleH);
            g.DrawString("TRANSFER SLIP", titleFont, Brushes.Black, new Rectangle(startX, y, width - docNoWidth, titleH), centerFmt);

            // Document No Box
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

            // Calculate widths for the upper sections
            float[] topCols = { width * 0.15f, width * 0.25f, width * 0.15f, width * 0.15f, width * 0.15f, width * 0.15f };
            float cx = startX;

            string[] topLabels = { "Issue Date", "Location", "Prepared by:", "Checked by:", "Received by:", "Encoded by:" };
            string[] topValues = { data.IssueDate, "", data.PreparedBy, data.CheckedBy, data.ReceivedBy, data.EncodedBy };

            for (int i = 0; i < topCols.Length; i++)
            {
                // Draw vertical lines
                if (i > 0) g.DrawLine(pen, cx, y, cx, y + persH);

                if (i == 1) // Location has a split sub-header
                {
                    g.DrawString(topLabels[i], headerFont, Brushes.Black, new RectangleF(cx, y, topCols[i], persH / 2), centerFmt);
                    g.DrawLine(pen, cx, y + persH / 2, cx + topCols[i], y + persH / 2); // Horizontal split

                    float halfLoc = topCols[i] / 2;
                    g.DrawLine(pen, cx + halfLoc, y + persH / 2, cx + halfLoc, y + persH); // Vertical split
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

            // Main Columns: MatCode(15%), MatName(21%), Rev(5%), Insp(10%), Prod(10%), Qty(19%), Remarks(20%)
            float[] colW = { width * 0.15f, width * 0.21f, width * 0.05f, width * 0.10f, width * 0.10f, width * 0.19f, width * 0.20f };
            string[] headers = { "Material Code", "Material Name", "Rev.\nNo", "Inspection\nDate", "Production\nDate", "Quantity", "Remarks" };

            cx = startX;
            for (int i = 0; i < colW.Length; i++)
            {
                if (i > 0) g.DrawLine(pen, cx, y, cx, y + headH); // Vertical Lines

                if (i == 5) // Quantity has sub-headers (No.Box, PPS, Pcs)
                {
                    g.DrawString(headers[i], headerFont, Brushes.Black, new RectangleF(cx, y, colW[i], headH / 2), centerFmt);
                    g.DrawLine(pen, cx, y + headH / 2, cx + colW[i], y + headH / 2); // Horizontal split

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
            int numRows = 10; // Draw 10 empty lines for the grid

            // Flatten the column widths for data drawing
            float[] dataColW = { colW[0], colW[1], colW[2], colW[3], colW[4], colW[5] / 3, colW[5] / 3, colW[5] / 3, colW[6] };

            for (int r = 0; r < numRows; r++)
            {
                cx = startX;
                for (int c = 0; c < dataColW.Length; c++)
                {
                    if (c > 0) g.DrawLine(pen, cx, y, cx, y + rowH); // Vertical grid lines

                    // If we have actual data in the list, print it
                    if (data != null && r < data.Rows.Count)
                    {
                        var dRow = data.Rows[r];
                        // (Paste this instead)
                        string cellData = "";
                        switch (c)
                        {
                            case 0: cellData = dRow.MaterialCode; break;
                            case 1: cellData = dRow.MaterialName; break;
                            case 2: cellData = dRow.RevNo; break;
                            case 3: cellData = dRow.InspectionDate; break;
                            case 4: cellData = dRow.ProductionDate; break;
                            case 5: cellData = dRow.NoBox.ToString(); break;
                            case 6: cellData = dRow.PPS.ToString(); break;
                            case 7: cellData = dRow.Pcs; break;
                            case 8: cellData = dRow.Remarks; break;
                        }
                        g.DrawString(cellData, bodyFont, Brushes.Black, new RectangleF(cx, y, dataColW[c], rowH), centerFmt);
                    }
                    cx += dataColW[c];
                }

                // Outer border and horizontal line
                g.DrawLine(pen, startX, y + rowH, startX + width, y + rowH);
                g.DrawLine(pen, startX, y, startX, y + rowH); // Left border
                g.DrawLine(pen, startX + width, y, startX + width, y + rowH); // Right border
                y += rowH;
            }

            // --- 6. FOOTER ---
            g.DrawString("CF-140 (Rev.00)", smallFont, Brushes.Black, startX, y + 5);
            y += 25;
            g.DrawLine(dashedPen, startX, y, startX + width, y); // The dashed cut line

            // ADD THIS LINE: Return the final Y position + 20 pixels of spacing
            return y + 20;
        }
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            TransferSlipData myData = data.FirstOrDefault();

            if (myData == null)
            {
                e.Graphics.DrawString("No data available to print.", new Font("Arial", 12), Brushes.Black, new PointF(100, 100));
                return;
            }

            int middleOfPage =  DrawTransferSlip(
                e.Graphics,
                e.MarginBounds.Width,
                e.MarginBounds.Height,
                e.MarginBounds.Left,
                e.MarginBounds.Top,
                myData
            );

            DrawTransferSlip(
                    e.Graphics,
                    e.MarginBounds.Width,
                    e.MarginBounds.Height,
                    e.MarginBounds.Left,
                    middleOfPage,
                    myData
                );
        }
        
        private void TxtScanData_KeyDown(object sender, KeyEventArgs e)
        {
            var List = new TransactionRepo();
            string whid = CmbWHid.Text;
            var data = List.GetRackLocations(whid);

            if (!data.Contains(TxtRackno.Text))
            {
                MessageBox.Show("Rack no. is invalid!", "Error location");
                TxtRackno.Focus();
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                var ParsedData = TxtScanData.Text;
                var IsProcessed = OnScanProcess(ParsedData);
                if (IsProcessed)
                {
                    TxtScanData.Clear();
                    e.SuppressKeyPress = true;
                    TxtScanData.Focus();
                }
                else
                {
                    TxtScanData.Clear();
                }
            }
        }
        
        private void CmbWHid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadAutosuggest();
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

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument1;
            printPreviewDialog.Width = 800;
            printPreviewDialog.Height = 800;
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
    }
}
