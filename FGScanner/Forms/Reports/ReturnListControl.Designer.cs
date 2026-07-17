namespace FGScanner.Forms.Reports
{
    partial class ReturnListControl
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
            SearchButton = new System.Windows.Forms.Button();
            TransferTocomboBox = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            EndDate = new System.Windows.Forms.DateTimePicker();
            label1 = new System.Windows.Forms.Label();
            StartDate = new System.Windows.Forms.DateTimePicker();
            label3 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            returnBoxLabel = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            ReturnQuantityLabel = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            ReturnTimeLabel = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            ReturnItemLabel = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            TransferLabel = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            ReturnDate = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            ReturnIDLabel = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ReturnTable = new System.Windows.Forms.DataGridView();
            label5 = new System.Windows.Forms.Label();
            ReturnItemTable = new System.Windows.Forms.DataGridView();
            CancelReturnButton = new System.Windows.Forms.Button();
            GenerateSlipbutton = new System.Windows.Forms.Button();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReturnTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ReturnItemTable).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SearchButton);
            groupBox1.Controls.Add(TransferTocomboBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(EndDate);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(StartDate);
            groupBox1.Controls.Add(label3);
            groupBox1.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(27, 21);
            groupBox1.Margin = new System.Windows.Forms.Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4);
            groupBox1.Size = new System.Drawing.Size(469, 247);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filter";
            // 
            // SearchButton
            // 
            SearchButton.Location = new System.Drawing.Point(112, 185);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new System.Drawing.Size(171, 36);
            SearchButton.TabIndex = 17;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            // 
            // TransferTocomboBox
            // 
            TransferTocomboBox.FormattingEnabled = true;
            TransferTocomboBox.Items.AddRange(new object[] { "SINA", "ASSA", "SINB", "ASSB", "MOLD" });
            TransferTocomboBox.Location = new System.Drawing.Point(112, 139);
            TransferTocomboBox.Name = "TransferTocomboBox";
            TransferTocomboBox.Size = new System.Drawing.Size(318, 26);
            TransferTocomboBox.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(38, 147);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(65, 18);
            label2.TabIndex = 15;
            label2.Text = "Transfer to:";
            // 
            // EndDate
            // 
            EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            EndDate.Location = new System.Drawing.Point(111, 85);
            EndDate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            EndDate.Name = "EndDate";
            EndDate.Size = new System.Drawing.Size(319, 26);
            EndDate.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(51, 93);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(54, 18);
            label1.TabIndex = 13;
            label1.Text = "End date:";
            // 
            // StartDate
            // 
            StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            StartDate.Location = new System.Drawing.Point(112, 33);
            StartDate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            StartDate.Name = "StartDate";
            StartDate.Size = new System.Drawing.Size(319, 26);
            StartDate.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(43, 41);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(61, 18);
            label3.TabIndex = 11;
            label3.Text = "Start date:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(returnBoxLabel);
            groupBox2.Controls.Add(label19);
            groupBox2.Controls.Add(ReturnQuantityLabel);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(ReturnTimeLabel);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(ReturnItemLabel);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(TransferLabel);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(ReturnDate);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(ReturnIDLabel);
            groupBox2.Controls.Add(label6);
            groupBox2.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(27, 290);
            groupBox2.Margin = new System.Windows.Forms.Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4);
            groupBox2.Size = new System.Drawing.Size(469, 289);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Return Information";
            // 
            // returnBoxLabel
            // 
            returnBoxLabel.AutoSize = true;
            returnBoxLabel.Location = new System.Drawing.Point(132, 237);
            returnBoxLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            returnBoxLabel.Name = "returnBoxLabel";
            returnBoxLabel.Size = new System.Drawing.Size(23, 18);
            returnBoxLabel.TabIndex = 25;
            returnBoxLabel.Text = "---";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(24, 237);
            label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(55, 18);
            label19.TabIndex = 24;
            label19.Text = "Total Box:";
            // 
            // ReturnQuantityLabel
            // 
            ReturnQuantityLabel.AutoSize = true;
            ReturnQuantityLabel.Location = new System.Drawing.Point(132, 206);
            ReturnQuantityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ReturnQuantityLabel.Name = "ReturnQuantityLabel";
            ReturnQuantityLabel.Size = new System.Drawing.Size(23, 18);
            ReturnQuantityLabel.TabIndex = 23;
            ReturnQuantityLabel.Text = "---";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(24, 206);
            label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(78, 18);
            label17.TabIndex = 22;
            label17.Text = "Total Quantity:";
            // 
            // ReturnTimeLabel
            // 
            ReturnTimeLabel.AutoSize = true;
            ReturnTimeLabel.Location = new System.Drawing.Point(114, 137);
            ReturnTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ReturnTimeLabel.Name = "ReturnTimeLabel";
            ReturnTimeLabel.Size = new System.Drawing.Size(23, 18);
            ReturnTimeLabel.TabIndex = 21;
            ReturnTimeLabel.Text = "---";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(24, 137);
            label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(70, 18);
            label15.TabIndex = 20;
            label15.Text = "Return time:";
            // 
            // ReturnItemLabel
            // 
            ReturnItemLabel.AutoSize = true;
            ReturnItemLabel.Location = new System.Drawing.Point(132, 175);
            ReturnItemLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ReturnItemLabel.Name = "ReturnItemLabel";
            ReturnItemLabel.Size = new System.Drawing.Size(23, 18);
            ReturnItemLabel.TabIndex = 19;
            ReturnItemLabel.Text = "---";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(24, 175);
            label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(64, 18);
            label13.TabIndex = 18;
            label13.Text = "Item count:";
            // 
            // TransferLabel
            // 
            TransferLabel.AutoSize = true;
            TransferLabel.Location = new System.Drawing.Point(312, 61);
            TransferLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            TransferLabel.Name = "TransferLabel";
            TransferLabel.Size = new System.Drawing.Size(23, 18);
            TransferLabel.TabIndex = 17;
            TransferLabel.Text = "---";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(222, 61);
            label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(65, 18);
            label10.TabIndex = 16;
            label10.Text = "Transfer To:";
            // 
            // ReturnDate
            // 
            ReturnDate.AutoSize = true;
            ReturnDate.Location = new System.Drawing.Point(114, 97);
            ReturnDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ReturnDate.Name = "ReturnDate";
            ReturnDate.Size = new System.Drawing.Size(23, 18);
            ReturnDate.TabIndex = 15;
            ReturnDate.Text = "---";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(24, 97);
            label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(70, 18);
            label8.TabIndex = 14;
            label8.Text = "Return date:";
            // 
            // ReturnIDLabel
            // 
            ReturnIDLabel.AutoSize = true;
            ReturnIDLabel.Location = new System.Drawing.Point(114, 61);
            ReturnIDLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            ReturnIDLabel.Name = "ReturnIDLabel";
            ReturnIDLabel.Size = new System.Drawing.Size(23, 18);
            ReturnIDLabel.TabIndex = 13;
            ReturnIDLabel.Text = "---";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(24, 61);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(57, 18);
            label6.TabIndex = 12;
            label6.Text = "Return ID:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(511, 7);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(99, 16);
            label4.TabIndex = 18;
            label4.Text = "Return Document List";
            // 
            // ReturnTable
            // 
            ReturnTable.AllowUserToAddRows = false;
            ReturnTable.BackgroundColor = System.Drawing.Color.White;
            ReturnTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReturnTable.Location = new System.Drawing.Point(511, 31);
            ReturnTable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ReturnTable.Name = "ReturnTable";
            ReturnTable.Size = new System.Drawing.Size(567, 237);
            ReturnTable.TabIndex = 17;
            ReturnTable.CellContentClick += ReturnTable_CellContentClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(511, 276);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(61, 16);
            label5.TabIndex = 20;
            label5.Text = "Return Items";
            // 
            // ReturnItemTable
            // 
            ReturnItemTable.AllowUserToAddRows = false;
            ReturnItemTable.BackgroundColor = System.Drawing.Color.White;
            ReturnItemTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReturnItemTable.Location = new System.Drawing.Point(511, 299);
            ReturnItemTable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ReturnItemTable.Name = "ReturnItemTable";
            ReturnItemTable.Size = new System.Drawing.Size(567, 280);
            ReturnItemTable.TabIndex = 19;
            // 
            // CancelReturnButton
            // 
            CancelReturnButton.Location = new System.Drawing.Point(624, 585);
            CancelReturnButton.Name = "CancelReturnButton";
            CancelReturnButton.Size = new System.Drawing.Size(98, 35);
            CancelReturnButton.TabIndex = 22;
            CancelReturnButton.Text = "Cancel Return";
            CancelReturnButton.UseVisualStyleBackColor = true;
            CancelReturnButton.Click += CancelReturnButton_Click;
            // 
            // GenerateSlipbutton
            // 
            GenerateSlipbutton.Location = new System.Drawing.Point(510, 585);
            GenerateSlipbutton.Name = "GenerateSlipbutton";
            GenerateSlipbutton.Size = new System.Drawing.Size(108, 35);
            GenerateSlipbutton.TabIndex = 21;
            GenerateSlipbutton.Text = "Generate Slip";
            GenerateSlipbutton.UseVisualStyleBackColor = true;
            GenerateSlipbutton.Click += GenerateSlipbutton_Click;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // ReturnListControl
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(CancelReturnButton);
            Controls.Add(GenerateSlipbutton);
            Controls.Add(label5);
            Controls.Add(ReturnItemTable);
            Controls.Add(label4);
            Controls.Add(ReturnTable);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Name = "ReturnListControl";
            Size = new System.Drawing.Size(1127, 656);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReturnTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)ReturnItemTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ComboBox TransferTocomboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker StartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label returnBoxLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label ReturnQuantityLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label ReturnTimeLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label ReturnItemLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label TransferLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label ReturnDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label ReturnIDLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView ReturnTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView ReturnItemTable;
        private System.Windows.Forms.Button CancelReturnButton;
        private System.Windows.Forms.Button GenerateSlipbutton;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}
