using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using Superpower.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FGScanner.Forms.Reports
{
    public partial class ShipmentReport : UserControl
    {
        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
        private readonly ExcelService _excelService;
        private string _userid;
        private string controlnumber;

        public ShipmentReport(string userid)
        {
            InitializeComponent();
            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
            _excelService = new(_queries);
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void LoadShipmentTable(List<TransactionHistory> data)
        {
            try
            {

                if (data.Count != 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Shipment Control Number", typeof(string));
                    dt.Columns.Add("Shipment Date", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Remarks", typeof(string));
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            item.ControlNumber.ToString(),
                            item.EntryDate.ToString("MM/dd/yyyy"),
                            item.Quantity.ToString(),
                            item.Box.ToString(),
                            item.Remarks
                        );
                    }
                    ShipmentTable.Columns.Clear();
                    ShipmentTable.DataSource = dt;

                    ShipmentTable.ReadOnly = true;

                    ShipmentTable.Columns["Shipment Control Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ShipmentTable.Columns["Shipment Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmentTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmentTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmentTable.Columns["Remarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    DataGridViewButtonColumn btnCol = new()
                    {
                        Name = "Actionbuttons",
                        HeaderText = "",
                        Text = "View Items",
                        UseColumnTextForButtonValue = true
                    };

                    ShipmentTable.Columns.Add(btnCol);
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    ShipmentTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }
        private async Task LoadShipmentItemTable(string controlnumber)
        {
            try
            {
                var result = await _service.LoadShipmentItems(controlnumber);

                var totalQuantity = result.Sum(x => x.Quantity);
                var totalBox = result.Sum(x => x.Box);

                if (result.Count != 0)
                {
                    DataTable dt = new();
                    dt.Columns.Add("Part number", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    foreach (var item in result)
                    {
                        dt.Rows.Add(
                            item.Partnumber.ToString(),
                            item.ProdDate.ToString("MM/dd/yyyy"),
                            item.ProdVer.ToString(),
                            item.Quantity.ToString(),
                            item.Box.ToString()
                        );
                    }
                    ShipmentItemTable.Columns.Clear();
                    ShipmentItemTable.DataSource = dt;

                    ShipmentItemTable.ReadOnly = true;

                    ShipmentItemTable.Columns["Part number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ShipmentItemTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmentItemTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmentItemTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ShipmentItemTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    TotalQuantityLabel.Text = totalQuantity.ToString("N0");
                    TotalBoxLabel.Text = totalBox.ToString();
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    ShipmentTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }

        private async void FilterButton_Click(object sender, EventArgs e)
        {
            try
            {
                string shipmentID = ShipmentID.Text;
                DateTime? startDate = StartDate.Value.Date;
                DateTime? endDate = EndDate.Value.Date;


                var result = await _service.GetShipmentList(shipmentID, startDate, endDate);


                if (result == null)
                {
                    MessageBox.Show("No Data found.");
                    return;
                }

                LoadShipmentTable(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void GenerateButton_Click(object sender, EventArgs e)
        {
            string Filename = $@"PackingList_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            using var sf = new SaveFileDialog();
            sf.Filter = "Excel Files|*.xlsx";
            sf.Title = "Save Packing List";
            sf.DefaultExt = "xlsx";
            sf.FileName = Filename;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                string fileparth = sf.FileName;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Generating packing list...";

                var progress = new Progress<int>(value =>
                {
                    toolStripProgressBar1.Value = value;
                    toolStripStatusLabel1.Text = $"Generating packing list... {value}%";
                });

                try
                {
                    toolStripStatusLabel1.Text = "Processing: 0%";
                    var (isSuccess, Message) = await _excelService.AutofillPackingListTemplate(fileparth, controlnumber, progress);
                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                    }
                    else
                    {
                        MessageBox.Show(Message);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing file: {ex.Message}");
                }
                finally
                {
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel1.Text = "Ready";
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel1.Visible = false;
                }

            }
        }

        private async void CancelShipmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to cancel this shipment?", "Cancel Shipment");

                if (result == DialogResult.OK)
                {
                    var (isSuccess, Message) = await _service.CancelShipment(controlnumber, _userid);

                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                        ShipmentTable.Refresh();
                        ShipmentItemTable.Refresh();
                    }
                    else
                    {
                        MessageBox.Show(Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void ShipmentTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ShipmentTable.Columns[e.ColumnIndex].Name == "Actionbuttons")
            {
                DataGridViewRow selectedRow = ShipmentTable.Rows[e.RowIndex];

                if (selectedRow == null)
                {
                    MessageBox.Show("Empty row data.");
                    return;
                }

                controlnumber = selectedRow.Cells["Shipment Control Number"].Value.ToString();
                string ShipmentDate = selectedRow.Cells["Shipment Date"].Value.ToString();
                string remarks = selectedRow.Cells["Remarks"].Value.ToString();

                if (remarks == "Cancelled Shipment")
                {
                    CancelShipmentButton.Enabled = false;
                }

                ShipmentDateLabel.Text = ShipmentDate;
                ShipmentIDLabel.Text = controlnumber;
                await LoadShipmentItemTable(controlnumber);
            }
        }

        private async void ExportExcel_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            string Date = today.ToString("yyyyMMdd");
            string fileName = $"ShipmentList_{Date}.xlsx";

            DateTime? startDate = StartDate.Value.Date;
            DateTime? endDate = EndDate.Value.Date;

            using SaveFileDialog Save = new();
            Save.Filter = "Excel files (*.xlsx)|*.xlsx";
            Save.Title = "Save Exported Data";
            Save.DefaultExt = "xlsx";
            Save.FileName = fileName;

            if (Save.ShowDialog() == DialogResult.OK)
            {
                string filepath = Save.FileName;
                var result = await _queries.GetShipmentData(startDate, endDate);

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


                string[] columnheaders = ["Control number", "Part Number", "Customer", "Production Date",
                                              "Production Version", "Quantity", "Box", "Entry Date"];


                var reportInfo = new ReportGeneration<Models.ShipmentReport>
                {
                    Title = "Packing Report",
                    Columns = columnheaders,
                    Items = result
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
