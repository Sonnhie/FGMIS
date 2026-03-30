using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FGScanner.Util;
using FGScanner.Model;

namespace FGScanner
{
    public partial class WHDataEntryIN : Form
    {
        private readonly string _TransactionType = string.Empty;

        public WHDataEntryIN(string TransactionType)
        {
            InitializeComponent();
            _TransactionType = TransactionType;
            LoadAutosuggest();
            LoadStorageLocations();
            Loadtransactionlogs();
        }
        private bool OnScanProcess(string QRCode)
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

            var customer = Insert.GetCustomer(itemModel.PartNumber);

            try
            {
                Insert.InsertTransaction(new InventoryTransactionModel
                {
                    PartNumber = itemModel.PartNumber,
                    ProductionDate = itemModel.ProductionDate,
                    ProductionVersion = itemModel.ProductionVer,
                    Customer = customer,
                    Quantity = itemModel.Quantity,
                    TransactionType = _TransactionType,
                    Location = TxtRackno.Text,
                    TransactionDate = DateTime.Now,
                    Remarks = CmbRemarks.Text,
                    Storage_location = CmbStorageLocation.Text,
                    WhId = CmbWHid.Text
                });

                Insert.RunMovementClassification();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

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
        private void LoadAutosuggest()
        {
            string WhId = CmbWHid.Text;
            var List = new TransactionRepo();
            var data = List.GetRackLocations(WhId);

            TxtRackno.CharacterCasing = CharacterCasing.Upper;

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            auto.AddRange(data.ToArray());

            TxtRackno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TxtRackno.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TxtRackno.AutoCompleteCustomSource = auto;
        }
        public void Loadtransactionlogs()
        {
            try
            {
                var Method = new TransactionRepo();
                var Datas = Method.GetTransactionHistory();

                if (Datas != null)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Entry Date", typeof(string));
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Remarks", typeof(string));
                    dt.Columns.Add("Storage location", typeof(string));

                    foreach (var Data in Datas)
                    {
                        dt.Rows.Add
                        (
                          Data.TransactionDate.ToString("MM/dd/yyyy"),
                          Data.PartNumber,
                          Data.Quantity,
                          Data.ProductionDate.ToString("MM/dd/yyyy"),
                          Data.ProductionVersion,
                          Data.Customer,
                          Data.Location,
                          Data.Remarks,
                          Data.Storage_location
                        );
                    }

                    LogsTable.Columns.Clear();
                    LogsTable.ReadOnly = true;
                    LogsTable.DataSource = dt;

                    LogsTable.Columns["Entry Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Remarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Storage location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        private void TxtScanData_KeyDown(object sender, KeyEventArgs e)
        {
            var List = new TransactionRepo();
            string WhId = CmbWHid.Text;
            var data = List.GetRackLocations(WhId);

            if (!data.Contains(TxtRackno.Text))
            {
                MessageBox.Show("Rack no. is invalid!", "Error location");
                TxtRackno.Focus();
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                var ParsedData = TxtScanData.Text;
                var IsProcessed = OnScanProcess(ParsedData);
                if (IsProcessed)
                {
                    TxtScanData.Clear();
                    e.SuppressKeyPress = true;
                    TxtScanData.Focus();
                    Loadtransactionlogs();
                }
                else
                {
                    TxtScanData.Clear();
                }
            }
        }

        private void CmbWHid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadAutosuggest();
        }
    }
}