using System;
using System.Drawing;
using System.Windows.Forms;

namespace FGScanner
{
    public partial class SplashScreen : Form
    {
        private readonly Label _percentLabel;

        public SplashScreen()
        {
            InitializeComponent();

            DoubleBuffered = true;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(720, 460);
            pictureBox1.BackColor = Color.FromArgb(15, 23, 42);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            Panel statusPanel = new()
            {
                Dock = DockStyle.Bottom,
                Height = 100,
                BackColor = Color.FromArgb(15, 23, 42)
            };

            Label productLabel = new()
            {
                AutoSize = false,
                Location = new Point(28, 13),
                Size = new Size(560, 25),
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Text = "FGIMS  •  Finished Goods Inventory Management"
            };

            progresslabel.Parent = statusPanel;
            progresslabel.AutoSize = false;
            progresslabel.Location = new Point(28, 42);
            progresslabel.Size = new Size(610, 20);
            progresslabel.Font = new Font("Segoe UI", 9F);
            progresslabel.ForeColor = Color.FromArgb(203, 213, 225);
            progresslabel.TextAlign = ContentAlignment.MiddleLeft;

            metroProgressBar1.Parent = statusPanel;
            metroProgressBar1.Location = new Point(28, 72);
            metroProgressBar1.Size = new Size(664, 7);
            metroProgressBar1.Style = MetroFramework.MetroColorStyle.Blue;

            _percentLabel = new Label
            {
                AutoSize = false,
                Location = new Point(638, 42),
                Size = new Size(54, 20),
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleRight,
                Text = "0%"
            };

            statusPanel.Controls.Add(productLabel);
            statusPanel.Controls.Add(progresslabel);
            statusPanel.Controls.Add(_percentLabel);
            statusPanel.Controls.Add(metroProgressBar1);
            Controls.Add(statusPanel);
            statusPanel.BringToFront();
        }

        public void UpdateProgress(int percent, string message)
        {
            percent = Math.Max(metroProgressBar1.Minimum, Math.Min(metroProgressBar1.Maximum, percent));
            metroProgressBar1.Value = percent;
            progresslabel.Text = message;
            _percentLabel.Text = $"{percent}%";
            Application.DoEvents();
        }
    }
}
