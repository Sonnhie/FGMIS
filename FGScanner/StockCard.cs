using FGScanner.Model;
using FGScanner.Util;
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
    public partial class StockCard : Form
    {
        public StockCard()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                string partnumber = PartnumberTextbox.Text;
                DateTime postingDate1 = PostingDate1.Value.Date;
                DateTime postingDate2 = PostingDate2.Value.Date;
                var Repo = new TransactionRepo();
                var Data = Repo.GetINOUTinventory(partnumber, postingDate1, postingDate2);
                var result = Data.FirstOrDefault();


                if (Data == null || !Data.Any())
                {
                    MessageBox.Show("No Record Found.");
                    return;
                }

                if (result?.info == null)
                {
                    MessageBox.Show("No Header Data Found.");
                    return;
                }

                if (Data != null)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Inventory Date", typeof(string));
                    dt.Columns.Add("IN", typeof(string));
                    dt.Columns.Add("OUT", typeof(string));
                    dt.Columns.Add("Running Stock", typeof(string));
                    dt.Columns.Add("Remarks", typeof(string));

                    var Header = result.info;


                    partnumberlbl.Text = Header.PartNumber.ToString() ?? string.Empty;
                    partnamelbl.Text = Header.PartName.ToString() ?? string.Empty;
                    customerlbl.Text = Header.customer.ToString() ?? string.Empty;
                    endstocklbl.Text = Header.BegginingBalance.ToString() ?? string.Empty;

                    var Records = result.ledgers;

                    foreach (var item in Records)
                    {
                        dt.Rows.Add
                        (
                           item.EntryDate.ToString("MM/dd/yyyy"),
                           item.TotalIn,
                           item.TotalOut,
                           item.RunningbBalance,
                           item.Remarks
                        );
                    }


                    StockCardtable.Columns.Clear();
                    StockCardtable.DataSource = dt;
                    StockCardtable.ReadOnly = true;

                    StockCardtable.Columns["Inventory Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["IN"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["OUT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["Running Stock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    StockCardtable.Columns["Remarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PartnumberTextbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private async Task AutoFillTemplate(StockResult result, string Filepath, IProgress<int> Progress)
        {
            if (result == null || result.ledgers.Count == 0)
            { return; }

            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "SF-78-FG003_Rev.00_Stock Card.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
            {
                var ws = package.Workbook.Worksheets["Sheet1"];
                var startrow = 7;

                var Header = result.info;

                ws.Cells["B3"].Value = Header.PartNumber.ToString() ?? string.Empty;
                ws.Cells["E4"].Value = Header.BegginingBalance.ToString() ?? string.Empty;
                ws.Cells["B5"].Value = Header.PartName.ToString() ?? string.Empty;

                int current = 0;

                var Records = result.ledgers;

                foreach (var item in Records)
                {
                    current++;

                    ws.Cells[startrow, 1].Value = item.EntryDate.ToString("MM/dd/yyyy");
                    ws.Cells[startrow, 2].Value = item.TotalIn;
                    ws.Cells[startrow, 3].Value = item.TotalOut;
                    ws.Cells[startrow, 4].Value = item.RunningbBalance;
                    ws.Cells[startrow, 6].Value = item.Remarks;

                    startrow++;

                    int percent = (int)((double)current / (double)Records.Count * 100);
                    percent = Math.Min(percent, 100);
                    Progress?.Report(percent);
                    await Task.Delay(100);
                }

                package.SaveAs(new FileInfo(Filepath));
                Progress?.Report(100);
            }
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            string partnumber = partnumberlbl.Text;
            DateTime postingDate1 = PostingDate1.Value.Date;
            DateTime postingDate2 = PostingDate2.Value.Date;
            string Filename = $@"StockCard_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            var method = new TransactionRepo();
            var Data = method.GetINOUTinventory(partnumber, postingDate1, postingDate2);
            var result = Data.FirstOrDefault();  

            if (Data == null || !Data.Any())
            {
                MessageBox.Show("No Record Found.");
                return;
            }

            if (result?.info == null)
            {
                MessageBox.Show("No items to generate!", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Excel Files|*.xlsx";
                sf.Title = "Save Transfer Slip";
                sf.DefaultExt = "xlsx";
                sf.FileName = Filename;

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string filepath = sf.FileName;

                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = "Generating Stock Card...";

                    var progress = new Progress<int>(value =>
                    {
                        toolStripProgressBar1.Value = value;
                        toolStripStatusLabel1.Text = $"Generating Stock Card... {value}%";
                    });

                    try
                    {
                        await AutoFillTemplate(result, filepath, progress);
                        toolStripProgressBar1.Value = 100;
                        toolStripStatusLabel1.Text = "Generating completed successfully!";
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
                else
                {
                    MessageBox.Show("Generation canceled.");
                }
            }
        }
    }
}
