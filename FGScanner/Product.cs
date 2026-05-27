using FGScanner.Util;
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
    public partial class Product : Form
    {
        private int page = 1;
        private int pageSize = 50;
        private int totalRecords = 0;
        private string _userid = string.Empty;

        public Product(string userid)
        {
            InitializeComponent();
            this._userid = userid;
            LoadProducts();
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            ProductMasterlist productMasterlist = new ProductMasterlist();
            productMasterlist.ShowDialog();
            LoadProducts();
        }

        public void LoadProducts()
        {
            try
            {
                string search = TxtPartnumber.Text.ToString();

                var Repo = new TransactionRepo();
                var Data = Repo.GetProduct(search, page, pageSize);
                var TotalRows = Repo.GetTotalProductRows(search);
                totalRecords = (int)Math.Ceiling((double)TotalRows / pageSize);

                if (Data != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Part Name", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("PPS", typeof(string));

                    LblPage.Text = $"Page {page} of {totalRecords}";

                    foreach (var item in Data)
                    {
                        dt.Rows.Add
                        (
                           item.Id,
                           item.PartNumber,
                           item.PartName,
                           item.CustomerId,
                           item.PPS.ToString("N0")
                        );
                    }

                    LogsTable.Columns.Clear();
                    LogsTable.DataSource = dt;
                   // LogsTable.ReadOnly = true;

                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Part Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["PPS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    LogsTable.Columns["Part Number"].ReadOnly =true;
                    LogsTable.Columns["Part Name"].ReadOnly = true;
                    LogsTable.Columns["Customer"].ReadOnly = true;
                    LogsTable.Columns["PPS"].ReadOnly = true;

                    LogsTable.Columns["ID"].Visible = false;

                    if (_userid == "N. Marquez")
                    {
                        DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn
                        {
                            Name = "ActionCheckbox",
                            HeaderText = "Action",
                        };
                        LogsTable.EditMode = DataGridViewEditMode.EditOnEnter;
                        LogsTable.Columns.Add(dataGridViewCheckBoxColumn);
                    }

                    LogsTable.Columns["ActionCheckbox"].ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            List<int> selectedIds = new List<int>();
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected items?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < LogsTable.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)LogsTable.Rows[i].Cells["ActionCheckbox"];
                    if (chk.Value != null && (bool)chk.Value == true)
                    {
                        int id = Convert.ToInt32(LogsTable.Rows[i].Cells["ID"].Value);
                        var repo = new TransactionRepo();
                        repo.DeleteProduct(id);
                        selectedIds.Add(id);
                    }
                }

                if (selectedIds.Count > 0)
                {
                    MessageBox.Show($"{selectedIds.Count} item(s) deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("No items selected for deletion.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (page < totalRecords)
            {
                page++;
                BtnPrev.Enabled = true;
                LoadProducts();
            }
            else
            {
                BtnNext.Enabled = false;
            }
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (page >= (totalRecords - page))
            {
                page--;
                BtnNext.Enabled = true;
                LoadProducts();
            }
            else
            {
                BtnPrev.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadProducts();
        }

        private void TxtPartnumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }
    }
}
