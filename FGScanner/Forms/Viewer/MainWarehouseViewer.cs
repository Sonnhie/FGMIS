using FGScanner.Database;
using FGScanner.Models;
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
using FGScanner.UI;

namespace FGScanner.Forms.Viewer
{
    public partial class MainWarehouseViewer : UserControl
    {
        private readonly SemaphoreSlim _dbLock = new SemaphoreSlim(1, 1);
        private bool _isInitializing;
        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
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
        private bool _realtimeSubscribed;
        private Label _searchStatusLabel;
        private readonly HashSet<string> _searchMatches = new(StringComparer.OrdinalIgnoreCase);
        private string _selectedLocation;

        public MainWarehouseViewer(string userid)
        {
            InitializeComponent();

            timer1.Interval = (int)TimeSpan.FromMinutes(2).TotalMilliseconds;

            // Setup UI and Services FIRST
            ViewerPresentation.EnableDoubleBuffering(this);
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
            _searchStatusLabel = ViewerPresentation.ConfigureCollapsibleDetails(
                this, panel3, panel1, label13, label1, TxtPartnumber, () =>
                {
                    _selectedLocation = null;
                    ApplyRackEmphasis();
                });

            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
            _excelService = new(_queries);
            _printService = new(_queries);
        }


        private void InitializeRackViews(string[] Racks)
        {
            CinemaRackLayout.Build(flowLayoutPanel1, Racks, RackConfig, rackButtons, Buttom_Click);
        }

        private async Task LoadData(string partnumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(partnumber))
                {
                    _searchMatches.Clear();
                    _searchStatusLabel.Text = string.Empty;
                    ListGrid.DataSource = null;
                    ApplyRackEmphasis();
                    return;
                }

                _searchMatches.Clear();
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
                            if (!string.IsNullOrWhiteSpace(Data.Location))
                            {
                                _searchMatches.Add(Data.Location);
                            }

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
                    _searchStatusLabel.Text = _searchMatches.Count == 1
                        ? "1 location"
                        : $"{_searchMatches.Count} locations";
                    ApplyRackEmphasis();
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

