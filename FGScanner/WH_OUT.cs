using FGScanner.Model;
using FGScanner.Util;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class WH_OUT : Form
    {
        private readonly string _TransactionType = string.Empty;
        private readonly db_connection _Connection;
        private readonly BindingList<ScannedModel> ShippingItems = new BindingList<ScannedModel>();
        private readonly BindingList<DPIList> DPIItems = new BindingList<DPIList>();
        private Dictionary<string, DPIList> DPIDict = new Dictionary<string, DPIList>();
        private string _userid = string.Empty;
        private HashSet<string> warnedPartNumbers = new HashSet<string>();
        public WH_OUT(string TransactionType, string user)
        {
            InitializeComponent();
            _TransactionType = TransactionType;
            _Connection = new db_connection();
            progressBar.Visible = false;
            toolStripStatusLabel1.Text = "";
            Loadreference();
            btnSave.Enabled = false;
            _userid = user;
        }

        private void Loadreference()
        {
            DPIDict = DPIItems
                 .GroupBy(x => x.Partnumber)
                 .ToDictionary(
                    g => g.Key,
                    g => new DPIList
                    {
                        Partnumber = g.Key,
                        Quantity = g.Sum(x => x.Quantity),
                        PPS = g.First().PPS,
                        Box = g.Sum(x => x.Box)
                    }
                );
        }

        private bool OnScanProcess(string QRCode, string location)
        {
            var Process = new ScannerUtility();
            var Insert = new TransactionRepo();

           
            if (string.IsNullOrEmpty(QRCode)) return false;
            if (!Process.ProcessQRData(QRCode, out var itemModel, out var error))
            {
                MessageBox.Show(error, "Error");
                return false;
            }

            if (string.IsNullOrEmpty(itemModel.PartNumber))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            if (DPIDict == null || !DPIDict.TryGetValue(itemModel.PartNumber, out var reference))
            {
                
                string fallbackPart = "";
                if (itemModel.PartNumber.Contains("-"))
                {
                    int lastDash = itemModel.PartNumber.LastIndexOf("-");
                    fallbackPart = itemModel.PartNumber.Substring(0, lastDash);
                }

            
                if (string.IsNullOrEmpty(fallbackPart) || !DPIDict.TryGetValue(fallbackPart, out reference))
                {
                    MessageBox.Show($"Part Number {itemModel.PartNumber} not found in DPI list.", "DPI Error");
                    return false;
                }
            }


            // Calculations
            var currentScannedQty = ShippingItems.Where(x => x.PartNumber == itemModel.PartNumber).Sum(x => x.Quantity);
            var newTotal = currentScannedQty + itemModel.Quantity;
            int stockCount = Insert.CheckStock(itemModel.PartNumber, itemModel.ProductionDate, location);

            // --- VALIDATION 1: DPI Excel Reference ---
            if (newTotal > reference.Quantity)
            {
                MessageBox.Show($"Exceeds DPI! Allowed: {reference.Quantity}, Scanned: {newTotal}", "DPI Error");
                return false;
            }

            // --- VALIDATION 2: Physical Stock ---
            if (newTotal > stockCount)
            {
                if (!warnedPartNumbers.Contains(itemModel.PartNumber))
                {
                    MessageBox.Show(
                        $"Stock Overflow for {itemModel.PartNumber}!\n" +
                        $"Available: {stockCount}\nScanned: {currentScannedQty}\nIncoming: {itemModel.Quantity}",
                        "Stock Warning");
                    warnedPartNumbers.Add(itemModel.PartNumber);
                }
                return false;
            }

            // --- ALL CHECKS PASSED: INSERT DATA ---
            ShippingItems.Add(new ScannedModel
            {
                TransactionDate = DateTime.Now,
                Customer = Insert.GetCustomer(itemModel.PartNumber),
                PartNumber = itemModel.PartNumber,
                ProductionDate = itemModel.ProductionDate,
                ProductionVersion = itemModel.ProductionVer,
                Quantity = itemModel.Quantity,
                TransactionType = _TransactionType,
                Location = location,
                Storage_location = "9151",
                Remarks = "N/A",
                TransactionId = TxtcontrolNumber.Text,
            });

            // Post-Insert logic
            if (ShippingItems.Sum(x => x.Quantity) == DPIItems.Sum(x => x.Quantity))
            {
                btnSave.Enabled = true;
            }

            UpdateShiplogs();
            return true;
        }

        private void UpdateShiplogs()
        {
            BindingSource bs = new BindingSource
            {
                DataSource = ShippingItems
            };
            logstable.DataSource = bs;
        }

 

        private void WH_OUT_Load(object sender, EventArgs e)
        {
            timer1.Start();
            TxtcontrolNumber.Text = GenerateTransactionNumber();
            Loadreference();
        }

        private string GenerateTransactionNumber()
        {
            var Method = new TransactionRepo();
            int seq = Method.GetNextShipmentId();
            return $"SHIPID-{DateTime.Now:yyyyMMdd}-{seq:D4}";
        }

        private void WH_OUT_Shown(object sender, EventArgs e)
        {
          
        }

        private async Task<bool> UploadData()
        {

            if (ShippingItems.Count == 0)
            {
                MessageBox.Show("No items to upload!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                TransactionId = item.TransactionId,
                                WhId = CmbWHid.Text,
                                User = _userid
                            }, con, tx);

                            await Repo.InsertShipmentTransaction(new InventoryTransactionModel
                            {
                                PartNumber = item.PartNumber,
                                ProductionDate = item.ProductionDate,
                                Customer = item.Customer,
                                Quantity = item.Quantity,
                                TransactionDate = item.TransactionDate,
                                ProductionVersion = item.ProductionVersion,
                                TransactionId = item.TransactionId,
                                WhId = CmbWHid.Text
                            }, con, tx);
                        }
                        //Repo.RunMovementClassification();
                        tx.Commit();                        
                        MessageBox.Show("Data uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show($"Error uploading data: {ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        private async Task AutofillTemplate(List<OrdersSummary> orders, string Filepath, IProgress<int> Progress)
        {
            if (orders == null || orders.Count == 0)
            {
                return;
            }

            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");


            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Templates", "packinglist.xlsx");
            //string savePath = $@"ShipmentReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
            {
                var ws = package.Workbook.Worksheets["WarehouseCopy"];
                var wscopy = package.Workbook.Worksheets["InvoiceCopy"];


                var summarizedOrders =  orders
                    .GroupBy(o => o.Partnumber)
                    .Select(g => new OrdersSummary
                    {
                        Partnumber = g.Key,
                        Quantity = g.Sum(o => o.Quantity),
                        Box = g.Sum(o => o.Box),
                        Customer = g.First().Customer,
                    })
                    .ToList();
                                


                var startrow = 10;
                DateTime today = DateTime.Now;
                string date = today.ToString("MM/dd/yyyy");
                string time = today.ToString("HH:mm:ss");

                ws.Cells["C6"].Value = date;
                ws.Cells["C7"].Value = time;
                ws.Cells["J6"].Value = orders[0].Customer;
                ws.Cells["J7"].Value = orders[0].TransactionId;

                int current = 0;

                foreach (var items in summarizedOrders)
                {
                    int PPS = items.Quantity / items.Box;
                    ws.Cells[startrow, 2].Value = items.Partnumber;
                    ws.Cells[startrow, 5].Value = items.Box;
                    ws.Cells[startrow, 6].Value = PPS;
                    ws.Cells[startrow, 7].Value = items.Quantity;
                }

                foreach (var item in orders)
                {
                    current++;
                    int PPS = item.Quantity / item.Box;

                    ws.Cells[startrow, 2].Value = item.Partnumber;
                    ws.Cells[startrow, 4].Value = item.ProductionDate.ToString("MM/dd/yyyy");
                    ws.Cells[startrow, 5].Value = item.Box;
                    ws.Cells[startrow, 6].Value = PPS;
                    ws.Cells[startrow, 7].Value = item.Quantity;
                    startrow++;

                    int percent = (int)((double)current / (double)orders.Count * 100);
                    percent = Math.Min(percent, 100);
                    Progress?.Report(percent);
                    await Task.Delay(100);
                }

                package.SaveAs(new FileInfo(Filepath));
                Progress?.Report(100);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ShippingItems.Clear();
            logstable.Refresh();
        }

        private async void btnSave_Click_1(object sender, EventArgs e)
        {
            await UploadData();
        }

        private async void BtnGenerate_Click_1(object sender, EventArgs e)
        {
            string TransactionID = TxtcontrolNumber.Text;
            string Filename = $@"PackingList_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            var method = new TransactionRepo();
            List<OrdersSummary> order = method.GetPackinglist(TransactionID);

            if (order.Count == 0)
            {
                MessageBox.Show("No items to generate!", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Excel Files|*.xlsx";
                sf.Title = "Save Packing List";
                sf.DefaultExt = "xlsx";
                sf.FileName = Filename;

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string filepath = sf.FileName;

                    if (order.Count == 0)
                    {
                        MessageBox.Show("No Data Found.");
                        return;
                    }

                    progressBar.Value = 0;
                    progressBar.Visible = true;
                    toolStripStatusLabel1.Text = "Generating packing list...";

                    var progress = new Progress<int>(value =>
                    {
                        progressBar.Value = value;
                        toolStripStatusLabel1.Text = $"Generating packing list... {value}%";
                    });

                    try
                    {
                        await AutofillTemplate(order, filepath, progress);
                        progressBar.Value = 100;
                        toolStripStatusLabel1.Text = "Generating completed successfully!";
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
                        progressBar.Value = 0;
                        progressBar.Visible = false;
                        toolStripStatusLabel1.Text = "";
                        TxtcontrolNumber.Text = GenerateTransactionNumber();
                        ShippingItems.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Generation canceled.");
                }
            }
        }

        private void CmbWHid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private async void button1_Click(object sender, EventArgs e)
        {
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
                        Loadreference();
                        DPILogs();
                        progressBar.Visible = false;
                        toolStripStatusLabel1.Text = "Processing completed!";
                    }
                }
            }
        }

        private async Task ProcessUpload(FileInfo fileInfo, IProgress<int> progress)
        {
            DPIItems.Clear();
            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                int startRow = 3;
                int rowCount = ws.Dimension.Rows;
                int totalRows = rowCount - startRow + 1;

                int current = 0;

                for (int row = startRow; row <= rowCount; row++)
                {
                    current++;
                    DPIItems.Add(new DPIList
                    {
                        Partnumber = ws.Cells[row, 2].Value.ToString(),
                        Quantity = Convert.ToInt32(ws.Cells[row, 5].Value),
                        PPS = Convert.ToInt32(ws.Cells[row, 4].Value),
                        Box = Convert.ToInt32(ws.Cells[row, 6].Value)
                    });

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

        private void DPILogs()
        {
            if (DPIDict == null)
            {
                return;
            }

            BindingSource bs = new BindingSource
            {
                DataSource = DPIDict.Values.ToList()
            };
            DPILogsTable.DataSource = bs;
            DPILogsTable.Columns["Partnumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DPILogsTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DPILogsTable.Columns["PPS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DPILogsTable.Columns["Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void DPILogsTable_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = DPILogsTable.Rows[e.RowIndex];
            if (row.IsNewRow) return;
            var cellValue  = row.Cells["Partnumber"].Value.ToString();
            if (cellValue == null) return;
            var partnumber = cellValue.ToString();

            if (string.IsNullOrEmpty(partnumber))
            {
                return;
            }

            if (!DPIDict.TryGetValue(partnumber, out var reference))
            {
                return;
            }

            var ShippingSum = ShippingItems.Where(x => x.PartNumber == partnumber).Sum(x => x.Quantity);
            if(ShippingSum <= 0)
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else if (ShippingSum > reference.Quantity)
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;
                
            }
             else if (ShippingSum == reference.Quantity)
            {
                row.DefaultCellStyle.BackColor = Color.LimeGreen;
               
            }
             else
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DPIItems.Clear();
            DPIDict?.Clear();
            DPILogsTable.DataSource = null;
            DPILogsTable.Refresh();
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
                        await ProcessScanUpload(fileInfo, progress);
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = "Processing failed!";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Loadreference();
                        DPILogs();
                        progressBar.Visible = false;
                        toolStripStatusLabel1.Text = "Processing completed!";
                    }
                }
            }
        }

        private async Task ProcessScanUpload(FileInfo fileInfo, IProgress<int> progress)
        {

            try
            {
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
                    for (int row = startRow; row <= rowCount; row++)
                    {
                        current++;
                        qrcodedata = ws.Cells[row, 1].Value.ToString();
                        location = ws.Cells[row, 2].Value.ToString().ToUpper();
                        if (qrcodedata != null)
                        {
                            OnScanProcess(qrcodedata, location);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
