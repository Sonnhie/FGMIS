using FGScanner.Database;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
using FGScanner.Util;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FGScanner.Forms.DataEntry
{
    public partial class Shipments : UserControl
    {

        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
        private readonly ExcelService _excelService;
        private string _userid;
        private List<ScannedData> validScan = new List<ScannedData>();
        private BindingList<DPIList> DPIItems = new BindingList<DPIList>();
        private Dictionary<string, DPIList> DPIDict = new(StringComparer.OrdinalIgnoreCase);

        private static string NormalizePartNumber(string partNumber) => partNumber?.Trim() ?? string.Empty;

        public Shipments(string userid)
        {
            InitializeComponent();
            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
            _excelService = new(_queries);
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            UploadItemButton.Enabled = false;
            GeneratePackingListBtn.Enabled = false;
        }

        private void Shipments_Load(object sender, EventArgs e)
        {
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void ReferenceData()
        {
            if (DPIItems == null)
            {
                return;
            }

            DPIDict = DPIItems
                .Where(x => !string.IsNullOrWhiteSpace(x.Partnumber))
                .GroupBy(x => NormalizePartNumber(x.Partnumber), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(x => x.Key, x => new DPIList
                {
                    Partnumber = x.Key,
                    Quantity = x.Sum(s => s.Quantity),
                    PPS = x.First().PPS,
                    Box = x.Sum(m => m.Box)
                }, StringComparer.OrdinalIgnoreCase);
        }

        private void LoadDPITable()
        {
            try
            {
                if (DPIDict == null)
                {
                    return;
                }

                var data = DPIItems
                    .GroupBy(x => new { x.Partnumber, x.PPS })
                    .Select(x => new
                    {
                        Partnumber = x.Key.Partnumber,
                        quanitty = x.Sum(x => x.Quantity),
                        box = x.Sum(x => x.Box),
                        pps = x.Key.PPS
                    })
                    .ToList();
                if (data.Count != 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Part Number", typeof(string)); ;
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("Box Count", typeof(int));
                    dt.Columns.Add("PPS", typeof(string));
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            item.Partnumber,
                            item.quanitty,
                            item.box,
                            item.pps
                        );
                    }
                    DPITable.Columns.Clear();
                    DPITable.DataSource = dt;


                    int count = data.Count;
                    int sumQuantity = data.Sum(item => item.quanitty);
                    int totalBoxCount = data.Sum(item => item.box);

                    DPIPartcountLabel.Text = count.ToString();
                    DPITotalQuantityLabel.Text = sumQuantity.ToString();
                    DPITotalBoxLabel.Text = totalBoxCount.ToString();

                    DPITable.ReadOnly = true;

                    DPITable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    DPITable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    DPITable.Columns["Box Count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    DPITable.Columns["PPS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    DPITable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }

        private void LoadInventoryTable()
        {
            try
            {
                var data = validScan
                           .GroupBy(item => new { item.PartNumber, item.ProductionDate, item.ProductionVersion, item.CustomerId, item.Location })
                           .Select(item => new
                           {
                               Partnumber = item.Key.PartNumber,
                               Productiondate = item.Key.ProductionDate,
                               Productionversion = item.Key.ProductionVersion,
                               QuantityLabel = item.Sum(x => x.Quantity),
                               location = item.Key.Location,
                               TotalBox = item.Count(),
                               Customerid = item.Key.CustomerId,
                           })
                           .ToList();
                if (data.Count != 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("Box Count", typeof(int));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            item.Partnumber,
                            item.Productiondate.ToString("MM/dd/yyyy"),
                            item.Productionversion,
                            item.QuantityLabel,
                            item.TotalBox,
                            item.location,
                            item.Customerid
                        );
                    }
                    ShipmenTable.Columns.Clear();
                    ShipmenTable.DataSource = dt;


                    int count = data.Count;
                    int sumQuantity = data.Sum(item => item.QuantityLabel);
                    int totalBoxCount = data.Sum(item => item.TotalBox);
                    string customerId = data.FirstOrDefault()?.Customerid ?? string.Empty;

                    PartcountLabel.Text = count.ToString();
                    QuantityLabel.Text = sumQuantity.ToString();
                    BoxLabel.Text = totalBoxCount.ToString();
                    CustomerLabel.Text = customerId;
                    ShipmentIdLabel.Text = GenerateTransactionNumber();

                    ShipmenTable.ReadOnly = true;

                    ShipmenTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ShipmenTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ShipmenTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ShipmenTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ShipmenTable.Columns["Box Count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ShipmenTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    ShipmenTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }

        private string GenerateTransactionNumber()
        {
            var Method = new TransactionRepo();
            int seq = Method.GetNextShipmentId();
            return $"SHIPID-{DateTime.Now:yyyyMMdd}-{seq:D4}";
        }

        private async void DPIFileButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DPIItems.Clear();
                string filePath = openFileDialog.FileName;
                DPITextBox.Text = filePath;
                FileInfo fileinfo = new(filePath);
                var progress = new Progress<int>(value =>
                {
                    toolStripProgressBar1.Value = value;
                    toolStripStatusLabel1.Text = $"Processing: {value}%";
                });

                try
                {

                    toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Processing: 0%";
                    var result = await _excelService.ProcessDPIUpload(fileinfo, progress);
                    foreach (var item in result.Items)
                    {
                        DPIItems.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing file: {ex.Message}");
                }
                finally
                {
                    LoadDPITable();
                    ReferenceData();
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel1.Text = "Ready";
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel1.Visible = false;
                }
            }
        }

        private async void SelectFileButton_Click(object sender, EventArgs e)
        {
            string warehouse = WarehouseComboBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(warehouse))
            {
                MessageBox.Show("Please select a warehouse.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            string filePath = openFileDialog.FileName;
            FileTextbox.Text = filePath;
            FileInfo fileInfo = new(filePath);

            var progress = new Progress<int>(value =>
            {
                toolStripProgressBar1.Value = value;
                toolStripStatusLabel1.Text = $"Processing: {value}%";
            });

            validScan.Clear();

            try
            {
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Processing: 0%";

                var result = await _excelService.ProcessFgShipmentUpload(fileInfo, progress, warehouse);

                if (result.isSuccess && result.ScanItem != null)
                {
                    HashSet<string> missingDpiItems = new();
                    Dictionary<string, string> excessItems = new();
                    Dictionary<string, string> stockOverflowItems = new();

                    // DPI limits apply per part; stock limits apply per exact inventory record.
                    Dictionary<string, int> dpiRunningTotals = new(StringComparer.OrdinalIgnoreCase);
                    Dictionary<string, int> stockRunningTotals = new();

                    var stockDICT = await _queries.GetStocks(result.ScanItem);

                    static string GetKey(string partNo, DateOnly prodDate, string prodVer, string wh, string loc)
                        => $"{partNo?.Trim().ToUpper()}|{prodDate:yyyy-MM-dd}|{prodVer?.Trim().ToUpper()}|{wh?.Trim().ToUpper()}|{loc?.Trim().ToUpper()}";

                    foreach (var item in result.ScanItem)
                    {
                        if (string.IsNullOrWhiteSpace(item.PartNumber)) continue;

                        string dpiKey = NormalizePartNumber(item.PartNumber);
                        string stockKey = GetKey(item.PartNumber, item.ProductionDate, item.ProductionVersion, warehouse, item.Location);

                        // Check DPI Master Plan existence
                        if (!DPIDict.TryGetValue(dpiKey, out var reference))
                        {
                            missingDpiItems.Add(item.PartNumber);
                            continue;
                        }

                        dpiRunningTotals.TryGetValue(dpiKey, out int currentDpiScan);
                        stockRunningTotals.TryGetValue(stockKey, out int currentStockScan);

                        int projectedDpiQty = currentDpiScan + item.Quantity;
                        int projectedStockQty = currentStockScan + item.Quantity;

                        if (projectedDpiQty > reference.Quantity)
                        {
                            excessItems[item.PartNumber] = $"- {item.PartNumber} (Attempted: {projectedDpiQty}, Limit: {reference.Quantity}, Production: {item.ProductionDate:yyyy-MM-dd}, Rack: {item.Location})";
                            continue;
                        }

                        stockDICT.TryGetValue(stockKey, out int stockCount);
                        if (projectedStockQty > stockCount)
                        {
                            stockOverflowItems[item.PartNumber] = $"- {item.PartNumber} (Attempted: {projectedStockQty}, Stock: {stockCount}, Production: {item.ProductionDate:yyyy-MM-dd}, Rack: {item.Location})";
                            continue;
                        }

                        // Accept item and update accumulated totals
                        validScan.Add(item);
                        dpiRunningTotals[dpiKey] = projectedDpiQty;
                        stockRunningTotals[stockKey] = projectedStockQty;
                    }

                    // A shipment must be completely valid. Do not allow a partial file to be uploaded.
                    if (missingDpiItems.Count > 0 || excessItems.Count > 0 || stockOverflowItems.Count > 0)
                    {
                        string warningMessage = "Shipment validation failed. No items were loaded for upload:\n\n";

                        if (missingDpiItems.Count > 0)
                        {
                            var missingToDisplay = missingDpiItems.Take(10);
                            warningMessage += $"Missing from DPI Plan ({missingDpiItems.Count} item(s)):\n- {string.Join("\n- ", missingToDisplay)}";
                            if (missingDpiItems.Count > 10)
                                warningMessage += $"\n...and {missingDpiItems.Count - 10} more.";
                            warningMessage += "\n";
                        }

                        if (excessItems.Count > 0)
                        {
                            var excessToDisplay = excessItems.Values.Take(10);
                            warningMessage += $"Exceeded DPI Limits:\n{string.Join("\n", excessToDisplay)}";
                            if (excessItems.Count > 10)
                                warningMessage += $"\n...and {excessItems.Count - 10} more.";
                            warningMessage += "\n";
                        }

                        if (stockOverflowItems.Count > 0)
                        {
                            var overflowToDisplay = stockOverflowItems.Values;
                            warningMessage += $"Stock Overflows:\n{string.Join("\n", overflowToDisplay)}";
                            //if (stockOverflowItems.Count > 10)
                            //    warningMessage += $"\n...and {stockOverflowItems.Count - 10} more.";
                        }

                        validScan.Clear();
                        MessageBox.Show(warningMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("File validated and loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(result.Message, "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                LoadInventoryTable();
                DPITable.Refresh();
                toolStripProgressBar1.Value = 0;
                toolStripStatusLabel1.Text = "Ready";
                toolStripProgressBar1.Visible = false;
                toolStripStatusLabel1.Visible = false;
                UploadItemButton.Enabled = validScan.Count > 0;
            }
        }

        private void DPITable_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = DPITable.Rows[e.RowIndex];
            if (row.IsNewRow) return;
            var partnumber = row.Cells["Part Number"].Value.ToString();
            if (string.IsNullOrEmpty(partnumber))
            {
                return;
            }

            if (!DPIDict.TryGetValue(partnumber, out var reference))
            {
                return;
            }

            var ShippingItems = validScan.Where(x => x.PartNumber == partnumber).Sum(x => x.Quantity);

            var ShippingItemsBox = validScan.Where(x => x.PartNumber == partnumber).Count();

            if (ShippingItems < reference.Quantity)
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else if (ShippingItems > reference.Quantity)
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;
            }
            else if (ShippingItems == reference.Quantity)
            {
                row.DefaultCellStyle.BackColor = Color.LimeGreen;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
        }

        private void DPIClearButton_Click(object sender, EventArgs e)
        {
            DPIItems.Clear();
            DPIDict.Clear();
            DPITable.DataSource = null;
            DPITable.Refresh();
            DPITextBox.Text = string.Empty;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            validScan.Clear();
            ShipmenTable.DataSource = null;
            ShipmenTable.Refresh();
            UploadItemButton.Enabled = false;
            FileTextbox.Text = string.Empty;
        }

        private async void UploadItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                string warehouse = WarehouseComboBox.Text;
                string marketcode = MarketCode.Text;
                if (string.IsNullOrWhiteSpace(warehouse))
                {
                    MessageBox.Show("Please select a warehouse.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(marketcode))
                {
                    MessageBox.Show("Please select a customer code.");
                    return;
                }

                if (validScan.Count == 0)
                {
                    MessageBox.Show("No file to upload");
                    return;
                }

                var result = MessageBox.Show($"Are you sure you want to save {validScan.Count} transactions?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                string transactionType = "OUT";
                string shipmentId = ShipmentIdLabel.Text;

                var isExixt = await _queries.CheckShipmentIdDuplicate(shipmentId);
                if (isExixt != null)
                {
                    MessageBox.Show("Shipment Id already used", "Duplicate control number");
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    var (isSuccess, Message) = await _service.InsertFGOutgoing(validScan, warehouse, shipmentId, transactionType, _userid, "FG", marketcode);
                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                        GeneratePackingListBtn.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void GeneratePackingListBtn_Click(object sender, EventArgs e)
        {
            string shipmentId = ShipmentIdLabel.Text;
            string Filename = $@"PackingList_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            using var sf = new SaveFileDialog();
            sf.Filter = "Excel Files|*.xlsx";
            sf.Title = "Save Packing List";
            sf.DefaultExt = "xlsx";
            sf.FileName = Filename;


            if (sf.ShowDialog() == DialogResult.OK)
            {
                string fileparth = sf.FileName;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Text = "Generating packing list...";

                var progress = new Progress<int>(value =>
                {
                    toolStripProgressBar1.Value = value;
                    toolStripStatusLabel1.Text = $"Generating packing list... {value}%";
                });

                try
                {
                    toolStripStatusLabel1.Text = "Processing: 0%";
                    var (isSuccess, Message) = await _excelService.AutofillPackingListTemplate(fileparth, shipmentId, progress);
                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                    }
                    else
                    {
                        MessageBox.Show(Message);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing file: {ex.Message}");
                }
                finally
                {
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel1.Text = "Ready";
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel1.Visible = false;

                    DPIItems.Clear();
                    DPIDict.Clear();
                    DPITable.DataSource = null;
                    DPITable.Refresh();
                    DPITextBox.Text = string.Empty;

                    validScan.Clear();
                    ShipmenTable.DataSource = null;
                    ShipmenTable.Refresh();
                    UploadItemButton.Enabled = false;
                    FileTextbox.Text = string.Empty;
                    GeneratePackingListBtn.Enabled = false;
                }
            }
        }


    }
}
