using FGScanner.Model;
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
    public partial class warehousemenu : Form
    {
        public warehousemenu()
        {
            InitializeComponent();
        }

        private void BtnDataEntryIn_Click(object sender, EventArgs e)
        {
            string TransactionType = "IN";
            WHDataEntryIN wh = new WHDataEntryIN(TransactionType);
            wh.Show();
            this.Hide();
        }

        private void BtnDataEntryOut_Click(object sender, EventArgs e)
        {
            string TransactionType = "OUT";
            WH_OUT whout = new WH_OUT(TransactionType);
            whout.Show();
            this.Hide();
        }

        private void BtnBacktoLogin_Click(object sender, EventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Hide();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            //WHDataEntry_Ship_OUT_ viewer = new WHDataEntry_Ship_OUT_();
            //viewer.Show();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


    }
}
