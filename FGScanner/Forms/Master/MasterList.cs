using FGScanner.Database;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
using FGScanner.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner.Forms.Master
{
    public partial class MasterList : UserControl
    {
        private int page = 1;
        private int pageSize = 50;
        private int totalPage = 0;
        private string _userid = string.Empty;
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
        private readonly ExcelService _excelService;

        public MasterList(string userid)
        {
            InitializeComponent();
            this._userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _excelService = new(_queries);
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
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
                           item.Partnumber,
                           item.Partname,
                           item.CustomerId,
                           item.Pps.ToString("N0")
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
                        DataGridViewButtonColumn dataGridViewButtonColumn = new()
                        {
                            Name = "ActionButton",
                            HeaderText = "Action",
                            Text = "Delete",
                            UseColumnTextForButtonValue = true
                        };
                        LogsTable.EditMode = DataGridViewEditMode.EditOnEnter;
                        LogsTable.Columns.Add(dataGridViewButtonColumn);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void MasterList_Load(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async void addbtn_Click(object sender, EventArgs e)
        {
            ProductMasterlist productMasterlist = new ProductMasterlist();
            productMasterlist.ShowDialog();
            await LoadProducts();
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

        private async void LogsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && LogsTable.Columns[e.ColumnIndex].Name == "ActionButton")
            {
                DataGridViewRow selectedRow = LogsTable.Rows[e.RowIndex];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                var result = MessageBox.Show("Are you sure you want to delete this item?","Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var isDeleted = await _queries.DeletePartnumber(id);
                    if (isDeleted.isSuccess)
                    {
                        MessageBox.Show(isDeleted.Message);
                        await LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show(isDeleted.Message);
                    }
                }
            }
        }
    }
}
