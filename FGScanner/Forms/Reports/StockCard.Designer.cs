namespace FGScanner
{
    partial class StockCard
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            label8 = new System.Windows.Forms.Label();
            warehouseidcmb = new System.Windows.Forms.ComboBox();
            label7 = new System.Windows.Forms.Label();
            ProdVerComboButton = new System.Windows.Forms.ComboBox();
            SearchBtn = new System.Windows.Forms.Button();
            PostingDate2 = new System.Windows.Forms.DateTimePicker();
            PostingDate1 = new System.Windows.Forms.DateTimePicker();
            PartnumberTextbox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            StockCardtable = new System.Windows.Forms.DataGridView();
            total_sum = new System.Windows.Forms.Label();
            BtnExport = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            endstocklbl = new System.Windows.Forms.Label();
            customerlbl = new System.Windows.Forms.Label();
            partnamelbl = new System.Windows.Forms.Label();
            partnumberlbl = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)StockCardtable).BeginInit();
            groupBox2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(warehouseidcmb);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(ProdVerComboButton);
            groupBox1.Controls.Add(SearchBtn);
            groupBox1.Controls.Add(PostingDate2);
            groupBox1.Controls.Add(PostingDate1);
            groupBox1.Controls.Add(PartnumberTextbox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(36, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(516, 161);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filters";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F);
            label8.Location = new System.Drawing.Point(265, 127);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(65, 18);
            label8.TabIndex = 17;
            label8.Text = "Warehouse:";
            // 
            // warehouseidcmb
            // 
            warehouseidcmb.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            warehouseidcmb.FormattingEnabled = true;
            warehouseidcmb.Items.AddRange(new object[] { "WH1", "WH2" });
            warehouseidcmb.Location = new System.Drawing.Point(335, 119);
            warehouseidcmb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            warehouseidcmb.Name = "warehouseidcmb";
            warehouseidcmb.Size = new System.Drawing.Size(135, 26);
            warehouseidcmb.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F);
            label7.Location = new System.Drawing.Point(35, 127);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(53, 18);
            label7.TabIndex = 15;
            label7.Text = "Prod Ver:";
            // 
            // ProdVerComboButton
            // 
            ProdVerComboButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ProdVerComboButton.FormattingEnabled = true;
            ProdVerComboButton.Items.AddRange(new object[] { "SA1", "ABE", "SBE", "SBA", "ABA", "PK1" });
            ProdVerComboButton.Location = new System.Drawing.Point(107, 119);
            ProdVerComboButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ProdVerComboButton.Name = "ProdVerComboButton";
            ProdVerComboButton.Size = new System.Drawing.Size(135, 26);
            ProdVerComboButton.TabIndex = 14;
            // 
            // SearchBtn
            // 
            SearchBtn.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            SearchBtn.Location = new System.Drawing.Point(415, 29);
            SearchBtn.Name = "SearchBtn";
            SearchBtn.Size = new System.Drawing.Size(69, 27);
            SearchBtn.TabIndex = 13;
            SearchBtn.Text = "Search";
            SearchBtn.UseVisualStyleBackColor = true;
            SearchBtn.Click += SearchBtn_Click;
            // 
            // PostingDate2
            // 
            PostingDate2.Checked = false;
            PostingDate2.CustomFormat = "yyyy-MM-dd";
            PostingDate2.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            PostingDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            PostingDate2.Location = new System.Drawing.Point(213, 75);
            PostingDate2.Name = "PostingDate2";
            PostingDate2.Size = new System.Drawing.Size(85, 27);
            PostingDate2.TabIndex = 7;
            // 
            // PostingDate1
            // 
            PostingDate1.Checked = false;
            PostingDate1.CustomFormat = "yyyy-MM-dd";
            PostingDate1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            PostingDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            PostingDate1.Location = new System.Drawing.Point(107, 75);
            PostingDate1.Name = "PostingDate1";
            PostingDate1.Size = new System.Drawing.Size(85, 27);
            PostingDate1.TabIndex = 6;
            // 
            // PartnumberTextbox
            // 
            PartnumberTextbox.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            PartnumberTextbox.Location = new System.Drawing.Point(106, 33);
            PartnumberTextbox.Name = "PartnumberTextbox";
            PartnumberTextbox.Size = new System.Drawing.Size(266, 26);
            PartnumberTextbox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            label3.Location = new System.Drawing.Point(196, 78);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(15, 19);
            label3.TabIndex = 4;
            label3.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F);
            label2.Location = new System.Drawing.Point(6, 84);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(84, 18);
            label2.TabIndex = 2;
            label2.Text = "Inventory date:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F);
            label1.Location = new System.Drawing.Point(14, 42);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(74, 18);
            label1.TabIndex = 0;
            label1.Text = "Part Number:";
            // 
            // StockCardtable
            // 
            StockCardtable.AllowUserToAddRows = false;
            StockCardtable.AllowUserToDeleteRows = false;
            StockCardtable.BackgroundColor = System.Drawing.Color.White;
            StockCardtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            StockCardtable.Location = new System.Drawing.Point(39, 233);
            StockCardtable.Name = "StockCardtable";
            StockCardtable.ReadOnly = true;
            StockCardtable.Size = new System.Drawing.Size(1146, 382);
            StockCardtable.TabIndex = 3;
            // 
            // total_sum
            // 
            total_sum.AutoSize = true;
            total_sum.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            total_sum.Location = new System.Drawing.Point(367, 33);
            total_sum.Name = "total_sum";
            total_sum.Size = new System.Drawing.Size(110, 17);
            total_sum.TabIndex = 13;
            total_sum.Text = "Beginning Stock:";
            // 
            // BtnExport
            // 
            BtnExport.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            BtnExport.Location = new System.Drawing.Point(1094, 189);
            BtnExport.Name = "BtnExport";
            BtnExport.Size = new System.Drawing.Size(91, 31);
            BtnExport.TabIndex = 12;
            BtnExport.Text = "Export Csv";
            BtnExport.UseVisualStyleBackColor = true;
            BtnExport.Click += BtnExport_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(24, 33);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(91, 17);
            label4.TabIndex = 14;
            label4.Text = "Part Number:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(endstocklbl);
            groupBox2.Controls.Add(customerlbl);
            groupBox2.Controls.Add(partnamelbl);
            groupBox2.Controls.Add(partnumberlbl);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(total_sum);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(558, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(627, 161);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Stock Information";
            // 
            // endstocklbl
            // 
            endstocklbl.AutoSize = true;
            endstocklbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            endstocklbl.Location = new System.Drawing.Point(474, 33);
            endstocklbl.Name = "endstocklbl";
            endstocklbl.Size = new System.Drawing.Size(0, 15);
            endstocklbl.TabIndex = 21;
            // 
            // customerlbl
            // 
            customerlbl.AutoSize = true;
            customerlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            customerlbl.Location = new System.Drawing.Point(105, 104);
            customerlbl.Name = "customerlbl";
            customerlbl.Size = new System.Drawing.Size(0, 15);
            customerlbl.TabIndex = 20;
            // 
            // partnamelbl
            // 
            partnamelbl.AutoSize = true;
            partnamelbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            partnamelbl.Location = new System.Drawing.Point(105, 68);
            partnamelbl.Name = "partnamelbl";
            partnamelbl.Size = new System.Drawing.Size(0, 15);
            partnamelbl.TabIndex = 19;
            // 
            // partnumberlbl
            // 
            partnumberlbl.AutoSize = true;
            partnumberlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            partnumberlbl.Location = new System.Drawing.Point(105, 33);
            partnumberlbl.Name = "partnumberlbl";
            partnumberlbl.Size = new System.Drawing.Size(0, 15);
            partnumberlbl.TabIndex = 18;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label6.Location = new System.Drawing.Point(24, 68);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(76, 17);
            label6.TabIndex = 16;
            label6.Text = "Part Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label5.Location = new System.Drawing.Point(24, 104);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(71, 17);
            label5.TabIndex = 15;
            label5.Text = "Customer:";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 714);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 11, 0);
            statusStrip1.Size = new System.Drawing.Size(1224, 23);
            statusStrip1.TabIndex = 16;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(84, 17);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 18);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // StockCard
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(5F, 14F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1224, 737);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox2);
            Controls.Add(BtnExport);
            Controls.Add(StockCardtable);
            Controls.Add(groupBox1);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "StockCard";
            Text = "StockCard";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)StockCardtable).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker PostingDate2;
        private System.Windows.Forms.DateTimePicker PostingDate1;
        private System.Windows.Forms.TextBox PartnumberTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView StockCardtable;
        private System.Windows.Forms.Label total_sum;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label endstocklbl;
        private System.Windows.Forms.Label customerlbl;
        private System.Windows.Forms.Label partnamelbl;
        private System.Windows.Forms.Label partnumberlbl;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ProdVerComboButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox warehouseidcmb;
    }
}