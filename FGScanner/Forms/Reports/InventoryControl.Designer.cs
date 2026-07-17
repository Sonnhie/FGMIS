namespace FGScanner.Forms.Reports
{
    partial class InventoryControl
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
            SearchButton = new System.Windows.Forms.Button();
            BtnExport = new System.Windows.Forms.Button();
            TxtPartnumber = new System.Windows.Forms.TextBox();
            LogsTable = new System.Windows.Forms.DataGridView();
            BtnPrev = new System.Windows.Forms.Button();
            BtnNext = new System.Windows.Forms.Button();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            total_box_lbl = new System.Windows.Forms.Label();
            total_sum = new System.Windows.Forms.Label();
            LblPage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)LogsTable).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // SearchButton
            // 
            SearchButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            SearchButton.Location = new System.Drawing.Point(276, 15);
            SearchButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new System.Drawing.Size(77, 33);
            SearchButton.TabIndex = 27;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // BtnExport
            // 
            BtnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnExport.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            BtnExport.Location = new System.Drawing.Point(371, 16);
            BtnExport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnExport.Name = "BtnExport";
            BtnExport.Size = new System.Drawing.Size(98, 32);
            BtnExport.TabIndex = 26;
            BtnExport.Text = "Export to CSV";
            BtnExport.UseVisualStyleBackColor = true;
            BtnExport.Click += BtnExport_Click;
            // 
            // TxtPartnumber
            // 
            TxtPartnumber.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TxtPartnumber.Location = new System.Drawing.Point(20, 19);
            TxtPartnumber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            TxtPartnumber.Name = "TxtPartnumber";
            TxtPartnumber.PlaceholderText = "Search partnumber";
            TxtPartnumber.Size = new System.Drawing.Size(252, 26);
            TxtPartnumber.TabIndex = 25;
            // 
            // LogsTable
            // 
            LogsTable.AllowUserToAddRows = false;
            LogsTable.AllowUserToDeleteRows = false;
            LogsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LogsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            LogsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            LogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogsTable.Location = new System.Drawing.Point(23, 64);
            LogsTable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            LogsTable.Name = "LogsTable";
            LogsTable.RowHeadersWidth = 51;
            LogsTable.Size = new System.Drawing.Size(1077, 520);
            LogsTable.TabIndex = 28;
            LogsTable.CellContentClick += LogsTable_CellContentClick;
            LogsTable.SelectionChanged += LogsTable_SelectionChanged;
            // 
            // BtnPrev
            // 
            BtnPrev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnPrev.Location = new System.Drawing.Point(950, 590);
            BtnPrev.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnPrev.Name = "BtnPrev";
            BtnPrev.Size = new System.Drawing.Size(74, 36);
            BtnPrev.TabIndex = 30;
            BtnPrev.Text = "Prev Page";
            BtnPrev.UseVisualStyleBackColor = true;
            BtnPrev.Click += BtnPrev_Click;
            // 
            // BtnNext
            // 
            BtnNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnNext.Location = new System.Drawing.Point(1028, 590);
            BtnNext.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnNext.Name = "BtnNext";
            BtnNext.Size = new System.Drawing.Size(72, 36);
            BtnNext.TabIndex = 29;
            BtnNext.Text = "Next Page";
            BtnNext.UseVisualStyleBackColor = true;
            BtnNext.Click += BtnNext_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            statusStrip1.Location = new System.Drawing.Point(0, 634);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            statusStrip1.Size = new System.Drawing.Size(1127, 22);
            statusStrip1.TabIndex = 31;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 18);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            toolStripStatusLabel1.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(71, 17);
            toolStripProgressBar1.Visible = false;
            // 
            // total_box_lbl
            // 
            total_box_lbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            total_box_lbl.AutoSize = true;
            total_box_lbl.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold);
            total_box_lbl.Location = new System.Drawing.Point(637, 590);
            total_box_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            total_box_lbl.Name = "total_box_lbl";
            total_box_lbl.Size = new System.Drawing.Size(60, 19);
            total_box_lbl.TabIndex = 34;
            total_box_lbl.Text = "Total Box:";
            // 
            // total_sum
            // 
            total_sum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            total_sum.AutoSize = true;
            total_sum.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold);
            total_sum.Location = new System.Drawing.Point(770, 590);
            total_sum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            total_sum.Name = "total_sum";
            total_sum.Size = new System.Drawing.Size(85, 19);
            total_sum.TabIndex = 33;
            total_sum.Text = "Total Quantity:";
            // 
            // LblPage
            // 
            LblPage.AutoSize = true;
            LblPage.Location = new System.Drawing.Point(23, 587);
            LblPage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            LblPage.Name = "LblPage";
            LblPage.Size = new System.Drawing.Size(63, 16);
            LblPage.TabIndex = 32;
            LblPage.Text = "Page 1 of 300";
            // 
            // InventoryControl
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(total_box_lbl);
            Controls.Add(total_sum);
            Controls.Add(LblPage);
            Controls.Add(statusStrip1);
            Controls.Add(BtnPrev);
            Controls.Add(BtnNext);
            Controls.Add(LogsTable);
            Controls.Add(SearchButton);
            Controls.Add(BtnExport);
            Controls.Add(TxtPartnumber);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Name = "InventoryControl";
            Size = new System.Drawing.Size(1127, 656);
            Load += InventoryControl_Load;
            ((System.ComponentModel.ISupportInitialize)LogsTable).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.DataGridView LogsTable;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label total_box_lbl;
        private System.Windows.Forms.Label total_sum;
        private System.Windows.Forms.Label LblPage;
    }
}
