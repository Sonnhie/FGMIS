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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmbWHid = new System.Windows.Forms.ComboBox();
            this.cmbto = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbfrom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtRackno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtScanData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LblTranscType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LblTotalQuantity = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.LblTotalbox = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.controlnumberLabel = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.remarktext = new System.Windows.Forms.RichTextBox();
            this.returnlogs = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.GBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.returnlogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.GBtn);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.returnlogs);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1245, 702);
            this.panel2.TabIndex = 2;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.remarktext);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.CmbWHid);
            this.groupBox1.Controls.Add(this.cmbto);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbfrom);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TxtRackno);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtScanData);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(42, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 384);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Inputs";
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
            // 
            // cmbto
            // 
            this.cmbto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbto.FormattingEnabled = true;
            this.cmbto.Items.AddRange(new object[] {
            "FG",
            "TEMPORARY FG"});
            this.cmbto.Location = new System.Drawing.Point(157, 225);
            this.cmbto.Name = "cmbto";
            this.cmbto.Size = new System.Drawing.Size(121, 23);
            this.cmbto.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(60, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Transfer to:";
            // 
            // cmbfrom
            // 
            this.cmbfrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbfrom.FormattingEnabled = true;
            this.cmbfrom.Items.AddRange(new object[] {
            "9151",
            "9150"});
            this.cmbfrom.Location = new System.Drawing.Point(157, 174);
            this.cmbfrom.Name = "cmbfrom";
            this.cmbfrom.Size = new System.Drawing.Size(121, 23);
            this.cmbfrom.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Storage Location:";
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
            // TxtRackno
            // 
            this.TxtRackno.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRackno.Location = new System.Drawing.Point(158, 125);
            this.TxtRackno.Name = "TxtRackno";
            this.TxtRackno.Size = new System.Drawing.Size(120, 23);
            this.TxtRackno.TabIndex = 3;
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
            // TxtScanData
            // 
            this.TxtScanData.Location = new System.Drawing.Point(158, 34);
            this.TxtScanData.Name = "TxtScanData";
            this.TxtScanData.Size = new System.Drawing.Size(276, 23);
            this.TxtScanData.TabIndex = 1;
            this.TxtScanData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtScanData_KeyDown);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LblTranscType);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.LblTotalQuantity);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.LblTotalbox);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.controlnumberLabel);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(42, 442);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 208);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transaction Information:";
            // 
            // LblTranscType
            // 
            this.LblTranscType.AutoSize = true;
            this.LblTranscType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTranscType.Location = new System.Drawing.Point(207, 166);
            this.LblTranscType.Name = "LblTranscType";
            this.LblTranscType.Size = new System.Drawing.Size(83, 17);
            this.LblTranscType.TabIndex = 8;
            this.LblTranscType.Text = "Return to FVI";
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
            // controlnumberLabel
            // 
            this.controlnumberLabel.AutoSize = true;
            this.controlnumberLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlnumberLabel.Location = new System.Drawing.Point(207, 50);
            this.controlnumberLabel.Name = "controlnumberLabel";
            this.controlnumberLabel.Size = new System.Drawing.Size(23, 17);
            this.controlnumberLabel.TabIndex = 2;
            this.controlnumberLabel.Text = "---";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Return Remarks:";
            // 
            // remarktext
            // 
            this.remarktext.Location = new System.Drawing.Point(157, 281);
            this.remarktext.Name = "remarktext";
            this.remarktext.Size = new System.Drawing.Size(277, 65);
            this.remarktext.TabIndex = 12;
            this.remarktext.Text = "";
            // 
            // returnlogs
            // 
            this.returnlogs.BackgroundColor = System.Drawing.Color.White;
            this.returnlogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.returnlogs.Location = new System.Drawing.Point(524, 48);
            this.returnlogs.Name = "returnlogs";
            this.returnlogs.Size = new System.Drawing.Size(690, 188);
            this.returnlogs.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(522, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 17);
            this.label13.TabIndex = 12;
            this.label13.Text = "Return Items";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1129, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 33);
            this.button1.TabIndex = 13;
            this.button1.Text = "Clear Items";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // GBtn
            // 
            this.GBtn.Location = new System.Drawing.Point(1038, 242);
            this.GBtn.Name = "GBtn";
            this.GBtn.Size = new System.Drawing.Size(85, 33);
            this.GBtn.TabIndex = 15;
            this.GBtn.Text = "Print";
            this.GBtn.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(947, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 33);
            this.button2.TabIndex = 16;
            this.button2.Text = "Upload";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(524, 298);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(690, 317);
            this.dataGridView1.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(522, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Warehouse Return List";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1129, 621);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 33);
            this.button3.TabIndex = 19;
            this.button3.Text = "Export To Csv";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // WarehouseReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 724);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WarehouseReturn";
            this.Text = "WarehouseReturn";
            this.Load += new System.EventHandler(this.WarehouseReturn_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.returnlogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CmbWHid;
        private System.Windows.Forms.ComboBox cmbto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbfrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtRackno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtScanData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label LblTranscType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label LblTotalQuantity;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label LblTotalbox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label controlnumberLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView returnlogs;
        private System.Windows.Forms.RichTextBox remarktext;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button GBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
    }
}