using FGScanner.Forms.DataEntry;
using FGScanner.Forms.Master;
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
            //bPPSToolStripMenuItem.Visible = false;
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

        private void DisplayUsercontrol(UserControl forms)
        {
            panel1.Controls.Clear();
            forms.Dock = DockStyle.Fill;
            panel1.Controls.Add(forms);
        }

        private void LoadDashboard()
        {
            Dashboard dashboard = new();
            DisplayUsercontrol(dashboard);
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
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
            InventoryControl inventoryControl = new(_userid);
            Form1 Stock = new(inventoryControl);
            Stock.Show();
        }


        private void inventorySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new();
            DisplayUsercontrol(dashboard);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
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
            SlowMovingControl slowMovingControl = new();
            DisplayUsercontrol(slowMovingControl);
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
            ReturnListControl returnListControl = new(_userid);
            DisplayUsercontrol(returnListControl);
        }

        private void packingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShipmentReport shipmentReport = new(_userid);
            DisplayUsercontrol(shipmentReport);
        }

        private void iNOUTLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ledger ledger = new();
            DisplayUsercontrol(ledger);
        }

        private void masterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterList masterList = new(_userid);
            DisplayUsercontrol(masterList);
        }

        private void transferLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLocations changeLocations = new(_userid);
            DisplayUsercontrol(changeLocations);
        }

        private void bPPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BPPS bPPS = new(_userid);
            DisplayUsercontrol(bPPS);
        }

        private void incomingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Incoming incoming = new(_userid);
            DisplayUsercontrol(incoming);
        }

        private void outgoingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shipments shipments = new(_userid);
            DisplayUsercontrol(shipments);
        }

        private void warehouseReturnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WarehouseReturn warehouseReturn = new(_userid);
            DisplayUsercontrol(warehouseReturn);
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
