namespace FGScanner.Forms.Viewer
{
    partial class EcozoneMainViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new System.Windows.Forms.Panel();
            button2 = new System.Windows.Forms.Button();
            label10 = new System.Windows.Forms.Label();
            RackDataGridView = new System.Windows.Forms.DataGridView();
            total_box_lbl = new System.Windows.Forms.Label();
            total_sum = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            LblRack = new System.Windows.Forms.Label();
            TxtPartnumber = new System.Windows.Forms.TextBox();
            ListGrid = new System.Windows.Forms.DataGridView();
            label2 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel3 = new System.Windows.Forms.Panel();
            label13 = new System.Windows.Forms.Label();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            timer1 = new System.Windows.Forms.Timer(components);
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDialog1 = new System.Windows.Forms.PrintDialog();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RackDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ListGrid).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(RackDataGridView);
            panel1.Controls.Add(total_box_lbl);
            panel1.Controls.Add(total_sum);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(LblRack);
            panel1.Controls.Add(TxtPartnumber);
            panel1.Controls.Add(ListGrid);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(500, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(554, 657);
            panel1.TabIndex = 8;
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button2.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button2.Location = new System.Drawing.Point(420, 542);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(113, 46);
            button2.TabIndex = 34;
            button2.Text = "Generate Ledger";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label10
            // 
            label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            label10.Location = new System.Drawing.Point(23, 290);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(78, 19);
            label10.TabIndex = 32;
            label10.Text = "Rack Details:";
            // 
            // RackDataGridView
            // 
            RackDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RackDataGridView.BackgroundColor = System.Drawing.Color.White;
            RackDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RackDataGridView.Location = new System.Drawing.Point(23, 320);
            RackDataGridView.Name = "RackDataGridView";
            RackDataGridView.RowHeadersWidth = 51;
            RackDataGridView.Size = new System.Drawing.Size(511, 214);
            RackDataGridView.TabIndex = 31;
            // 
            // total_box_lbl
            // 
            total_box_lbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            total_box_lbl.AutoSize = true;
            total_box_lbl.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Bold);
            total_box_lbl.Location = new System.Drawing.Point(22, 542);
            total_box_lbl.Name = "total_box_lbl";
            total_box_lbl.Size = new System.Drawing.Size(55, 18);
            total_box_lbl.TabIndex = 30;
            total_box_lbl.Text = "Total Box:";
            // 
            // total_sum
            // 
            total_sum.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            total_sum.AutoSize = true;
            total_sum.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Bold);
            total_sum.Location = new System.Drawing.Point(22, 569);
            total_sum.Name = "total_sum";
            total_sum.Size = new System.Drawing.Size(78, 18);
            total_sum.TabIndex = 29;
            total_sum.Text = "Total Quantity:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Gold;
            label5.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F);
            label5.ForeColor = System.Drawing.Color.Black;
            label5.Location = new System.Drawing.Point(249, 44);
            label5.Name = "label5";
            label5.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            label5.Size = new System.Drawing.Size(89, 28);
            label5.TabIndex = 28;
            label5.Text = "OTHER CUSTOMER";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.SkyBlue;
            label4.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F);
            label4.ForeColor = System.Drawing.Color.Black;
            label4.Location = new System.Drawing.Point(197, 44);
            label4.Name = "label4";
            label4.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            label4.Size = new System.Drawing.Size(34, 28);
            label4.TabIndex = 27;
            label4.Text = "BIPH";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.LightGreen;
            label3.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F);
            label3.ForeColor = System.Drawing.Color.Black;
            label3.Location = new System.Drawing.Point(149, 44);
            label3.Name = "label3";
            label3.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            label3.Size = new System.Drawing.Size(34, 28);
            label3.TabIndex = 26;
            label3.Text = "EPPI";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.MediumPurple;
            label8.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F);
            label8.ForeColor = System.Drawing.Color.Black;
            label8.Location = new System.Drawing.Point(89, 44);
            label8.Name = "label8";
            label8.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            label8.Size = new System.Drawing.Size(44, 28);
            label8.TabIndex = 25;
            label8.Text = "YAZAKI";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.White;
            label7.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F);
            label7.ForeColor = System.Drawing.Color.Black;
            label7.Location = new System.Drawing.Point(19, 44);
            label7.Name = "label7";
            label7.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            label7.Size = new System.Drawing.Size(55, 28);
            label7.TabIndex = 24;
            label7.Text = "Available";
            // 
            // LblRack
            // 
            LblRack.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            LblRack.AutoSize = true;
            LblRack.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            LblRack.Location = new System.Drawing.Point(114, 290);
            LblRack.Name = "LblRack";
            LblRack.Size = new System.Drawing.Size(27, 19);
            LblRack.TabIndex = 15;
            LblRack.Text = "---";
            // 
            // TxtPartnumber
            // 
            TxtPartnumber.Location = new System.Drawing.Point(149, 103);
            TxtPartnumber.Name = "TxtPartnumber";
            TxtPartnumber.Size = new System.Drawing.Size(160, 22);
            TxtPartnumber.TabIndex = 13;
            TxtPartnumber.TextChanged += TxtPartnumber_TextChanged;
            // 
            // ListGrid
            // 
            ListGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ListGrid.BackgroundColor = System.Drawing.Color.White;
            ListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListGrid.Location = new System.Drawing.Point(22, 134);
            ListGrid.Name = "ListGrid";
            ListGrid.RowHeadersWidth = 51;
            ListGrid.Size = new System.Drawing.Size(511, 123);
            ListGrid.TabIndex = 12;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(17, 1108);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(107, 21);
            label2.TabIndex = 11;
            label2.Text = "Rack Details:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label6.Location = new System.Drawing.Point(17, 11);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(55, 19);
            label6.TabIndex = 7;
            label6.Text = "Legends:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(22, 106);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(119, 19);
            label1.TabIndex = 0;
            label1.Text = "Search Part number:";
            // 
            // panel3
            // 
            panel3.Controls.Add(label13);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(0, 0);
            panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(500, 45);
            panel3.TabIndex = 9;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label13.Location = new System.Drawing.Point(254, 11);
            label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(115, 21);
            label13.TabIndex = 0;
            label13.Text = "RACK VIEWER";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 45);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(12);
            flowLayoutPanel1.Size = new System.Drawing.Size(500, 612);
            flowLayoutPanel1.TabIndex = 10;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // printDocument1
            // 
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // EcozoneMainViewer
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            Name = "EcozoneMainViewer";
            Size = new System.Drawing.Size(1054, 657);
            Load += EcozoneMainViewer_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RackDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)ListGrid).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label total_box_lbl;
        private System.Windows.Forms.Label total_sum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblRack;
        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.DataGridView ListGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView RackDataGridView;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}
