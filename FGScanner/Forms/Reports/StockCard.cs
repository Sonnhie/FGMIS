using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Repositories;
using FGScanner.Services;
using FGScanner.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class StockCard : Form
    {
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;
        private readonly ExcelService _excelService;

        public StockCard()
        {
            InitializeComponent();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            _dbContext = new();
            _queries = new(_dbContext);
            _excelService = new(_queries);
        }

        private async Task LoadData()
        {
            try
            {
                string partnumber = PartnumberTextbox.Text;
                string warehouseid = warehouseidcmb.Text;
                DateTime postingDate1 = PostingDate1.Value.Date;
                DateTime postingDate2 = PostingDate2.Value.Date;
                string prodver = ProdVerComboButton.Text;

                var data = await _queries.GetStockLedger(partnumber, postingDate1, postingDate2, prodver, warehouseid);
              

                if (data == null || data.Ledgers.Count == 0)
                {
                    MessageBox.Show("No Record Found.");
                    return;
                }

                partnumberlbl.Text = data.PartNumber;
                partnamelbl.Text = data.PartName;
                customerlbl.Text = data.Customer;

                if (data != null)
                {
                    DataTable dt = new();

                    dt.Columns.Add("Inventory Date", typeof(string));
                    dt.Columns.Add("IN", typeof(string));
                    dt.Columns.Add("OUT", typeof(string));
                    dt.Columns.Add("Running Stock", typeof(string));
                    dt.Columns.Add("Remarks", typeof(string));
                    dt.Columns.Add("PIC", typeof(string));



                    partnumberlbl.Text = partnumber;

                    //var Records = result.ledgers;

                    foreach (var item in data.Ledgers)
                    {
                        dt.Rows.Add
                        (
                           item.InventoryDate.ToString(),
                           item.In,
                           item.Out,
                           item.RunningStock,
                           item.Remarks ?? string.Empty,
                           item.Incharge.ToString() ?? string.Empty
                        );
                    }


                    StockCardtable.Columns.Clear();
                    StockCardtable.DataSource = dt;
                    StockCardtable.ReadOnly = true;

                    StockCardtable.Columns["Inventory Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["IN"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["OUT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["Running Stock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["Remarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    StockCardtable.Columns["PIC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void SearchBtn_Click(object sender, EventArgs e)
        {
           await LoadData();
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            string partnumber = PartnumberTextbox.Text;
            string warehouseid = warehouseidcmb.Text;
            DateTime postingDate1 = PostingDate1.Value.Date;
            DateTime postingDate2 = PostingDate2.Value.Date;
            string prodver = ProdVerComboButton.Text;
            string Filename = $@"StockCard_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            var data = await _queries.GetStockLedger(partnumber, postingDate1, postingDate2, prodver, warehouseid);


            if (data == null || data.Ledgers.Count == 0)
            {
                MessageBox.Show("No Record Found.");
                return;
            }

            using SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Excel Files|*.xlsx";
            sf.Title = "Save Transfer Slip";
            sf.DefaultExt = "xlsx";
            sf.FileName = Filename;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                string filepath = sf.FileName;

                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Generating Stock Card...";

                var progress = new Progress<int>(value =>
                {
                    toolStripProgressBar1.Value = value;
                    toolStripStatusLabel1.Text = $"Generating Stock Card... {value}%";
                });

                try
                {
                    var (isSuccess, Message) = await _excelService.AutofillStockCardTemplate(data, filepath, progress);
                    if (isSuccess)
                    {
                        MessageBox.Show(Message, "Generate Complete");
                    }
                    else
                    {
                        MessageBox.Show(Message);
                    }
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = "Export failed!";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Generation canceled.");
            }
        }
    }
}
