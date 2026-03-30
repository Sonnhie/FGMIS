namespace FGScanner
{
    partial class WHDataEntryIN
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
            this.components = new System.ComponentModel.Container();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtScanData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtRackno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CmbStorageLocation = new System.Windows.Forms.ComboBox();
            this.CmbRemarks = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CmbWHid = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LblCustomer = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LblQuantity = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.LblProVer = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LblProDate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblPartNumber = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LogsTable = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.LogsTable);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1245, 760);
            this.panel2.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmbWHid);
            this.groupBox1.Controls.Add(this.CmbRemarks);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.CmbStorageLocation);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TxtRackno);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtScanData);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(40, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 285);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Inputs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scan:";
            // 
            // TxtScanData
            // 
            this.TxtScanData.Location = new System.Drawing.Point(158, 34);
            this.TxtScanData.Name = "TxtScanData";
            this.TxtScanData.Size = new System.Drawing.Size(276, 23);
            this.TxtScanData.TabIndex = 1;
            this.TxtScanData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtScanData_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rack No:";
            // 
            // TxtRackno
            // 
            this.TxtRackno.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRackno.Location = new System.Drawing.Point(158, 125);
            this.TxtRackno.Name = "TxtRackno";
            this.TxtRackno.Size = new System.Drawing.Size(120, 23);
            this.TxtRackno.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Warehouse Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(32, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Storage Location:";
            // 
            // CmbStorageLocation
            // 
            this.CmbStorageLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbStorageLocation.FormattingEnabled = true;
            this.CmbStorageLocation.Items.AddRange(new object[] {
            "9151",
            "9150"});
            this.CmbStorageLocation.Location = new System.Drawing.Point(157, 174);
            this.CmbStorageLocation.Name = "CmbStorageLocation";
            this.CmbStorageLocation.Size = new System.Drawing.Size(121, 23);
            this.CmbStorageLocation.TabIndex = 7;
            // 
            // CmbRemarks
            // 
            this.CmbRemarks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbRemarks.FormattingEnabled = true;
            this.CmbRemarks.Items.AddRange(new object[] {
            "FG",
            "TEMPORARY FG"});
            this.CmbRemarks.Location = new System.Drawing.Point(157, 225);
            this.CmbRemarks.Name = "CmbRemarks";
            this.CmbRemarks.Size = new System.Drawing.Size(121, 23);
            this.CmbRemarks.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "FG Remarks:";
            // 
            // CmbWHid
            // 
            this.CmbWHid.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbWHid.FormattingEnabled = true;
            this.CmbWHid.Items.AddRange(new object[] {
            "WH1",
            "WH2"});
            this.CmbWHid.Location = new System.Drawing.Point(158, 79);
            this.CmbWHid.Name = "CmbWHid";
            this.CmbWHid.Size = new System.Drawing.Size(121, 23);
            this.CmbWHid.TabIndex = 10;
            this.CmbWHid.SelectedIndexChanged += new System.EventHandler(this.CmbWHid_SelectedIndexChanged_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LblCustomer);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.LblQuantity);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.LblProVer);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.LblProDate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.LblPartNumber);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(40, 342);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 228);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Item Information";
            // 
            // LblCustomer
            // 
            this.LblCustomer.AutoSize = true;
            this.LblCustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCustomer.Location = new System.Drawing.Point(382, 51);
            this.LblCustomer.Name = "LblCustomer";
            this.LblCustomer.Size = new System.Drawing.Size(23, 17);
            this.LblCustomer.TabIndex = 10;
            this.LblCustomer.Text = "---";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(303, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Customer:";
            // 
            // LblQuantity
            // 
            this.LblQuantity.AutoSize = true;
            this.LblQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblQuantity.Location = new System.Drawing.Point(182, 169);
            this.LblQuantity.Name = "LblQuantity";
            this.LblQuantity.Size = new System.Drawing.Size(23, 17);
            this.LblQuantity.TabIndex = 8;
            this.LblQuantity.Text = "---";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(35, 169);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 7;
            this.label11.Text = "Quantity:";
            // 
            // LblProVer
            // 
            this.LblProVer.AutoSize = true;
            this.LblProVer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblProVer.Location = new System.Drawing.Point(182, 129);
            this.LblProVer.Name = "LblProVer";
            this.LblProVer.Size = new System.Drawing.Size(23, 17);
            this.LblProVer.TabIndex = 6;
            this.LblProVer.Text = "---";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(35, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "Production Version:";
            // 
            // LblProDate
            // 
            this.LblProDate.AutoSize = true;
            this.LblProDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblProDate.Location = new System.Drawing.Point(182, 92);
            this.LblProDate.Name = "LblProDate";
            this.LblProDate.Size = new System.Drawing.Size(23, 17);
            this.LblProDate.TabIndex = 4;
            this.LblProDate.Text = "---";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(35, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Production Date:";
            // 
            // LblPartNumber
            // 
            this.LblPartNumber.AutoSize = true;
            this.LblPartNumber.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPartNumber.Location = new System.Drawing.Point(182, 51);
            this.LblPartNumber.Name = "LblPartNumber";
            this.LblPartNumber.Size = new System.Drawing.Size(23, 17);
            this.LblPartNumber.TabIndex = 2;
            this.LblPartNumber.Text = "---";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(35, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Part number:";
            // 
            // LogsTable
            // 
            this.LogsTable.BackgroundColor = System.Drawing.Color.White;
            this.LogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogsTable.Location = new System.Drawing.Point(515, 41);
            this.LogsTable.Name = "LogsTable";
            this.LogsTable.Size = new System.Drawing.Size(700, 529);
            this.LogsTable.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(511, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 21);
            this.label10.TabIndex = 5;
            this.label10.Text = "Recent Transaction";
            // 
            // WHDataEntryIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1245, 760);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WHDataEntryIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WHDataEntryIN";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ClockTimer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtScanData;
        private System.Windows.Forms.TextBox TxtRackno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CmbStorageLocation;
        private System.Windows.Forms.ComboBox CmbRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CmbWHid;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LblCustomer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LblQuantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblProVer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label LblProDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblPartNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView LogsTable;
    }
}