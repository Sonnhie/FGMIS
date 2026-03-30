using FGScanner.Model;
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
    public partial class InventoryForm : Form
    {
        private int page = 1;
        private int pageSize = 50;
        private int totalPage = 0;

        public InventoryForm()
        {
            InitializeComponent();
            InitializeFilter();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
        }


        private void InitializeFilter()
        {
            string partnumber = TxtPartnumber.Text;

            var Repo = new TransactionRepo();
            int totalRows = Repo.GetTotalRows(partnumber);

            totalPage = (int)Math.Ceiling((double)totalRows / pageSize);

            FilterData(partnumber);
        }

        private void FilterData(string partnumber)
        {
            try
            {
                var Method = new TransactionRepo();

                var data = Method.GetFilteredData(partnumber, page, pageSize);

                if (data != null)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Total Quantity", typeof(string));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Storage location", typeof(string));
                    dt.Columns.Add("Updated Inventory Date", typeof(string));
                    dt.Columns.Add("Movement Clsasification", typeof(string));

                    LblPage.Text = $"Page {page} of {totalPage}";

                    foreach (var item in data)
                    {

                        if (item.Quantity != 0)
                        {
                            dt.Rows.Add
                            (
                                item.PartNumber,
                                item.Customer,
                                item.ProductionDate.ToString("MM/dd/yyyy"),
                                item.ProductionVersion,
                                item.Box,
                                item.Quantity,
                                item.Location,
                                item.Storage_location,
                                item.Updated_date.ToString("MM/dd/yyyy"),
                                item.Status
                            );
                        }
                    }
                    LogsTable.Columns.Clear();
                    LogsTable.ReadOnly = true;
                    LogsTable.DataSource = dt;
                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Total Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Storage location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Updated Inventory Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Movement Clsasification"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void TxtPartnumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                timer1.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            InitializeFilter();
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            string Date = today.ToString("yyyyMMdd");
            string fileName = $"Inventory_{Date}.xlsx";

            using (SaveFileDialog Save = new SaveFileDialog())
            {
                Save.Filter = "Excel files (*.xlsx)|*.xlsx";
                Save.Title = "Save Exported Data";
                Save.DefaultExt = "xlsx";
                Save.FileName = fileName;

                if (Save.ShowDialog() == DialogResult.OK)
                {
                    string filepath = Save.FileName;
                    var Repo = new TransactionRepo();
                    DataTable Data = Repo.GetInventoryData();

                    //var Datas = GetData();


                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = $"Exporting...";

                    var progress = new Progress<int>(value =>
                    {
                        toolStripProgressBar1.Value = value;
                        toolStripStatusLabel1.Text = $"Exporting... {value}%";
                    });

                    try
                    {
                        await ExportService.ExportCSV(Data, filepath, progress, "Inventory");
                        toolStripProgressBar1.Value = 100;
                        toolStripStatusLabel1.Text = "Export completed successfully";
                        MessageBox.Show("Export completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (page < totalPage)
            {
                page++;
                BtnPrev.Enabled = true;
                InitializeFilter();
            }
            else
            {
                BtnNext.Enabled = false;
            }
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (page >= (totalPage - page))
            {
                page--;
                BtnNext.Enabled = true;
                InitializeFilter();
            }
            else
            {
                BtnPrev.Enabled=false;
            }
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
