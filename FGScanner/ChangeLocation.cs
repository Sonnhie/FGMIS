using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Util;
using OfficeOpenXml;
using OfficeOpenXml.Interfaces.SensitivityLabels;
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
        private readonly Database.db_connection _Connection;
        private string _userid = string.Empty;
        private HashSet<string> warnedPartNumbers = new HashSet<string>();
        public ChangeLocation(string userid)
        {
            InitializeComponent();
            _Connection = new Database.db_connection();
            _userid = userid;
            LoadRackLocations();
        }

        public void LoadRackLocations()
        {
            var List = new TransactionRepo();
            var data = List.GetRackLocations(wh_id.Text);
            curr_loc.DataSource = data;
            curr_loc.SelectedIndex = -1;
        }

        public void LoadNewRackLocations()
        {
            var List = new TransactionRepo();
            var data = List.GetRackLocations(wh_id.Text);
            
            nex_loc.DataSource = data;
            nex_loc.SelectedIndex = -1;
        }

        public void LoadPartnumbers()
        {
            string curr = curr_loc.Text.Trim();
            string next = wh_id.Text.Trim();

            var List = new TransactionRepo();
            var data = List.GetRackPartnumbers(curr, next);
          //  MessageBox.Show($"Searching for Loc: '{curr}', WH: '{next}'. Found {data.Count} items."); // Remove this after testing!

            part_number.DataSource = data;
            part_number.SelectedIndex = -1;
        }

        //public void LoadPPS()
        //{
        //    string part_num = part_number.Text.Trim();

        //    var List = new TransactionRepo();
        //    var data = List.GetPPS(part_num);
        //    //  MessageBox.Show($"Searching for Loc: '{curr}', WH: '{next}'. Found {data.Count} items."); // Remove this after testing!

        //    qty_text.Text = data;
        //}

        public void Loadtransactionlogs()
        {
            string location = curr_loc.Text;
            string whId = wh_id.Text;

            try
            {
                var Method = new TransactionRepo();
                var Datas = Method.GetItemByLocation(location, whId);
                var totalBox = Datas
                               .Sum(d => d.Box);

                var totalQty = Datas
                               .Sum(d => d.Quantity);
                total_box_lbl.Text = $"Total Box: {totalBox:N0}";
                total_sum.Text = $"Total Qty: {totalQty:N0}";

                if (Datas != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));

                    foreach (var Data in Datas)
                    {
                        if (Data.Quantity != 0)
                        {
                            dt.Rows.Add
                            (
                              Data.PartNumber,
                              Data.Quantity,
                              Data.Box,
                              Data.ProductionDate.ToString("MM/dd/yyyy"),
                              Data.ProductionVersion,
                              Data.Customer
                            );
                        }
                    }


                    logstable.Columns.Clear();
                    logstable.ReadOnly = true;
                    logstable.DataSource = dt;

                    logstable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    logstable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    logstable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    logstable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    logstable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    logstable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string location = curr_loc.Text.Trim();
            string whId = wh_id.Text.Trim();
            string new_location = nex_loc.Text.Trim();
            string selectedPart = part_number.Text.Trim();
            DateTime Prod_date = prod_lot.Value.Date;

            // 1. Safe parsing of numeric inputs
            if (!int.TryParse(box_qty.Text, out int boxes) || boxes <= 0)
            {
                MessageBox.Show("Please enter a valid number of boxes.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(qty_text.Text, out int PPS) || PPS <= 0)
            {
                MessageBox.Show("Please enter a valid Pieces Per Box (PPS).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validate empty textboxes
            if (string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(whId) || string.IsNullOrWhiteSpace(new_location))
            {
                MessageBox.Show("Please fill in all location fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var Method = new TransactionRepo();
            var Datas = Method.GetItemByLocation(location, whId);

            if (Datas == null || !Datas.Any())
            {
                MessageBox.Show("No items found at this location.", "Empty Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            // Find the specific part the user selected
            var targetItem = Datas.FirstOrDefault(d => d.PartNumber == selectedPart);
            var targetdate = Datas.FirstOrDefault(d => d.ProductionDate == Prod_date);

            if (targetItem == null)
            {
                MessageBox.Show($"Part {selectedPart} is not currently in location {location}.", "Part Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(targetdate == null)
            {
                MessageBox.Show($"Part {selectedPart} with production date of {Prod_date} is not currently in location {location}.", "Production Date Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Transfer {boxes} boxes of {selectedPart} to {new_location}?", "Confirm Transfer", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                try
                {
                    // 3. Loop and execute the transfer
                    for (int i = 0; i < boxes; i++)
                    {
                        // OUT transaction
                        Method.InsertTransaction(new InventoryTransactionModel
                        {
                            PartNumber = targetItem.PartNumber,
                            ProductionDate = targetdate.ProductionDate,
                            ProductionVersion = targetItem.ProductionVersion,
                            Customer = targetItem.Customer,
                            Quantity = PPS,
                            TransactionType = "OUT",
                            Location = location.ToUpper(),
                            TransactionDate = DateTime.Now,
                            Remarks = $"Transfer Location from {location} to {new_location}",
                            Storage_location = "9151",
                            WhId = whId,
                            User = _userid
                        });

                        // IN transaction
                        Method.InsertTransaction(new InventoryTransactionModel
                        {
                            PartNumber = targetItem.PartNumber,
                            ProductionDate = targetdate.ProductionDate,
                            ProductionVersion = targetItem.ProductionVersion,
                            Customer = targetItem.Customer,
                            Quantity = PPS,
                            TransactionType = "IN",
                            Location = new_location.ToUpper(),
                            TransactionDate = DateTime.Now,
                            Remarks = $"Transfer Location from {location} to {new_location}",
                            Storage_location = "9151",
                            WhId = whId,
                            User = _userid
                        });
                    }

                    MessageBox.Show($"{boxes} boxes successfully transferred to {new_location}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void logstable_SelectionChanged(object sender, EventArgs e)
        {

        }
        private void curr_loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadtransactionlogs();
            LoadPartnumbers();
        }

        private void wh_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRackLocations();
            LoadNewRackLocations();
            LoadPartnumbers();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void part_number_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Method = new TransactionRepo();
            var Datas = Method.GetPPS(part_number.Text);

            qty_text.Text = Datas.ToString();
        }
    }
}
