using FGScanner.Database;
using FGScanner.Models;
using FGScanner.Repositories;
using FGScanner.Services;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FGScanner.Forms.Reports
{
    public partial class ReturnListControl : UserControl
    {
        private readonly TransactionService _service;
        private readonly Queries _queries;
        private readonly InventoryDbContext _dbContext;
        private readonly ExcelService _excelService;
        private string _userid;
        private readonly PrintService _printService;
        private string controlnumber;
        private PrintDocumentDTO _documentToPrint;

        public ReturnListControl(string userid)
        {
            InitializeComponent();
            _userid = userid;
            _dbContext = new();
            _queries = new(_dbContext);
            _service = new(_queries);
            _excelService = new(_queries);
            _printService = new(_queries);
            GenerateSlipbutton.Enabled = false;
            CancelReturnButton.Enabled = false;
        }

        private void LoadReturnTable(List<ReturnTable> data)
        {
            try
            {
                if (data.Count != 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Return Control Number", typeof(string));
                    dt.Columns.Add("Return Date", typeof(string));
                    dt.Columns.Add("Return Time", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Total Box", typeof(string));
                    dt.Columns.Add("Transfer To", typeof(string));
                    dt.Columns.Add("Status", typeof(string));
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            item.TransactionId.ToString(),
                            item.EntryDate.ToString("MM/dd/yyyy"),
                            item.EntryDate.ToString("hh:mm:ss tt"),
                            item.Quantity.ToString(),
                            item.Box.ToString(),
                            item.ToLocation,
                            item.Status
                        );
                    }
                    ReturnTable.Columns.Clear();
                    ReturnTable.DataSource = dt;

                    ReturnTable.ReadOnly = true;



                    ReturnTable.Columns["Return Control Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ReturnTable.Columns["Return Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Return Time"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Transfer To"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnTable.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    DataGridViewButtonColumn btnCol = new()
                    {
                        Name = "Actionbuttons",
                        HeaderText = "",
                        Text = "View Items",
                        UseColumnTextForButtonValue = true
                    };

                    ReturnTable.Columns.Add(btnCol);
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    ReturnTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }
        private async Task LoadReturnItemTable(string controlnumber)
        {
            try
            {
                var result = await _service.LoadReturnItems(controlnumber);

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
                    ReturnItemTable.Columns.Clear();
                    ReturnItemTable.DataSource = dt;

                    ReturnItemTable.ReadOnly = true;

                    ReturnItemTable.Columns["Part number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    ReturnItemTable.Columns["Production Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnItemTable.Columns["Production Version"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnItemTable.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    ReturnItemTable.Columns["Total Box"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    ReturnTable.Text = totalQuantity.ToString("N0");
                    returnBoxLabel.Text = totalBox.ToString();
                    GenerateSlipbutton.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No inventory uploaded.");
                    ReturnItemTable.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }

        private async void ReturnTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ReturnTable.Columns[e.ColumnIndex].Name == "Actionbuttons")
            {
                DataGridViewRow selectedRow = ReturnTable.Rows[e.RowIndex];

                if (selectedRow == null)
                {
                    MessageBox.Show("Empty row data.");
                    return;
                }

                controlnumber = selectedRow.Cells["Return Control Number"].Value.ToString();
                string Returndate = selectedRow.Cells["Return Date"].Value.ToString();
                string Returntime = selectedRow.Cells["Return Time"].Value.ToString();
                string status = selectedRow.Cells["Status"].Value.ToString();
                string to = selectedRow.Cells["Transfer To"].Value.ToString();
                string qty = selectedRow.Cells["Quantity"].Value.ToString();


                if (status == "Cancelled Return")
                {
                    CancelReturnButton.Enabled = false;
                }
                else
                {
                    CancelReturnButton.Enabled = true;
                }


                ReturnDate.Text = Returndate;
                ReturnTimeLabel.Text = Returntime;
                ReturnIDLabel.Text = controlnumber;
                ReturnQuantityLabel.Text = qty;
                TransferLabel.Text = to;
                await LoadReturnItemTable(controlnumber);
            }
        }

        private async void GenerateSlipbutton_Click(object sender, EventArgs e)
        {
            string returnId = ReturnIDLabel.Text;
            if (string.IsNullOrWhiteSpace(returnId))
            {
                MessageBox.Show("Invalid document number.");
                return;
            }

            var result = await _service.getItemsByReturns(returnId);
            if (result == null)
            {
                MessageBox.Show($"No data found in transaction id {returnId}");
                return;
            }



            _documentToPrint = result
                .GroupBy(docgroup => docgroup.ControlNumber)
                .Select(docgroup => new PrintDocumentDTO
                {
                    DocNo = docgroup.Key,
                    EntryDate = docgroup.Max(x => x.EntryDate),
                    PreparedBy = docgroup.First().InCharge,
                    FromLocation = docgroup.First().ReturnTable.FromLocation,
                    ToLocation = docgroup.First().ReturnTable.ToLocation,
                    Items = [.. docgroup
                        .GroupBy(item => new { item.Partnumber, item.ProdDate })
                        .Select(itemgroup => new PrintItemDTO
                        {
                            PartNumber = itemgroup.Key.Partnumber,
                            ProductionDate = itemgroup.Key.ProdDate,
                            PartName = _queries.GetProductPartName(itemgroup.Key.Partnumber),
                            PPS = _queries.GetProductPPS(itemgroup.Key.Partnumber),
                            Quantity = itemgroup.Sum(x => x.Quantity),
                            Box = itemgroup.Sum(x => x.Box) ?? 0,
                            remarks = itemgroup.FirstOrDefault().Remarks
                        })]
                }).FirstOrDefault();


            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            printDocument1.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                Document = printDocument1,
                Width = 500,
                Height = 500
            };
            printPreviewDialog.PrintPreviewControl.Columns = 1;
            printPreviewDialog.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            _printService.PrintTransferSlip(_documentToPrint, e);
        }

        private async void CancelReturnButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to cancel this return?", "Cancel Return");

                if (result == DialogResult.OK)
                {
                    var (isSuccess, Message) = await _service.CancelReturn(controlnumber, _userid);

                    if (isSuccess)
                    {
                        MessageBox.Show(Message);
                        ReturnTable.Refresh();
                        ReturnItemTable.Refresh();
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

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string location = TransferTocomboBox.Text;
                //  string warehouseid = warehouseComboBox.Text;
                DateTime? startDate = StartDate.Value.Date;
                DateTime? endDate = EndDate.Value.Date;

                if (location == null)
                {
                    MessageBox.Show("Please select storage location.");
                    return;
                }

                var result = await _service.GetReturnList(location, startDate, endDate);
                if (result == null)
                {
                    MessageBox.Show("No Data found.");
                    return;
                }
                LoadReturnTable(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
