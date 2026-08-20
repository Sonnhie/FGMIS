using FGScanner.Models;
using FGScanner.Database;
using FGScanner.Repositories;
using FGScanner.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class StockEdit : Form
    {
        private string _partnumber, _customer, _location, _productionVersion, _whId, _userid;
        private DateOnly _productionDate;
        private int _box, _quantity, _pps;
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;

        private async void StockEdit_Load(object sender, EventArgs e)
        {
            await LoadStockInformation();
        }


        public StockEdit(int pps, string partnumber, string location, string productionVersion, DateOnly productionDate, int box, int quantity, string customer, string whId, string userid)
        {
            InitializeComponent();
            _dbContext = new();
            _queries = new(_dbContext);
            _partnumber = partnumber;
            _location = location;
            _whId = whId;
            _productionVersion = productionVersion;
            _productionDate = productionDate;
            _quantity = quantity;
            _customer = customer;
            _userid = userid;
            _box = box;
            _pps = pps;

        }


        private async Task LoadStockInformation()
        {
            var stock = new ActualInventory();
            stock = await _queries.GetStockInfo(_partnumber, _productionDate, _productionVersion, _location, _whId);

            partnumberlbl.Text = stock.Partnumber.ToString();
            customerlbl.Text = stock.Customer.ToString();
            proddatelbl.Text = stock.ProdDate.ToString("MM-dd-yyyy");
            prodverlbl.Text = stock.ProdVer.ToString();
            stockslbl.Text = stock.Quantity.ToString();
            locationlbl.Text = stock.Location.ToString();
            boxlbl.Text = stock.TotalBox.ToString();
        }


        private async void button1_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Are you sure you want to deduct this item?", "Manual deduction", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                string remarks = reason_txtbox.Text;
                if (string.IsNullOrEmpty(reason_txtbox.Text))
                {
                    MessageBox.Show("Please put reason of deduction.");
                    return;
                }

                if (int.TryParse(BoxTxt.Text, out int boxCount) && int.TryParse(Qtytxt.Text, out int Quantity))
                {

                    var items = new TransactionHistory
                    {
                        Partnumber = _partnumber,
                        ProdDate = _productionDate,
                        CustomerId = _customer,
                        ProdVer = _productionVersion,
                        Location = _location,
                        Remarks = remarks,
                        StorageLocation = "9151",
                        Box = boxCount,
                        Quantity = Quantity,
                        InCharge = _userid
                    };

                    var (isSuccess, Message) = await _queries.ManualDeduction(items);

                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                        await LoadStockInformation();
                    }
                    else
                    {
                        MessageBox.Show(Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of boxes to deduct.", "Invalid Input");
                }
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void Qtytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void Qtytxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(Qtytxt.Text, out int CurrentQuantity))
                {
                    int quantity = int.Parse(stockslbl.Text);
                    if (CurrentQuantity < 0)
                    {
                        BoxTxt.Text = "0";
                        return;
                    }

                    if (CurrentQuantity > quantity)
                    {
                        MessageBox.Show($"You cannot deduct more than the available quantity ({quantity}).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    var productinfo = await _queries.GetProductInfo(_partnumber);
                    int pps = 1;
                    if (productinfo.Pps > 0)
                    {
                        pps = productinfo.Pps;
                    }

                    int box = (int)Math.Ceiling((double)CurrentQuantity / pps);
                    BoxTxt.Text = box.ToString();
                }
                else
                {
                    BoxTxt.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
