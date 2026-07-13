using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
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
    public partial class TransferLocation : Form
    {
        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;
        private string _userid;
        private bool _isLoading = false;
        private List<ActualInventory> _currentInventoryData = new List<ActualInventory>();

        public TransferLocation(string userid)
        {
            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
            InitializeComponent();
        }

        private async Task LoadCurrRackLocationList()
        {
            try
            {
                string warehouseId = WarehouseComboBox.Text;
                var data = await _service.GetRackLocationsAsync(warehouseId);
                if (data != null)
                {
                    currLocationComboBox.DataSource = data;
                    currLocationComboBox.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No rack locations found for the selected warehouse.");
                    currLocationComboBox.DataSource = null;
                    newLocationComboBox.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rack locations: {ex.Message}");
            }
        }
        private async Task LoadNewRackLocationList()
        {
            try
            {
                string warehouseId = WarehouseComboBox.Text;
                var data = await _service.GetRackLocationsAsync(warehouseId);
                if (data != null)
                {
                    newLocationComboBox.DataSource = data;
                    newLocationComboBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rack locations: {ex.Message}");
            }
        }
        private async void WarehouseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            try
            {
                _isLoading = true;
                await LoadCurrRackLocationList();
                await LoadNewRackLocationList();
            }
            finally
            {
                _isLoading = false;
            }
        }
        private async Task LoadInventoryTable()
        {
            try
            {
                string warehouseId = WarehouseComboBox.Text;
                string currLocation = currLocationComboBox.Text;
                var data = await _service.GetActualInventories(warehouseId, currLocation);
                if (data != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Production Version", typeof(string));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("Box Count", typeof(int));
                    dt.Columns.Add("Customer", typeof(string));
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            item.Partnumber,
                            item.ProdDate.ToString("MM/dd/yyyy"),
                            item.ProdVer,
                            item.Quantity,
                            item.TotalBox,
                            item.CustomerId
                        );
                    }
                    RackTable.Columns.Clear();
                    RackTable.DataSource = dt;


                    int count = data.Count;
                    int sumQuantity = data.Sum(item => item.Quantity);
                    int totalBoxCount = data.Sum(item => item.TotalBox);
                    string customerId = data.FirstOrDefault()?.CustomerId ?? string.Empty;

                    PartcountLabel.Text = count.ToString();
                    QuantityLabel.Text = sumQuantity.ToString();
                    BoxLabel.Text = totalBoxCount.ToString();
                    CustomerLabel.Text = customerId;

                    RackTable.Columns["Part Number"].ReadOnly = true;
                    RackTable.Columns["Production Date"].ReadOnly = true;
                    RackTable.Columns["Production Version"].ReadOnly = true;
                    RackTable.Columns["Quantity"].ReadOnly = true;
                    RackTable.Columns["Box Count"].ReadOnly = true;
                    RackTable.Columns["Customer"].ReadOnly = true;

                    RackTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Box Count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                    {
                        Name = "Select",
                        HeaderText = "Select",
                        Width = 50,
                        ReadOnly = false
                    };
                    RackTable.EditMode = DataGridViewEditMode.EditOnEnter;
                    RackTable.Columns.Add(checkBoxColumn);

                    DataGridViewTextBoxColumn textboxColumn = new DataGridViewTextBoxColumn
                    {
                        Name = "Box",
                        HeaderText = "Box"
                    };
                    RackTable.Columns.Add(textboxColumn);
                }
                else
                {
                    MessageBox.Show("No inventory found for the selected location.");
                    RackTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }
        private async void currLocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            await LoadInventoryTable();
        }
        private async void SelectFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                string warehouseId = WarehouseComboBox.Text;
                string currLocation = currLocationComboBox.Text;
                string newLocation = newLocationComboBox.Text;
                if (string.IsNullOrEmpty(warehouseId) || string.IsNullOrEmpty(currLocation) || string.IsNullOrEmpty(newLocation))
                {
                    MessageBox.Show("Please select a warehouse, current location, and new location.");
                    return;
                }
                List<ActualInventory> selectedInventories = new();
                foreach (DataGridViewRow row in RackTable.Rows)
                {
                    if (row.Cells["Select"].Value != null && (bool)row.Cells["Select"].Value)
                    {

                        int box = Convert.ToInt32(row.Cells["Box"].Value);
                        string partnumber = row.Cells["Part Number"].Value.ToString();
                        var checkPPS = await _queries.GetProductInfo(partnumber);
                        int pps = checkPPS.PPS;
                        int initialqty = box * pps;


                        ActualInventory inventory = new()
                        {
                            Partnumber = row.Cells["Part Number"].Value.ToString(),
                            ProdDate = DateTime.Parse(row.Cells["Production Date"].Value.ToString()),
                            ProdVer = row.Cells["Production Version"].Value.ToString(),
                            Quantity =  initialqty,
                            TotalBox = box,
                            CustomerId = row.Cells["Customer"].Value.ToString()
                        };
                        selectedInventories.Add(inventory);
                    }
                }
                if (selectedInventories.Count == 0)
                {
                    MessageBox.Show("Please select at least one inventory item to transfer.");
                    return;
                }
                var result = MessageBox.Show($"Transfer {selectedInventories.Count} items, {selectedInventories.Sum(i => i.Quantity)} Quantity, {selectedInventories.Sum(x => x.TotalBox)} Boxes from {currLocation} to {newLocation}?", "Transfer Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var results = await _service.SaveScannedItemsAsync(selectedInventories, currLocation, newLocation, warehouseId, currLocation, _userid);

                    if (results.isSuccess)
                    {
                        MessageBox.Show(results.Message);
                        await LoadInventoryTable();
                    }
                    else
                    {
                        MessageBox.Show(results.Message);
                    }
                    selectedInventories.Clear();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Error: " + ex.InnerException.Message;
                }
                MessageBox.Show($"Crash Details:\n{errorMessage}", "Error Details");
            }
        }
    }
}
