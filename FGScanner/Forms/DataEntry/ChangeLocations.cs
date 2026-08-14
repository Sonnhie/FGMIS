using FGScanner.Database;
using FGScanner.Model;
using FGScanner.Models;
using FGScanner.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
using System.Globalization;
using FGScanner.Services.Classes;

namespace FGScanner.Forms.DataEntry
{
    public partial class ChangeLocations : UserControl
    {
        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;
        private string _userid;
        private bool _isLoading = false;
        private readonly List<ActualInventory> _currentInventoryData = [];

        public ChangeLocations(string userid)
        {
            InitializeComponent();
            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
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
                    dt.Columns.Add("Total Quantity", typeof(int));
                    dt.Columns.Add("Box Count", typeof(int));
                    dt.Columns.Add("Customer", typeof(string));
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            item.Partnumber,
                            item.ProductionDate,
                            item.ProductionVersion,
                            item.Quantity,
                            item.Box,
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
                    RackTable.Columns["Total Quantity"].ReadOnly = true;
                    RackTable.Columns["Box Count"].ReadOnly = true;
                    RackTable.Columns["Customer"].ReadOnly = true;

                    RackTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RackTable.Columns["Total Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                        Name = "Quantity",
                        HeaderText = "Quantity"
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

        private async void currLocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            await LoadInventoryTable();
        }

        private async void TransferButton_Click(object sender, EventArgs e)
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

                var selectedRows = RackTable.Rows.Cast<DataGridViewRow>()
                                    .Where(row => row.Cells["Select"].Value != null && (bool)row.Cells["Select"].Value)
                                    .ToList();
                var partNumbers = selectedRows
                                .Select(row => row.Cells["Part Number"].Value.ToString())
                                .Distinct()
                                .ToList();

                var productDict = await _dbContext.Products
                                  .Where(p => partNumbers.Contains(p.PartNumber))
                                  .ToDictionaryAsync(x => x.PartNumber, x => x.PPS);

                List<ActualInventory> selectedInventories = [];
                foreach (var row in selectedRows)
                {
                    int Quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    string partnumber = row.Cells["Part Number"].Value.ToString();
                    string dateString = row.Cells["Production Date"].Value.ToString();
                    string[] dateFormats = {
                            "MM/dd/yyyy", "M/d/yyyy", "M/dd/yyyy", "MM/d/yyyy",
                            "MM-dd-yyyy", "M-d-yyyy", "M-dd-yyyy", "MM-d-yyyy"
                        };

                    DateTime safeProdDate = DateTime.ParseExact(
                        dateString,
                        dateFormats,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None
                    );

                    int pps = 1;
                    if (productDict.TryGetValue(partnumber, out int dbPPS) && dbPPS > 0)
                    {
                        pps = dbPPS;
                    }
                    int box = (int)Math.Ceiling((double)Quantity / pps);

                    ActualInventory inventory = new()
                    {
                        Partnumber = row.Cells["Part Number"].Value.ToString(),
                        ProdDate = safeProdDate,
                        ProdVer = row.Cells["Production Version"].Value.ToString(),
                        Quantity = Quantity,
                        TotalBox = box,
                        CustomerId = row.Cells["Customer"].Value.ToString()
                    };

                    selectedInventories.Add(inventory);
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
