namespace FGScanner
{
    partial class WH_OUT
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtRackno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CmbWHid = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtScanData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LblTranscType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LblTotalQuantity = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.LblTotalbox = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.TxtcontrolNumber = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.DPILogsTable = new System.Windows.Forms.DataGridView();
            this.logstable = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DPILogsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logstable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtRackno);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CmbWHid);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtScanData);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(45, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 216);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Inputs";
            // 
            // TxtRackno
            // 
            this.TxtRackno.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRackno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRackno.Location = new System.Drawing.Point(164, 139);
            this.TxtRackno.Name = "TxtRackno";
            this.TxtRackno.Size = new System.Drawing.Size(258, 25);
            this.TxtRackno.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rack Location:";
            // 
            // CmbWHid
            // 
            this.CmbWHid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbWHid.FormattingEnabled = true;
            this.CmbWHid.Items.AddRange(new object[] {
            "WH1",
            "WH2"});
            this.CmbWHid.Location = new System.Drawing.Point(164, 89);
            this.CmbWHid.Name = "CmbWHid";
            this.CmbWHid.Size = new System.Drawing.Size(258, 25);
            this.CmbWHid.TabIndex = 3;
            this.CmbWHid.SelectedIndexChanged += new System.EventHandler(this.CmbWHid_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Warehouse Code:";
            // 
            // TxtScanData
            // 
            this.TxtScanData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtScanData.Location = new System.Drawing.Point(164, 40);
            this.TxtScanData.Name = "TxtScanData";
            this.TxtScanData.Size = new System.Drawing.Size(258, 25);
            this.TxtScanData.TabIndex = 1;
            this.TxtScanData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtScanData_KeyDown_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(108, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scan:";
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
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(45, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(453, 228);
            this.groupBox2.TabIndex = 1;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Part number:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 738);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1245, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LblTranscType);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.LblTotalQuantity);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.LblTotalbox);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.TxtcontrolNumber);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(45, 468);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(453, 208);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transaction Information:";
            // 
            // LblTranscType
            // 
            this.LblTranscType.AutoSize = true;
            this.LblTranscType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTranscType.Location = new System.Drawing.Point(207, 166);
            this.LblTranscType.Name = "LblTranscType";
            this.LblTranscType.Size = new System.Drawing.Size(23, 17);
            this.LblTranscType.TabIndex = 8;
            this.LblTranscType.Text = "---";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "Transaction type:";
            // 
            // LblTotalQuantity
            // 
            this.LblTotalQuantity.AutoSize = true;
            this.LblTotalQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalQuantity.Location = new System.Drawing.Point(207, 128);
            this.LblTotalQuantity.Name = "LblTotalQuantity";
            this.LblTotalQuantity.Size = new System.Drawing.Size(23, 17);
            this.LblTotalQuantity.TabIndex = 6;
            this.LblTotalQuantity.Text = "---";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(55, 128);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 17);
            this.label15.TabIndex = 5;
            this.label15.Text = "Total Quantity:";
            // 
            // LblTotalbox
            // 
            this.LblTotalbox.AutoSize = true;
            this.LblTotalbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalbox.Location = new System.Drawing.Point(207, 91);
            this.LblTotalbox.Name = "LblTotalbox";
            this.LblTotalbox.Size = new System.Drawing.Size(23, 17);
            this.LblTotalbox.TabIndex = 4;
            this.LblTotalbox.Text = "---";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(55, 91);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 17);
            this.label17.TabIndex = 3;
            this.label17.Text = "Total Box:";
            // 
            // TxtcontrolNumber
            // 
            this.TxtcontrolNumber.AutoSize = true;
            this.TxtcontrolNumber.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtcontrolNumber.Location = new System.Drawing.Point(207, 50);
            this.TxtcontrolNumber.Name = "TxtcontrolNumber";
            this.TxtcontrolNumber.Size = new System.Drawing.Size(23, 17);
            this.TxtcontrolNumber.TabIndex = 2;
            this.TxtcontrolNumber.Text = "---";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(55, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(122, 17);
            this.label19.TabIndex = 1;
            this.label19.Text = "Document Number:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1024, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Upload DPI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DPILogsTable
            // 
            this.DPILogsTable.BackgroundColor = System.Drawing.Color.White;
            this.DPILogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DPILogsTable.Location = new System.Drawing.Point(517, 57);
            this.DPILogsTable.Name = "DPILogsTable";
            this.DPILogsTable.ReadOnly = true;
            this.DPILogsTable.Size = new System.Drawing.Size(695, 168);
            this.DPILogsTable.TabIndex = 5;
            this.DPILogsTable.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DPILogsTable_RowPrePaint);
            // 
            // logstable
            // 
            this.logstable.BackgroundColor = System.Drawing.Color.White;
            this.logstable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logstable.Location = new System.Drawing.Point(517, 286);
            this.logstable.Name = "logstable";
            this.logstable.ReadOnly = true;
            this.logstable.Size = new System.Drawing.Size(704, 348);
            this.logstable.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(516, 262);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 17);
            this.label12.TabIndex = 7;
            this.label12.Text = "Item List:";
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Location = new System.Drawing.Point(1130, 648);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(91, 30);
            this.BtnGenerate.TabIndex = 8;
            this.BtnGenerate.Text = "Generate PL";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1033, 648);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "Clear Items";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(936, 648);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Upload";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(516, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(205, 17);
            this.label13.TabIndex = 11;
            this.label13.Text = "Delivery Preparation Instruction:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1121, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 30);
            this.button3.TabIndex = 12;
            this.button3.Text = "Clear DPI";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // WH_OUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 760);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.logstable);
            this.Controls.Add(this.DPILogsTable);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WH_OUT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WH_OUT";
            this.Load += new System.EventHandler(this.WH_OUT_Load);
            this.Shown += new System.EventHandler(this.WH_OUT_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DPILogsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logstable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtScanData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbWHid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Label LblQuantity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LblProVer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label LblProDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblPartNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label LblTotalQuantity;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label LblTotalbox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label TxtcontrolNumber;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView DPILogsTable;
        private System.Windows.Forms.DataGridView logstable;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button BtnGenerate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label LblCustomer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LblTranscType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtRackno;
        private System.Windows.Forms.Button button3;
    }
}