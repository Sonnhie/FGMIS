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
    public partial class Viewer : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HTCAPTION = 0x2;

        private static Viewer _instance;

        private string _warehouseName;
        private readonly string _userid;
        public Viewer(string warehouse, string userid)
        {
            InitializeComponent();
            LoadViewer(warehouse, userid);
            _warehouseName = warehouse;
            _userid = userid;
            label5.Text = _userid;
            DisplayDateTime();
        }
        private void DisplayDateTime()
        {
            DateTime Today = DateTime.Now;
            string Time = DateTime.Now.ToString("HH:mm:ss");
            DateLbl.Text = Today.ToString("MM/dd/yyy");
            TimeLbl.Text = Time;
        }
        public static Viewer GetInstance(string warehouse, string userid)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new Viewer(warehouse, userid);
            }
            return _instance;
        }

        private void DisplayForm(Form forms)
        {
            panel1.Controls.Clear();
            forms.TopLevel = false;
            forms.Dock = DockStyle.Fill;
            panel1.Controls.Add(forms);
            forms.Show();
        }

        private void LoadViewer(string warehouseName, string userid) 
        {
            if (warehouseName == "Ecozone")
            {
                EcozoneViewer ecozoneViewer = new EcozoneViewer(userid);
                DisplayForm(ecozoneViewer);
            }
            else if(warehouseName == "Warehouse")
            {
                WHDataEntry_Ship_OUT_ Viewer = new WHDataEntry_Ship_OUT_(userid);
                DisplayForm(Viewer);
            }       
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void MaxBtn_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_GETMINMAXINFO = 0x24;

            if (m.Msg == WM_GETMINMAXINFO)
            {
                this.MaximumSize = Screen.FromHandle(this.Handle).WorkingArea.Size;
            }

            base.WndProc(ref m);
        }

        private void minimizedBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
