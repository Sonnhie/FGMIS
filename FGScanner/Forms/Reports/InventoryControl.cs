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

        public InventoryControl(string userid)
        {
            InitializeComponent();
            _userid = userid;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            TxtPartnumber.CharacterCasing = CharacterCasing.Upper;
            TxtCustomer.CharacterCasing = CharacterCasing.Upper;
            TxtLocation.CharacterCasing = CharacterCasing.Upper;
            TxtProductionVersion.CharacterCasing = CharacterCasing.Upper;
            WarehouseFilter.SelectedIndex = 0;
            PpsTypeFilter.SelectedIndex = 0;
            MovementFilter.SelectedIndex = 0;
            ProductionDateFrom.Checked = false;
            ProductionDateTo.Checked = false;
            _dbContext = new();
            _queries = new(_dbContext);
            _excelService = new(_queries);
        }

        public async Task FilterData()
        {
            try
            {
                if (ProductionDateFrom.Checked && ProductionDateTo.Checked &&
                    ProductionDateFrom.Value.Date > ProductionDateTo.Value.Date)
                {
                    MessageBox.Show(
                        "The production date From value cannot be later than the To value.",
                        "Invalid Date Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var data = await _queries.GetFilteredInventory(BuildInventoryFilter(), page, pageSize);

                totalPage = data.TotalPages == 0 ? 1 : data.TotalPages;
                BtnPrev.Enabled = page > 1;
                BtnNext.Enabled = page < totalPage;

                if (data != null)
                {
                    DataTable dt = new();

                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("PPS Type", typeof(string));
                    dt.Columns.Add("Production Date", typeof(DateTime));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Total Box", typeof(int));
                    dt.Columns.Add("Total Quantity", typeof(int));
                    dt.Columns.Add("PPS", typeof(decimal));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Storage location", typeof(string));
                    dt.Columns.Add("Warehouse Id", typeof(string));
                    dt.Columns.Add("Updated Inventory Date", typeof(DateTime));
                    dt.Columns.Add("Movement Classification", typeof(string));

                    LblPage.Text = $"Page {page} of {totalPage}";

                    foreach (var item in data.Items)
                    {
                        if (item.Quantity != 0)
                        {
                            int exactPps = _queries.GetProductPPS(item.Partnumber);
                            decimal displayedPps = InventoryPpsUtility.CalculateDisplayedPps(
                                item.Remarks,
                                item.Quantity,
                                item.TotalBox,
                                exactPps);
                            dt.Rows.Add
                            (
                                item.Partnumber,
                                item.Customer,
                                item.Remarks,
                                item.ProdDate.ToDateTime(TimeOnly.MinValue),
                                item.ProdVer,
                                item.TotalBox,
                                item.Quantity,
                                displayedPps,
                                item.Location,
                                item.StorageLocation,
                                item.WhId,
                                item.UpdatedDate ?? (object)DBNull.Value,
                                item.MovementClassification
                            );
                        }
                    }
                    LogsTable.Columns.Clear();
                    LogsTable.DataSource = dt;
                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["PPS Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Total Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["PPS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Storage location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Warehouse Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Updated Inventory Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Movement Classification"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    LogsTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["PPS Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["Production Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["Production Date"].DefaultCellStyle.Format = "MM/dd/yyyy";
                    LogsTable.Columns["Production Version"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["Total Box"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    LogsTable.Columns["Total Box"].DefaultCellStyle.Format = "N0";
                    LogsTable.Columns["Total Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    LogsTable.Columns["Total Quantity"].DefaultCellStyle.Format = "N0";
                    LogsTable.Columns["PPS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    LogsTable.Columns["PPS"].DefaultCellStyle.Format = "0.##";
                    LogsTable.Columns["Location"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["Storage location"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["Warehouse Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["Updated Inventory Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    LogsTable.Columns["Updated Inventory Date"].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm";
                    LogsTable.Columns["Movement Classification"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                    LogsTable.Columns["Part Number"].ReadOnly = true;
                    LogsTable.Columns["Customer"].ReadOnly = true;
                    LogsTable.Columns["PPS Type"].ReadOnly = true;
                    LogsTable.Columns["Production Date"].ReadOnly = true;
                    LogsTable.Columns["Production Version"].ReadOnly = true;
                    LogsTable.Columns["Total Box"].ReadOnly = true;
                    LogsTable.Columns["Total Quantity"].ReadOnly = true;
                    LogsTable.Columns["PPS"].ReadOnly = true;
                    LogsTable.Columns["Location"].ReadOnly = true;
                    LogsTable.Columns["Storage location"].ReadOnly = true;
                    LogsTable.Columns["Warehouse Id"].ReadOnly = true;
                    LogsTable.Columns["Updated Inventory Date"].ReadOnly = true;
                    LogsTable.Columns["Movement Classification"].ReadOnly = true;

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
                page = 1;
                BtnPrev.Enabled = false;
                BtnNext.Enabled = true;
                await FilterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void InventoryControl_Load(object sender, EventArgs e)
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
            if (page > 1)
            {
                page--;
                await FilterData();
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
                object productionDateValue = selectedRow.Cells["Production Date"].Value;
                DateOnly productionDate;

                if (productionDateValue is DateTime dateTime)
                {
                    productionDate = DateOnly.FromDateTime(dateTime);
                }
                else if (productionDateValue is DateOnly dateOnly)
                {
                    productionDate = dateOnly;
                }
                else if (!DateOnly.TryParse(
                    Convert.ToString(productionDateValue),
                    System.Globalization.CultureInfo.CurrentCulture,
                    System.Globalization.DateTimeStyles.None,
                    out productionDate))
                {
                    MessageBox.Show("The selected row does not contain a valid Production Date.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int box = Convert.ToInt32(selectedRow.Cells["Total Box"].Value);
                int quantity = Convert.ToInt32(selectedRow.Cells["Total Quantity"].Value);
                int PPS = Convert.ToInt32(selectedRow.Cells["PPS"].Value);
                string whId = selectedRow.Cells["Warehouse Id"].Value.ToString();

                var currentStock = await _queries.GetStockInfo(
                    partnumber,
                    productionDate,
                    productionVersion,
                    location,
                    whId);

                if (currentStock == null)
                {
                    MessageBox.Show(
                        "This stock record no longer exists in the current inventory. The table will now be refreshed.",
                        "Stock Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    await FilterData();
                    return;
                }

                StockEdit stockEdit = new(PPS, partnumber, location, productionVersion, productionDate, box, quantity, customer, whId, _userid);
                stockEdit.ShowDialog();
                await FilterData();
            }
        }

        private InventoryFilter BuildInventoryFilter()
        {
            return new InventoryFilter
            {
                Partnumber = TxtPartnumber.Text,
                Customer = TxtCustomer.Text,
                Location = TxtLocation.Text,
                ProductionVersion = TxtProductionVersion.Text,
                WarehouseId = WarehouseFilter.SelectedIndex > 0 ? WarehouseFilter.Text : null,
                PpsType = PpsTypeFilter.SelectedIndex > 0 ? PpsTypeFilter.Text : null,
                MovementClassification = MovementFilter.SelectedIndex > 0 ? MovementFilter.Text : null,
                ProductionDateFrom = ProductionDateFrom.Checked
                    ? DateOnly.FromDateTime(ProductionDateFrom.Value)
                    : null,
                ProductionDateTo = ProductionDateTo.Checked
                    ? DateOnly.FromDateTime(ProductionDateTo.Value)
                    : null
            };
        }

        private async void ClearFiltersButton_Click(object sender, EventArgs e)
        {
            TxtPartnumber.Clear();
            TxtCustomer.Clear();
            TxtLocation.Clear();
            TxtProductionVersion.Clear();
            WarehouseFilter.SelectedIndex = 0;
            PpsTypeFilter.SelectedIndex = 0;
            MovementFilter.SelectedIndex = 0;
            ProductionDateFrom.Checked = false;
            ProductionDateTo.Checked = false;
            page = 1;
            BtnPrev.Enabled = false;
            BtnNext.Enabled = true;
            await FilterData();
        }

        private void CheckTagsButton_Click(object sender, EventArgs e)
        {
            using var checkTagForm = new MonthEndCheckTagForm(_userid);
            checkTagForm.ShowDialog(this);
        }
    }
}
