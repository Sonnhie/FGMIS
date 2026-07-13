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
    public partial class Outgoing : Form
    {

        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;
        private readonly ExcelService _excelService;
        private string _userid;
        private List<ScannedData> validScan = new List<ScannedData>();
        private BindingList<DPIList> DPIItems = new BindingList<DPIList>();
        private Dictionary<string, DPIList> DPIDict = new Dictionary<string, DPIList>();

        public Outgoing(string userid)
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

        private void ReferenceData()
        {
            if (DPIItems == null)
            {
                return;
            }

            DPIDict = DPIItems
                .GroupBy(x => x.Partnumber)
                .ToDictionary(x => x.Key, x => new DPIList
                {
                    Partnumber = x.Key,
                    Quantity = x.Sum(s => s.Quantity),
                    PPS = x.First().PPS,
                    Box = x.Sum(m => m.Box)
                });
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
        private async void SelectFileButton_Click(object sender, EventArgs e)
        {
            string warehouse = WarehouseComboBox.Text;
            if (string.IsNullOrWhiteSpace(warehouse))
            {
                MessageBox.Show("Please select a warehouse.");
                return;
            }
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filePath = openFileDialog.FileName;
                FileTextbox.Text = filePath;
                FileInfo fileinfo = new(filePath);
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
                    var result = await _excelService.ProcessFgShipmentUpload(fileinfo, progress, warehouse);
                    if (result.isSuccess)
                    {

                        if (result.ScanItem != null)
                        {
                            HashSet<string> excessItems = new HashSet<string>();
                            HashSet<string> missingDpiItems = new HashSet<string>();

                            foreach (var item in result.ScanItem)
                            {
                                if (!DPIDict.TryGetValue(item.PartNumber, out var reference))
                                {
                                    missingDpiItems.Add(item.PartNumber);
                                    continue;
                                }

                                var currentScan = validScan.Where(x => x.PartNumber == item.PartNumber).Sum(x => x.Quantity);
                                var projectedQty = currentScan + item.Quantity;

                                if (projectedQty > reference.Quantity)
                                {
                                    excessItems.Add($"- {item.PartNumber} (Attempted: {projectedQty}, Limit: {reference.Quantity})");
                                }
                                else
                                {
                                    validScan.Add(item);
                                }
                            }

                            if (missingDpiItems.Count > 0 || excessItems.Count > 0)
                            {
                                string warningMessage = "Upload finished, but some items were skipped:\n\n";

                                if (missingDpiItems.Count > 0)
                                    warningMessage += $"Missing from DPI Plan: {missingDpiItems.Count} items.\n";

                                if (excessItems.Count > 0)
                                    warningMessage += $"Exceeded DPI Limits:\n{string.Join("\n", excessItems)}";

                                MessageBox.Show(warningMessage, "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        MessageBox.Show(result.Message, "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing file: {ex.Message}");
                }
                finally
                {
                    LoadInventoryTable();
                    DPITable.Refresh();
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel1.Text = "Ready";
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel1.Visible = false;
                    UploadItemButton.Enabled = true;

                }
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

                    ShipmenTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmenTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmenTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmenTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmenTable.Columns["Box Count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmenTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private string GenerateTransactionNumber()
        {
            var Method = new TransactionRepo();
            int seq = Method.GetNextShipmentId();
            return $"SHIPID-{DateTime.Now:yyyyMMdd}-{seq:D4}";
        }

        private async void UploadItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                string warehouse = WarehouseComboBox.Text;
                if (string.IsNullOrWhiteSpace(warehouse))
                {
                    MessageBox.Show("Please select a warehouse.");
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
                    var (isSuccess, Message) = await _service.InsertFGOutgoing(validScan, warehouse, shipmentId, transactionType, _userid, "FG");
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


            if(sf.ShowDialog() == DialogResult.OK)
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
                    var (isSuccess, Message) =  await _excelService.AutofillPackingListTemplate(fileparth, shipmentId, progress);
                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                    }
                    else
                    {
                        MessageBox.Show(Message);
                    }

                }
                catch(Exception ex)
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
