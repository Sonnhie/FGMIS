using FGScanner.Model;
using FGScanner.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FGScanner
{
    public partial class Report : Form
    {
        private Dictionary<int, MonthlyInventorySummary> MonthlyStocksCache = new Dictionary<int, MonthlyInventorySummary>();

        public Report()
        {
            InitializeComponent();  
        }

        private void Report_Load(object sender, EventArgs e)
        {
            int selectedYear = cmbYear.SelectedItem != null ? int.Parse(cmbYear.SelectedItem.ToString()) : DateTime.Now.Year;
            LoadCMBYearDataSource();
            PopulateStatusCards(selectedYear);
            PopulateCharts(selectedYear);
            StartPollingForUpdates();
            LoadSlowMovingItems();
        }

        private void PopulateStatusCards(int year)
        {
            try
            {
                GetTotalMonthlyStocks(year);
                GetMonthlyShipments(year);
                GetTotalReturns(year);
                GetLowStockItems();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error populating status cards: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateCharts(int year)
        {
            try
            {
                LoadLineChart(year);
                LoadPieChart();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error populating charts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCMBYearDataSource()
        {
            var Repo = new TransactionRepo();
            var Year = Repo.GetYear();

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            auto.AddRange(Year.Select(y => y.ToString()).ToArray());
            cmbYear.DataSource = auto;
        }

        private void GetTotalMonthlyStocks(int year)
        {
           int month = DateTime.Now.Month;
           
           var Repo = new TransactionRepo();
           var Data = Repo.GetMonthlyInventory(year);

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

        private void GetMonthlyShipments(int year)
        {
            int month = DateTime.Now.Month;
            var Repo = new TransactionRepo();
            var Data = Repo.GetMonthlyShipments(year);

            var orderedData = Data.OrderBy(d => d.Month).ToList();
            List<MonthlyShipment> monthlyShipments = new List<MonthlyShipment>();
            for (int i = 0; i < orderedData.Count; i++)
            {
                int Current = orderedData[i].Out;
                int Previous = i == 0 ? 0 : orderedData[i - 1].Out;
                int Change = Current - Previous;

                monthlyShipments.Add(new MonthlyShipment
                {
                    Month = orderedData[i].Month,
                    Out = orderedData[i].Out,
                    Change = Change,
                    ChangePercent = Previous == 0 ? 0 : (Change * 100.0 / Previous)
                });
            }

            var CurrentMonthData = monthlyShipments.FirstOrDefault(d => d.Month == month);
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

        private void GetTotalReturns(int year)
        {
            int month = DateTime.Now.Month;
            var Repo = new TransactionRepo();
            var Data = Repo.GetMonthlyReturn(year);
            var orderedData = Data.OrderBy(d => d.Month).ToList();
            List<MonthlyReturn> monthlyReturns = new List<MonthlyReturn>();
            for (int i = 0; i < orderedData.Count; i++)
            {
                int Current = orderedData[i].Out;
                int Previous = i == 0 ? 0 : orderedData[i - 1].Out;
                int Change = Current - Previous;

                monthlyReturns.Add(new MonthlyReturn
                {
                    Month = orderedData[i].Month,
                    Out = orderedData[i].Out,
                    Change = Change,
                    ChangePercent = Previous == 0 ? 0 : (Change * 100.0 / Previous)
                });
            }

            var CurrentMonthData = monthlyReturns.FirstOrDefault(d => d.Month == month);
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

        private void GetLowStockItems()
        {
            var Repo = new TransactionRepo();
            var Data = Repo.GetTotalSlowmovingItems();
            slowitem_lbl.Text = Data.ToString("N0");
        }

        private void LoadSlowMovingItems()
        {
            try
            {
                var Repo = new TransactionRepo();
                var Data = Repo.GetSlowMovingItems();

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
                    dt.Columns.Add("Classification", typeof(string));
                    dt.Columns.Add("Storage Location", typeof(string));

                    foreach (var item in Data)
                    {
                        if (item.Quantity != 0)
                        {
                            dt.Rows.Add
                                (
                                    item.Partnumber,
                                    item.Customer,
                                    item.ProdDate.ToString("MM/dd/yyyy"),
                                    item.Box,
                                    item.Quantity,
                                    item.Location,
                                    item.Lastmovement.ToString("MM/dd/yyyy"),
                                    item.Classification,
                                    item.Storage_location
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
                    SlowmovingTable.Columns["Classification"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    SlowmovingTable.Columns["Storage Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading slow moving items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLineChart(int year)
        {
            var Repo = new TransactionRepo();
            var Data = Repo.GetMonthlyInventory(year);
            int maxStock = Data.Max(d => d.EndingStock);
            double yInterval = maxStock > 0 ? Math.Ceiling(maxStock / 5.0 / 1000000) * 1000000 : 1000000;

            if (maxStock <= 50000)
            {
                yInterval = 10000;
            }
            else if (maxStock <= 100000)
            {
                yInterval = 20000;
            }
            else if (maxStock <= 500000)
            {
                yInterval = 50000;
            }
            else if (maxStock <= 1000000)
            {
                yInterval = 100000;
            }
            else
            {
                yInterval = Math.Ceiling(maxStock / 5.0 / 1000000) * 1000000;
            }

            chart1.Series.Clear();
            Series EndingStockSeries = new Series("Ending Stock");
            EndingStockSeries.ChartType = SeriesChartType.SplineArea;
            EndingStockSeries.BorderWidth = 3;
            EndingStockSeries.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            EndingStockSeries.MarkerSize = 8;
            EndingStockSeries.MarkerColor = Color.DarkBlue;
            EndingStockSeries.Color = Color.FromArgb(80, Color.Blue);
            chart1.Series.Add(EndingStockSeries);

            foreach (var item in Data)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Month);
                var point = EndingStockSeries.Points.AddXY(monthName, item.EndingStock);
                EndingStockSeries.Points[point].ToolTip = $"Ending Stock: {item.EndingStock:N0}";
            }
            var axisX = chart1.ChartAreas[0].AxisX;
            var axisY = chart1.ChartAreas[0].AxisY;
            var area = chart1.ChartAreas[0];
            axisX.Minimum = 1;
            axisX.Maximum = 12;
            axisX.Interval = 1;
            axisY.Minimum = 0;
            axisY.Maximum = maxStock + yInterval;
            axisY.Interval = yInterval;
            axisY.LabelStyle.Format = "N0";
            area.RecalculateAxesScale();
        }

        private void LoadPieChart()
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

            var Repo = new TransactionRepo();
            var Data = Repo.GetCurrentCustomerStock();

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

                if (customerColor.ContainsKey(item.Customer))
                {
                    point.Color = customerColor[item.Customer];
                }
                else
                {
                    point.Color = Color.LightGray;
                }
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedYear = Convert.ToInt32(cmbYear.SelectedItem);

            PopulateStatusCards(selectedYear);
            PopulateCharts(selectedYear);
        }

        private Dictionary<int, MonthlyInventorySummary> LoadMonthlyStockCache(int year)
        {
            var Repo = new TransactionRepo();
            var Data = Repo.GetMonthlyInventory(year);
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
                if (!newData.ContainsKey(item.Key) || newData[item.Key].EndingStock != item.Value.EndingStock)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            var selectedYear = cmbYear.SelectedItem != null ? int.Parse(cmbYear.SelectedItem.ToString()) : DateTime.Now.Year;
            var newData = LoadMonthlyStockCache(selectedYear);
            if (HasChangeds(newData, MonthlyStocksCache))
            {
                MonthlyStocksCache = newData;
                PopulateStatusCards(selectedYear);
                PopulateCharts(selectedYear);
            }
        }
    }
}
