using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
using FGScanner.Util;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FGScanner.Forms.Reports
{
    public partial class InventoryControl : UserControl
    {
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
        private readonly ExcelService _excelService;
        private int page = 1;
        private int pageSize = 50;
        private int totalPage = 0;
        private string _userid = string.Empty;
        private string _partnumber = string.Empty;

        public InventoryControl(string userid)
        {
            InitializeComponent();
            _userid = userid;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
            _dbContext = new();
            _queries = new(_dbContext);
            _excelService = new(_queries);
        }

        public async Task FilterData(string partnumber = null)
        {
            try
            {
                var data = await _queries.GetFilteredInventory(partnumber, page, pageSize);

                totalPage = data.TotalPages == 0 ? 1 : data.TotalPages;

                if (data != null)
                {
                    DataTable dt = new();

                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Total Quantity", typeof(string));
                    dt.Columns.Add("PPS", typeof(string));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Storage location", typeof(string));
                    dt.Columns.Add("Warehouse Id", typeof(string));
                    dt.Columns.Add("Updated Inventory Date", typeof(string));
                    dt.Columns.Add("Movement Clsasification", typeof(string));

                    LblPage.Text = $"Page {page} of {totalPage}";

                    foreach (var item in data.Items)
                    {
                        if (item.Quantity != 0)
                        {
                            int pps = _queries.GetProductPPS(item.Partnumber);
                            dt.Rows.Add
                            (
                                item.Partnumber,
                                item.Customer,
                                item.ProdDate.ToString("MM/dd/yyyy"),
                                item.ProdVer,
                                item.TotalBox.ToString(),
                                item.Quantity.ToString(),
                                pps,
                                item.Location,
                                item.StorageLocation,
                                item.WhId,
                                item.UpdatedDate,
                                item.MovementClassification
                            );
                        }
                    }
                    LogsTable.Columns.Clear();
                    LogsTable.DataSource = dt;
                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Total Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["PPS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Storage location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Warehouse Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Updated Inventory Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Movement Clsasification"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                    LogsTable.Columns["Part Number"].ReadOnly = true;
                    LogsTable.Columns["Customer"].ReadOnly = true;
                    LogsTable.Columns["Production Date"].ReadOnly = true;
                    LogsTable.Columns["Production Version"].ReadOnly = true;
                    LogsTable.Columns["Total Box"].ReadOnly = true;
                    LogsTable.Columns["Total Quantity"].ReadOnly = true;
                    LogsTable.Columns["PPS"].ReadOnly = true;
                    LogsTable.Columns["Location"].ReadOnly = true;
                    LogsTable.Columns["Storage location"].ReadOnly = true;
                    LogsTable.Columns["Warehouse Id"].ReadOnly = true;
                    LogsTable.Columns["Updated Inventory Date"].ReadOnly = true;
                    LogsTable.Columns["Movement Clsasification"].ReadOnly = true;

                    if (_userid.Contains("N. Marquez"))
                    {
                        DataGridViewButtonColumn dataGridViewButtonColumn = new()
                        {
                            Name = "ActionButton",
                            HeaderText = "Action",
                            Text = "Edit Stock",
                            UseColumnTextForButtonValue = true
                        };

                        LogsTable.EditMode = DataGridViewEditMode.EditOnEnter;
                        LogsTable.Columns.Add(dataGridViewButtonColumn);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {

                await FilterData(TxtPartnumber.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void InventoryControl_Load(object sender, EventArgs e)
        {
            await FilterData(TxtPartnumber.Text);
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            if (page < totalPage)
            {
                page++;
                BtnPrev.Enabled = true;
                await FilterData(TxtPartnumber.Text);
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
                await FilterData(TxtPartnumber.Text);
                BtnNext.Enabled = true;
            }
            else
            {
                BtnPrev.Enabled = false;
            }
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
                    var result = await _queries.GetInventoryDataAsync();

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
                                              "Quantity", "Total Box", "Storage Location", "Updated Inventory Date",
                                              "Movement Classification"];


                    var reportInfo = new ReportGeneration<InventoryReport>
                    {
                        Title = "Inventory Report",
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

        private void LogsTable_SelectionChanged(object sender, EventArgs e)
        {
            decimal totalqty = 0;
            decimal totalbox = 0;

            foreach (DataGridViewCell cell in LogsTable.SelectedCells)
            {
                if (cell.OwningColumn.Name == "Total Quantity")
                {
                    if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal qty))
                    {
                        totalqty += qty;
                    }
                }

                if (cell.OwningColumn.Name == "Total Box")
                {
                    if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal box))
                    {
                        totalbox += box;
                    }
                }
            }
            total_sum.Text = $"Total Quantity: {totalqty:N0}";
            total_box_lbl.Text = $"Total Box: {totalbox:N0}";
        }

        private async void LogsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && LogsTable.Columns[e.ColumnIndex].Name == "ActionButton")
            {
                DataGridViewRow selectedRow = LogsTable.Rows[e.RowIndex];
                string partnumber = selectedRow.Cells["Part Number"].Value.ToString();
                string location = selectedRow.Cells["Location"].Value.ToString();
                string customer = selectedRow.Cells["Customer"].Value.ToString();
                string productionVersion = selectedRow.Cells["Production Version"].Value.ToString();
                string dateString = Convert.ToString(selectedRow.Cells["Production Date"].Value);

                // If the date is invalid or blank, show an error and exit this block of code
                if (!DateOnly.TryParse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateOnly ProductionDate))
                {
                    MessageBox.Show("The selected row does not contain a valid Production Date.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int box = Convert.ToInt32(selectedRow.Cells["Total Box"].Value);
                int quantity = Convert.ToInt32(selectedRow.Cells["Total Quantity"].Value);
                int PPS = Convert.ToInt32(selectedRow.Cells["PPS"].Value);
                string whId = selectedRow.Cells["Warehouse Id"].Value.ToString();

                StockEdit stockEdit = new(PPS, partnumber, location, productionVersion, ProductionDate, box, quantity, customer, whId, _userid);
                stockEdit.ShowDialog();
                await FilterData(TxtPartnumber.Text);
            }
        }
    }
}
