using FGScanner.Model;
using FGScanner.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class WHDataEntryIN : Form
    {
        private readonly string _TransactionType = string.Empty;
        private string _userid = string.Empty;
        private List<string> _partnumbers = new List<string>();
        private List<string> _ppsMismatch = new List<string>();
        private readonly BindingList<InventoryTransactionModel> _transactions = new BindingList<InventoryTransactionModel>();

        public WHDataEntryIN(string TransactionType , string userid)
        {
            InitializeComponent();
            _TransactionType = TransactionType;
            _userid = userid;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            LoadStorageLocations();
            Loadtransactionlogs();
        }
        private bool OnScanProcess(string QRCode, string location)
        {
            var Process = new ScannerUtility();
            var Insert = new TransactionRepo();
             
            if (string.IsNullOrEmpty(QRCode))
            {
                MessageBox.Show("QR Code Error or empty!");  
                return false;
            }
              
            if (!Process.ProcessQRData(QRCode,out var itemModel, out var error))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(itemModel.PartNumber))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            // 1. Check if the product even exists in the system
            if (!Insert.CheckProductExist(itemModel.PartNumber))
            {
                if (!_partnumbers.Contains(itemModel.PartNumber))
                {
                    _partnumbers.Add(itemModel.PartNumber);
                }
                return false;
            }
            // 2. ONLY check PPS if the product actually exists, to avoid redundant errors
            else if (!Insert.CheckProductPPS(itemModel.PartNumber, itemModel.Quantity))
            {
                if (!_ppsMismatch.Contains(itemModel.PartNumber))
                {
                    _ppsMismatch.Add(itemModel.PartNumber);
                }
                return false;
            }

            var customer = Insert.GetCustomer(itemModel.PartNumber);
            
            _transactions.Add(new InventoryTransactionModel
            {
                PartNumber = itemModel.PartNumber,
                ProductionDate = itemModel.ProductionDate,
                ProductionVersion = itemModel.ProductionVer,
                Customer = customer,
                Quantity = itemModel.Quantity,
                TransactionType = _TransactionType,
                Location = location.ToUpper(),
                TransactionDate = DateTime.Now,
                Remarks = CmbRemarks.Text,
                Storage_location = CmbStorageLocation.Text,
                WhId = CmbWHid.Text,
                User = _userid
            });



            LblPartNumber.Text = itemModel.PartNumber;
            LblCustomer.Text = customer;
            LblProDate.Text = itemModel.ProductionDate.ToString("MM/dd/yyyy");
            LblProVer.Text = itemModel.ProductionVer;
            LblQuantity.Text = itemModel.Quantity.ToString();

            return true;
        }
        public void LoadStorageLocations()
        {
            var List = new TransactionRepo();
            var data = List.GetStorageLocations();
            CmbStorageLocation.DataSource = data;
            CmbStorageLocation.SelectedIndex = +1;
            CmbRemarks.SelectedIndex = 0;
        }
  
        public void Loadtransactionlogs()
        {
            try
            {
                var Method = new TransactionRepo();
                //var Datas = Method.GetTransactionHistory();
                var Datas = _transactions
                            .GroupBy(t => (t.PartNumber, t.ProductionDate, t.ProductionVersion, t.Customer, t.Location))
                            .Select(x => new
                            {
                                PostingDate = DateTime.Now,
                                Partnumber = x.Key.PartNumber,
                                Productiondate = x.Key.ProductionDate,
                                Productionver = x.Key.ProductionVersion,
                                PPS = x.Sum(t => t.Quantity) / x.Count(),
                                TotalBox = x.Count(),
                                TotalQuantity = x.Sum(t => t.Quantity),
                                CustomerID = x.Key.Customer,
                                RackLocation = x.Key.Location,
                            })
                            .ToList();
                if (Datas != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Entry Date", typeof(string));
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Total Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Location", typeof(string));

                    foreach (var Data in Datas)
                    {
                        dt.Rows.Add
                        (
                          Data.PostingDate.ToString("MM/dd/yyyy"),
                          Data.Partnumber,
                          Data.TotalQuantity,
                          Data.TotalBox,
                          Data.Productiondate.ToString("MM/dd/yyyy"),
                          Data.Productionver,
                          Data.CustomerID,
                          Data.RackLocation
                        );
                    }
                   
                    LogsTable.Columns.Clear();
                  //  LogsTable.ReadOnly = true;
                    LogsTable.DataSource = dt;

                    LogsTable.Columns["Entry Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Total Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    
                    LogsTable.Columns["Entry Date"].ReadOnly = true;
                    LogsTable.Columns["Part Number"].ReadOnly = true;
                    LogsTable.Columns["Total Quantity"].ReadOnly = true;
                    LogsTable.Columns["Total Box"].ReadOnly = true;
                    LogsTable.Columns["Production Date"].ReadOnly = true;
                    LogsTable.Columns["Production Version"].ReadOnly = true;
                    LogsTable.Columns["Customer"].ReadOnly = true;
                    LogsTable.Columns["Location"].ReadOnly = true;

                    //if (_userid == "N. Marquez")
                    //{
                    //    DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn
                    //    {
                    //        Name = "ActionCheckbox",
                    //        HeaderText = "Action",
                    //    };
                    //    LogsTable.EditMode = DataGridViewEditMode.EditOnEnter;
                    //    LogsTable.Columns.Add(dataGridViewCheckBoxColumn);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void CmbWHid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
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
                        toolStripProgressBar1.Value = value;
                        toolStripStatusLabel1.Text = $"Processing... {value}%";
                    });

                    try
                    {
                        toolStripProgressBar1.Visible = true;
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
                        Loadtransactionlogs();
                        toolStripProgressBar1.Visible = false;
                        toolStripStatusLabel1.Text = "Processing completed!";
                    }
                }
            }
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
                string location = null;

                _partnumbers.Clear();
                _ppsMismatch.Clear();

                for (int row = startRow; row <= rowCount; row++)
                {
                    current++;
                    qrcodedata = ws.Cells[row, 1].Value.ToString().ToUpper();
                    location = ws.Cells[row, 2].Value.ToString().ToUpper();

                    if (qrcodedata != null)
                    {
                      OnScanProcess(qrcodedata, location);
                      //dataset.Add((qrcodedata, location));
                    }

                    int percent = (int)((double)current / totalRows * 100);

                    percent = Math.Min(percent, 100);
                    if (current % 10 == 0)
                        progress?.Report(percent);
                    progress?.Report(percent);
                }

                if (_partnumbers.Count > 0 || _ppsMismatch.Count > 0)
                {
                    // 1. Only show the missing parts popup if there actually are missing parts
                    if (_partnumbers.Count > 0)
                    {
                        string details = string.Join(", ", _partnumbers);
                        MessageBox.Show($"The following part numbers do not exist in the system and were skipped:\n{details}",
                                        "Missing Products", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _partnumbers.Clear(); // Clear immediately after showing
                    }

                    // 2. Only show the PPS mismatch popup if there actually are mismatches
                    if (_ppsMismatch.Count > 0)
                    {
                        string ppsDetails = string.Join(", ", _ppsMismatch);
                        MessageBox.Show($"The following part numbers have a PPS quantity mismatch and were skipped:\n{ppsDetails}.",
                                        "PPS Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _ppsMismatch.Clear(); // Clear immediately after showing
                    }
                }
                await Task.Delay(100);
            }
            progress?.Report(100);
        }

        private void LogsTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LogsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> selectedIds = new List<int>();
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected items?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                for(int i = 0; i < LogsTable.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)LogsTable.Rows[i].Cells["ActionCheckbox"];
                    if (chk.Value != null && (bool)chk.Value == true)
                    {
                        int id = Convert.ToInt32(LogsTable.Rows[i].Cells["ID"].Value);
                        var repo = new TransactionRepo();
                        repo.DeleteTransaction(id);
                        selectedIds.Add(id);
                    }
                }

                if (selectedIds.Count > 0)
                {
                    MessageBox.Show($"{selectedIds.Count} item(s) deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Loadtransactionlogs();
                }
                else
                {
                    MessageBox.Show("No items selected for deletion.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void save_data_btn_Click(object sender, EventArgs e)
        {
            var data = _transactions.ToList();
            var Insert = new TransactionRepo();
            var result = MessageBox.Show($"Are you sure you want to save {data.Count} transactions?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (var items in data)
                {
                    try
                    {
                        Insert.InsertTransaction(new InventoryTransactionModel
                        {
                            PartNumber = items.PartNumber,
                            ProductionDate = items.ProductionDate,
                            ProductionVersion = items.ProductionVersion,
                            Customer = items.Customer,
                            Quantity = items.Quantity,
                            TransactionType = _TransactionType,
                            Location = items.Location.ToUpper(),
                            TransactionDate = DateTime.Now,
                            Remarks = CmbRemarks.Text,
                            Storage_location = CmbStorageLocation.Text,
                            WhId = CmbWHid.Text,
                            User = _userid
                        });

                        Insert.RunMovementClassification();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                _transactions.Clear();
                Loadtransactionlogs();
                MessageBox.Show("Transaction has been saved");
            }
            else
            {
                MessageBox.Show("Save operation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            _transactions.Clear();
            Loadtransactionlogs();
        }

        private void LogsTable_SelectionChanged(object sender, EventArgs e)
        {
            decimal totalqty = 0;
            decimal totalbox = 0;

            foreach (DataGridViewCell cell in LogsTable.SelectedCells)
            {
                if (cell.OwningColumn.Name == "Total Quantity")
                {
                    if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal qty))
                    {
                        totalqty += qty;
                    }
                }

                if (cell.OwningColumn.Name == "Total Box")
                {
                    if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal box))
                    {
                        totalbox += box;
                    }
                }
            }
            total_sum.Text = $"Total Quantity: {totalqty:N0}";
            total_box_lbl.Text = $"Total Box: {totalbox:N0}";
        }
    }
}