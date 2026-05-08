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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PostingDate2 = new System.Windows.Forms.DateTimePicker();
            this.PostingDate1 = new System.Windows.Forms.DateTimePicker();
            this.PartnumberTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StockCardtable = new System.Windows.Forms.DataGridView();
            this.total_sum = new System.Windows.Forms.Label();
            this.BtnExport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.partnumberlbl = new System.Windows.Forms.Label();
            this.partnamelbl = new System.Windows.Forms.Label();
            this.customerlbl = new System.Windows.Forms.Label();
            this.endstocklbl = new System.Windows.Forms.Label();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StockCardtable)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SearchBtn);
            this.groupBox1.Controls.Add(this.PostingDate2);
            this.groupBox1.Controls.Add(this.PostingDate1);
            this.groupBox1.Controls.Add(this.PartnumberTextbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(44, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 149);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filters";
            // 
            // PostingDate2
            // 
            this.PostingDate2.Checked = false;
            this.PostingDate2.CustomFormat = "yyyy-MM-dd";
            this.PostingDate2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostingDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PostingDate2.Location = new System.Drawing.Point(235, 90);
            this.PostingDate2.Name = "PostingDate2";
            this.PostingDate2.Size = new System.Drawing.Size(101, 25);
            this.PostingDate2.TabIndex = 7;
            // 
            // PostingDate1
            // 
            this.PostingDate1.Checked = false;
            this.PostingDate1.CustomFormat = "yyyy-MM-dd";
            this.PostingDate1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostingDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PostingDate1.Location = new System.Drawing.Point(108, 90);
            this.PostingDate1.Name = "PostingDate1";
            this.PostingDate1.Size = new System.Drawing.Size(101, 25);
            this.PostingDate1.TabIndex = 6;
            // 
            // PartnumberTextbox
            // 
            this.PartnumberTextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartnumberTextbox.Location = new System.Drawing.Point(108, 42);
            this.PartnumberTextbox.Name = "PartnumberTextbox";
            this.PartnumberTextbox.Size = new System.Drawing.Size(228, 25);
            this.PartnumberTextbox.TabIndex = 5;
            this.PartnumberTextbox.TextChanged += new System.EventHandler(this.PartnumberTextbox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(215, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Inventory date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Part Number:";
            // 
            // StockCardtable
            // 
            this.StockCardtable.AllowUserToAddRows = false;
            this.StockCardtable.AllowUserToDeleteRows = false;
            this.StockCardtable.BackgroundColor = System.Drawing.Color.White;
            this.StockCardtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StockCardtable.Location = new System.Drawing.Point(47, 217);
            this.StockCardtable.Name = "StockCardtable";
            this.StockCardtable.ReadOnly = true;
            this.StockCardtable.Size = new System.Drawing.Size(1131, 464);
            this.StockCardtable.TabIndex = 3;
            // 
            // total_sum
            // 
            this.total_sum.AutoSize = true;
            this.total_sum.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_sum.Location = new System.Drawing.Point(441, 30);
            this.total_sum.Name = "total_sum";
            this.total_sum.Size = new System.Drawing.Size(110, 17);
            this.total_sum.TabIndex = 13;
            this.total_sum.Text = "Beginning Stock:";
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(1106, 177);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(72, 29);
            this.BtnExport.TabIndex = 12;
            this.BtnExport.Text = "Export Csv";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Part Number:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.endstocklbl);
            this.groupBox2.Controls.Add(this.customerlbl);
            this.groupBox2.Controls.Add(this.partnamelbl);
            this.groupBox2.Controls.Add(this.partnumberlbl);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.total_sum);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(516, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(662, 149);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock Information";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(29, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Customer:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Part Name:";
            // 
            // partnumberlbl
            // 
            this.partnumberlbl.AutoSize = true;
            this.partnumberlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partnumberlbl.Location = new System.Drawing.Point(126, 30);
            this.partnumberlbl.Name = "partnumberlbl";
            this.partnumberlbl.Size = new System.Drawing.Size(0, 15);
            this.partnumberlbl.TabIndex = 18;
            // 
            // partnamelbl
            // 
            this.partnamelbl.AutoSize = true;
            this.partnamelbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partnamelbl.Location = new System.Drawing.Point(126, 63);
            this.partnamelbl.Name = "partnamelbl";
            this.partnamelbl.Size = new System.Drawing.Size(0, 15);
            this.partnamelbl.TabIndex = 19;
            // 
            // customerlbl
            // 
            this.customerlbl.AutoSize = true;
            this.customerlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerlbl.Location = new System.Drawing.Point(126, 96);
            this.customerlbl.Name = "customerlbl";
            this.customerlbl.Size = new System.Drawing.Size(0, 15);
            this.customerlbl.TabIndex = 20;
            // 
            // endstocklbl
            // 
            this.endstocklbl.AutoSize = true;
            this.endstocklbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endstocklbl.Location = new System.Drawing.Point(568, 30);
            this.endstocklbl.Name = "endstocklbl";
            this.endstocklbl.Size = new System.Drawing.Size(0, 15);
            this.endstocklbl.TabIndex = 21;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBtn.Location = new System.Drawing.Point(352, 42);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(82, 25);
            this.SearchBtn.TabIndex = 13;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 702);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1245, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // StockCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 724);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.StockCardtable);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockCard";
            this.Text = "StockCard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StockCardtable)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}