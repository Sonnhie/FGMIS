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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void BtnWarehousingArea_Click(object sender, EventArgs e)
        {
            warehousemenu wm = new warehousemenu();
            wm.Show();
            this.Hide();
        }
    }
}