            int buttonWidth = 70;
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
                Font = new Font("Bahnschrift", 11, FontStyle.Bold),
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
                        Font = new Font("Bahnschrift", 9, FontStyle.Regular),
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
            btn.ForeColor = Color.Black;
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
                    dt.Columns.Add("PPS Type", typeof(string));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("Total Box", typeof(int));
                    dt.Columns.Add("Production Date", typeof(DateTime));
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
                              Data.Remarks,
                              Data.Quantity,
                              Data.TotalBox,
                              Data.ProdDate.ToDateTime(TimeOnly.MinValue),
                              Data.ProdVer,
                              Data.Customer,
                              Data.WhId
                            );
                        }
                    }

                    RackDataGridView.Columns.Clear();
                    RackDataGridView.ReadOnly = true;
                    RackDataGridView.DataSource = dt;
                    ConfigureRackDetailsTable(RackDataGridView);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private static void ConfigureRackDetailsTable(DataGridView grid)
        {
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.Columns["PPS Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns["Warehouse"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            grid.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grid.Columns["Quantity"].DefaultCellStyle.Format = "N0";
            grid.Columns["Total Box"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grid.Columns["Total Box"].DefaultCellStyle.Format = "N0";
            grid.Columns["Production Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns["Production Date"].DefaultCellStyle.Format = "MM/dd/yyyy";
            grid.Columns["PPS Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns["Production Version"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns["Warehouse"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private async void Buttom_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string location = clickedButton.Tag as string ?? clickedButton.Text;
            _selectedLocation = location;
            LblRack.Text = location;
            ApplyRackEmphasis();
            ViewerPresentation.ShowDetails(this, panel1);
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
        }

        private void ApplyRackEmphasis()
        {
            bool hasSearch = !string.IsNullOrWhiteSpace(TxtPartnumber.Text);

            foreach (Control section in rackButtons.Values
                         .Select(button => button.Parent?.Parent)
                         .Where(section => section != null)
                         .Distinct())
            {
                section.BackColor = Color.FromArgb(37, 55, 77);
            }

            foreach (var item in rackButtons)
            {
                Button button = item.Value;
                bool isSelected = string.Equals(item.Key, _selectedLocation, StringComparison.OrdinalIgnoreCase);
                bool isMatch = _searchMatches.Contains(item.Key);

                button.FlatAppearance.BorderSize = isSelected ? 4 : isMatch ? 3 : 1;
                button.FlatAppearance.BorderColor = isSelected
                    ? Color.FromArgb(255, 111, 0)
                    : isMatch
                        ? Color.FromArgb(0, 120, 215)
                        : hasSearch ? Color.Gainsboro : Color.FromArgb(122, 136, 153);
                button.ForeColor = hasSearch && !isMatch && !isSelected ? Color.DarkGray : Color.Black;

                if (button.Parent?.Parent is Control section)
                {
                    if (isMatch)
                    {
                        section.BackColor = Color.FromArgb(0, 90, 160);
                    }

                    if (isSelected)
                    {
                        section.BackColor = Color.FromArgb(185, 78, 0);
                    }
                }
            }
        }

        private static Image GenerateQRCode(string QRData)
        {
            BarcodeDraw qrcodeDraw = BarcodeDrawFactory.CodeQr;
            Image qrcodeImage = qrcodeDraw.Draw(QRData, 100);
            return qrcodeImage;
        }

        private async void MainWarehouseViewer_Load(object sender, EventArgs e)
        {
            _isInitializing = true;
            timer1.Stop();
            flowLayoutPanel1.Visible = false;
            await _dbLock.WaitAsync();

            try
            {
                await LoadCache();
                InitializeRackViews(Racks);
                await LoadChangeRacks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load warehouse viewer: {ex.Message}", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ApplyRackEmphasis();
                flowLayoutPanel1.Visible = true;
                _dbLock.Release();
                _isInitializing = false;
                timer1.Start();
                StartRealtimeUpdates();
            }
        }

        private void StartRealtimeUpdates()
        {
            if (_realtimeSubscribed)
            {
                return;
            }

            _realtimeSubscribed = true;
            InventoryRealtimeClient.InventoryChanged += InventoryRealtimeClient_InventoryChanged;
            Disposed += (_, _) =>
            {
                InventoryRealtimeClient.InventoryChanged -= InventoryRealtimeClient_InventoryChanged;
                _realtimeSubscribed = false;
            };
            _ = InventoryRealtimeClient.StartAsync();
        }

        private void InventoryRealtimeClient_InventoryChanged(string warehouseId)
        {
            if (!string.IsNullOrWhiteSpace(warehouseId) &&
                !string.Equals(warehouseId, whId, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (IsDisposed || !IsHandleCreated)
            {
                return;
            }

            BeginInvoke(new Action(async () => await RefreshViewerAsync()));
        }

        private async Task RefreshViewerAsync()
        {
            await _dbLock.WaitAsync();
            try
            {
                await RefreshRackCountsAsync();
                if (!string.IsNullOrWhiteSpace(LblRack.Text) && LblRack.Text != "---")
                {
                    await Loadtransactionlogs(LblRack.Text);
                }
            }
            finally
            {
                _dbLock.Release();
            }
        }

        private async Task RefreshRackCountsAsync()
        {
            var previousCounts = new Dictionary<string, int>(RackCountCache);
            await LoadCache();

            foreach (string rackLabel in rackButtons.Keys)
            {
                previousCounts.TryGetValue(rackLabel, out int previousCount);
                RackCountCache.TryGetValue(rackLabel, out int currentCount);
                if (previousCount != currentCount)
                {
                    await UpdateRackUI(rackLabel);
                }
            }

            ApplyRackEmphasis();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                _ = InventoryRealtimeClient.StartAsync();
                await RefreshViewerAsync();
            }
            finally
            {
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
            if (_isInitializing)
            {
                return;
            }

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
