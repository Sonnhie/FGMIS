namespace FGScanner
{
    partial class EcozoneViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label11 = new System.Windows.Forms.Label();
            RackDataGridView = new System.Windows.Forms.DataGridView();
            label6 = new System.Windows.Forms.Label();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            panel3 = new System.Windows.Forms.Panel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            total_box_lbl = new System.Windows.Forms.Label();
            total_sum = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            LblRack = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            TxtPartnumber = new System.Windows.Forms.TextBox();
            ListGrid = new System.Windows.Forms.DataGridView();
            label2 = new System.Windows.Forms.Label();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)RackDataGridView).BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ListGrid).BeginInit();
            SuspendLayout();
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label11.Location = new System.Drawing.Point(400, 15);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(115, 21);
            label11.TabIndex = 0;
            label11.Text = "RACK VIEWER";
            // 
            // RackDataGridView
            // 
            RackDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RackDataGridView.BackgroundColor = System.Drawing.Color.White;
            RackDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RackDataGridView.Location = new System.Drawing.Point(29, 623);
            RackDataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            RackDataGridView.Name = "RackDataGridView";
            RackDataGridView.RowHeadersWidth = 51;
            RackDataGridView.Size = new System.Drawing.Size(512, 362);
            RackDataGridView.TabIndex = 0;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label6.Location = new System.Drawing.Point(24, 12);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(77, 21);
            label6.TabIndex = 7;
            label6.Text = "Legends:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 59);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(12, 12, 12, 12);
            flowLayoutPanel1.Size = new System.Drawing.Size(1234, 1009);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // panel3
            // 
            panel3.Controls.Add(label11);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(0, 0);
            panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(1234, 59);
            panel3.TabIndex = 8;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Location = new System.Drawing.Point(0, 1068);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.ShowItemToolTips = true;
            statusStrip1.Size = new System.Drawing.Size(1815, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(31, 114);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(165, 21);
            label1.TabIndex = 0;
            label1.Text = "Search Part number:";
            // 
            // panel1
            // 
            panel1.Controls.Add(total_box_lbl);
            panel1.Controls.Add(total_sum);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(LblRack);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(TxtPartnumber);
            panel1.Controls.Add(ListGrid);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(RackDataGridView);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(1234, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(581, 1068);
            panel1.TabIndex = 7;
            // 
            // total_box_lbl
            // 
            total_box_lbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            total_box_lbl.AutoSize = true;
            total_box_lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            total_box_lbl.Location = new System.Drawing.Point(26, 992);
            total_box_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            total_box_lbl.Name = "total_box_lbl";
            total_box_lbl.Size = new System.Drawing.Size(67, 17);
            total_box_lbl.TabIndex = 30;
            total_box_lbl.Text = "Total Box:";
            // 
            // total_sum
            // 
            total_sum.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            total_sum.AutoSize = true;
            total_sum.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            total_sum.Location = new System.Drawing.Point(196, 992);
            total_sum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            total_sum.Name = "total_sum";
            total_sum.Size = new System.Drawing.Size(97, 17);
            total_sum.TabIndex = 29;
            total_sum.Text = "Total Quantity:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Gold;
            label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label5.ForeColor = System.Drawing.Color.Black;
            label5.Location = new System.Drawing.Point(349, 47);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            label5.Size = new System.Drawing.Size(125, 27);
            label5.TabIndex = 28;
            label5.Text = "OTHER CUSTOMER";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.SkyBlue;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.Black;
            label4.Location = new System.Drawing.Point(276, 47);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            label4.Size = new System.Drawing.Size(47, 27);
            label4.TabIndex = 27;
            label4.Text = "BIPH";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.LightGreen;
            label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label3.ForeColor = System.Drawing.Color.Black;
            label3.Location = new System.Drawing.Point(209, 47);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            label3.Size = new System.Drawing.Size(43, 27);
            label3.TabIndex = 26;
            label3.Text = "EPPI";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.MediumPurple;
            label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label8.ForeColor = System.Drawing.Color.Black;
            label8.Location = new System.Drawing.Point(124, 47);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            label8.Size = new System.Drawing.Size(60, 27);
            label8.TabIndex = 25;
            label8.Text = "YAZAKI";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.White;
            label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label7.ForeColor = System.Drawing.Color.Black;
            label7.Location = new System.Drawing.Point(26, 47);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            label7.Size = new System.Drawing.Size(69, 27);
            label7.TabIndex = 24;
            label7.Text = "Available";
            // 
            // LblRack
            // 
            LblRack.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            LblRack.AutoSize = true;
            LblRack.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            LblRack.Location = new System.Drawing.Point(155, 590);
            LblRack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LblRack.Name = "LblRack";
            LblRack.Size = new System.Drawing.Size(22, 15);
            LblRack.TabIndex = 15;
            LblRack.Text = "---";
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button1.Location = new System.Drawing.Point(428, 992);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(113, 46);
            button1.TabIndex = 14;
            button1.Text = "Generate Ledger";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // TxtPartnumber
            // 
            TxtPartnumber.Location = new System.Drawing.Point(231, 114);
            TxtPartnumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TxtPartnumber.Name = "TxtPartnumber";
            TxtPartnumber.Size = new System.Drawing.Size(223, 23);
            TxtPartnumber.TabIndex = 13;
            TxtPartnumber.TextChanged += TxtPartnumber_TextChanged_1;
            // 
            // ListGrid
            // 
            ListGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ListGrid.BackgroundColor = System.Drawing.Color.White;
            ListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListGrid.Location = new System.Drawing.Point(36, 165);
            ListGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ListGrid.Name = "ListGrid";
            ListGrid.RowHeadersWidth = 51;
            ListGrid.Size = new System.Drawing.Size(505, 385);
            ListGrid.TabIndex = 12;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(24, 583);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(107, 21);
            label2.TabIndex = 11;
            label2.Text = "Rack Details:";
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
            // EcozoneViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1815, 1090);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "EcozoneViewer";
            Text = "EcozoneViewer";
            Load += EcozoneViewer_Load;
            ((System.ComponentModel.ISupportInitialize)RackDataGridView).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ListGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView RackDataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView ListGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.Button button1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label LblRack;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label total_box_lbl;
        private System.Windows.Forms.Label total_sum;
    }
}