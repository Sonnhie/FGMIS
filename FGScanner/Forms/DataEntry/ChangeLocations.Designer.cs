namespace FGScanner.Forms.DataEntry
{
    partial class ChangeLocations
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
            label3 = new System.Windows.Forms.Label();
            newLocationComboBox = new System.Windows.Forms.ComboBox();
            currLocationComboBox = new System.Windows.Forms.ComboBox();
            WarehouseComboBox = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            TransferButton = new System.Windows.Forms.Button();
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
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RackTable).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(newLocationComboBox);
            groupBox1.Controls.Add(currLocationComboBox);
            groupBox1.Controls.Add(WarehouseComboBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(TransferButton);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(18, 20);
            groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox1.Size = new System.Drawing.Size(484, 213);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Data Entry";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(47, 47);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(117, 19);
            label3.TabIndex = 7;
            label3.Text = "Select WarehouseID:";
            // 
            // newLocationComboBox
            // 
            newLocationComboBox.FormattingEnabled = true;
            newLocationComboBox.Items.AddRange(new object[] { "WH1", "WH2" });
            newLocationComboBox.Location = new System.Drawing.Point(172, 142);
            newLocationComboBox.Name = "newLocationComboBox";
            newLocationComboBox.Size = new System.Drawing.Size(197, 27);
            newLocationComboBox.TabIndex = 6;
            // 
            // currLocationComboBox
            // 
            currLocationComboBox.FormattingEnabled = true;
            currLocationComboBox.Location = new System.Drawing.Point(172, 87);
            currLocationComboBox.Name = "currLocationComboBox";
            currLocationComboBox.Size = new System.Drawing.Size(197, 27);
            currLocationComboBox.TabIndex = 5;
            currLocationComboBox.SelectedIndexChanged += currLocationComboBox_SelectedIndexChanged;
            // 
            // WarehouseComboBox
            // 
            WarehouseComboBox.FormattingEnabled = true;
            WarehouseComboBox.Items.AddRange(new object[] { "WH1", "WH2" });
            WarehouseComboBox.Location = new System.Drawing.Point(172, 39);
            WarehouseComboBox.Name = "WarehouseComboBox";
            WarehouseComboBox.Size = new System.Drawing.Size(197, 27);
            WarehouseComboBox.TabIndex = 4;
            WarehouseComboBox.SelectedIndexChanged += WarehouseComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(47, 150);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(118, 19);
            label2.TabIndex = 3;
            label2.Text = "Select New Location:";
            // 
            // TransferButton
            // 
            TransferButton.Location = new System.Drawing.Point(375, 142);
            TransferButton.Name = "TransferButton";
            TransferButton.Size = new System.Drawing.Size(92, 27);
            TransferButton.TabIndex = 2;
            TransferButton.Text = "Transfer";
            TransferButton.UseVisualStyleBackColor = true;
            TransferButton.Click += TransferButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(43, 95);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(122, 19);
            label1.TabIndex = 0;
            label1.Text = "Select Rack Location:";
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
            groupBox2.Location = new System.Drawing.Point(518, 20);
            groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            groupBox2.Size = new System.Drawing.Size(431, 213);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Rack Information:";
            // 
            // CustomerLabel
            // 
            CustomerLabel.AutoSize = true;
            CustomerLabel.Location = new System.Drawing.Point(334, 47);
            CustomerLabel.Name = "CustomerLabel";
            CustomerLabel.Size = new System.Drawing.Size(21, 19);
            CustomerLabel.TabIndex = 10;
            CustomerLabel.Text = "--";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(260, 47);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(62, 19);
            label4.TabIndex = 9;
            label4.Text = "Customer:";
            // 
            // QuantityLabel
            // 
            QuantityLabel.AutoSize = true;
            QuantityLabel.Location = new System.Drawing.Point(166, 142);
            QuantityLabel.Name = "QuantityLabel";
            QuantityLabel.Size = new System.Drawing.Size(21, 19);
            QuantityLabel.TabIndex = 8;
            QuantityLabel.Text = "--";
            // 
            // BoxLabel
            // 
            BoxLabel.AutoSize = true;
            BoxLabel.Location = new System.Drawing.Point(166, 92);
            BoxLabel.Name = "BoxLabel";
            BoxLabel.Size = new System.Drawing.Size(21, 19);
            BoxLabel.TabIndex = 7;
            BoxLabel.Text = "--";
            // 
            // PartcountLabel
            // 
            PartcountLabel.AutoSize = true;
            PartcountLabel.Location = new System.Drawing.Point(166, 47);
            PartcountLabel.Name = "PartcountLabel";
            PartcountLabel.Size = new System.Drawing.Size(21, 19);
            PartcountLabel.TabIndex = 6;
            PartcountLabel.Text = "--";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(37, 142);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(85, 19);
            label7.TabIndex = 5;
            label7.Text = "Total Quantity:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(37, 95);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(60, 19);
            label6.TabIndex = 4;
            label6.Text = "Total Box:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(37, 47);
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
            RackTable.Location = new System.Drawing.Point(18, 239);
            RackTable.Name = "RackTable";
            RackTable.Size = new System.Drawing.Size(931, 331);
            RackTable.TabIndex = 3;
            // 
            // ChangeLocations
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(RackTable);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "ChangeLocations";
            Size = new System.Drawing.Size(980, 594);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RackTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox newLocationComboBox;
        private System.Windows.Forms.ComboBox currLocationComboBox;
        private System.Windows.Forms.ComboBox WarehouseComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button TransferButton;
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
    }
}
