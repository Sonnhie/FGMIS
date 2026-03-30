namespace FGScanner
{
    partial class WarehouseReturn
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.CmbWHid = new MetroFramework.Controls.MetroComboBox();
            this.controlnumberLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtRackno = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbto = new MetroFramework.Controls.MetroComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.remarktext = new System.Windows.Forms.RichTextBox();
            this.LblStorageLocation = new System.Windows.Forms.Label();
            this.cmbfrom = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtScanData = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.returnlogs = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.GBtn = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.returnlogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.CmbWHid);
            this.panel1.Controls.Add(this.controlnumberLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtRackno);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbto);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.remarktext);
            this.panel1.Controls.Add(this.LblStorageLocation);
            this.panel1.Controls.Add(this.cmbfrom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtScanData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 702);
            this.panel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(66, 424);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Select Warehouse";
            // 
            // CmbWHid
            // 
            this.CmbWHid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbWHid.FormattingEnabled = true;
            this.CmbWHid.ItemHeight = 23;
            this.CmbWHid.Items.AddRange(new object[] {
            "WH1",
            "WH2"});
            this.CmbWHid.Location = new System.Drawing.Point(69, 447);
            this.CmbWHid.Name = "CmbWHid";
            this.CmbWHid.Size = new System.Drawing.Size(225, 29);
            this.CmbWHid.TabIndex = 21;
            this.CmbWHid.SelectedIndexChanged += new System.EventHandler(this.CmbWHid_SelectedIndexChanged);
            // 
            // controlnumberLabel
            // 
            this.controlnumberLabel.AutoSize = true;
            this.controlnumberLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlnumberLabel.Location = new System.Drawing.Point(66, 176);
            this.controlnumberLabel.Name = "controlnumberLabel";
            this.controlnumberLabel.Size = new System.Drawing.Size(28, 17);
            this.controlnumberLabel.TabIndex = 20;
            this.controlnumberLabel.Text = "----";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(66, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Document No:";
            // 
            // TxtRackno
            // 
            this.TxtRackno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRackno.FormattingEnabled = true;
            this.TxtRackno.Location = new System.Drawing.Point(69, 233);
            this.TxtRackno.Margin = new System.Windows.Forms.Padding(2);
            this.TxtRackno.Name = "TxtRackno";
            this.TxtRackno.Size = new System.Drawing.Size(205, 21);
            this.TxtRackno.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(66, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "To Storage loaction:";
            // 
            // cmbto
            // 
            this.cmbto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbto.FormattingEnabled = true;
            this.cmbto.ItemHeight = 23;
            this.cmbto.Items.AddRange(new object[] {
            "SINA",
            "ASSA",
            "SINB",
            "ASSB"});
            this.cmbto.Location = new System.Drawing.Point(69, 375);
            this.cmbto.Name = "cmbto";
            this.cmbto.Size = new System.Drawing.Size(225, 29);
            this.cmbto.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 515);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Reason of return:";
            // 
            // remarktext
            // 
            this.remarktext.Location = new System.Drawing.Point(64, 539);
            this.remarktext.Name = "remarktext";
            this.remarktext.Size = new System.Drawing.Size(231, 94);
            this.remarktext.TabIndex = 14;
            this.remarktext.Text = "";
            // 
            // LblStorageLocation
            // 
            this.LblStorageLocation.AutoSize = true;
            this.LblStorageLocation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStorageLocation.Location = new System.Drawing.Point(66, 272);
            this.LblStorageLocation.Name = "LblStorageLocation";
            this.LblStorageLocation.Size = new System.Drawing.Size(144, 17);
            this.LblStorageLocation.TabIndex = 13;
            this.LblStorageLocation.Text = "From Storage Location:";
            // 
            // cmbfrom
            // 
            this.cmbfrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbfrom.FormattingEnabled = true;
            this.cmbfrom.ItemHeight = 23;
            this.cmbfrom.Items.AddRange(new object[] {
            "9151",
            "9150"});
            this.cmbfrom.Location = new System.Drawing.Point(69, 295);
            this.cmbfrom.Name = "cmbfrom";
            this.cmbfrom.Size = new System.Drawing.Size(225, 29);
            this.cmbfrom.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select Rack no.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Scan QR code";
            // 
            // TxtScanData
            // 
            this.TxtScanData.Location = new System.Drawing.Point(64, 97);
            this.TxtScanData.Name = "TxtScanData";
            this.TxtScanData.Size = new System.Drawing.Size(231, 20);
            this.TxtScanData.TabIndex = 0;
            this.TxtScanData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 702);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1245, 22);
            this.statusStrip1.TabIndex = 1;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.GBtn);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.returnlogs);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1245, 702);
            this.panel2.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(1001, 262);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 39);
            this.button3.TabIndex = 7;
            this.button3.Text = "Generate Return Slip";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(898, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 39);
            this.button2.TabIndex = 6;
            this.button2.Text = "Upload";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1104, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 39);
            this.button1.TabIndex = 5;
            this.button1.Text = "Clear Items";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(368, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 21);
            this.label13.TabIndex = 4;
            this.label13.Text = "Return Items";
            // 
            // returnlogs
            // 
            this.returnlogs.BackgroundColor = System.Drawing.Color.White;
            this.returnlogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.returnlogs.Location = new System.Drawing.Point(372, 49);
            this.returnlogs.Name = "returnlogs";
            this.returnlogs.RowHeadersWidth = 51;
            this.returnlogs.Size = new System.Drawing.Size(829, 205);
            this.returnlogs.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(372, 363);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(829, 297);
            this.dataGridView1.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(368, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 21);
            this.label7.TabIndex = 9;
            this.label7.Text = "Warehouse Return";
            // 
            // GBtn
            // 
            this.GBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBtn.Location = new System.Drawing.Point(784, 262);
            this.GBtn.Name = "GBtn";
            this.GBtn.Size = new System.Drawing.Size(97, 39);
            this.GBtn.TabIndex = 10;
            this.GBtn.Text = "Generate Return Slip";
            this.GBtn.UseVisualStyleBackColor = true;
            this.GBtn.Click += new System.EventHandler(this.GBtn_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // WarehouseReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 724);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WarehouseReturn";
            this.Text = "WarehouseReturn";
            this.Load += new System.EventHandler(this.WarehouseReturn_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.returnlogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TxtScanData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox remarktext;
        private System.Windows.Forms.Label LblStorageLocation;
        private MetroFramework.Controls.MetroComboBox cmbfrom;
        private System.Windows.Forms.DataGridView returnlogs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroComboBox cmbto;
        private System.Windows.Forms.ComboBox TxtRackno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label controlnumberLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroComboBox CmbWHid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button GBtn;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}