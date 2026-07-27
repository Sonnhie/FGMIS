using FGScanner.Database;
using FGScanner.Repositories;
using FGScanner.Services;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace FGScanner.Forms.Viewer
{
    public partial class MainWarehouseViewer : UserControl
    {
        private readonly SemaphoreSlim _dbLock = new SemaphoreSlim(1, 1);
        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;
        private readonly ExcelService _excelService;
        private readonly PrintService _printService;

        private Dictionary<string, Button> rackButtons = [];
        private Dictionary<string, int> RackCountCache = [];
        private Dictionary<string, string> RackCustomerCache = [];
        private Dictionary<string, int> LastRackIDCache = [];

        private readonly string[] Racks = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "FL" };
        private readonly string whId = "WH2";
        private readonly Dictionary<string, (int rows, int cols)> RackConfig = new Dictionary<string, (int rows, int cols)>()
        {
            { "A", (3,7) }, { "B", (3, 7) }, { "C", (3, 7) }, { "D", (3, 7) }, { "E", (3, 7) },
            { "F", (3, 7) }, { "G", (3, 7) }, { "H", (3, 7) }, { "I", (3, 7) }, { "J", (3, 7) },
            { "FL", (1, 15) }
        };

        private List<FGScanner.Models.InventoryCardData> cardsToPrint = new();
        private int currentCardIndex = 0;
        private string _userid = string.Empty;

        public MainWarehouseViewer(string userid)
        {
            InitializeComponent();

            timer1.Interval = 2000;
            timer1.Start();
            typeof(FlowLayoutPanel)
            .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .SetValue(flowLayoutPanel1, true, null);
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
            _excelService = new(_queries);
            _printService = new(_queries);
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

        private async Task LoadData(string partnumber)
        {
            try
            {
                var Datas = await _queries.GetItemByPartnumber(partnumber, whId);

                if (Datas != null)
                {
                    DataTable dt = new();

                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));


                    foreach (var Data in Datas)
                    {
                        if (Data.Quantity != 0)
                        {
                            dt.Rows.Add
                            (
                              Data.Location,
                              Data.Quantity,
                              Data.TotalBox
                            );
                        }
                    }

                    ListGrid.Columns.Clear();
                    ListGrid.ReadOnly = true;
                    ListGrid.DataSource = dt;
                    ListGrid.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ListGrid.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ListGrid.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void GenerateRackView(string RackID)
        {
            var config = RackConfig.TryGetValue(RackID, out (int rows, int cols) value) ? value : (3, 7);
            int rackRows = config.Item1;
            int rackColumns = config.Item2;

            int buttonWidth = 80;
            int buttonHeight = 40;
            int spacing = 2;

            int RackLabelIdentifiation1 = 0;
            int RackLabelIdentifiation2 = 0;

            Panel rackPanel = new()
            {
                Width = (rackColumns + 1) * (buttonWidth + spacing),
                Height = rackRows * (buttonHeight + spacing),
                Margin = new Padding(10),
                Tag = RackID,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Rack title label
            Label rackTitle = new()
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

                    Button btn = new()
                    {
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Left = col * (buttonWidth + spacing),
                        Top = row * (buttonHeight + spacing),
                        Text = RackLabel,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        BackColor = Color.White,
                        ForeColor = Color.Black
                    };

                    btn.Click += Buttom_Click;

                    rackPanel.Controls.Add(btn);
                    rackButtons[RackLabel] = btn;
                }
            }

            flowLayoutPanel1.Controls.Add(rackPanel);
        }

        private async Task UpdateRackUI(string rackLabel)
        {
            if (!rackButtons.TryGetValue(rackLabel, out Button btn))
            {
                return;
            }

            int RackCountValue = RackCountCache.TryGetValue(rackLabel, out int quantity) ? quantity : 0;
            var customer = await _queries.GetRackCustomer(rackLabel, whId);


            if (customer == "EPPI" && RackCountValue > 0)
            {
                btn.BackColor = Color.LightGreen;
            }
            else if (customer == "YAZAKI" && RackCountValue > 0)
            {
                btn.BackColor = Color.MediumPurple;
            }
            else if (customer == "BIPH" && RackCountValue > 0)
            {
                btn.BackColor = Color.SkyBlue;
            }
            else if (RackCountValue > 0)
            {
                btn.BackColor = Color.Gold;
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }

        private async Task LoadCache()
        {
            var result = await _queries.GetRackQuantity(whId);
            RackCountCache = result.ToDictionary(x => x.Location, x => x.Quantity);
        }

        private async Task LoadChangeRacks()
        {
            Dictionary<string, int> Ids = [];
            Ids = await _queries.GetRackIds(whId);

            foreach (var item in Ids)
            {
                if (!LastRackIDCache.TryGetValue(item.Key, out int value) || value != item.Value)
                {
                    int newCount = await _queries.GetRackQty(item.Key, whId);
                    RackCountCache[item.Key] = newCount;
                    await UpdateRackUI(item.Key);
                    value = item.Value;
                    LastRackIDCache[item.Key] = value;
                }
            }
        }

        public async Task Loadtransactionlogs(string location)
        {
            try
            {
                var Datas = await _queries.GetItemByLocation(location, whId);
                var totalBox = Datas
                               .Sum(d => d.TotalBox);
                var totalQty = Datas
                               .Sum(d => d.Quantity);
                total_box_lbl.Text = $"Total Box: {totalBox:N0}";
                total_sum.Text = $"Total Qty: {totalQty:N0}";

                if (Datas != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Warehouse", typeof(string));


                    foreach (var Data in Datas)
                    {
                        if (Data.Quantity != 0)
                        {
                            dt.Rows.Add
                            (
                              Data.Partnumber,
                              Data.Quantity,
                              Data.TotalBox,
                              Data.ProdDate.ToString("MM/dd/yyyy"),
                              Data.ProdVer,
                              Data.CustomerId,
                              Data.WhId
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

        private async void Buttom_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string location = clickedButton.Text;
            timer1.Stop();
            await _dbLock.WaitAsync();
            try
            {
                await Loadtransactionlogs(location);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                _dbLock.Release();
                timer1.Start();
            }

            LblRack.Text = location;
        }

        private static Image GenerateQRCode(string QRData)
        {
            BarcodeDraw qrcodeDraw = BarcodeDrawFactory.CodeQr;
            Image qrcodeImage = qrcodeDraw.Draw(QRData, 100);
            return qrcodeImage;
        }

        private async void MainWarehouseViewer_Load(object sender, EventArgs e)
        {
            await LoadCache();
            InitializeRackViews(Racks);
            timer1.Start();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            await _dbLock.WaitAsync();
            try
            {
                await LoadChangeRacks();
            }
            finally
            {
                _dbLock.Release();
                timer1.Start();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            _printService.ProcessPrintPage(cardsToPrint, e, _userid);
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _printService.Reset();
        }

        private async void TxtPartnumber_TextChanged(object sender, EventArgs e)
        {
            string partnumber = TxtPartnumber.Text;
            timer1.Stop();

            await _dbLock.WaitAsync(); // Wait for a green light
            try
            {
                await LoadData(partnumber);
            }
            finally
            {
                _dbLock.Release(); // Turn the light green for the next operation
                timer1.Start();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string location = LblRack.Text;

            if (string.IsNullOrEmpty(location))
            {
                MessageBox.Show("Invalid rack location.");
                return;
            }

            timer1.Stop();
            await _dbLock.WaitAsync();

            try
            {
                var data = await _queries.GetInventoryCardDataByLocation(location, whId, _userid);
                cardsToPrint.Clear();
                cardsToPrint.AddRange(data);
            }
            finally
            {
                _dbLock.Release();
            }


            if (cardsToPrint == null || cardsToPrint.Count == 0)
            {
                MessageBox.Show("No inventory found in this location.", "Empty Rack", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var card in cardsToPrint)
            {
                int id = card.id;
                card.ControlNo = id;
                string qrPayload = $"{card.ControlNo}/{card.PartNo}/O{card.GrandTotalQuantity}QB{card.GrandTotalBoxes}PPS{card.PPS}ERP{card.ErpLocation}";
                card.QrCode = GenerateQRCode(qrPayload);
            }

            foreach (PaperSize ps in printDocument1.PrinterSettings.PaperSizes)
            {
                if (ps.Kind == PaperKind.A4)
                {
                    printDocument1.DefaultPageSettings.PaperSize = ps;
                    break;
                }
            }
            printDocument1.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

            printDocument1.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            printDocument1.BeginPrint -= new PrintEventHandler(printDocument1_BeginPrint);
            printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);

            PrintPreviewDialog printPreviewDialog = new();
            printPreviewDialog.Document = printDocument1;
            printPreviewDialog.Width = 800;
            printPreviewDialog.Height = 800;
            printPreviewDialog.PrintPreviewControl.Columns = cardsToPrint.Count >= 2 ? 2 : 1;
            printPreviewDialog.ShowDialog();
        }
    }
}
