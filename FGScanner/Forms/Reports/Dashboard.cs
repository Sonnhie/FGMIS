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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace FGScanner.Forms.DataEntry
{
    public partial class Dashboard : UserControl
    {
        private Dictionary<int, MonthlyInventorySummary> MonthlyStocksCache = [];
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
        private readonly SemaphoreSlim _refreshLock = new(1, 1);
        private bool _isInitializing;

        public Dashboard()
        {
            InitializeComponent();
            _dbContext = new();
            _queries = new(_dbContext);
            ApplyModernStyling();
        }

        private void ApplyModernStyling()
        {
            // Modern card styling with accent bars
            panel2.Paint += (s, e) => DrawCard(e.Graphics, panel2.ClientRectangle, Color.FromArgb(37, 99, 235));
            panel3.Paint += (s, e) => DrawCard(e.Graphics, panel3.ClientRectangle, Color.FromArgb(16, 185, 129));
            panel4.Paint += (s, e) => DrawCard(e.Graphics, panel4.ClientRectangle, Color.FromArgb(245, 158, 11));
            panel5.Paint += (s, e) => DrawCard(e.Graphics, panel5.ClientRectangle, Color.FromArgb(239, 68, 68));

            // Modern label typography
            label3.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(100, 116, 139);
            monthstock_lbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            monthstock_lbl.ForeColor = Color.FromArgb(15, 23, 42);

            label6.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(100, 116, 139);
            ship_lbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            ship_lbl.ForeColor = Color.FromArgb(15, 23, 42);

            label8.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(100, 116, 139);
            return_lbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            return_lbl.ForeColor = Color.FromArgb(15, 23, 42);

            label11.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label11.ForeColor = Color.FromArgb(100, 116, 139);
            slowitem_lbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            slowitem_lbl.ForeColor = Color.FromArgb(239, 68, 68);

            label2.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(30, 41, 59);

            label13.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label13.ForeColor = Color.FromArgb(71, 85, 105);

            ConfigureChartBaseStyles();
        }

        private static void DrawCard(Graphics g, Rectangle bounds, Color accentColor)
        {
            if (bounds.Width < 4 || bounds.Height < 4) return;

            using var borderPen = new Pen(Color.FromArgb(226, 232, 240), 1);
            g.DrawRectangle(borderPen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);

            using var accentBrush = new SolidBrush(accentColor);
            g.FillRectangle(accentBrush, bounds.X, bounds.Y, bounds.Width, 3);
        }

        private void ConfigureChartBaseStyles()
        {
            cartesianChart1.BackColor = Color.White;
            pieChart1.BackColor = Color.White;
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            _isInitializing = true;
            timer1.Stop();
            await _refreshLock.WaitAsync();

            try
            {
                await LoadCMBYearDataSource();

                int selectedYear = DateTime.Now.Year;

                // Use int.TryParse for safer conversion
                if (cmbYear.SelectedItem != null && int.TryParse(cmbYear.SelectedItem.ToString(), out int parsedYear))
                {
                    selectedYear = parsedYear;
                }

                await PopulateStatusCards(selectedYear);
                await PopulateCharts(selectedYear);
                await LoadSlowMovingItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report dashboard: {ex.Message}", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _refreshLock.Release();
                _isInitializing = false;
                StartPollingForUpdates();
            }
        }

        private async Task PopulateStatusCards(int year)
        {
            try
            {
                await GetTotalMonthlyStocks(year);
                await GetMonthlyShipments(year);
                await GetTotalReturns(year);
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
            if (CurrentMonthData == null)
            {
                monthstock_lbl.Text = "0";
                increase_lbl.Text = "No data";
                increase_lbl.ForeColor = Color.FromArgb(100, 116, 139);
                return;
            }

            monthstock_lbl.Text = CurrentMonthData.EndingStock.ToString("N0");

            if (CurrentMonthData.Change >= 0)
            {
                increase_lbl.Text = $"▲ +{CurrentMonthData.Change:N0} (+{CurrentMonthData.ChangePercent:N1}%) vs prev";
                increase_lbl.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                increase_lbl.Text = $"▼ -{Math.Abs(CurrentMonthData.Change):N0} ({CurrentMonthData.ChangePercent:N1}%) vs prev";
                increase_lbl.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }

        private async Task GetMonthlyShipments(int year)
        {
            int month = DateTime.Now.Month;

            var Data = await _queries.GetMonthlyShipment(year);

            var CurrentMonthData = Data.FirstOrDefault(d => d.Month == month);
            if (CurrentMonthData == null)
            {
                ship_lbl.Text = "0";
                shipanalytic_lbl.Text = "No data";
                shipanalytic_lbl.ForeColor = Color.FromArgb(100, 116, 139);
                return;
            }

            ship_lbl.Text = CurrentMonthData.Out.ToString("N0");
            if (CurrentMonthData.Change >= 0)
            {
                shipanalytic_lbl.Text = $"▲ +{CurrentMonthData.Change:N0} (+{CurrentMonthData.ChangePercent:N1}%) vs prev";
                shipanalytic_lbl.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                shipanalytic_lbl.Text = $"▼ -{Math.Abs(CurrentMonthData.Change):N0} ({CurrentMonthData.ChangePercent:N1}%) vs prev";
                shipanalytic_lbl.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }

        private async Task GetTotalReturns(int year)
        {
            int month = DateTime.Now.Month;

            var Data = await _queries.GetMonthlyReturns(year);

            var CurrentMonthData = Data.FirstOrDefault(d => d.Month == month);
            if (CurrentMonthData == null)
            {
                return_lbl.Text = "0";
                returnanalytic_lbl.Text = "No data";
                returnanalytic_lbl.ForeColor = Color.FromArgb(100, 116, 139);
                return;
            }

            return_lbl.Text = CurrentMonthData.Out.ToString("N0");
            if (CurrentMonthData.Change >= 0)
            {
                returnanalytic_lbl.Text = $"▲ +{CurrentMonthData.Change:N0} (+{CurrentMonthData.ChangePercent:N1}%) vs prev";
                returnanalytic_lbl.ForeColor = Color.FromArgb(239, 68, 68);
            }
            else
            {
                returnanalytic_lbl.Text = $"▼ -{Math.Abs(CurrentMonthData.Change):N0} ({CurrentMonthData.ChangePercent:N1}%) vs prev";
                returnanalytic_lbl.ForeColor = Color.FromArgb(16, 185, 129);
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
                cartesianChart1.Series = Array.Empty<ISeries>();
                return;
            }

            var ordered = Data.OrderBy(d => d.Month).ToList();
            var endingStocks = ordered.Select(d => (double)d.EndingStock).ToArray();
            var months = ordered.Select(d => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(d.Month)).ToArray();

            cartesianChart1.Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = endingStocks,
                    Name = "Ending Stock",
                    Fill = new LinearGradientPaint(
                        new[] { new SKColor(59, 130, 246, 110), new SKColor(59, 130, 246, 5) },
                        new SKPoint(0.5f, 0),
                        new SKPoint(0.5f, 1)),
                    Stroke = new SolidColorPaint(new SKColor(37, 99, 235)) { StrokeThickness = 3 },
                    GeometrySize = 7,
                    GeometryFill = new SolidColorPaint(SKColors.White),
                    GeometryStroke = new SolidColorPaint(new SKColor(37, 99, 235)) { StrokeThickness = 2 },
                    LineSmoothness = 0.45,
                    YToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue:N0} pcs"
                }
            };

            cartesianChart1.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = months,
                    LabelsPaint = new SolidColorPaint(new SKColor(100, 116, 139)),
                    TextSize = 11,
                    SeparatorsPaint = null
                }
            };

            cartesianChart1.YAxes = new Axis[]
            {
                new Axis
                {
                    LabelsPaint = new SolidColorPaint(new SKColor(100, 116, 139)),
                    TextSize = 11,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(241, 245, 249)) { StrokeThickness = 1 },
                    Labeler = val => val >= 1_000_000 ? $"{(val / 1_000_000):N1}M" : val >= 1_000 ? $"{(val / 1_000):N0}K" : val.ToString("N0")
                }
            };

            cartesianChart1.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
            cartesianChart1.TooltipBackgroundPaint = new SolidColorPaint(new SKColor(30, 41, 59));
            cartesianChart1.TooltipTextPaint = new SolidColorPaint(new SKColor(241, 245, 249));
            cartesianChart1.TooltipTextSize = 12;
        }

        private async Task LoadPieChart()
        {
            var Data = await _queries.GetCustomerStocksAsync();

            if (Data == null || Data.Count == 0)
            {
                pieChart1.Series = Array.Empty<ISeries>();
                return;
            }

            Dictionary<string, SKColor> skCustomerColors = new(StringComparer.OrdinalIgnoreCase)
            {
                { "EPPI", new SKColor(16, 185, 129) },     // Emerald
                { "CBMP", new SKColor(139, 92, 246) },    // Violet
                { "BIPH", new SKColor(14, 165, 233) },     // Sky Blue
                { "YAZAKI", new SKColor(249, 115, 22) },   // Warm Amber
                { "IONICS", new SKColor(99, 102, 241) },   // Indigo
                { "ZAMA" , new SKColor(244, 63, 94) },     // Rose
                { "JCM", new SKColor(6, 182, 212) },       // Cyan
                { "EXCELITAS", new SKColor(100, 116, 139) } // Slate
            };

            var pieSeries = new List<ISeries>();
            var totalStock = Data.Where(item => item.Stock > 0).Sum(item => item.Stock);

            foreach (var item in Data.Where(item => item.Stock > 0))
            {
                var customer = string.IsNullOrWhiteSpace(item.Customer) ? "Unknown" : item.Customer;
                var stock = item.Stock;
                var share = totalStock == 0 ? 0d : (double)stock / totalStock;
                SKColor color = skCustomerColors.TryGetValue(customer, out var skColor) ? skColor : new SKColor(148, 163, 184);

                pieSeries.Add(new PieSeries<long>
                {
                    Values = new[] { stock },
                    Name = customer,
                    Fill = new SolidColorPaint(color),
                    Stroke = new SolidColorPaint(SKColors.White) { StrokeThickness = 2 },
                    Pushout = 4,
                    InnerRadius = 55,
                    ToolTipLabelFormatter = _ => $"{customer}: {stock:N0} pcs ({share:P1})"
                });
            }

            pieChart1.Series = pieSeries.ToArray();
            pieChart1.LegendPosition = LiveChartsCore.Measure.LegendPosition.Bottom;
            pieChart1.LegendTextPaint = new SolidColorPaint(new SKColor(71, 85, 105));
            pieChart1.LegendTextSize = 11;
            // LiveCharts pie charts only support centered tooltips.
            pieChart1.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Center;
            pieChart1.TooltipBackgroundPaint = new SolidColorPaint(new SKColor(30, 41, 59));
            pieChart1.TooltipTextPaint = new SolidColorPaint(new SKColor(241, 245, 249));
            pieChart1.TooltipTextSize = 12;
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
            if (_isInitializing || cmbYear.SelectedItem == null)
            {
                return;
            }

            int selectedYear = Convert.ToInt32(cmbYear.SelectedItem);
            await _refreshLock.WaitAsync();

            try
            {
                await PopulateStatusCards(selectedYear);
                await PopulateCharts(selectedYear);
            }
            finally
            {
                _refreshLock.Release();
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            await _refreshLock.WaitAsync();

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
                _refreshLock.Release();
                timer1.Start();
            }
        }
    }
}
