using FGScanner.Database;
using FGScanner.Repositories;
using FGScanner.Services;
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
        private int totalPage = 0;
        private string _userid = string.Empty;
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;
        private readonly ExcelService _excelService;

        public Product(string userid)
        {
            InitializeComponent();
            this._userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _excelService = new(_queries);
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
        }

        private async void addbtn_Click(object sender, EventArgs e)
        {
            ProductMasterlist productMasterlist = new ProductMasterlist();
            productMasterlist.ShowDialog();
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            try
            {
                string search = TxtPartnumber.Text;

                var Data = await _queries.GetFilteredProductList(search, page, pageSize);

                if (Data == null || Data.Items.Count == 0)
                {
                    MessageBox.Show("No Record Found.");
                    return;
                }

                totalPage = Data.TotalPages == 0 ? 1 : Data.TotalPages;

                if (Data != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Part Name", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("PPS", typeof(string));

                    LblPage.Text = $"Page {page} of {totalPage}";

                    foreach (var item in Data.Items)
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

                    LogsTable.Columns["Part Number"].ReadOnly = true;
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
                        LogsTable.Columns["ActionCheckbox"].ReadOnly = false;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            //List<int> selectedIds = new();
            //DialogResult result = MessageBox.Show("Are you sure you want to delete the selected items?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (result == DialogResult.Yes)
            //{
            //    for (int i = 0; i < LogsTable.Rows.Count; i++)
            //    {
            //        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)LogsTable.Rows[i].Cells["ActionCheckbox"];
            //        if (chk.Value != null && (bool)chk.Value == true)
            //        {
            //            int id = Convert.ToInt32(LogsTable.Rows[i].Cells["ID"].Value);
            //            var repo = new TransactionRepo();
            //            repo.DeleteProduct(id);
            //            selectedIds.Add(id);
            //        }
            //    }

            //    if (selectedIds.Count > 0)
            //    {
            //        MessageBox.Show($"{selectedIds.Count} item(s) deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        LoadProducts();
            //    }
            //    else
            //    {
            //        MessageBox.Show("No items selected for deletion.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            if (page < totalPage)
            {
                page++;
                BtnPrev.Enabled = true;
                await LoadProducts();
            }
            else
            {
                BtnNext.Enabled = false;
            }
        }

        private async void BtnPrev_Click(object sender, EventArgs e)
        {
            if (page > 1)
            {
                page--;
                await LoadProducts();
                BtnNext.Enabled = true;
            }
            else
            {
                BtnPrev.Enabled = false;
            }
        }

        private async void Product_Load(object sender, EventArgs e)
        {
            await LoadProducts();
        }
    }
}
