namespace FGScanner
{
    partial class InventoryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            LogsTable = new System.Windows.Forms.DataGridView();
            TxtPartnumber = new System.Windows.Forms.TextBox();
            BtnExport = new System.Windows.Forms.Button();
            BtnNext = new System.Windows.Forms.Button();
            BtnPrev = new System.Windows.Forms.Button();
            LblPage = new System.Windows.Forms.Label();
            total_sum = new System.Windows.Forms.Label();
            total_box_lbl = new System.Windows.Forms.Label();
            SearchButton = new System.Windows.Forms.Button();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogsTable).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            statusStrip1.Location = new System.Drawing.Point(0, 737);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            statusStrip1.Size = new System.Drawing.Size(1245, 23);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 18);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(71, 17);
            // 
            // LogsTable
            // 
            LogsTable.AllowUserToAddRows = false;
            LogsTable.AllowUserToDeleteRows = false;
            LogsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LogsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            LogsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bahnschrift", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            LogsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            LogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            LogsTable.DefaultCellStyle = dataGridViewCellStyle2;
            LogsTable.Location = new System.Drawing.Point(31, 61);
            LogsTable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            LogsTable.Name = "LogsTable";
            LogsTable.RowHeadersWidth = 51;
            LogsTable.Size = new System.Drawing.Size(1191, 584);
            LogsTable.TabIndex = 2;
            LogsTable.CellContentClick += LogsTable_CellContentClick;
            LogsTable.SelectionChanged += LogsTable_SelectionChanged;
            // 
            // TxtPartnumber
            // 
            TxtPartnumber.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TxtPartnumber.Location = new System.Drawing.Point(32, 26);
            TxtPartnumber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            TxtPartnumber.Name = "TxtPartnumber";
            TxtPartnumber.PlaceholderText = "Search partnumber";
            TxtPartnumber.Size = new System.Drawing.Size(252, 26);
            TxtPartnumber.TabIndex = 3;
            // 
            // BtnExport
            // 
            BtnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnExport.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            BtnExport.Location = new System.Drawing.Point(383, 23);
            BtnExport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnExport.Name = "BtnExport";
            BtnExport.Size = new System.Drawing.Size(98, 32);
            BtnExport.TabIndex = 18;
            BtnExport.Text = "Export to CSV";
            BtnExport.UseVisualStyleBackColor = true;
            BtnExport.Click += BtnExport_Click;
            // 
            // BtnNext
            // 
            BtnNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnNext.Location = new System.Drawing.Point(1150, 665);
            BtnNext.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnNext.Name = "BtnNext";
            BtnNext.Size = new System.Drawing.Size(72, 36);
            BtnNext.TabIndex = 19;
            BtnNext.Text = "Next Page";
            BtnNext.UseVisualStyleBackColor = true;
            BtnNext.Click += BtnNext_Click;
            // 
            // BtnPrev
            // 
            BtnPrev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnPrev.Location = new System.Drawing.Point(1072, 665);
            BtnPrev.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnPrev.Name = "BtnPrev";
            BtnPrev.Size = new System.Drawing.Size(74, 36);
            BtnPrev.TabIndex = 20;
            BtnPrev.Text = "Prev Page";
            BtnPrev.UseVisualStyleBackColor = true;
            BtnPrev.Click += BtnPrev_Click;
            // 
            // LblPage
            // 
            LblPage.AutoSize = true;
            LblPage.Location = new System.Drawing.Point(31, 665);
            LblPage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            LblPage.Name = "LblPage";
            LblPage.Size = new System.Drawing.Size(59, 14);
            LblPage.TabIndex = 21;
            LblPage.Text = "Page 1 of 300";
            // 
            // total_sum
            // 
            total_sum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            total_sum.AutoSize = true;
            total_sum.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold);
            total_sum.Location = new System.Drawing.Point(815, 665);
            total_sum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            total_sum.Name = "total_sum";
            total_sum.Size = new System.Drawing.Size(85, 19);
            total_sum.TabIndex = 22;
            total_sum.Text = "Total Quantity:";
            // 
            // total_box_lbl
            // 
            total_box_lbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            total_box_lbl.AutoSize = true;
            total_box_lbl.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Bold);
            total_box_lbl.Location = new System.Drawing.Point(682, 665);
            total_box_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            total_box_lbl.Name = "total_box_lbl";
            total_box_lbl.Size = new System.Drawing.Size(60, 19);
            total_box_lbl.TabIndex = 23;
            total_box_lbl.Text = "Total Box:";
            // 
            // SearchButton
            // 
            SearchButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            SearchButton.Location = new System.Drawing.Point(288, 22);
            SearchButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new System.Drawing.Size(77, 33);
            SearchButton.TabIndex = 24;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // InventoryForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(5F, 14F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new System.Drawing.Size(1245, 760);
            Controls.Add(SearchButton);
            Controls.Add(total_box_lbl);
            Controls.Add(BtnExport);
            Controls.Add(total_sum);
            Controls.Add(LblPage);
            Controls.Add(BtnPrev);
            Controls.Add(BtnNext);
            Controls.Add(TxtPartnumber);
            Controls.Add(LogsTable);
            Controls.Add(statusStrip1);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "InventoryForm";
            Text = "InventoryForm";
            Load += InventoryForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView LogsTable;
        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Label LblPage;
        private System.Windows.Forms.Label total_sum;
        private System.Windows.Forms.Label total_box_lbl;
        private System.Windows.Forms.Button SearchButton;
    }
}