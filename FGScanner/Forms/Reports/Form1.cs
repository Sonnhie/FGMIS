using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGScanner.UI;

namespace FGScanner.Forms.Reports
{
    public partial class Form1 : Form
    {
        public Form1(UserControl userControl)
        {
            InitializeComponent();
            EnterpriseTheme.Apply(this);
            LoadControl(userControl);
        }

        private void DisplayUsercontrol(UserControl forms)
        {
            EnterpriseTheme.Apply(forms);
            panel1.Controls.Clear();
            forms.Dock = DockStyle.Fill;
            panel1.Controls.Add(forms);
        }

        private void LoadControl(UserControl forms)
        {
            DisplayUsercontrol(forms);
        }
    }
}
