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
        private readonly string userid = string.Empty;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HTCAPTION = 0x2;

        public MainForm(string userid)
        {
            InitializeComponent();
            LoadDashboard();
            this.userid = userid;
            LblUser.Text = userid;
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
            forms.Dock = DockStyle.Fill;
            panel1.Controls.Add(forms);
            forms.Show();
        }

        private void LoadDashboard()
        {
            Report report = new Report();
            DisplayForm(report);
        }

        private void iNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TransactionType = "IN";
            WHDataEntryIN DataEntryIn = new WHDataEntryIN(_TransactionType);
            DisplayForm(DataEntryIn);
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void oUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TransactionType = "OUT";
            WH_OUT DataEntryOut = new WH_OUT(_TransactionType);
            DisplayForm(DataEntryOut);
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
            InventoryForm inventory = new InventoryForm();
            DisplayForm(inventory);
        }

        private void cHANGELOCATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLocation changeLocation = new ChangeLocation();
            DisplayForm(changeLocation);
        }

        private void inventorySummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            DisplayForm(report);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void warehouseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseReturn wr = new WarehouseReturn(userid);
            DisplayForm(wr);
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
            var form = Viewer.GetInstance(warehouse,userid);

            form.Show();
            form.BringToFront();
        }

        private void ecozoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string warehouse = "Ecozone";
            var form = Viewer.GetInstance(warehouse, userid);

            form.Show();
            form.BringToFront();
        }

        private void warehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WHRForm wr = new WHRForm();
            DisplayForm(wr);
        }

        private void packingListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
