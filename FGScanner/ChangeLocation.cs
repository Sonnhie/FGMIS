using FGScanner.Model;
using FGScanner.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class ChangeLocation : Form
    {
        private readonly db_connection _Connection;

        public ChangeLocation()
        {
            InitializeComponent();
            _Connection = new db_connection();
            LoadAutosuggestCurr();
            LoadAutosuggestNew();
        }

        private readonly BindingList<ScannedModel> ShippingItems = new BindingList<ScannedModel>();
        private bool OnScanProcess(string QRCode)
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

            int partcount = ShippingItems.Count(x => x.PartNumber == itemModel.PartNumber);
            int StockCount = Insert.CheckStock(itemModel.PartNumber, TxtCurrRackno.Text);
            var lastIndex = ShippingItems.Count - 1;

            if (StockCount > partcount)
            {

                ShippingItems.Add(new ScannedModel
                {
                    TransactionDate = DateTime.Now,
                    Customer = Insert.GetCustomer(itemModel.PartNumber),
                    PartNumber = itemModel.PartNumber,
                    ProductionDate = itemModel.ProductionDate,
                    ProductionVersion = itemModel.ProductionVer,
                    Quantity = itemModel.Quantity,
                    TransactionType = "OUT",
                    Location = TxtCurrRackno.Text,
                    New_Location = CmbNewRack.Text,
                    Storage_location = LblStorageloc.Text,
                    Remarks = LblRemarks.Text,
                    TransactionId = "",
                });
            }
            else
            {
                MessageBox.Show($"Scanned quantity for part number {itemModel.PartNumber} exceeds available stock in the selected rack. Current scanned quantity: {partcount}, Available stock: {StockCount}", "Stock Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            UpdateShiplogs();

            var customer = Insert.GetCustomer(itemModel.PartNumber);

            LblPartNumber.Text = itemModel.PartNumber;
            LblProDate.Text = itemModel.ProductionDate.ToString("dd-MM-yy");
            LblProVer.Text = itemModel.ProductionVer;
            LblQuantity.Text = itemModel.Quantity.ToString();
            LblCustomer.Text = customer;


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

        private void LoadAutosuggestCurr()
        {
            string whid = CmbWHid.Text;
            var List = new TransactionRepo();
            var data = List.GetRackLocations(whid);

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            auto.AddRange(data.ToArray());

            TxtCurrRackno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TxtCurrRackno.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TxtCurrRackno.AutoCompleteCustomSource = auto;
            TxtCurrRackno.DataSource = auto;
        }

        private void LoadAutosuggestNew()
        {
            string whid = CmbWHid.Text;
            var List = new TransactionRepo();
            var data = List.GetRackLocations(whid);

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            auto.AddRange(data.ToArray());

            CmbNewRack.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CmbNewRack.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CmbNewRack.AutoCompleteCustomSource = auto;
            CmbNewRack.DataSource = auto;
        }

        private void TxtScanData_KeyDown(object sender, KeyEventArgs e)
        {
            var List = new TransactionRepo();
            string whid = CmbWHid.Text;
            var data = List.GetRackLocations(whid);

            if (!data.Contains(TxtCurrRackno.Text))
            {
                MessageBox.Show("Rack no. is invalid!", "Error location");
                TxtCurrRackno.Focus();
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
                }
                else
                {
                    TxtScanData.Clear();
                }
            }
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
                                WhId = CmbWHid.Text
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
                                WhId= CmbWHid.Text 
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

        private async void btnSave_Click(object sender, EventArgs e)
        {
            bool success =  await PullOutItem();
            if (success)
            {
                ShippingItems.Clear();
            }
        }

        private void CmbWHid_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAutosuggestCurr();
            LoadAutosuggestNew();
        }
    }
}
