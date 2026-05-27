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
    public partial class StockEdit : Form
    {
        private string _partnumber,_customer, _location, _productionVersion, _whId, _userid;



        private DateTime _productionDate;


        private int _box, _quantity, _pps;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private readonly db_connection _Connection;

        public StockEdit(int pps,string partnumber, string location, string productionVersion, DateTime productionDate, int box, int quantity, string customer, string whId, string userid)
        {
            InitializeComponent();
            _Connection = new db_connection();
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

            partnumberlbl.Text = _partnumber;
            customerlbl.Text = _customer;
            locationlbl.Text = _location;
            prodverlbl.Text = _productionVersion;
            proddatelbl.Text = _productionDate.ToString("MM/dd/yyyy");
            stockslbl.Text = _quantity.ToString();
            boxlbl.Text = _box.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int boxCount))
            {
                if (boxCount <= 0)
                {
                    MessageBox.Show("Please enter a number greater than 0.", "Invalid Input");
                    return;
                }

                if (boxCount <= _box)
                {
                    InsertData(boxCount);
                }
                else
                {
                    // If they ask for more than what's available, show the error
                    MessageBox.Show($"You cannot deduct more than the available boxes ({_box}).", "Invalid Input");
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid number of boxes to deduct.", "Invalid Input");
            }
        }


        private void InsertData(int totalBoxes)
        {
            using (SqlConnection conn = _Connection.Getconnection())
            {
                conn.Open();

                // 1. Begin the transaction ONCE before the loop
                SqlTransaction tx = conn.BeginTransaction();

                try
                {
                    string sql = "INSERT INTO transaction_history (TransactionId, partnumber, prod_date, customer_id, quantity, prod_ver, entry_date, transaction_type, location, remarks, storage_location, WH_id, in_charge) " +
                                 "VALUES (@TransactionId, @partnumber, @prod_date, @customer_id, @quantity, @prod_ver, @entry_date, @transaction_type, @location, @remarks, @storage_location, @WH_id,  @_in_charge)";

                    // 2. Put the loop INSIDE the try block, running the exact number of times requested
                    for (int i = 1; i <= totalBoxes; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.Add("@TransactionId", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                            cmd.Parameters.Add("@partnumber", SqlDbType.NVarChar).Value = _partnumber;
                            cmd.Parameters.Add("@prod_date", SqlDbType.Date).Value = _productionDate;
                            cmd.Parameters.Add("@customer_id", SqlDbType.NVarChar).Value = _customer;
                            cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = _pps;
                            cmd.Parameters.Add("@prod_ver", SqlDbType.NVarChar).Value = _productionVersion;
                            cmd.Parameters.Add("@entry_date", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@transaction_type", SqlDbType.NVarChar).Value = "OUT";
                            cmd.Parameters.Add("@location", SqlDbType.NVarChar).Value = _location;

                            // Optional: You can use the 'i' variable to tag the remarks so you know exactly which box this was!
                            cmd.Parameters.Add("@remarks", SqlDbType.NVarChar).Value = $"Box {i + 1} of {totalBoxes} Deducted";

                            cmd.Parameters.Add("@storage_location", SqlDbType.NVarChar).Value = "9151";
                            cmd.Parameters.Add("@WH_id", SqlDbType.NVarChar).Value = _whId;
                            cmd.Parameters.Add("@_in_charge", SqlDbType.NVarChar).Value = _userid;

                            cmd.ExecuteNonQuery(); // Execute this specific row
                        }
                    }

                    // 3. Commit ONCE at the very end. If any box failed, this line will never run.
                    tx.Commit();

                    MessageBox.Show($"Successfully recorded {totalBoxes} OUT transactions!", "Success");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    // 4. If ANY loop iteration fails, it rolls back everything so your inventory doesn't get messed up
                    tx.Rollback();
                    MessageBox.Show("Error processing transaction. No changes were saved. \n\nDetails: " + ex.Message, "SQL Error");
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
