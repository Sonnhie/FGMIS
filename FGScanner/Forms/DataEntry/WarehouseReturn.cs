using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
using FGScanner.Util;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic.ApplicationServices;
using Superpower.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FGScanner.Forms.DataEntry
{
    public partial class WarehouseReturn : UserControl
    {
        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
        private readonly ExcelService _excelService;
        private readonly PrintService _printService;
        private string _userid;
        private List<ScannedData> validScan = new List<ScannedData>();
        private List<Transaction> data = new List<Transaction>();
        private PrintDocumentDTO _documentToPrint;

        public WarehouseReturn(string userid)
        {
            InitializeComponent();
            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
            _excelService = new(_queries);
            _printService = new(_queries);
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            UploadItemButton.Enabled = false;
            GenerateReturnSlipBtn.Enabled = false;
        }

        private void LoadReturnTable()
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
                    ReturnTable.Columns.Clear();
                    ReturnTable.DataSource = dt;


                    int count = data.GroupBy(x => x.Partnumber).Count();
                    int sumQuantity = data.Sum(item => item.QuantityLabel);
                    int totalBoxCount = data.Sum(item => item.TotalBox);
                    string customerId = data.FirstOrDefault()?.Customerid ?? string.Empty;

                    PartcountLabel.Text = count.ToString();
                    QuantityLabel.Text = sumQuantity.ToString();
                    BoxLabel.Text = totalBoxCount.ToString();
                    ReturnIdLabel.Text = GenerateTransactionNumber();

                    ReturnTable.ReadOnly = true;

                    ReturnTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Box Count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    ReturnTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }
        private static string GenerateTransactionNumber()
        {
            var Method = new TransactionRepo();
            int seq = Method.GetLatestReturnId();
            return $"AS-{DateTime.Now:yyyyMMdd}-{seq:D2}";
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

                // 1. Parse Excel file into memory
                var fileResult = await _excelService.ProcessReturnUpload(fileInfo, progress, warehouse);

                if (!fileResult.isSuccess)
                {
                    MessageBox.Show(fileResult.Message, "File Read Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Load items into UI preview grid (No DB insert yet)
                validScan.AddRange(fileResult.ScanItem);

                MessageBox.Show($"Loaded {validScan.Count} item(s) from Excel file. Review items before uploading.", "File Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LoadReturnTable(); // Bind validScan to DataGridView
                toolStripProgressBar1.Value = 0;
                toolStripStatusLabel1.Text = "Ready";
                toolStripProgressBar1.Visible = false;
                toolStripStatusLabel1.Visible = false;
                UploadItemButton.Enabled = validScan.Count > 0;
            }
        }

        private async void UploadItemButton_Click(object sender, EventArgs e)
        {
            string warehouse = WarehouseComboBox.Text.Trim();
            string remarks = RemarkTextbox.Text.Trim();
            string transferTo = LocationComboBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(warehouse))
            {
                MessageBox.Show("Please select a warehouse.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(transferTo))
            {
                MessageBox.Show("Please select a Storage Location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(remarks))
            {
                MessageBox.Show("Please put remarks.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (validScan == null || validScan.Count == 0)
            {
                MessageBox.Show("No items to upload. Please select and load a file first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmation dialog
            var confirmResult = MessageBox.Show(
                $"Are you sure you want to save {validScan.Count} transaction(s)?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult != DialogResult.Yes) return;

            string transactionType = "OUT";
            string returnId = ReturnIdLabel.Text.Trim();

            // Lock UI controls to prevent double submissions
            UploadItemButton.Enabled = false;
            SelectFileButton.Enabled = false;

            try
            {
                // Check duplicate Return ID prior to submission
                var existingReturn = await _queries.CheckReturnIdDuplicate(returnId);
                if (existingReturn != null)
                {
                    MessageBox.Show("Return ID is already used. Please refresh or enter a new control number.", "Duplicate Return ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate stock limits & write to Database in Service Layer
                var (isSuccess, message, validItems, overflowWarnings) = await _service.InsertReturns(
                    validScan,
                    warehouse,
                    returnId,
                    transactionType,
                    _userid,
                    remarks,
                    transferTo
                );

                if (isSuccess)
                {
                    if (overflowWarnings.Count > 0)
                    {
                        string warningMessage = "Upload finished, but some items were skipped due to stock limits:\n\n" +
                                               $"Stock Overflows:\n- {string.Join("\n- ", overflowWarnings)}";
                        MessageBox.Show(warningMessage, "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Post-success cleanup
                    GenerateReturnSlipBtn.Enabled = true;
                    validScan.Clear(); // Clear memory buffer to prevent double uploads
                    LoadReturnTable(); // Refresh UI table
                }
                else
                {
                    MessageBox.Show(message, "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SelectFileButton.Enabled = true;
                UploadItemButton.Enabled = validScan.Count > 0;
            }
        }

        private async void GenerateReturnSlipBtn_Click(object sender, EventArgs e)
        {
            string docNumber = ReturnIdLabel.Text;
            if (string.IsNullOrWhiteSpace(docNumber))
            {
                MessageBox.Show("Invalid document number.");
                return;
            }

            var result = await _service.getItemsByReturns(docNumber);

            _documentToPrint = result
                .GroupBy(docgroup => docgroup.ControlNumber)
                .Select(docgroup => new PrintDocumentDTO
                {
                    DocNo = docgroup.Key,
                    EntryDate = docgroup.Max(x => x.EntryDate),
                    PreparedBy = docgroup.First().InCharge,
                    FromLocation = docgroup.First().ReturnTable.FromLocation,
                    ToLocation = docgroup.First().ReturnTable.ToLocation,
                    Items = [.. docgroup
                        .GroupBy(item => new { item.Partnumber, item.ProdDate })
                        .Select(itemgroup => new PrintItemDTO
                        {
                            PartNumber = itemgroup.Key.Partnumber,
                            ProductionDate = itemgroup.Key.ProdDate,
                            PartName = _queries.GetProductPartName(itemgroup.Key.Partnumber),
                            PPS =  _queries.GetProductPPS(itemgroup.Key.Partnumber),
                            Quantity = itemgroup.Sum(x => x.Quantity),
                            Box = itemgroup.Sum(x => x.Box) ?? 0,
                            remarks = itemgroup.FirstOrDefault().Remarks
                        })]
                }).FirstOrDefault();


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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            _printService.PrintTransferSlip(_documentToPrint, e);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            validScan.Clear();
            ReturnTable.DataSource = null;
            ReturnTable.Refresh();
            FileTextbox.Text = null;
            UploadItemButton.Enabled = false;
            GenerateReturnSlipBtn.Enabled = false;
        }
    }
}
