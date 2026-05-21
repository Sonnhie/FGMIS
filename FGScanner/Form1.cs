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
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible= false;
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

        private int _transferRowIndex = 0;
        private int DrawTransferSlip(Graphics g, int width, int height, int startX, int startY, TransferSlipData data, string label)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // --- PENS & FONTS ---
            Pen pen = new Pen(Color.Black, 1);
            Pen dashedPen = new Pen(Color.Black, 1) { DashPattern = new float[] { 4, 4 } };
            Font titleFont = new Font("Arial", 22, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 7, FontStyle.Regular);
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

        private void Returntable_SelectionChanged(object sender, EventArgs e)
        {
            decimal total = 0;

            foreach (DataGridViewCell cell in Returntable.SelectedCells)
            {
                if (cell.OwningColumn.Name == "Total Quantity")
                {
                    if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal qty))
                    {
                        total += qty;
                    }
                }
            }
            total_sum.Text = $"Total Quantity: {total:N0}";
        }

    }
}

