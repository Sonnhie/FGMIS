using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
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
    public partial class BPPS : UserControl
    {
        private readonly TransactionService _service;
        private readonly Queries _queries;
        //private readonly Dbcontext _dbContext;
        private readonly InventoryDbContext _dbContext;
        private readonly ExcelService _excelService;
        private string _userid;
        private List<ScannedData> validScan = new List<ScannedData>();
        public BPPS(string userid)
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
        }

        private void BPPS_Load(object sender, EventArgs e)
        {
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private async void SelectFileButton_Click(object sender, EventArgs e)
        {
            string warehouse = WarehouseComboBox.Text;
            if (string.IsNullOrWhiteSpace(warehouse))
            {
                MessageBox.Show("Please select a warehouse.");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
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

                    try
                    {
                        validScan.Clear();
                        toolStripProgressBar1.Visible = true;
                        toolStripStatusLabel1.Visible = true;
                        toolStripStatusLabel1.Text = "Processing: 0%";
                        var result = await _excelService.ProcessBPPSUpload(fileinfo, progress, warehouse);
                        validScan.AddRange(result.ScanItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing file: {ex.Message}");
                    }
                    finally
                    {
                        LoadInventoryTable();
                        toolStripProgressBar1.Value = 0;
                        toolStripStatusLabel1.Text = "Ready";
                        toolStripProgressBar1.Visible = false;
                        toolStripStatusLabel1.Visible = false;
                        UploadItemButton.Enabled = true;

                    }
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
                    RackTable.Columns.Clear();
                    RackTable.DataSource = dt;


                    int count = data.Count;
                    int sumQuantity = data.Sum(item => item.QuantityLabel);
                    int totalBoxCount = data.Sum(item => item.TotalBox);
                    string customerId = data.FirstOrDefault()?.Customerid ?? string.Empty;

                    PartcountLabel.Text = count.ToString();
                    QuantityLabel.Text = sumQuantity.ToString();
                    BoxLabel.Text = totalBoxCount.ToString();
                    CustomerLabel.Text = customerId;

                    RackTable.ReadOnly = true;

                    RackTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Box Count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    RackTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
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
                if (result == DialogResult.Yes)
                {
                    List<string> rackList = [];
                    List<string> errorUpload = [];

                    rackList = await _service.GetRackLocationsAsync(warehouse);

                    foreach (var item in validScan)
                    {
                        var isExist = rackList.FirstOrDefault(x => x.Contains(item.Location));
                        if (isExist == null)
                        {
                            errorUpload.Add(item.PartNumber);
                            continue;
                        }
                    }

                    if (errorUpload.Count > 0)
                    {
                        MessageBox.Show($"Upload Error: {errorUpload.Count} item(s) failed to upload due to an invalid location.", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    var (isSuccess, Message) = await _service.InsertBPPS(validScan, warehouse, _userid);
                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                        validScan.Clear();
                        RackTable.DataSource = null;
                        LoadInventoryTable();
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

        private void ClearButton_Click(object sender, EventArgs e)
        {
            validScan.Clear();
            LoadInventoryTable();
            FileTextbox.Text = "";
            UploadItemButton.Enabled = false;
        }
    }
}
