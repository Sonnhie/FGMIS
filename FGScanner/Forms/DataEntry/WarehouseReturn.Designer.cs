namespace FGScanner.Forms.DataEntry
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new System.Windows.Forms.GroupBox();
            LocationComboBox = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            RemarkTextbox = new System.Windows.Forms.RichTextBox();
            label4 = new System.Windows.Forms.Label();
            GenerateReturnSlipBtn = new System.Windows.Forms.Button();
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
            ReturnIdLabel = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            QuantityLabel = new System.Windows.Forms.Label();
            BoxLabel = new System.Windows.Forms.Label();
            PartcountLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            ReturnTable = new System.Windows.Forms.DataGridView();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReturnTable).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LocationComboBox);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(RemarkTextbox);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(GenerateReturnSlipBtn);
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
            groupBox1.Location = new System.Drawing.Point(12, 13);
            groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Size = new System.Drawing.Size(622, 350);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Data Entry";
            // 
            // LocationComboBox
            // 
            LocationComboBox.FormattingEnabled = true;
            LocationComboBox.Items.AddRange(new object[] { "SINA", "SINB", "ASSB", "ASSA" });
            LocationComboBox.Location = new System.Drawing.Point(172, 129);
            LocationComboBox.Name = "LocationComboBox";
            LocationComboBox.Size = new System.Drawing.Size(263, 27);
            LocationComboBox.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(45, 133);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(115, 19);
            label6.TabIndex = 16;
            label6.Text = "To Storage Location:";
            // 
            // RemarkTextbox
            // 
            RemarkTextbox.Location = new System.Drawing.Point(171, 173);
            RemarkTextbox.Name = "RemarkTextbox";
            RemarkTextbox.Size = new System.Drawing.Size(418, 66);
            RemarkTextbox.TabIndex = 15;
            RemarkTextbox.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(101, 173);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(59, 19);
            label4.TabIndex = 14;
            label4.Text = "Remarks:";
            // 
            // GenerateReturnSlipBtn
            // 
            GenerateReturnSlipBtn.Location = new System.Drawing.Point(171, 300);
            GenerateReturnSlipBtn.Name = "GenerateReturnSlipBtn";
            GenerateReturnSlipBtn.Size = new System.Drawing.Size(92, 27);
            GenerateReturnSlipBtn.TabIndex = 13;
            GenerateReturnSlipBtn.Text = "Generate";
            GenerateReturnSlipBtn.UseVisualStyleBackColor = true;
            GenerateReturnSlipBtn.Click += GenerateReturnSlipBtn_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(89, 304);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(71, 19);
            label11.TabIndex = 12;
            label11.Text = "Return Slip:";
            // 
            // ClearButton
            // 
            ClearButton.Location = new System.Drawing.Point(441, 128);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new System.Drawing.Size(92, 27);
            ClearButton.TabIndex = 11;
            ClearButton.Text = "Clear Upload";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // UploadItemButton
            // 
            UploadItemButton.Location = new System.Drawing.Point(171, 258);
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
            label2.Location = new System.Drawing.Point(112, 263);
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
            label3.Location = new System.Drawing.Point(43, 47);
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
            label1.Location = new System.Drawing.Point(90, 96);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 19);
            label1.TabIndex = 0;
            label1.Text = "File upload:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ReturnIdLabel);
            groupBox3.Controls.Add(label21);
            groupBox3.Controls.Add(QuantityLabel);
            groupBox3.Controls.Add(BoxLabel);
            groupBox3.Controls.Add(PartcountLabel);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label8);
            groupBox3.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox3.Location = new System.Drawing.Point(659, 25);
            groupBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox3.Size = new System.Drawing.Size(442, 338);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Information:";
            // 
            // ReturnIdLabel
            // 
            ReturnIdLabel.AutoSize = true;
            ReturnIdLabel.Location = new System.Drawing.Point(154, 107);
            ReturnIdLabel.Name = "ReturnIdLabel";
            ReturnIdLabel.Size = new System.Drawing.Size(21, 19);
            ReturnIdLabel.TabIndex = 22;
            ReturnIdLabel.Text = "--";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(26, 107);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(60, 19);
            label21.TabIndex = 21;
            label21.Text = "Return Id:";
            // 
            // QuantityLabel
            // 
            QuantityLabel.AutoSize = true;
            QuantityLabel.Location = new System.Drawing.Point(374, 49);
            QuantityLabel.Name = "QuantityLabel";
            QuantityLabel.Size = new System.Drawing.Size(21, 19);
            QuantityLabel.TabIndex = 8;
            QuantityLabel.Text = "--";
            // 
            // BoxLabel
            // 
            BoxLabel.AutoSize = true;
            BoxLabel.Location = new System.Drawing.Point(154, 74);
            BoxLabel.Name = "BoxLabel";
            BoxLabel.Size = new System.Drawing.Size(21, 19);
            BoxLabel.TabIndex = 7;
            BoxLabel.Text = "--";
            // 
            // PartcountLabel
            // 
            PartcountLabel.AutoSize = true;
            PartcountLabel.Location = new System.Drawing.Point(154, 49);
            PartcountLabel.Name = "PartcountLabel";
            PartcountLabel.Size = new System.Drawing.Size(21, 19);
            PartcountLabel.TabIndex = 6;
            PartcountLabel.Text = "--";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(245, 49);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(85, 19);
            label7.TabIndex = 5;
            label7.Text = "Total Quantity:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(25, 77);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(60, 19);
            label5.TabIndex = 4;
            label5.Text = "Total Box:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(25, 49);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(113, 19);
            label8.TabIndex = 3;
            label8.Text = "Part number Count:";
            // 
            // ReturnTable
            // 
            ReturnTable.AllowUserToAddRows = false;
            ReturnTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ReturnTable.BackgroundColor = System.Drawing.Color.White;
            ReturnTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReturnTable.Location = new System.Drawing.Point(12, 369);
            ReturnTable.Name = "ReturnTable";
            ReturnTable.Size = new System.Drawing.Size(1089, 252);
            ReturnTable.TabIndex = 16;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 634);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1127, 22);
            statusStrip1.TabIndex = 18;
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
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // WarehouseReturn
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(statusStrip1);
            Controls.Add(ReturnTable);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Name = "WarehouseReturn";
            Size = new System.Drawing.Size(1127, 656);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReturnTable).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox LocationComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox RemarkTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GenerateReturnSlipBtn;
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
        private System.Windows.Forms.Label ReturnIdLabel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label BoxLabel;
        private System.Windows.Forms.Label PartcountLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView ReturnTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}
