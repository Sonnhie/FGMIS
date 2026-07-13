using FGScanner.Forms.DataEntry;
using FGScanner.Forms.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class MainForm : Form
    {
        private string _TransactionType = string.Empty;
        private readonly string _userid = string.Empty;
        private readonly int usergroup = 0;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HTCAPTION = 0x2;

        public MainForm(string userid, int usergroup)
        {
            InitializeComponent();
            LoadDashboard();
            this._userid = userid;
            this.usergroup = usergroup;
            LblUser.Text = userid;
            AccessControlFeatures(usergroup);

        }

        private void AccessControlFeatures(int usergroup)
        {
            if (usergroup != 2)
            {
     
                rackViewerToolStripMenuItem.Enabled = false;
                warehouseToolStripMenuItem.Enabled = false;
                packingListToolStripMenuItem.Enabled = false;
                iNOUTLedgerToolStripMenuItem.Enabled = false;
                dataEntryToolStripMenuItem.Enabled = false;
            }
        }

        private void DisplayDateTime()
        {
            DateTime Today = DateTime.Now;
            string Time = DateTime.Now.ToString("HH:mm:ss");
            DateLbl.Text = Today.ToString("MM/dd/yyy");
            TimeLbl.Text = Time;
        }

        private void DisplayForm(Form forms)
        {
            panel1.Controls.Clear();
            forms.TopLevel = false;
            forms.FormBorderStyle = FormBorderStyle.None;
            forms.Dock = DockStyle.Fill;
            
            panel1.Controls.Add(forms);
            forms.Show();
        }

        private void LoadDashboard()
        {
            //Report report = new Report();
            //DisplayForm(report);
        }

        private void iNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_TransactionType = "IN";
            //WHDataEntryIN DataEntryIn = new WHDataEntryIN(_TransactionType, _userid);
            //DisplayForm(DataEntryIn);
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void rackViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to back to login?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                login login = new login();
                login.Show();
                this.Hide();
            }
        }

        private void stockListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryForm inventory = new InventoryForm(_userid);
            DisplayForm(inventory);
        }


        private void inventorySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Report report = new Report();
            //DisplayForm(report);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayDateTime();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void slowMovingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Slowmoving form = new Slowmoving();
            DisplayForm(form);
        }

        private void warehouseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string warehouse = "Warehouse";
            var form = Viewer.GetInstance(warehouse, _userid);

            form.Show();
            form.BringToFront();
        }

        private void ecozoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string warehouse = "Ecozone";
            var form = Viewer.GetInstance(warehouse, _userid);

            form.Show();
            form.BringToFront();
        }

        private void warehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WithdrawalSlips ws = new WithdrawalSlips(_userid);
            DisplayForm(ws);
        }

        private void packingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PackingList packingList = new PackingList();
            //DisplayForm(packingList);

            Shipment shipment = new Shipment(_userid);
            DisplayForm(shipment);
        }

        private void iNOUTLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockCard stockCard = new StockCard();
            DisplayForm(stockCard);
        }

        private void dataEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void masterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = new Product(_userid);
            DisplayForm(product);
        }

        private void transferLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransferLocation transferLocation = new TransferLocation(_userid);
            DisplayForm(transferLocation);
        }

        private void bPPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BPPSEntry bpps = new BPPSEntry(_userid);
            DisplayForm(bpps);
        }

        private void incomingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FGEntry fGEntry = new FGEntry(_userid);
            DisplayForm(fGEntry);
        }

        private void outgoingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Outgoing outgoing = new Outgoing(_userid);
            DisplayForm(outgoing);
        }

        private void warehouseReturnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Return @return = new Return(_userid);
            DisplayForm(@return);
        }
    }
}
