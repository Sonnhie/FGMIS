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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int percent, string message)
        {
            //if (InvokeRequired)
            //{
            //    Invoke(new Action(() => UpdateProgress(percent, message)));
            //    return;
            //}

            metroProgressBar1.Value = percent;
            progresslabel.Parent = pictureBox1;
            progresslabel.Text = message;

            Application.DoEvents();
        }
    }
}
