using FGScanner.Model;
using FGScanner.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class PackingList : Form
    {

        private int page = 1;
        private int pageSize = 50;
        private int totalRecords = 0;
     
        public PackingList()
        {
            InitializeComponent();
            progressBar.Visible = false;
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel1.Text = "";
            TxtDocNumber.CharacterCasing = CharacterCasing.Upper;
        }

        private void LoadData()
        {
            Filtereddata();
        }

        private void Filtereddata()
        {
            try
            {
                string docnum = TxtDocNumber.Text;
                DateTime from = PostingDate1.Value.Date;
                DateTime to = PostingDate2.Value.Date;

                var Repo = new TransactionRepo();
                var Data = Repo.GetPackingListData(docnum, from, to, page, pageSize);
                var TotalRows = Repo.GetTotalPackingTableRows(docnum, from, to);
                totalRecords = (int)Math.Ceiling((double)TotalRows / pageSize);

                if (Data != null)
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Transaction ID", typeof(string));
                    dt.Columns.Add("Posting Date", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Total Quantity", typeof(string));

                    LblPage.Text = $"Page {page} of {totalRecords}";

                    foreach (var item in Data)
                    {
                        dt.Rows.Add
                        (
                           item.transaction_id,
                           item.posting_date,
                           item.total_box,
                           item.quantity
                        );
                    }


                    PackinglistTable.Columns.Clear();
                    PackinglistTable.DataSource = dt;
                    PackinglistTable.ReadOnly = true;

                    PackinglistTable.Columns["Transaction ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    PackinglistTable.Columns["Posting Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    PackinglistTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    PackinglistTable.Columns["Total Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                    {
                        HeaderText = "Action",
                        Text = "Generate document",
                        Name = "btngen",
                        UseColumnTextForButtonValue = true,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    PackinglistTable.Columns.Add(btn);
                    btn.DisplayIndex = PackinglistTable.Columns.Count - 1;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PackingList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            string postingDate1 = PostingDate1.Value.ToString("yyyyMMdd");
            string postingDate2 = PostingDate2.Value.ToString("yyyyMMdd");
            string FileName = $"PackingList_{postingDate1}-{postingDate2}.xlsx";

            using (SaveFileDialog Save = new SaveFileDialog())
            {
                Save.Filter = "Excel files (*.xlsx)|*.xlsx";
                Save.Title = "Save Exported Data";
                Save.DefaultExt = "xlsx";
                Save.FileName = FileName;

                if (Save.ShowDialog() == DialogResult.OK)
                {
                    string docnum = TxtDocNumber.Text;
                    DateTime from = PostingDate1.Value.Date;
                    DateTime to = PostingDate2.Value.Date;
                  

                    string filepath = Save.FileName;
                    var Repo = new TransactionRepo();
                    DataTable data = Repo.GetPackingListExport(docnum, from, to);

                    progressBar.Value = 0;
                    progressBar.Visible = true;
                    toolStripStatusLabel1.Visible = true;
                    toolStripStatusLabel1.Text = $"Exporting...";

                    var progress = new Progress<int>(value =>
                    {
                        progressBar.Value = value;
                        toolStripStatusLabel1.Text = $"Exporting... {value}%";
                    });

                    var columnMap = new Dictionary<string, string>
                    {
                        { "transaction_id", "Document No" },
                        { "entry_date", "Issue Date" },
                        { "total_box", "Total Box" },
                        { "total_quantity", "Total Quantity" }
                    };

                    try
                    {
                        await ExportService.ExportCSV(data, columnMap, filepath, progress, "Packing lists");
                        progressBar.Value = 100;
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
                        progressBar.Value = 0;
                        progressBar.Visible = false;
                        toolStripStatusLabel1.Text = "";
                    }
                }
            }
        }

        private void PostingDate1_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void PostingDate2_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void TxtDocNumber_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void Returntable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == PackinglistTable.Columns["btngen"].Index)
            {
                string documentNo = PackinglistTable.Rows[e.RowIndex].Cells["Transaction ID"].Value.ToString();
                string Filename = $@"PackingList_{documentNo}.xlsx";
                var Repo = new TransactionRepo();
                List<OrdersSummary> order = Repo.GetPackinglist(documentNo);

                if (order.Count == 0)
                {
                    MessageBox.Show("No items to generate!", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "Excel Files|*.xlsx";
                    sf.Title = "Save Packing List";
                    sf.DefaultExt = "xlsx";
                    sf.FileName = Filename;

                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string filepath = sf.FileName;

                        if (order.Count == 0)
                        {
                            MessageBox.Show("No Data Found.");
                            return;
                        }

                        progressBar.Value = 0;
                        progressBar.Visible = true;
                        toolStripStatusLabel1.Text = "Generating packing list...";

                        var progress = new Progress<int>(value =>
                        {
                            progressBar.Value = value;
                            toolStripStatusLabel1.Text = $"Generating packing list... {value}%";
                        });

                        try
                        {
                            await AutofillTemplate(order, filepath, progress);
                            progressBar.Value = 100;
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
                            progressBar.Value = 0;
                            progressBar.Visible = false;
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

        private async Task AutofillTemplate(List<OrdersSummary> orders, string Filepath, IProgress<int> Progress)
        {
            if (orders == null || orders.Count == 0)
            {
                return;
            }

            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");


            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "packinglist.xlsx");
            //string savePath = $@"ShipmentReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
            {
                var ws = package.Workbook.Worksheets["LOT DATE"];
                var startrow = 10;
                DateTime today = DateTime.Now;
                string date = today.ToString("MM/dd/yyyy");
                string time = today.ToString("HH:mm:ss");

                ws.Cells["C6"].Value = date;
                ws.Cells["C7"].Value = time;
                ws.Cells["J6"].Value = orders[0].Customer;
                ws.Cells["J7"].Value = orders[0].TransactionId;

                int current = 0;

                foreach (var item in orders)
                {
                    current++;
                    int PPS = item.Quantity / item.Box;

                    ws.Cells[startrow, 2].Value = item.Partnumber;
                    ws.Cells[startrow, 4].Value = item.ProductionDate.ToString("MM/dd/yyyy");
                    ws.Cells[startrow, 5].Value = item.Box;
                    ws.Cells[startrow, 6].Value = PPS;
                    ws.Cells[startrow, 7].Value = item.Quantity;
                    startrow++;

                    int percent = (int)((double)current / (double)orders.Count * 100);
                    percent = Math.Min(percent, 100);
                    Progress?.Report(percent);
                    await Task.Delay(100);
                }

                package.SaveAs(new FileInfo(Filepath));
                Progress?.Report(100);
            }
        }
    }
}
