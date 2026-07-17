namespace FGScanner.Forms.DataEntry
{
    partial class Shipments
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
            groupBox2 = new System.Windows.Forms.GroupBox();
            DPIClearButton = new System.Windows.Forms.Button();
            DPITextBox = new System.Windows.Forms.TextBox();
            DPIFileButton = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            GeneratePackingListBtn = new System.Windows.Forms.Button();
            label11 = new System.Windows.Forms.Label();
            ClearButton = new System.Windows.Forms.Button();
            UploadItemButton = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            FileTextbox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            WarehouseComboBox = new System.Windows.Forms.ComboBox();
            SelectFileButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            ShipmentIdLabel = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            DPITotalQuantityLabel = new System.Windows.Forms.Label();
            DPITotalBoxLabel = new System.Windows.Forms.Label();
            DPIPartcountLabel = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            CustomerLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            QuantityLabel = new System.Windows.Forms.Label();
            BoxLabel = new System.Windows.Forms.Label();
            PartcountLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            ShipmenTable = new System.Windows.Forms.DataGridView();
            label19 = new System.Windows.Forms.Label();
            DPITable = new System.Windows.Forms.DataGridView();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ShipmenTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DPITable).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(DPIClearButton);
            groupBox2.Controls.Add(DPITextBox);
            groupBox2.Controls.Add(DPIFileButton);
            groupBox2.Controls.Add(label6);
            groupBox2.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(21, 12);
            groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox2.Size = new System.Drawing.Size(484, 124);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "DPI Data Entry";
            // 
            // DPIClearButton
            // 
            DPIClearButton.Location = new System.Drawing.Point(336, 72);
            DPIClearButton.Name = "DPIClearButton";
            DPIClearButton.Size = new System.Drawing.Size(92, 27);
            DPIClearButton.TabIndex = 11;
            DPIClearButton.Text = "Clear Upload";
            DPIClearButton.UseVisualStyleBackColor = true;
            DPIClearButton.Click += DPIClearButton_Click;
            // 
            // DPITextBox
            // 
            DPITextBox.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            DPITextBox.Location = new System.Drawing.Point(105, 40);
            DPITextBox.Name = "DPITextBox";
            DPITextBox.ReadOnly = true;
            DPITextBox.Size = new System.Drawing.Size(216, 27);
            DPITextBox.TabIndex = 8;
            // 
            // DPIFileButton
            // 
            DPIFileButton.Location = new System.Drawing.Point(336, 39);
            DPIFileButton.Name = "DPIFileButton";
            DPIFileButton.Size = new System.Drawing.Size(92, 27);
            DPIFileButton.TabIndex = 2;
            DPIFileButton.Text = "Select file";
            DPIFileButton.UseVisualStyleBackColor = true;
            DPIFileButton.Click += DPIFileButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(24, 48);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(70, 19);
            label6.TabIndex = 0;
            label6.Text = "File upload:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(GeneratePackingListBtn);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(ClearButton);
            groupBox1.Controls.Add(UploadItemButton);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(FileTextbox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(WarehouseComboBox);
            groupBox1.Controls.Add(SelectFileButton);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(21, 143);
            groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Size = new System.Drawing.Size(484, 215);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "FG Data Entry";
            // 
            // GeneratePackingListBtn
            // 
            GeneratePackingListBtn.Location = new System.Drawing.Point(171, 171);
            GeneratePackingListBtn.Name = "GeneratePackingListBtn";
            GeneratePackingListBtn.Size = new System.Drawing.Size(92, 27);
            GeneratePackingListBtn.TabIndex = 13;
            GeneratePackingListBtn.Text = "Generate";
            GeneratePackingListBtn.UseVisualStyleBackColor = true;
            GeneratePackingListBtn.Click += GeneratePackingListBtn_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(85, 175);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(76, 19);
            label11.TabIndex = 12;
            label11.Text = "Packing List:";
            // 
            // ClearButton
            // 
            ClearButton.Location = new System.Drawing.Point(377, 129);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new System.Drawing.Size(92, 27);
            ClearButton.TabIndex = 11;
            ClearButton.Text = "Clear Upload";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // UploadItemButton
            // 
            UploadItemButton.Location = new System.Drawing.Point(171, 132);
            UploadItemButton.Name = "UploadItemButton";
            UploadItemButton.Size = new System.Drawing.Size(92, 27);
            UploadItemButton.TabIndex = 10;
            UploadItemButton.Text = "Upload Data";
            UploadItemButton.UseVisualStyleBackColor = true;
            UploadItemButton.Click += UploadItemButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(113, 137);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(48, 19);
            label2.TabIndex = 9;
            label2.Text = "Upload:";
            // 
            // FileTextbox
            // 
            FileTextbox.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FileTextbox.Location = new System.Drawing.Point(171, 88);
            FileTextbox.Name = "FileTextbox";
            FileTextbox.ReadOnly = true;
            FileTextbox.Size = new System.Drawing.Size(195, 27);
            FileTextbox.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(44, 47);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(117, 19);
            label3.TabIndex = 7;
            label3.Text = "Select WarehouseID:";
            // 
            // WarehouseComboBox
            // 
            WarehouseComboBox.FormattingEnabled = true;
            WarehouseComboBox.Items.AddRange(new object[] { "WH1", "WH2" });
            WarehouseComboBox.Location = new System.Drawing.Point(172, 39);
            WarehouseComboBox.Name = "WarehouseComboBox";
            WarehouseComboBox.Size = new System.Drawing.Size(194, 27);
            WarehouseComboBox.TabIndex = 4;
            // 
            // SelectFileButton
            // 
            SelectFileButton.Location = new System.Drawing.Point(377, 88);
            SelectFileButton.Name = "SelectFileButton";
            SelectFileButton.Size = new System.Drawing.Size(92, 27);
            SelectFileButton.TabIndex = 2;
            SelectFileButton.Text = "Select file";
            SelectFileButton.UseVisualStyleBackColor = true;
            SelectFileButton.Click += SelectFileButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(91, 96);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 19);
            label1.TabIndex = 0;
            label1.Text = "File upload:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ShipmentIdLabel);
            groupBox3.Controls.Add(label21);
            groupBox3.Controls.Add(DPITotalQuantityLabel);
            groupBox3.Controls.Add(DPITotalBoxLabel);
            groupBox3.Controls.Add(DPIPartcountLabel);
            groupBox3.Controls.Add(label16);
            groupBox3.Controls.Add(label17);
            groupBox3.Controls.Add(label18);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(CustomerLabel);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(QuantityLabel);
            groupBox3.Controls.Add(BoxLabel);
            groupBox3.Controls.Add(PartcountLabel);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label8);
            groupBox3.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox3.Location = new System.Drawing.Point(19, 364);
            groupBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox3.Size = new System.Drawing.Size(486, 254);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Information:";
            // 
            // ShipmentIdLabel
            // 
            ShipmentIdLabel.AutoSize = true;
            ShipmentIdLabel.Location = new System.Drawing.Point(197, 223);
            ShipmentIdLabel.Name = "ShipmentIdLabel";
            ShipmentIdLabel.Size = new System.Drawing.Size(21, 19);
            ShipmentIdLabel.TabIndex = 22;
            ShipmentIdLabel.Text = "--";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(69, 223);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(69, 19);
            label21.TabIndex = 21;
            label21.Text = "Shipping Id:";
            // 
            // DPITotalQuantityLabel
            // 
            DPITotalQuantityLabel.AutoSize = true;
            DPITotalQuantityLabel.Location = new System.Drawing.Point(420, 81);
            DPITotalQuantityLabel.Name = "DPITotalQuantityLabel";
            DPITotalQuantityLabel.Size = new System.Drawing.Size(21, 19);
            DPITotalQuantityLabel.TabIndex = 18;
            DPITotalQuantityLabel.Text = "--";
            // 
            // DPITotalBoxLabel
            // 
            DPITotalBoxLabel.AutoSize = true;
            DPITotalBoxLabel.Location = new System.Drawing.Point(197, 81);
            DPITotalBoxLabel.Name = "DPITotalBoxLabel";
            DPITotalBoxLabel.Size = new System.Drawing.Size(21, 19);
            DPITotalBoxLabel.TabIndex = 17;
            DPITotalBoxLabel.Text = "--";
            // 
            // DPIPartcountLabel
            // 
            DPIPartcountLabel.AutoSize = true;
            DPIPartcountLabel.Location = new System.Drawing.Point(197, 56);
            DPIPartcountLabel.Name = "DPIPartcountLabel";
            DPIPartcountLabel.Size = new System.Drawing.Size(21, 19);
            DPIPartcountLabel.TabIndex = 16;
            DPIPartcountLabel.Text = "--";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(291, 81);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(85, 19);
            label16.TabIndex = 15;
            label16.Text = "Total Quantity:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(68, 84);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(60, 19);
            label17.TabIndex = 14;
            label17.Text = "Total Box:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(68, 56);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(113, 19);
            label18.TabIndex = 13;
            label18.Text = "Part number Count:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(44, 21);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(145, 19);
            label10.TabIndex = 12;
            label10.Text = "DPI Uploaded Information:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(44, 129);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(141, 19);
            label9.TabIndex = 11;
            label9.Text = "FG Uploaded Information:";
            // 
            // CustomerLabel
            // 
            CustomerLabel.AutoSize = true;
            CustomerLabel.Location = new System.Drawing.Point(365, 165);
            CustomerLabel.Name = "CustomerLabel";
            CustomerLabel.Size = new System.Drawing.Size(21, 19);
            CustomerLabel.TabIndex = 10;
            CustomerLabel.Text = "--";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(291, 165);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(62, 19);
            label4.TabIndex = 9;
            label4.Text = "Customer:";
            // 
            // QuantityLabel
            // 
            QuantityLabel.AutoSize = true;
            QuantityLabel.Location = new System.Drawing.Point(420, 190);
            QuantityLabel.Name = "QuantityLabel";
            QuantityLabel.Size = new System.Drawing.Size(21, 19);
            QuantityLabel.TabIndex = 8;
            QuantityLabel.Text = "--";
            // 
            // BoxLabel
            // 
            BoxLabel.AutoSize = true;
            BoxLabel.Location = new System.Drawing.Point(197, 190);
            BoxLabel.Name = "BoxLabel";
            BoxLabel.Size = new System.Drawing.Size(21, 19);
            BoxLabel.TabIndex = 7;
            BoxLabel.Text = "--";
            // 
            // PartcountLabel
            // 
            PartcountLabel.AutoSize = true;
            PartcountLabel.Location = new System.Drawing.Point(197, 165);
            PartcountLabel.Name = "PartcountLabel";
            PartcountLabel.Size = new System.Drawing.Size(21, 19);
            PartcountLabel.TabIndex = 6;
            PartcountLabel.Text = "--";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(291, 190);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(85, 19);
            label7.TabIndex = 5;
            label7.Text = "Total Quantity:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(68, 193);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(60, 19);
            label5.TabIndex = 4;
            label5.Text = "Total Box:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(68, 165);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(113, 19);
            label8.TabIndex = 3;
            label8.Text = "Part number Count:";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label20.Location = new System.Drawing.Point(544, 326);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(55, 23);
            label20.TabIndex = 19;
            label20.Text = "FG List:";
            // 
            // ShipmenTable
            // 
            ShipmenTable.AllowUserToAddRows = false;
            ShipmenTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ShipmenTable.BackgroundColor = System.Drawing.Color.White;
            ShipmenTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ShipmenTable.Location = new System.Drawing.Point(542, 355);
            ShipmenTable.Name = "ShipmenTable";
            ShipmenTable.Size = new System.Drawing.Size(541, 263);
            ShipmenTable.TabIndex = 18;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label19.Location = new System.Drawing.Point(542, 12);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(60, 23);
            label19.TabIndex = 17;
            label19.Text = "DPI List:";
            // 
            // DPITable
            // 
            DPITable.AllowUserToAddRows = false;
            DPITable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            DPITable.BackgroundColor = System.Drawing.Color.White;
            DPITable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DPITable.Location = new System.Drawing.Point(542, 41);
            DPITable.Name = "DPITable";
            DPITable.Size = new System.Drawing.Size(541, 276);
            DPITable.TabIndex = 16;
            DPITable.RowPrePaint += DPITable_RowPrePaint;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 634);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1127, 22);
            statusStrip1.TabIndex = 20;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            toolStripStatusLabel1.Visible = false;
            // 
            // Shipments
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(statusStrip1);
            Controls.Add(label20);
            Controls.Add(ShipmenTable);
            Controls.Add(label19);
            Controls.Add(DPITable);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "Shipments";
            Size = new System.Drawing.Size(1127, 656);
            Load += Shipments_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ShipmenTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)DPITable).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button DPIClearButton;
        private System.Windows.Forms.TextBox DPITextBox;
        private System.Windows.Forms.Button DPIFileButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button GeneratePackingListBtn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button UploadItemButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FileTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox WarehouseComboBox;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label ShipmentIdLabel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label DPITotalQuantityLabel;
        private System.Windows.Forms.Label DPITotalBoxLabel;
        private System.Windows.Forms.Label DPIPartcountLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label CustomerLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label BoxLabel;
        private System.Windows.Forms.Label PartcountLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DataGridView ShipmenTable;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridView DPITable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
