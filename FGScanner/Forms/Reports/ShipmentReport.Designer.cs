namespace FGScanner.Forms.Reports
{
    partial class ShipmentReport
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
            FilterButton = new System.Windows.Forms.Button();
            ShipmentID = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            EndDate = new System.Windows.Forms.DateTimePicker();
            label1 = new System.Windows.Forms.Label();
            StartDate = new System.Windows.Forms.DateTimePicker();
            label3 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            ShipmentDateLabel = new System.Windows.Forms.Label();
            TotalBoxLabel = new System.Windows.Forms.Label();
            TotalQuantityLabel = new System.Windows.Forms.Label();
            ShipmentIDLabel = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            ShipmentTable = new System.Windows.Forms.DataGridView();
            label4 = new System.Windows.Forms.Label();
            ShipmentItemTable = new System.Windows.Forms.DataGridView();
            CancelShipmentButton = new System.Windows.Forms.Button();
            GenerateButton = new System.Windows.Forms.Button();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ShipmentTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ShipmentItemTable).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FilterButton);
            groupBox1.Controls.Add(ShipmentID);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(EndDate);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(StartDate);
            groupBox1.Controls.Add(label3);
            groupBox1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(19, 18);
            groupBox1.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            groupBox1.Size = new System.Drawing.Size(435, 129);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filters";
            // 
            // FilterButton
            // 
            FilterButton.Location = new System.Drawing.Point(345, 80);
            FilterButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            FilterButton.Name = "FilterButton";
            FilterButton.Size = new System.Drawing.Size(67, 26);
            FilterButton.TabIndex = 13;
            FilterButton.Text = "Filter";
            FilterButton.UseVisualStyleBackColor = true;
            FilterButton.Click += FilterButton_Click;
            // 
            // ShipmentID
            // 
            ShipmentID.Location = new System.Drawing.Point(128, 79);
            ShipmentID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ShipmentID.Name = "ShipmentID";
            ShipmentID.Size = new System.Drawing.Size(213, 27);
            ShipmentID.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(46, 84);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(75, 19);
            label2.TabIndex = 11;
            label2.Text = "Shipment ID:";
            // 
            // EndDate
            // 
            EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            EndDate.Location = new System.Drawing.Point(258, 38);
            EndDate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            EndDate.Name = "EndDate";
            EndDate.Size = new System.Drawing.Size(83, 27);
            EndDate.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(229, 46);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(23, 19);
            label1.TabIndex = 9;
            label1.Text = "To:";
            // 
            // StartDate
            // 
            StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            StartDate.Location = new System.Drawing.Point(128, 38);
            StartDate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            StartDate.Name = "StartDate";
            StartDate.Size = new System.Drawing.Size(83, 27);
            StartDate.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(72, 44);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(39, 19);
            label3.TabIndex = 7;
            label3.Text = "From:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ShipmentDateLabel);
            groupBox2.Controls.Add(TotalBoxLabel);
            groupBox2.Controls.Add(TotalQuantityLabel);
            groupBox2.Controls.Add(ShipmentIDLabel);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(470, 18);
            groupBox2.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            groupBox2.Size = new System.Drawing.Size(484, 129);
            groupBox2.TabIndex = 19;
            groupBox2.TabStop = false;
            groupBox2.Text = "Shipment Information";
            // 
            // ShipmentDateLabel
            // 
            ShipmentDateLabel.AutoSize = true;
            ShipmentDateLabel.Location = new System.Drawing.Point(397, 36);
            ShipmentDateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ShipmentDateLabel.Name = "ShipmentDateLabel";
            ShipmentDateLabel.Size = new System.Drawing.Size(27, 19);
            ShipmentDateLabel.TabIndex = 19;
            ShipmentDateLabel.Text = "---";
            // 
            // TotalBoxLabel
            // 
            TotalBoxLabel.AutoSize = true;
            TotalBoxLabel.Location = new System.Drawing.Point(361, 77);
            TotalBoxLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            TotalBoxLabel.Name = "TotalBoxLabel";
            TotalBoxLabel.Size = new System.Drawing.Size(27, 19);
            TotalBoxLabel.TabIndex = 18;
            TotalBoxLabel.Text = "---";
            // 
            // TotalQuantityLabel
            // 
            TotalQuantityLabel.AutoSize = true;
            TotalQuantityLabel.Location = new System.Drawing.Point(105, 77);
            TotalQuantityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            TotalQuantityLabel.Name = "TotalQuantityLabel";
            TotalQuantityLabel.Size = new System.Drawing.Size(27, 19);
            TotalQuantityLabel.TabIndex = 17;
            TotalQuantityLabel.Text = "---";
            // 
            // ShipmentIDLabel
            // 
            ShipmentIDLabel.AutoSize = true;
            ShipmentIDLabel.Location = new System.Drawing.Point(105, 36);
            ShipmentIDLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ShipmentIDLabel.Name = "ShipmentIDLabel";
            ShipmentIDLabel.Size = new System.Drawing.Size(27, 19);
            ShipmentIDLabel.TabIndex = 16;
            ShipmentIDLabel.Text = "---";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(285, 36);
            label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(89, 19);
            label9.TabIndex = 15;
            label9.Text = "Shipment Date:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(290, 77);
            label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(60, 19);
            label8.TabIndex = 14;
            label8.Text = "Total Box:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(16, 77);
            label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(85, 19);
            label7.TabIndex = 13;
            label7.Text = "Total Quantity:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(23, 36);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(75, 19);
            label6.TabIndex = 12;
            label6.Text = "Shipment ID:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label5.Location = new System.Drawing.Point(19, 149);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(84, 19);
            label5.TabIndex = 21;
            label5.Text = "Document List";
            // 
            // ShipmentTable
            // 
            ShipmentTable.AllowUserToAddRows = false;
            ShipmentTable.BackgroundColor = System.Drawing.Color.White;
            ShipmentTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ShipmentTable.Location = new System.Drawing.Point(19, 171);
            ShipmentTable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ShipmentTable.Name = "ShipmentTable";
            ShipmentTable.Size = new System.Drawing.Size(537, 356);
            ShipmentTable.TabIndex = 20;
            ShipmentTable.CellContentClick += ShipmentTable_CellContentClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(575, 148);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(85, 19);
            label4.TabIndex = 23;
            label4.Text = "Shipment Item";
            // 
            // ShipmentItemTable
            // 
            ShipmentItemTable.AllowUserToAddRows = false;
            ShipmentItemTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ShipmentItemTable.BackgroundColor = System.Drawing.Color.White;
            ShipmentItemTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ShipmentItemTable.Location = new System.Drawing.Point(575, 171);
            ShipmentItemTable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ShipmentItemTable.Name = "ShipmentItemTable";
            ShipmentItemTable.Size = new System.Drawing.Size(383, 358);
            ShipmentItemTable.TabIndex = 22;
            // 
            // CancelShipmentButton
            // 
            CancelShipmentButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            CancelShipmentButton.Location = new System.Drawing.Point(694, 535);
            CancelShipmentButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            CancelShipmentButton.Name = "CancelShipmentButton";
            CancelShipmentButton.Size = new System.Drawing.Size(95, 26);
            CancelShipmentButton.TabIndex = 25;
            CancelShipmentButton.Text = "Cancel Shipment";
            CancelShipmentButton.UseVisualStyleBackColor = true;
            CancelShipmentButton.Click += CancelShipmentButton_Click;
            // 
            // GenerateButton
            // 
            GenerateButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            GenerateButton.Location = new System.Drawing.Point(575, 535);
            GenerateButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            GenerateButton.Name = "GenerateButton";
            GenerateButton.Size = new System.Drawing.Size(115, 26);
            GenerateButton.TabIndex = 24;
            GenerateButton.Text = "Generate Packing List";
            GenerateButton.UseVisualStyleBackColor = true;
            GenerateButton.Click += GenerateButton_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 572);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            statusStrip1.Size = new System.Drawing.Size(980, 22);
            statusStrip1.TabIndex = 26;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(71, 16);
            toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            toolStripStatusLabel1.Visible = false;
            // 
            // ShipmentReport
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(statusStrip1);
            Controls.Add(CancelShipmentButton);
            Controls.Add(GenerateButton);
            Controls.Add(label4);
            Controls.Add(ShipmentItemTable);
            Controls.Add(label5);
            Controls.Add(ShipmentTable);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Name = "ShipmentReport";
            Size = new System.Drawing.Size(980, 594);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ShipmentTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)ShipmentItemTable).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button FilterButton;
        private System.Windows.Forms.TextBox ShipmentID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker StartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ShipmentDateLabel;
        private System.Windows.Forms.Label TotalBoxLabel;
        private System.Windows.Forms.Label TotalQuantityLabel;
        private System.Windows.Forms.Label ShipmentIDLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView ShipmentTable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView ShipmentItemTable;
        private System.Windows.Forms.Button CancelShipmentButton;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
