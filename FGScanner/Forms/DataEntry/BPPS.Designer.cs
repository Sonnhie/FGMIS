namespace FGScanner.Forms.DataEntry
{
    partial class BPPS
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            ClearButton = new System.Windows.Forms.Button();
            UploadItemButton = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            FileTextbox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            WarehouseComboBox = new System.Windows.Forms.ComboBox();
            SelectFileButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            CustomerLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            QuantityLabel = new System.Windows.Forms.Label();
            BoxLabel = new System.Windows.Forms.Label();
            PartcountLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            RackTable = new System.Windows.Forms.DataGridView();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RackTable).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ClearButton);
            groupBox1.Controls.Add(UploadItemButton);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(FileTextbox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(WarehouseComboBox);
            groupBox1.Controls.Add(SelectFileButton);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(16, 15);
            groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Size = new System.Drawing.Size(567, 179);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Data Entry";
            // 
            // ClearButton
            // 
            ClearButton.Location = new System.Drawing.Point(441, 129);
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
            UploadItemButton.Text = "Upload file";
            UploadItemButton.UseVisualStyleBackColor = true;
            UploadItemButton.Click += UploadItemButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(115, 137);
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
            FileTextbox.Size = new System.Drawing.Size(264, 27);
            FileTextbox.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(46, 47);
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
            WarehouseComboBox.Size = new System.Drawing.Size(263, 27);
            WarehouseComboBox.TabIndex = 4;
            // 
            // SelectFileButton
            // 
            SelectFileButton.Location = new System.Drawing.Point(441, 88);
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
            label1.Location = new System.Drawing.Point(93, 96);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 19);
            label1.TabIndex = 0;
            label1.Text = "File upload:";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox2.Controls.Add(CustomerLabel);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(QuantityLabel);
            groupBox2.Controls.Add(BoxLabel);
            groupBox2.Controls.Add(PartcountLabel);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(603, 15);
            groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox2.Size = new System.Drawing.Size(499, 179);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Uploaded Info:";
            // 
            // CustomerLabel
            // 
            CustomerLabel.AutoSize = true;
            CustomerLabel.Location = new System.Drawing.Point(340, 39);
            CustomerLabel.Name = "CustomerLabel";
            CustomerLabel.Size = new System.Drawing.Size(21, 19);
            CustomerLabel.TabIndex = 10;
            CustomerLabel.Text = "--";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(266, 39);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(62, 19);
            label4.TabIndex = 9;
            label4.Text = "Customer:";
            // 
            // QuantityLabel
            // 
            QuantityLabel.AutoSize = true;
            QuantityLabel.Location = new System.Drawing.Point(395, 85);
            QuantityLabel.Name = "QuantityLabel";
            QuantityLabel.Size = new System.Drawing.Size(21, 19);
            QuantityLabel.TabIndex = 8;
            QuantityLabel.Text = "--";
            // 
            // BoxLabel
            // 
            BoxLabel.AutoSize = true;
            BoxLabel.Location = new System.Drawing.Point(172, 85);
            BoxLabel.Name = "BoxLabel";
            BoxLabel.Size = new System.Drawing.Size(21, 19);
            BoxLabel.TabIndex = 7;
            BoxLabel.Text = "--";
            // 
            // PartcountLabel
            // 
            PartcountLabel.AutoSize = true;
            PartcountLabel.Location = new System.Drawing.Point(172, 39);
            PartcountLabel.Name = "PartcountLabel";
            PartcountLabel.Size = new System.Drawing.Size(21, 19);
            PartcountLabel.TabIndex = 6;
            PartcountLabel.Text = "--";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(266, 85);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(85, 19);
            label7.TabIndex = 5;
            label7.Text = "Total Quantity:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(43, 88);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(60, 19);
            label6.TabIndex = 4;
            label6.Text = "Total Box:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(43, 39);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(113, 19);
            label5.TabIndex = 3;
            label5.Text = "Part number Count:";
            // 
            // RackTable
            // 
            RackTable.AllowUserToAddRows = false;
            RackTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RackTable.BackgroundColor = System.Drawing.Color.White;
            RackTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RackTable.Location = new System.Drawing.Point(16, 209);
            RackTable.Name = "RackTable";
            RackTable.Size = new System.Drawing.Size(1086, 404);
            RackTable.TabIndex = 4;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 634);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1127, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // BPPS
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(statusStrip1);
            Controls.Add(RackTable);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "BPPS";
            Size = new System.Drawing.Size(1127, 656);
            Load += BPPS_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RackTable).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button UploadItemButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FileTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox WarehouseComboBox;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label CustomerLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label BoxLabel;
        private System.Windows.Forms.Label PartcountLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView RackTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
