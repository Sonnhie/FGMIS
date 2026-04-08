namespace FGScanner
{
    partial class WHRForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtDocNumber = new System.Windows.Forms.TextBox();
            this.PostingDate1 = new System.Windows.Forms.DateTimePicker();
            this.PostingDate2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStorageLocation = new System.Windows.Forms.ComboBox();
            this.Returntable = new System.Windows.Forms.DataGridView();
            this.BtnNext = new System.Windows.Forms.Button();
            this.BtnPrev = new System.Windows.Forms.Button();
            this.LblPage = new System.Windows.Forms.Label();
            this.BtnExport = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Returntable)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStorageLocation);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.PostingDate2);
            this.groupBox1.Controls.Add(this.PostingDate1);
            this.groupBox1.Controls.Add(this.TxtDocNumber);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(55, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1134, 98);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(622, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(424, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Posting date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Document #:";
            // 
            // TxtDocNumber
            // 
            this.TxtDocNumber.Location = new System.Drawing.Point(152, 42);
            this.TxtDocNumber.Name = "TxtDocNumber";
            this.TxtDocNumber.Size = new System.Drawing.Size(228, 25);
            this.TxtDocNumber.TabIndex = 5;
            this.TxtDocNumber.TextChanged += new System.EventHandler(this.TxtDocNumber_TextChanged);
            // 
            // PostingDate1
            // 
            this.PostingDate1.Checked = false;
            this.PostingDate1.CustomFormat = "yyyy-MM-dd";
            this.PostingDate1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostingDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PostingDate1.Location = new System.Drawing.Point(515, 42);
            this.PostingDate1.Name = "PostingDate1";
            this.PostingDate1.Size = new System.Drawing.Size(101, 25);
            this.PostingDate1.TabIndex = 6;
            this.PostingDate1.ValueChanged += new System.EventHandler(this.PostingDate1_ValueChanged);
            // 
            // PostingDate2
            // 
            this.PostingDate2.Checked = false;
            this.PostingDate2.CustomFormat = "yyyy-MM-dd";
            this.PostingDate2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostingDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PostingDate2.Location = new System.Drawing.Point(642, 42);
            this.PostingDate2.Name = "PostingDate2";
            this.PostingDate2.Size = new System.Drawing.Size(101, 25);
            this.PostingDate2.TabIndex = 7;
            this.PostingDate2.ValueChanged += new System.EventHandler(this.PostingDate2_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(789, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Storage Location:";
            // 
            // cmbStorageLocation
            // 
            this.cmbStorageLocation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStorageLocation.FormattingEnabled = true;
            this.cmbStorageLocation.Items.AddRange(new object[] {
            "SINA",
            "ASSA",
            "MOLD",
            "ASSB",
            "SINB"});
            this.cmbStorageLocation.Location = new System.Drawing.Point(905, 40);
            this.cmbStorageLocation.Name = "cmbStorageLocation";
            this.cmbStorageLocation.Size = new System.Drawing.Size(101, 25);
            this.cmbStorageLocation.TabIndex = 9;
            this.cmbStorageLocation.SelectedIndexChanged += new System.EventHandler(this.cmbStorageLocation_SelectedIndexChanged);
            // 
            // Returntable
            // 
            this.Returntable.AllowUserToDeleteRows = false;
            this.Returntable.BackgroundColor = System.Drawing.Color.White;
            this.Returntable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Returntable.Location = new System.Drawing.Point(58, 190);
            this.Returntable.Name = "Returntable";
            this.Returntable.ReadOnly = true;
            this.Returntable.Size = new System.Drawing.Size(1131, 437);
            this.Returntable.TabIndex = 2;
            this.Returntable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Returntable_CellClick);
            // 
            // BtnNext
            // 
            this.BtnNext.Location = new System.Drawing.Point(1134, 633);
            this.BtnNext.Name = "BtnNext";
            this.BtnNext.Size = new System.Drawing.Size(55, 30);
            this.BtnNext.TabIndex = 3;
            this.BtnNext.Text = "Next";
            this.BtnNext.UseVisualStyleBackColor = true;
            this.BtnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // BtnPrev
            // 
            this.BtnPrev.Location = new System.Drawing.Point(1073, 633);
            this.BtnPrev.Name = "BtnPrev";
            this.BtnPrev.Size = new System.Drawing.Size(55, 30);
            this.BtnPrev.TabIndex = 4;
            this.BtnPrev.Text = "Prev";
            this.BtnPrev.UseVisualStyleBackColor = true;
            this.BtnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // LblPage
            // 
            this.LblPage.AutoSize = true;
            this.LblPage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPage.Location = new System.Drawing.Point(60, 633);
            this.LblPage.Name = "LblPage";
            this.LblPage.Size = new System.Drawing.Size(75, 17);
            this.LblPage.TabIndex = 5;
            this.LblPage.Text = "Page 1 of 5";
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(1117, 155);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(72, 29);
            this.BtnExport.TabIndex = 6;
            this.BtnExport.Text = "Export Csv";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 702);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1245, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // WHRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 724);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.LblPage);
            this.Controls.Add(this.BtnPrev);
            this.Controls.Add(this.BtnNext);
            this.Controls.Add(this.Returntable);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WHRForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.PLForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Returntable)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtDocNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStorageLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker PostingDate2;
        private System.Windows.Forms.DateTimePicker PostingDate1;
        private System.Windows.Forms.DataGridView Returntable;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Label LblPage;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}