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
        public Report()
        {
            InitializeComponent();
            LoadCMBYearDataSource();
            MonthlyInventory();
            MonthlyShipmentInventory();
            CustomerStock();
        }

        private void Report_Load(object sender, EventArgs e)
        {

        }

        private void LoadCMBYearDataSource()
        {
            var Repo = new TransactionRepo();
            var Year = Repo.GetYear();

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            auto.AddRange(Year.Select(y => y.ToString()).ToArray());
            cmbYear.DataSource = auto;
        }

        private void MonthlyInventory()
        {
            int year = Convert.ToInt32(cmbYear.SelectedItem);

            var Repo = new TransactionRepo();
            var Data = Repo.GetMonthlyInventory(year);


            chart1.Series.Clear();

            Series seriesIN = new Series("IN");
            seriesIN.ChartType = SeriesChartType.Column;
            Series seriesOUT = new Series("OUT");
            seriesOUT.ChartType = SeriesChartType.Column;
            seriesIN.Legend = "IN";
            seriesOUT.Legend = "OUT";
            chart1.Series.Add(seriesIN);
            chart1.Series.Add(seriesOUT);
            


            foreach (var item in Data)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Month);

                int pointIndexIN =  chart1.Series["IN"].Points.AddXY(monthName, item.In);
                int pointIndexOUT = chart1.Series["OUT"].Points.AddXY(monthName, item.Out);

                //chart1.Series["IN"].Points[pointIndexIN].Label = "IN";
                //chart1.Series["OUT"].Points[pointIndexOUT].Label = "OUT";

                //chart1.Series["IN"].Points[pointIndexIN].LabelForeColor = Color.White;
                //chart1.Series["OUT"].Points[pointIndexOUT].LabelForeColor = Color.White;

                chart1.Series["IN"].Points[pointIndexIN].Color = Color.Teal;
                chart1.Series["OUT"].Points[pointIndexOUT].Color = Color.Orange;

                chart1.Series["IN"].Points[pointIndexIN].ToolTip = $"Month: {monthName}\nIN: {item.In}";
                chart1.Series["OUT"].Points[pointIndexOUT].ToolTip = $"Month: {monthName}\nOUT: {item.Out}";
            }
            chart1.ChartAreas[0].AxisX.Interval = 1;

        }

        private void MonthlyShipmentInventory()
        {
            int year = Convert.ToInt32(cmbYear.SelectedItem);

            var Repo = new TransactionRepo();
            var Data = Repo.GetShipment(year);

            chart3.Series.Clear();

            Series seriesOUT = new Series("OUT");
            seriesOUT.ChartType = SeriesChartType.Column;

            chart3.Series.Add(seriesOUT);

            foreach (var item in Data)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Month);
                int pointIndexOUT = chart3.Series["OUT"].Points.AddXY(monthName, item.Out);

                chart3.Series["OUT"].Points[pointIndexOUT].Color = Color.Teal;

                chart3.Series["OUT"].Points[pointIndexOUT].ToolTip = $"Month: {monthName}\nOUT: {item.Out}";
            }
            chart3.ChartAreas[0].AxisX.Interval = 1;
        }

        private void CustomerStock()
        {
            var Repo = new TransactionRepo();
            var Data = Repo.GetCurrentCustomerStock();

            chart2.Series.Clear();
            Series series = new Series("CustomerStock");
            series.ChartType = SeriesChartType.Pie;

            chart2.Series.Add(series);
            
            foreach (var item in Data)
            {
                chart2.Series["CustomerStock"].Points.AddXY(item.Customer, item.Stock);
                chart2.Series["CustomerStock"].ToolTip =
                "#AXISLABEL\nStock: #VALY\nPercentage: #PERCENT";
                chart2.Series["CustomerStock"].Label = "#AXISLABEL #PERCENT{P0}";
                chart2.Series["CustomerStock"].LabelForeColor = Color.White;
                chart2.Series["CustomerStock"].Font = new Font("Segoe UI", 9, FontStyle.Bold);
                chart2.Palette = ChartColorPalette.SeaGreen;
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonthlyInventory();
            MonthlyShipmentInventory();
        }
    }
}
