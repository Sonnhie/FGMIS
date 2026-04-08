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

namespace FGScanner
{
    public partial class Slowmoving : Form
    {
        public Slowmoving()
        {
            InitializeComponent();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void Slowmoving_Load(object sender, EventArgs e)
        {
            string partnumber = TxtPartnumber.Text;
            LoadFilteredData(partnumber);
        }

        private void LoadFilteredData(string partnumber)
        {
            try
            {
                var Repo = new TransactionRepo();
                var Data = Repo.GetSlowMovingItems(partnumber);

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
                    LogsTable.Columns.Clear();
                    LogsTable.ReadOnly = true;
                    LogsTable.DataSource = dt;
                    LogsTable.Columns["Part Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    LogsTable.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Last Moving Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Classification"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    LogsTable.Columns["Storage Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void TxtPartnumber_TextChanged(object sender, EventArgs e)
        {
            string partnumber = TxtPartnumber.Text;
            LoadFilteredData(partnumber);
        }

        private DataTable GetData()
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn col in LogsTable.Columns)
            {
                dt.Columns.Add(col.HeaderText);
            }
            foreach(DataGridViewRow row in LogsTable.Rows)
            {
                DataRow rowdata = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                    rowdata[cell.ColumnIndex] = cell.Value;
                    dt.Rows.Add(rowdata);
                
            }
            return dt;
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            string Date = today.ToString("yyyyMMdd");
            string fileName = $"SlowMoving_{Date}.xlsx";

            using (SaveFileDialog Save = new SaveFileDialog())
            {
                Save.Filter = "Excel files (*.xlsx)|*.xlsx";
                Save.Title = "Save Exported Data";
                Save.DefaultExt = "xlsx";
                Save.FileName = fileName;

                if (Save.ShowDialog() == DialogResult.OK)
                {
                    string filepath = Save.FileName;
                    var Repo = new TransactionRepo();
                    var Data = Repo.GetSlowMoving();
                    
                    //var Datas = GetData();
                    
                    
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = $"Exporting...";

                    var progress = new Progress<int>(value =>
                    {
                        toolStripProgressBar1.Value = value;
                        toolStripStatusLabel1.Text = $"Exporting... {value}%";
                    });
                   
                    var columnMap = new Dictionary<string, string>
                    {
                        { "partnumber", "Part Number" },
                        { "customer", "Customer" },
                        { "prod_date", "Lot date" },
                        { "prod_ver", "Prod Ver" },
                        { "location", "Location" },
                        { "quantity", "Quantity" },
                        { "total_box", "Total Box" },
                        { "storage_location", "Storage Location" },
                        { "last_out_date", "Last Movement date" },
                        { "movement_classification", "Movement Classification" }
                    };

                    try
                    {
                        await ExportService.ExportCSV(Data, columnMap, filepath, progress, "Slow moving");
                        toolStripProgressBar1.Value = 100;
                        toolStripStatusLabel1.Text = "Export completed successfully";
                        MessageBox.Show("Export completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
}
