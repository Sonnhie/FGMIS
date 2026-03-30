using FGScanner.Model;
using FGScanner.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace FGScanner
{
    public partial class EcozoneViewer : Form
    {
        Dictionary<string, Button> rackButtons = new Dictionary<string, Button>();
        Dictionary<string, int> RackCountCache = new Dictionary<string, int>();
        Dictionary<string, int> LastRackIDCache = new Dictionary<string, int>();
        private readonly string[] Racks = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q","R","S","T", "FL", "2F" };
        
        private readonly Dictionary<string, (int rows, int cols)> RackConfig = new Dictionary<string, (int rows, int cols)>()
        {
            { "A", (3,14) }, { "B", (3, 14) }, { "C", (3, 14) }, { "D", (3, 14) }, { "E", (3, 14) },
            { "F", (3, 10) }, { "G", (3, 10) }, { "H", (3, 10) }, { "I", (3, 10) }, { "J", (3, 9) },
            { "K", (3, 14) }, { "L", (3, 14) }, { "M", (3, 14) }, { "N", (3, 14) }, { "O", (3, 14) },
            { "P", (3, 14) }, { "Q", (3, 14) }, { "R", (3, 14) }, { "S", (3, 14) }, { "T", (3, 16) },
            { "FL", (2, 14) }, { "2F", (2, 14) }
        };
        // 1. Holds all the cards we are about to print
        private List<InventoryCardData> cardsToPrint = new List<InventoryCardData>();

        // 2. Keeps track of which page the printer is currently on
        private int currentCardIndex = 0;
        private string _userid = string.Empty;
        public EcozoneViewer(string userid)
        {
            InitializeComponent();
            timer1.Interval = 2000;
            timer1.Start();
            typeof(FlowLayoutPanel)
            .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .SetValue(flowLayoutPanel1, true, null);
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
            _userid = userid;
        }

        private void InitializeRackViews(string[] Racks)
        {
            
            foreach (var rack in Racks)
            {
                flowLayoutPanel1.SuspendLayout();
                GenerateRackView(rack);
                flowLayoutPanel1.ResumeLayout();
            }
            
        }
        private void LoadRackCount()
        {
            var Method = new TransactionRepo();
            RackCountCache = Method.GetTotalItemPerRack("WH2").ToDictionary(x => x.RackId, x => x.Count);
        }

        private void LoadData(string partnumber)
        {
            try
            {
                var Method = new TransactionRepo();
                var Datas = Method.RacksList(partnumber, "WH2");

                if (Datas != null)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Pick Order", typeof(string));


                    foreach (var Data in Datas)
                    {
                        if (Data.Quantity != 0)
                        {
                            dt.Rows.Add
                            (
                              Data.RackId,
                              Data.Quantity,
                              Data.Box,
                              Data.PickStatus + " to pick"
                            );
                        }
                    }

                    ListGrid.Columns.Clear();
                    ListGrid.ReadOnly = true;
                    ListGrid.DataSource = dt;
                    ListGrid.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ListGrid.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ListGrid.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ListGrid.Columns["Pick Order"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void GenerateRackView(string RackID)
        {
            var config = RackConfig.ContainsKey(RackID) ? RackConfig[RackID] : (3, 7);
            int rackRows = config.Item1;
            int rackColumns = config.Item2;

            int buttonWidth = 70;
            int buttonHeight = 40;
            int spacing = 2;

            int RackLabelIdentifiation1 = 0;
            int RackLabelIdentifiation2 = 0;

            Panel rackPanel = new Panel
            {
                Width = (rackColumns + 1) * (buttonWidth + spacing),
                Height = rackRows * (buttonHeight + spacing),
                Margin = new Padding(10),
                Tag = RackID,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Rack title label
            Label rackTitle = new Label
            {
                Text = RackID,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Width = buttonWidth,
                Height = rackPanel.Height,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };

            rackPanel.Controls.Add(rackTitle);

            for (int row = 0; row < rackRows; row++)
            {
                RackLabelIdentifiation1++;
                RackLabelIdentifiation2 = 0;

                for (int col = 1; col <= rackColumns; col++)
                {
                    RackLabelIdentifiation2++;

                    string RackLabel = $"{RackID}{RackLabelIdentifiation1}-{RackLabelIdentifiation2:D2}";

                    Button btn = new Button
                    {
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Left = col * (buttonWidth + spacing),
                        Top = row * (buttonHeight + spacing),
                        Text = RackLabel,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        BackColor = Color.Green,
                        ForeColor = Color.White
                    };

                    btn.Click += Buttom_Click;

                    rackPanel.Controls.Add(btn);
                    rackButtons[RackLabel] = btn;
                }
            }

            flowLayoutPanel1.Controls.Add(rackPanel);
        }

        private void UpdateRackUI(string rackLabel)
        {
            if (!rackButtons.ContainsKey(rackLabel))
            {
                return;
            }

            Button btn = rackButtons[rackLabel];
            int RackCountValue = RackCountCache.TryGetValue(rackLabel, out int count) ? count : 0;

            if (RackCountValue == 0)
            {
                btn.BackColor = Color.Green;
                btn.ForeColor = Color.White;
            }
            else if (RackCountValue >= 1)
            {
                btn.BackColor = Color.Blue;
                btn.ForeColor = Color.White;
            }
        }

        private void LoadChangeRacks()
        {
            var repo = new TransactionRepo();
            var NewRackIDs = repo.GetLatestId();

            foreach (var item in NewRackIDs)
            {
                if (!LastRackIDCache.ContainsKey(item.Key) || LastRackIDCache[item.Key] != item.Value)
                {
                    int newCount = repo.GetItemCountByRack(item.Key, "WH2");

                    RackCountCache[item.Key] = newCount;

                    UpdateRackUI(item.Key);

                    LastRackIDCache[item.Key] = item.Value;
                }
            }
        }

        public void Loadtransactionlogs(string location)
        {
            try
            {
                var Method = new TransactionRepo();
                var Datas = Method.GetItemByLocation(location, "WH2");

                if (Datas != null)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Pick Order", typeof(string));

                    foreach (var Data in Datas)
                    {
                        if (Data.Quantity != 0)
                        {
                            dt.Rows.Add
                            (
                              Data.PartNumber,
                              Data.Quantity,
                              Data.Box,
                              Data.ProductionDate.ToString("MM/dd/yyyy"),
                              Data.ProductionVersion,
                              Data.Customer,
                              Data.Status + " to pick"
                            );
                        }
                    }

                    RackDataGridView.Columns.Clear();
                    RackDataGridView.ReadOnly = true;
                    RackDataGridView.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Buttom_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string location = clickedButton.Text;

            Loadtransactionlogs(location);
            LblRack.Text = location;
           // MessageBox.Show($"You clicked: {clickedButton.Text}");

        }

        private void EcozoneViewer_Load(object sender, EventArgs e)
        {
            LoadRackCount();
            InitializeRackViews(Racks);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadChangeRacks();
        }

        private void TxtPartnumber_TextChanged_1(object sender, EventArgs e)
        {
            string partnumber = TxtPartnumber.Text;
            LoadData(partnumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string location = LblRack.Text;
            string whid = "WH2";

            var Repo = new TransactionRepo();
            cardsToPrint = Repo.GetInventoryCardData(location, whid);

            if (cardsToPrint == null || cardsToPrint.Count == 0)
            {
                MessageBox.Show("No inventory found in this location.", "Empty Rack", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Generate QR codes for all cards
            foreach (var card in cardsToPrint)
            {
                int id = Repo.GetId(card.PartNo);
                card.ControlNo = id;
                string qrPayload = $"{card.ControlNo}/{card.PartNo}/O{card.GrandTotalQuantity}QB{card.GrandTotalBoxes}PPS{card.PPS}ERP{card.ErpLocation}";
                card.QrCode = GenerateQRCode(qrPayload);
                
            }

            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

            printDocument1.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            printDocument1.BeginPrint -= new PrintEventHandler(printDocument1_BeginPrint);
            printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument1;
            printPreviewDialog.Width = 800;
            printPreviewDialog.Height = 800;
            printPreviewDialog.PrintPreviewControl.Columns = cardsToPrint.Count >= 2 ? 2 : 1;
            printPreviewDialog.ShowDialog();
        }

        private void DrawInventoryCard(Graphics g, int width, int height, int startX, int startY, InventoryCardData data)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // --- SETUP FONTS & PENS ---
            Pen borderPen = new Pen(Color.Black, 1);
            Pen linePen = new Pen(Color.Black, 1);
            Pen dashedPen = new Pen(Color.Black, 1) { DashPattern = new float[] { 4, 4 } };

            Font smallFont = new Font("Arial", 8);
            Font bodyFont = new Font("Arial", 10, FontStyle.Bold);
            Font headerFont = new Font("Arial", 18, FontStyle.Regular);
            Font largeDataFont = new Font("Arial Narrow", 28, FontStyle.Regular);
            Font watermarkFont = new Font("Arial", 60, FontStyle.Bold);

            Brush textBrush = Brushes.Black;
            Brush watermarkBrush = new SolidBrush(Color.FromArgb(60, 128, 128, 128));

            StringFormat centerFmt = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat rightFmt = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
            StringFormat leftFmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

            int stubHeight = 80;
            int mainHeight = height - stubHeight - 30;

            // --- DRAW BORDER ---
            g.DrawRectangle(borderPen, startX, startY, width, mainHeight);

            // --- DRAW HEADERS WITH REAL DATA ---
            int currentY = startY;
            int rowH = 25;

            g.DrawString("Inventory Card", bodyFont, textBrush, new Rectangle(startX, currentY, width, rowH), centerFmt);
            currentY += rowH; g.DrawLine(borderPen, startX, currentY, startX + width, currentY);

            // Month & Year (e.g., "2026 MARCH")
            rowH = 35;
            g.DrawString(data.MonthYear, headerFont, textBrush, new Rectangle(startX, currentY, width, rowH), centerFmt);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            // ERP & Prepared By
            rowH = 30;
            int midX = startX + (int)(width * 0.34);
            int midX2 = startX + (int)(width * 0.50);
            int midX3 = startX + (int)(width * 0.75);
            g.DrawString("ERP Location:", bodyFont, textBrush, new Rectangle(startX + 5, currentY, midX - startX, rowH), leftFmt);

            // FILL ERP LOCATION
            g.DrawString(data.ErpLocation, headerFont, textBrush, new Rectangle(midX, currentY, midX2 - midX, rowH), centerFmt);

            g.DrawString("Prepared by:", bodyFont, textBrush, new Rectangle(midX2 + 5, currentY, midX3 - midX2, rowH), leftFmt);

            // FILL PREPARED BY
            g.DrawString(_userid, headerFont, textBrush, new Rectangle(midX3, currentY, (startX + width) - midX3, rowH), centerFmt);

            g.DrawLine(linePen, midX, currentY, midX, currentY + rowH);
            g.DrawLine(linePen, midX2, currentY, midX2, currentY + rowH);
            g.DrawLine(linePen, midX3, currentY, midX3, currentY + rowH);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            // Control No
            rowH = 50;
            g.DrawString("Control no.", bodyFont, textBrush, new Rectangle(startX + 5, currentY, midX - startX, rowH), leftFmt);

            // FILL CONTROL NO
            g.DrawString(data.ControlNo.ToString(), largeDataFont, textBrush, new Rectangle(midX, currentY, (startX + width) - midX, rowH), centerFmt);
            g.DrawLine(linePen, midX, currentY, midX, currentY + rowH * 2);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            // Part No
            g.DrawString("Part No.", headerFont, textBrush, new Rectangle(startX + 5, currentY, midX - startX, rowH), leftFmt);

            // FILL PART NO
            g.DrawString(data.PartNo, largeDataFont, textBrush, new Rectangle(midX, currentY, (startX + width) - midX, rowH), centerFmt);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            // --- DRAW TABLE GRID & ROWS ---
            rowH = 40;
            int tableStartY = currentY;
            float[] colW = { width * 0.34f, width * 0.16f, width * 0.22f, width * 0.28f };
            string[] cols = { "Lot No.", "No.\nof Boxes", "Quantity", "Total Qty." };

            float cx = startX;
            for (int i = 0; i < cols.Length; i++)
            {
                g.DrawString(cols[i], bodyFont, textBrush, new RectangleF(cx, currentY, colW[i], rowH), centerFmt);
                cx += colW[i];
                if (i < cols.Length - 1) g.DrawLine(linePen, cx, currentY, cx, startY + mainHeight - rowH);
            }
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            // LOOP THROUGH ACTUAL DATABASE ROWS
            for (int r = 0; r < 14; r++) // We loop 9 times to draw the empty grid lines even if there are only 2 rows
            {
                if (r < data.Rows.Count)
                {
                    var row = data.Rows[r]; // Get the specific row from our DB list
                    cx = startX;
                    g.DrawString(row.LotNo, largeDataFont, textBrush, new RectangleF(cx, currentY, colW[0], rowH), centerFmt); cx += colW[0];
                    g.DrawString(row.Boxes.ToString(), largeDataFont, textBrush, new RectangleF(cx, currentY, colW[1], rowH), centerFmt); cx += colW[1];
                    g.DrawString(row.Quantity.ToString(), largeDataFont, textBrush, new RectangleF(cx, currentY, colW[2], rowH), centerFmt); cx += colW[2];
                    g.DrawString(row.TotalQty.ToString("N0"), largeDataFont, textBrush, new RectangleF(cx, currentY, colW[3], rowH), centerFmt);
                }
                currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);
            }

            // --- GRAND TOTALS ---
            g.DrawString("Grand Total", headerFont, textBrush, new Rectangle(startX + 5, currentY, (int)colW[0], rowH), leftFmt);

            // FILL GRAND TOTAL BOXES
            g.DrawString(data.GrandTotalBoxes.ToString(), largeDataFont, textBrush, new RectangleF(startX + colW[0], currentY, colW[1], rowH), centerFmt);

            // FILL GRAND TOTAL QUANTITY
            g.DrawString(data.GrandTotalQuantity.ToString("N0"), largeDataFont, textBrush, new RectangleF(startX + colW[0] + colW[1] + colW[2], currentY, colW[3], rowH), centerFmt);

            // --- FOOTER & CUT LINE ---
            int footerY = startY + mainHeight + 2;
            g.DrawString("CF-260(Rev. 00)", smallFont, textBrush, startX, footerY);
            g.DrawString(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), smallFont, textBrush, new Rectangle(startX, footerY, width, 15), rightFmt);

            int cutLineY = footerY + 15;
            g.DrawLine(dashedPen, startX, cutLineY, startX + width, cutLineY);

            // --- BOTTOM STUBS (WITH DB DATA AND REAL QR CODE) ---
            int stubY = cutLineY + 10;
            int stubW = (width / 2) - 10;

            for (int i = 0; i < 2; i++)
            {
                int sx = startX + (i * (stubW + 20));
                g.DrawRectangle(linePen, sx, stubY, stubW, stubHeight);

                int labelW = 70;
                int qrSize = stubHeight - 10;
                int dataW = stubW - labelW - qrSize - 10;

                g.DrawLine(linePen, sx + labelW, stubY, sx + labelW, stubY + stubHeight);
                g.DrawLine(linePen, sx + labelW + dataW, stubY, sx + labelW + dataW, stubY + stubHeight);
                g.DrawLine(linePen, sx, stubY + 26, sx + labelW + dataW, stubY + 26);
                g.DrawLine(linePen, sx, stubY + 52, sx + labelW + dataW, stubY + 52);

                g.DrawString("Control no.", smallFont, textBrush, sx + 2, stubY + 6);
                g.DrawString("Part No.", smallFont, textBrush, sx + 2, stubY + 32);
                g.DrawString("Quantity", smallFont, textBrush, sx + 2, stubY + 58);

                // FILL STUB DATA
                g.DrawString(data.ControlNo.ToString(), smallFont, textBrush, new Rectangle(sx + labelW, stubY, dataW, 26), centerFmt);
                g.DrawString(data.PartNo, smallFont, textBrush, new Rectangle(sx + labelW, stubY + 26, dataW, 26), centerFmt);
                g.DrawString(data.GrandTotalQuantity.ToString(), smallFont, textBrush, new Rectangle(sx + labelW, stubY + 52, dataW, 26), centerFmt);

                // DRAW THE REAL QR CODE FROM THE DATA MODEL
                if (data.QrCode != null)
                {
                    g.DrawImage(data.QrCode, sx + labelW + dataW + 5, stubY + 5, qrSize, qrSize);
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            InventoryCardData cardForthisPage = cardsToPrint[currentCardIndex];

            DrawInventoryCard(e.Graphics, e.MarginBounds.Width, e.MarginBounds.Height, e.MarginBounds.Left, e.MarginBounds.Top, cardForthisPage);
            currentCardIndex++;

            if (currentCardIndex < cardsToPrint.Count)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private Image GenerateQRCode(string QRData)
        {
            BarcodeDraw qrcodeDraw = BarcodeDrawFactory.CodeQr;
            Image qrcodeImage = qrcodeDraw.Draw(QRData, 100);
            return qrcodeImage;
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            currentCardIndex = 0;
        }
    }
}
