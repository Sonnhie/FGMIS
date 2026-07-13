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
        private string _partnumber,_customer, _location, _productionVersion, _whId, _userid;
        private DateTime _productionDate;
        private int _box, _quantity, _pps;
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;

        private void StockEdit_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private readonly Util.db_connection _Connection;

        public StockEdit(int pps,string partnumber, string location, string productionVersion, DateTime productionDate, int box, int quantity, string customer, string whId, string userid)
        {
            InitializeComponent();
            _dbContext = new();
            _queries = new(_dbContext);
            _Connection = new Util.db_connection();
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

            InitializeLabels();
        }

        private void InitializeLabels()
        {
            partnumberlbl.Text = _partnumber;
            customerlbl.Text = _customer;
            locationlbl.Text = _location;
            prodverlbl.Text = _productionVersion;
            proddatelbl.Text = _productionDate.ToString("MM/dd/yyyy");
            stockslbl.Text = _quantity.ToString();
            boxlbl.Text = _box.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Are you sure you want to deduct this item?", "Manual deduction", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if(result == DialogResult.OK)
            {
                string remarks = reason_txtbox.Text;
                if (string.IsNullOrEmpty(reason_txtbox.Text))
                {
                    MessageBox.Show("Please put reason of deduction.");
                    return;
                }

                var inventoryData = await _queries.CheckIfExist(_partnumber, _location, _productionDate);

                if (inventoryData == null)
                {
                    MessageBox.Show("This data not exist in the inventory.", "Inventory Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (int.TryParse(BoxTxt.Text, out int boxCount))
                {
                    if (boxCount <= 0)
                    {
                        MessageBox.Show("Please enter a number greater than 0.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (inventoryData.TotalBox < boxCount)
                    {
                        MessageBox.Show($"You cannot deduct more than the available box ({inventoryData.TotalBox}).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var items = new Transaction
                    {
                        Partnumber = _partnumber,
                        ProdDate = _productionDate,
                        CustomerId = _customer,
                        ProdVer = _productionVersion,
                        Location = _location,
                        Remarks = remarks,
                        StorageLocation = "9151",
                    };

                    var (isSuccess, Message) = await _queries.ManualDeduction(items, boxCount);

                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
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
    }
}
