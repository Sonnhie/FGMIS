using FGScanner.Database;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FGScanner.Forms.DataEntry
{
    public partial class Dashboard : UserControl
    {
        private Dictionary<int, MonthlyInventorySummary> MonthlyStocksCache = [];
        private readonly Queries _queries;
        private readonly Dbcontext _dbContext;

        public Dashboard()
        {
            InitializeComponent();
            _dbContext = new();
            _queries = new(_dbContext);
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadCMBYearDataSource();

                int selectedYear = DateTime.Now.Year;
                if (cmbYear.SelectedItem != null)
                {
                    selectedYear = int.Parse(cmbYear.SelectedItem.ToString());
                }

                await PopulateStatusCards(selectedYear);
                await PopulateCharts(selectedYear);
                await LoadSlowMovingItems();
                StartPollingForUpdates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report dashboard: {ex.Message}", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PopulateStatusCards(int year)
        {
            try
            {
                await GetTotalMonthlyStocks(year);
                GetMonthlyShipments(year);
                GetTotalReturns(year);
                await GetLowStockItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating status cards: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PopulateCharts(int year)
        {
            try
            {
                await LoadLineChart(year);
                await LoadPieChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating charts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCMBYearDataSource()
        {
            var result = await _queries.GetYear();
            cmbYear.DataSource = result;
        }

        private async Task GetTotalMonthlyStocks(int year)
        {
            int month = DateTime.Now.Month;

            var Data = await _queries.GetMonthlySummary(year);

            var orderedData = Data.OrderBy(d => d.Month).ToList();

            List<MonthlyInventorySummary> monthlyInventorySummaries = new List<MonthlyInventorySummary>();
            for (int i = 0; i < orderedData.Count; i++)
            {
                int Current = orderedData[i].EndingStock;
                int Previous = i == 0 ? 0 : orderedData[i - 1].EndingStock;
                int Change = Current - Previous;
                monthlyInventorySummaries.Add(new MonthlyInventorySummary
                {
                    Month = orderedData[i].Month,
                    In = orderedData[i].In,
                    Out = orderedData[i].Out,
                    EndingStock = Current,
                    ChangePercent = Previous == 0 ? 0 : (Change * 100.0 / Previous),
                    Change = Change
                });
            }

            var CurrentMonthData = monthlyInventorySummaries.FirstOrDefault(d => d.Month == month);
            monthstock_lbl.Text = CurrentMonthData != null ? CurrentMonthData.EndingStock.ToString("N0") : "0";

            if (CurrentMonthData.Change >= 0)
            {
                increase_lbl.Text = $"▲{CurrentMonthData.Change:N0} (+{CurrentMonthData.ChangePercent:N2}%)";
                increase_lbl.ForeColor = Color.Green;
            }
            else
            {
                increase_lbl.Text = $"▼{Math.Abs(CurrentMonthData.Change):N0} ({CurrentMonthData.ChangePercent:N2}%)";
                increase_lbl.ForeColor = Color.Red;
            }
        }

        private async void GetMonthlyShipments(int year)
        {
            int month = DateTime.Now.Month;

            var Data = await _queries.GetMonthlyShipment(year);

            var CurrentMonthData = Data.FirstOrDefault(d => d.Month == month);
            ship_lbl.Text = CurrentMonthData != null ? CurrentMonthData.Out.ToString("N0") : "0";
            if (CurrentMonthData.Change >= 0)
            {
                shipanalytic_lbl.Text = $"▲{CurrentMonthData.Change:N0} (+{CurrentMonthData.ChangePercent:N2}%)";
                shipanalytic_lbl.ForeColor = Color.Green;
            }
            else
            {
                shipanalytic_lbl.Text = $"▼{Math.Abs(CurrentMonthData.Change):N0} ({CurrentMonthData.ChangePercent:N2}%)";
                shipanalytic_lbl.ForeColor = Color.Red;
            }
        }

        private async void GetTotalReturns(int year)
        {
            int month = DateTime.Now.Month;

            var Data = await _queries.GetMonthlyReturns(year);

            var CurrentMonthData = Data.FirstOrDefault(d => d.Month == month);
            return_lbl.Text = CurrentMonthData != null ? CurrentMonthData.Out.ToString("N0") : "0";
            if (CurrentMonthData.Change >= 0)
            {
                returnanalytic_lbl.Text = $"▲{CurrentMonthData.Change:N0} (+{CurrentMonthData.ChangePercent:N2}%)";
                returnanalytic_lbl.ForeColor = Color.Red;
            }
            else
            {
                returnanalytic_lbl.Text = $"▼{Math.Abs(CurrentMonthData.Change):N0} ({CurrentMonthData.ChangePercent:N2}%)";
                returnanalytic_lbl.ForeColor = Color.Green;
            }
        }

        private async Task GetLowStockItems()
        {
            var data = await _queries.GetSlowMovingItem();
            slowitem_lbl.Text = data.ToString("N0");
        }

        private async Task LoadSlowMovingItems()
        {
            try
            {
                var Data = await _queries.GetSlowMovingDataAsync();

                if (Data != null)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Part Number", typeof(string));
                    dt.Columns.Add("Customer", typeof(string));
                    dt.Columns.Add("Production Date", typeof(string));
                    dt.Columns.Add("Box", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Location", typeof(string));
                    dt.Columns.Add("Last Moving Date", typeof(string));
                    dt.Columns.Add("Storage Location", typeof(string));

                    foreach (var item in Data)
                    {
                        if (item.quantity != 0)
                        {
                            dt.Rows.Add
                                (
                                    item.partnumber,
                                    item.customer,
                                    item.proddate.ToString("MM/dd/yyyy"),
                                    item.box,
                                    item.quantity,
                                    item.location,
                                    item.updatedInventory.HasValue ? item.updatedInventory.Value.ToString("MM/dd/yyyy") : "",
                                    item.storagelocation
                                );
                        }
                    }

                    SlowmovingTable.Columns.Clear();
                    SlowmovingTable.ReadOnly = true;
                    SlowmovingTable.DataSource = dt;
                    SlowmovingTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    SlowmovingTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    SlowmovingTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    SlowmovingTable.Columns["Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    SlowmovingTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    SlowmovingTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    SlowmovingTable.Columns["Last Moving Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    SlowmovingTable.Columns["Storage Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading slow moving items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadLineChart(int year)
        {

            var Data = await _queries.GetMonthlySummary(year);

            if (Data == null || Data.Count == 0)
            {
                chart1.Series.Clear(); // Clear the chart so it doesn't show old data
                return;
            }

            int maxStock = Data.Max(d => d.EndingStock);

            double yInterval = maxStock > 0 ? Math.Ceiling(maxStock / 5.0 / 30000000) * 30000000 : 30000000;

            if (maxStock <= 5000000)
            {
                yInterval = 1000000;
            }
            else if (maxStock <= 10000000)
            {
                yInterval = 5000000;
            }
            else if (maxStock <= 15000000)
            {
                yInterval = 10000000;
            }
            else if (maxStock <= 30000000)
            {
                yInterval = 20000000;
            }
            else
            {
                yInterval = Math.Ceiling(maxStock / 5.0 / 30000000) * 30000000;
            }



            chart1.Series.Clear();
            Series EndingStockSeries = new("Ending Stock")
            {
                ChartType = SeriesChartType.SplineArea,
                BorderWidth = 3,
                MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle,
                MarkerSize = 8,
                MarkerColor = Color.DarkBlue,
                Color = Color.FromArgb(80, Color.Blue)
            };
            chart1.Series.Add(EndingStockSeries);

            foreach (var item in Data)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Month);
                int pointIndex = EndingStockSeries.Points.AddXY(item.Month, item.EndingStock);
                var point = EndingStockSeries.Points[pointIndex];
                point.AxisLabel = monthName;
                point.ToolTip = $"Ending Stock: {item.EndingStock:N0}";
            }

            var axisX = chart1.ChartAreas[0].AxisX;
            var axisY = chart1.ChartAreas[0].AxisY;
            var area = chart1.ChartAreas[0];
            axisX.Minimum = 1;
            axisX.Maximum = 12;
            axisX.Interval = 1;
            axisY.Minimum = 0;
            axisY.Maximum = (maxStock == 0) ? yInterval : (maxStock + yInterval);
            axisY.Interval = yInterval;
            axisY.LabelStyle.Format = "N0";
            area.RecalculateAxesScale();
        }

        private async Task LoadPieChart()
        {
            Dictionary<string, Color> customerColor = new Dictionary<string, Color>()
            {
                { "EPPI", Color.Yellow },
                { "CBMP", Color.Green },
                { "BIPH", Color.Blue },
                { "YAZAKI", Color.Orange },
                { "IONICS", Color.Silver },
                { "ZAMA" , Color.LightGray},
                { "JCM", Color.MediumPurple },
                { "EXCELITAS", Color.Gray }
            };

            var Data = await _queries.GetCustomerStocksAsync();

            chart2.Series.Clear();
            Series series = new Series("CustomerStock");
            series.ChartType = SeriesChartType.Pie;

            chart2.Series.Add(series);

            foreach (var item in Data)
            {
                int pointindex = chart2.Series["CustomerStock"].Points.AddXY(item.Customer, item.Stock);
                chart2.Series["CustomerStock"].ToolTip =
                "#AXISLABEL\nStock: #VALY\nPercentage: #PERCENT";
                chart2.Series["CustomerStock"].Label = "#AXISLABEL #PERCENT{P0}";
                chart2.Series["CustomerStock"].LabelForeColor = Color.Black;
                chart2.Series["CustomerStock"].Font = new Font("Segoe UI", 9, FontStyle.Bold);


                var point = chart2.Series["CustomerStock"].Points[pointindex];

                if (customerColor.TryGetValue(item.Customer, out Color value))
                {
                    point.Color = value;
                }
                else
                {
                    point.Color = Color.LightGray;
                }
            }
        }

        private async Task<Dictionary<int, MonthlyInventorySummary>> LoadMonthlyStockCache(int year)
        {
            var Data = await _queries.GetMonthlySummary(year);
            return Data.ToDictionary(d => d.Month, d => d);
        }

        private bool HasChangeds(Dictionary<int, MonthlyInventorySummary> newData, Dictionary<int, MonthlyInventorySummary> oldData)
        {
            if (oldData.Count != newData.Count)
            {
                return true;
            }

            foreach (var item in oldData)
            {
                if (!newData.TryGetValue(item.Key, out MonthlyInventorySummary value) || value.EndingStock != item.Value.EndingStock)
                {
                    //MessageBox.Show($"Data change detected for Month: {item.Key}. Old Ending Stock: {item.Value.EndingStock}, New Ending Stock: {newData[item.Key].EndingStock}", "Data Change Detected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

        private void StartPollingForUpdates()
        {
            timer1.Interval = 5000;
            timer1.Start();
        }

        private async void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedYear = Convert.ToInt32(cmbYear.SelectedItem);

            await PopulateStatusCards(selectedYear);
            await PopulateCharts(selectedYear);
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            try
            {
                var selectedYear = cmbYear.SelectedItem != null
                    ? int.Parse(cmbYear.SelectedItem.ToString())
                    : DateTime.Now.Year;

                var newData = await LoadMonthlyStockCache(selectedYear);

                if (HasChangeds(newData, MonthlyStocksCache))
                {
                    MonthlyStocksCache = newData;
                    await PopulateStatusCards(selectedYear);
                    await PopulateCharts(selectedYear);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Timer error: {ex.Message}");
            }
            finally
            {
                timer1.Start();
            }
        }
    }
}
