using FGScanner.Model;
using FGScanner.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class ChangeLocation : Form
    {
        private readonly db_connection _Connection;
        private string _userid = string.Empty;
        private HashSet<string> warnedPartNumbers = new HashSet<string>();
        public ChangeLocation(string userid)
        {
            InitializeComponent();
            _Connection = new db_connection();
            _userid = userid;
        }

        private readonly BindingList<ScannedModel> ShippingItems = new BindingList<ScannedModel>();
        private bool OnScanProcess(string QRCode, string from, string to)
        {
            var Process = new ScannerUtility();
            var Insert = new TransactionRepo();

            if (string.IsNullOrEmpty(QRCode))
            {
                MessageBox.Show("QR Code Error or empty!");
                return false;
            }

            if (!Process.ProcessQRData(QRCode, out var itemModel, out var error))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(itemModel.PartNumber))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            // 1. CALCULATE TOTAL QUANTITY: Sum the 'Quantity' property of items already in the list
            int currentScannedQty = ShippingItems
                                    .Where(x => x.PartNumber == itemModel.PartNumber)
                                    .Sum(x => x.Quantity);
            var newTotal = currentScannedQty + itemModel.Quantity;
            int stockAvailable = Insert.CheckStock(itemModel.PartNumber, itemModel.ProductionDate, from);

            if (newTotal > stockAvailable)
            {
                if (!warnedPartNumbers.Contains(itemModel.PartNumber))
                {
                    MessageBox.Show(
                    $"Stock Overflow for {itemModel.PartNumber}!\n" +
                    $"Available: {stockAvailable}\nScanned: {currentScannedQty}\nIncoming: {itemModel.Quantity}",
                    "Stock Warning");

                    warnedPartNumbers.Add(itemModel.PartNumber);
                }
                return false;
            }

            ShippingItems.Add(new ScannedModel
            {
                TransactionDate = DateTime.Now,
                Customer = Insert.GetCustomer(itemModel.PartNumber),
                PartNumber = itemModel.PartNumber,
                ProductionDate = itemModel.ProductionDate,
                ProductionVersion = itemModel.ProductionVer,
                Quantity = itemModel.Quantity, // Use the actual quantity from the QR
                TransactionType = "OUT",
                Location = from.ToUpper(),
                New_Location = to,
                Storage_location = cmbfrom.Text,
                Remarks = "",
                TransactionId = "",
                user = _userid
            });

            UpdateShiplogs();
            return true;
        }


        private async Task ProcessUpload(FileInfo fileInfo, IProgress<int> progress)
        {
            if (string.IsNullOrWhiteSpace(CmbWHid.Text))
            {
                MessageBox.Show("Select warehouse first.");
                return;
            }

            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                int startRow = 1;
                int rowCount = ws.Dimension.Rows;
                int totalRows = rowCount - startRow + 1;

                int current = 0;
                string qrcodedata = null;
                string fromlocation = null;
                string tolocation = null;

                for (int row = startRow; row <= rowCount; row++)
                {
                    current++;
                    qrcodedata = ws.Cells[row, 1].Value.ToString();
                    fromlocation = ws.Cells[row, 2].Value.ToString().ToUpper();
                    tolocation = ws.Cells[row, 3].Value.ToString().ToUpper();
                    if (qrcodedata != null)
                    {
                        OnScanProcess(qrcodedata, fromlocation, tolocation);
                    }

                    int percent = (int)((double)current / totalRows * 100);
                    percent = Math.Min(percent, 100);
                    if (current % 10 == 0)
                        progress?.Report(percent);
                    progress?.Report(percent);
                }
                await Task.Delay(100);
            }

            progress?.Report(100);
        }

        private void UpdateShiplogs()
        {
            BindingSource bs = new BindingSource
            {
                DataSource = ShippingItems
            };
            logstable.DataSource = bs;
        }


        private async Task<bool> PullOutItem()
        {
            if (ShippingItems.Count == 0)
            {
                MessageBox.Show("No items to pullout!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var Repo = new TransactionRepo();

            using (SqlConnection con = _Connection.Getconnection())
            {
                await con.OpenAsync();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in ShippingItems)
                        {
                            await Repo.InsertSingleTransaction(new InventoryTransactionModel
                            {
                                TransactionId = item.TransactionId,
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
                                WhId = CmbWHid.Text,
                                User = _userid
                            }, con, tx);

                            await Repo.InsertSingleTransaction(new InventoryTransactionModel
                            {
                                TransactionId = item.TransactionId,
                                PartNumber = item.PartNumber,
                                ProductionDate = item.ProductionDate,
                                Customer = item.Customer,
                                Quantity = item.Quantity,
                                TransactionType = "IN",
                                TransactionDate = item.TransactionDate,
                                ProductionVersion = item.ProductionVersion,
                                Location = item.New_Location,
                                Remarks = item.Remarks,
                                Storage_location = item.Storage_location,
                                WhId= CmbWHid.Text,
                                User = _userid
                            }, con, tx);
                        }
                        //Repo.RunMovementClassification();
                        tx.Commit();
                        MessageBox.Show("Transfer Completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show($"Error Transfering items: {ex.Message}", "Transfer items Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        private async void UploadScanDataBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CmbWHid.Text))
            {
                MessageBox.Show("Select warehouse first.");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                openFileDialog.Title = "Select an Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filepath = openFileDialog.FileName;

                    FileInfo fileInfo = new FileInfo(filepath);

                    var progress = new Progress<int>(value =>
                    {
                        progressBar.Value = value;
                        toolStripStatusLabel1.Text = $"Processing... {value}%";
                    });

                    try
                    {
                        progressBar.Visible = true;
                        toolStripStatusLabel1.Text = "Processing...";
                        await ProcessUpload(fileInfo, progress);
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = "Processing failed!";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        progressBar.Visible = false;
                        toolStripStatusLabel1.Text = "Processing completed!";
                    }
                    toolStripStatusLabel1.Visible = false;
                }
            }
        }

        private void logstable_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await PullOutItem();
        }

        private void logstable_SelectionChanged(object sender, EventArgs e)
        {
            if (logstable.SelectedRows.Count == 1)
            {
                DataGridViewRow r = logstable.SelectedRows[0];

                string partnumber = r.Cells["PartNumber"].Value?.ToString();
                string productiondate =Convert.ToDateTime(r.Cells["ProductionDate"].Value).ToString("dd-MM-yyyy");
                string prodver = r.Cells["ProductionVersion"].Value?.ToString();
                string quantity = r.Cells["Quantity"].Value?.ToString() ?? string.Empty;
                string customer = r.Cells["Customer"].Value?.ToString() ?? string.Empty;
                string from = r.Cells["Location"].Value?.ToString() ?? string.Empty;
                string to = r.Cells["New_Location"].Value?.ToString() ?? string.Empty;

                LblPartNumber.Text = partnumber;
                LblProDate.Text = productiondate;
                LblCustomer.Text = customer;
                LblProVer.Text = prodver;
                LblQuantity.Text = quantity;
                fromlbl.Text = from;
                toLbl.Text = to;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShippingItems.Clear();
            UpdateShiplogs();
        }
    }
}
