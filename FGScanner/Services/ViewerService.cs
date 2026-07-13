using FGScanner.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner.Services
{
    public class ViewerService
    {
        private readonly Queries _queries;
        public ViewerService(Queries queries)
        {
            _queries = queries; 
        }

        Dictionary<string, Button> rackButtons = new Dictionary<string, Button>();
        Dictionary<string, int> RackCountCache = new Dictionary<string, int>();
        Dictionary<string, int> LastRackIDCache = new Dictionary<string, int>();
        private readonly string[] Racks = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "EX-A", "EX-B", "EX-C", "EX-D", "EX-E", "EX-F", "EX-K", "EX-L", "2F", "FL" };
        private readonly string whId = "WH1";
        private readonly Dictionary<string, (int rows, int cols)> RackConfig = new Dictionary<string, (int rows, int cols)>()
        {
            { "A", (3,14) }, { "B", (3, 14) }, { "C", (3, 14) }, { "D", (3, 14) }, { "E", (3, 14) },
            { "F", (3, 10) }, { "G", (3, 10) }, { "H", (3, 10) }, { "I", (3, 10) }, { "J", (3, 9) },
            { "K", (3, 14) }, { "L", (3, 14) }, { "M", (3, 14) }, { "N", (3, 14) }, { "O", (3, 14) },
            { "P", (3, 14) }, { "Q", (3, 14) }, { "R", (3, 14) }, { "S", (3, 14) }, { "T", (3, 16) },
            { "EX-A", (3, 14) }, { "EX-B", (3, 14) }, { "EX-C", (3, 14) }, { "EX-D", (3, 14) }, { "EX-E", (3, 14) },
            { "EX-F", (3, 14) }, { "EX-K", (3, 14) }, { "EX-L", (3, 14) }, { "2F", (4, 14) }, { "FL", (3, 13) }
        };

        public void GenerateRackView(string RackID, FlowLayoutPanel flowLayoutPanel1, EventArgs e)
        {
            var config = RackConfig.TryGetValue(RackID, out (int rows, int cols) value) ? value : (3, 7);
            int rackRows = config.Item1;
            int rackColumns = config.Item2;

            int buttonWidth = 80;
            int buttonHeight = 40;
            int spacing = 2;

            int RackLabelIdentifiation1 = 0;
            int RackLabelIdentifiation2 = 0;

            Panel rackPanel = new Panel
            {
                Width = (rackColumns + 1) * (buttonWidth + spacing),
                Height = rackRows * (buttonHeight + spacing),
                Margin = new Padding(10),
                Tag = RackID,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Rack title label
            Label rackTitle = new Label
            {
                Text = RackID,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Width = buttonWidth,
                Height = rackPanel.Height,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };

            rackPanel.Controls.Add(rackTitle);

            for (int row = 0; row < rackRows; row++)
            {
                RackLabelIdentifiation1++;
                RackLabelIdentifiation2 = 0;

                for (int col = 1; col <= rackColumns; col++)
                {
                    RackLabelIdentifiation2++;

                    string RackLabel = $"{RackID}{RackLabelIdentifiation1}-{RackLabelIdentifiation2:D2}";

                    Button btn = new Button
                    {
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Left = col * (buttonWidth + spacing),
                        Top = row * (buttonHeight + spacing),
                        Text = RackLabel,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        BackColor = Color.White,
                        ForeColor = Color.Black
                    };

                    //btn.Click += e;

                    rackPanel.Controls.Add(btn);
                    rackButtons[RackLabel] = btn;
                }
            }

            flowLayoutPanel1.Controls.Add(rackPanel);
        }

        private void Buttom_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            //string location = clickedButton.Text;

            //Loadtransactionlogs(location);
            //LblRack.Text = location;
            //// MessageBox.Show($"You clicked: {clickedButton.Text}");

        }
    }
}
