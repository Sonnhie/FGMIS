using FGScanner.Database;
using FGScanner.Models;
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


namespace FGScanner.Forms.Reports
{
    public partial class SlowMovingControl : UserControl
    {
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;
        private readonly ExcelService _excelService;
        private int page = 1;
        private int pageSize = 50;
        private int totalPage = 0;

        public SlowMovingControl()
        {
            InitializeComponent();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
            _dbContext = new();
            _queries = new(_dbContext);
            _excelService = new(_queries);
        }
        public async Task FilterData()
        {
            try
            {
                string partnumber = TxtPartnumber.Text;
                var data = await _queries.GetFilteredSlowMovingInventory(partnumber, page, pageSize);

                totalPage = data.TotalPages == 0 ? 1 : data.TotalPages;

                if (data != null)
                {
                    DataTable dt = new();

                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Box", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Last Moving Date", typeof(string));
                    dt.Columns.Add("Classification", typeof(string));
                    dt.Columns.Add("Storage Location", typeof(string));

                    LblPage.Text = $"Page {page} of {totalPage}";

                    foreach (var item in data.Items)
                    {
                        if (item.Quantity != 0)
                        {
                            dt.Rows.Add
                            (
                                item.Partnumber,
                                item.CustomerId,
                                item.ProdDate.ToString("MM/dd/yyyy"),
                                item.ProdVer,
                                item.TotalBox,
                                item.Quantity,
                                item.Location,
                                item.Last_Out_Date,
                                item.MovementClassification,
                                item.StorageLocation
                            );
                        }
                    }
                    LogsTable.ReadOnly = true;
                    LogsTable.Columns.Clear();
                    LogsTable.DataSource = dt;
                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Last Moving Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Classification"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Storage Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private async void SlowMovingControl_Load(object sender, EventArgs e)
        {
            await FilterData();
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            await FilterData();
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            if (page < totalPage)
            {
                page++;
                BtnPrev.Enabled = true;
                await FilterData();
            }
            else
            {
                BtnNext.Enabled = false;
            }
        }

        private async void BtnPrev_Click(object sender, EventArgs e)
        {
            if (page < totalPage)
            {
                page++;
                BtnPrev.Enabled = true;
                await FilterData();
            }
            else
            {
                BtnNext.Enabled = false;
            }
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            string Date = today.ToString("yyyyMMdd");
            string fileName = $"SlowMoving_{Date}.xlsx";

            using SaveFileDialog Save = new SaveFileDialog();
            Save.Filter = "Excel files (*.xlsx)|*.xlsx";
            Save.Title = "Save Exported Data";
            Save.DefaultExt = "xlsx";
            Save.FileName = fileName;

            if (Save.ShowDialog() == DialogResult.OK)
            {
                string filepath = Save.FileName;
                var result = await _queries.GetSlowMovingDataAsync();

                if (result.Count == 0)
                {
                    MessageBox.Show("No data to be generate.");
                    return;
                }

                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = $"Exporting...";

                var progress = new Progress<int>(value =>
                {
                    toolStripProgressBar1.Value = value;
                    toolStripStatusLabel1.Text = $"Exporting... {value}%";
                });


                string[] columnheaders = ["Part Number", "Customer", "Lot Date", "Prod Ver", "Location",
                                              "Quantity", "Total Box", "Storage Location", "Last Movement Date"];


                var reportInfo = new ReportGeneration<SlowMovingReport>
                {
                    Title = "Slow Moving Report",
                    Columns = columnheaders,
                    Items = result,
                };

                try
                {
                    var (isSuccess, Message) = await _excelService.GenerateReportExcel(reportInfo, filepath, progress);
                    if (isSuccess)
                    {
                        toolStripProgressBar1.Value = 100;
                        toolStripStatusLabel1.Text = Message;
                        MessageBox.Show(Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        toolStripProgressBar1.Value = 0;
                        MessageBox.Show(Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }
    }
}
