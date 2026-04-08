using FGScanner.Model;
using FGScanner.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace FGScanner
{
    public partial class WHRForm : Form
    {
        private int page = 1;
        private int pageSize = 50;
        private int totalRecords = 0;
        private List<TransferSlipData> data = new List<TransferSlipData>();

        public WHRForm()
        {
            InitializeComponent();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            TxtDocNumber.CharacterCasing = CharacterCasing.Upper;
            cmbStorageLocation.SelectedIndex = 0;
        }

        private void LoadData() 
        {
            Filtereddata();
        }

        private void Filtereddata()
        {
            try
            {
                string docnum = TxtDocNumber.Text;
                DateTime from = PostingDate1.Value.Date;
                DateTime to = PostingDate2.Value.Date;
                string location = cmbStorageLocation.Text;

                var Repo = new TransactionRepo();
                var Data = Repo.GetWHReturnData(docnum, location, from, to, page, pageSize);
                var TotalRows = Repo.GetTotalReturnTableRows(docnum, location, from, to);
                totalRecords = (int)Math.Ceiling((double)TotalRows / pageSize);

                if (Data != null)
                {
                    DataTable dt = new DataTable();
                    
                    dt.Columns.Add("Transaction ID", typeof(string));
                    dt.Columns.Add("Posting Date", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Total Quantity", typeof(string));
                    dt.Columns.Add("Storage Location", typeof(string));
                    dt.Columns.Add("Transfer To", typeof(string));

                    LblPage.Text = $"Page {page} of {totalRecords}";

                    foreach (var item in Data)
                    {
                        dt.Rows.Add
                        (
                           item.DocumentNo,
                           item.IssueDate,
                           item.TotalBoxes,
                           item.TotalQuantity,
                           item.LocationFrom,
                           item.LocationTo 
                        );
                    }


                    Returntable.Columns.Clear();
                    Returntable.DataSource = dt;
                    Returntable.ReadOnly = true;

                    Returntable.Columns["Transaction ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    Returntable.Columns["Posting Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    Returntable.Columns["Storage Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    Returntable.Columns["Transfer To"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    Returntable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    Returntable.Columns["Total Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                    {
                        HeaderText = "Action",
                        Text = "Generate document",
                        Name = "btngen",
                        UseColumnTextForButtonValue = true,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    Returntable.Columns.Add(btn);
                    btn.DisplayIndex = Returntable.Columns.Count - 1;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void cmbStorageLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void PLForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            string postingDate1 = PostingDate1.Value.ToString("yyyyMMdd");
            string postingDate2 = PostingDate2.Value.ToString("yyyyMMdd");
            string FileName = $"WH_Return_{postingDate1}-{postingDate2}.xlsx";

            using (SaveFileDialog Save = new SaveFileDialog())
            {
                Save.Filter = "Excel files (*.xlsx)|*.xlsx";
                Save.Title = "Save Exported Data";
                Save.DefaultExt = "xlsx";
                Save.FileName = FileName;

                if (Save.ShowDialog() == DialogResult.OK)
                {
                    string docnum = TxtDocNumber.Text;
                    DateTime from = PostingDate1.Value.Date;
                    DateTime to = PostingDate2.Value.Date;
                    string location = cmbStorageLocation.Text;

                    string filepath = Save.FileName;
                    var Repo = new TransactionRepo();
                    DataTable data = Repo.GetWHReturnExport(docnum, location, from, to);

                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = $"Exporting...";

                    var progress = new Progress<int>(value =>
                    {
                        toolStripProgressBar1.Value = value;
                        toolStripStatusLabel1.Text = $"Exporting... {value}%";
                    });

                    var columnMap = new Dictionary<string, string>
                    {
                        { "transaction_id", "Document No" },
                        { "storage_location", "From Location" },
                        { "ToStorageLocation", "To Location" },
                        { "entry_date", "Issue Date" },
                        { "total_box", "Total Box" },
                        { "total_quantity", "Total Quantity" }
                    };

                    try
                    {
                        await ExportService.ExportCSV(data, columnMap, filepath, progress, "Warehouse Return");
                        toolStripProgressBar1.Value = 100;
                        toolStripStatusLabel1.Text = "Export completed successfully";
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
                        toolStripProgressBar1.Value = 0;
                        toolStripProgressBar1.Visible = false;
                        toolStripStatusLabel1.Text = "";
                    }
                }
            }
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (page >= (totalRecords - page))
            {
                page--;
                BtnNext.Enabled = true;
                LoadData();
            }
            else
            {
                BtnPrev.Enabled = false;
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (page < totalRecords)
            {
                page++;
                BtnPrev.Enabled = true;
                LoadData();
            }
            else
            {
                BtnNext.Enabled = false;
            }
        }

        private void Returntable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == Returntable.Columns["btngen"].Index)
            {
                string documentNo = Returntable.Rows[e.RowIndex].Cells["Transaction ID"].Value.ToString();
                
                var Repo = new TransactionRepo();
                data = Repo.GetTransferSlipData(documentNo);

                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
                printDocument1.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage);
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
                {
                    Document = printDocument1,
                    Width = 800,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterScreen
                };

                printPreviewDialog.ShowDialog();
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
            Font bodyFont = new Font("Arial", 8, FontStyle.Regular);
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

                if (i == 1) // Location has a split sub-header AND data values
                {
                    // 1. Top Section: "Location" Header (18 pixels tall)
                    g.DrawString(topLabels[i], headerFont, Brushes.Black, new RectangleF(cx, y, topCols[i], 18), centerFmt);
                    g.DrawLine(pen, cx, y + 18, cx + topCols[i], y + 18); // First horizontal line

                    // Draw the vertical line down the middle
                    float halfLoc = topCols[i] / 2;
                    g.DrawLine(pen, cx + halfLoc, y + 18, cx + halfLoc, y + persH);

                    // 2. Middle Section: "From" and "To" Sub-headers (14 pixels tall)
                    g.DrawString("From", smallFont, Brushes.Black, new RectangleF(cx, y + 18, halfLoc, 14), centerFmt);
                    g.DrawString("To", smallFont, Brushes.Black, new RectangleF(cx + halfLoc, y + 18, halfLoc, 14), centerFmt);
                    g.DrawLine(pen, cx, y + 32, cx + topCols[i], y + 32); // Second horizontal line

                    // 3. Bottom Section: FILL THE ACTUAL VALUES HERE!
                    // We use data.LocationFrom and data.LocationTo and position them in the bottom space
                    g.DrawString(data.LocationFrom, bodyFont, Brushes.Black, new RectangleF(cx, y + 32, halfLoc, persH - 32), centerFmt);
                    g.DrawString(data.LocationTo, bodyFont, Brushes.Black, new RectangleF(cx + halfLoc, y + 32, halfLoc, persH - 32), centerFmt);
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

            int middleOfPage = DrawTransferSlip(
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

        private void TxtDocNumber_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void PostingDate1_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void PostingDate2_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
