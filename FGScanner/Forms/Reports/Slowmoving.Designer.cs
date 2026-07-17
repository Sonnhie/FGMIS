namespace FGScanner
{
    partial class Slowmoving
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
            TxtPartnumber = new System.Windows.Forms.TextBox();
            LogsTable = new System.Windows.Forms.DataGridView();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            BtnExport = new System.Windows.Forms.Button();
            SearchButton = new System.Windows.Forms.Button();
            LblPage = new System.Windows.Forms.Label();
            total_box_lbl = new System.Windows.Forms.Label();
            total_sum = new System.Windows.Forms.Label();
            BtnPrev = new System.Windows.Forms.Button();
            BtnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)LogsTable).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // TxtPartnumber
            // 
            TxtPartnumber.Location = new System.Drawing.Point(33, 29);
            TxtPartnumber.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            TxtPartnumber.Name = "TxtPartnumber";
            TxtPartnumber.PlaceholderText = "Search Partnumber. customer, production version";
            TxtPartnumber.Size = new System.Drawing.Size(207, 23);
            TxtPartnumber.TabIndex = 19;
            // 
            // LogsTable
            // 
            LogsTable.AllowUserToAddRows = false;
            LogsTable.AllowUserToDeleteRows = false;
            LogsTable.AllowUserToResizeRows = false;
            LogsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LogsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            LogsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            LogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogsTable.Location = new System.Drawing.Point(26, 76);
            LogsTable.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            LogsTable.Name = "LogsTable";
            LogsTable.RowHeadersWidth = 51;
            LogsTable.Size = new System.Drawing.Size(1087, 557);
            LogsTable.TabIndex = 18;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 712);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            statusStrip1.Size = new System.Drawing.Size(1140, 25);
            statusStrip1.TabIndex = 29;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(63, 19);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 20);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // BtnExport
            // 
            BtnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnExport.Location = new System.Drawing.Point(1027, 22);
            BtnExport.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            BtnExport.Name = "BtnExport";
            BtnExport.Size = new System.Drawing.Size(86, 37);
            BtnExport.TabIndex = 30;
            BtnExport.Text = "Export Data";
            BtnExport.UseVisualStyleBackColor = true;
            BtnExport.Click += BtnExport_Click;
            // 
            // SearchButton
            // 
            SearchButton.Location = new System.Drawing.Point(243, 27);
            SearchButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new System.Drawing.Size(74, 25);
            SearchButton.TabIndex = 31;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // LblPage
            // 
            LblPage.AutoSize = true;
            LblPage.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            LblPage.Location = new System.Drawing.Point(26, 641);
            LblPage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            LblPage.Name = "LblPage";
            LblPage.Size = new System.Drawing.Size(74, 18);
            LblPage.TabIndex = 32;
            LblPage.Text = "Page 1 of 300";
            // 
            // total_box_lbl
            // 
            total_box_lbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            total_box_lbl.AutoSize = true;
            total_box_lbl.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            total_box_lbl.Location = new System.Drawing.Point(526, 641);
            total_box_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            total_box_lbl.Name = "total_box_lbl";
            total_box_lbl.Size = new System.Drawing.Size(60, 19);
            total_box_lbl.TabIndex = 36;
            total_box_lbl.Text = "Total Box:";
            // 
            // total_sum
            // 
            total_sum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            total_sum.AutoSize = true;
            total_sum.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            total_sum.Location = new System.Drawing.Point(679, 641);
            total_sum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            total_sum.Name = "total_sum";
            total_sum.Size = new System.Drawing.Size(85, 19);
            total_sum.TabIndex = 35;
            total_sum.Text = "Total Quantity:";
            // 
            // BtnPrev
            // 
            BtnPrev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnPrev.Location = new System.Drawing.Point(963, 641);
            BtnPrev.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnPrev.Name = "BtnPrev";
            BtnPrev.Size = new System.Drawing.Size(74, 36);
            BtnPrev.TabIndex = 34;
            BtnPrev.Text = "Prev Page";
            BtnPrev.UseVisualStyleBackColor = true;
            BtnPrev.Click += BtnPrev_Click;
            // 
            // BtnNext
            // 
            BtnNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnNext.Location = new System.Drawing.Point(1041, 641);
            BtnNext.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            BtnNext.Name = "BtnNext";
            BtnNext.Size = new System.Drawing.Size(72, 36);
            BtnNext.TabIndex = 33;
            BtnNext.Text = "Next Page";
            BtnNext.UseVisualStyleBackColor = true;
            BtnNext.Click += BtnNext_Click;
            // 
            // Slowmoving
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(5F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1140, 737);
            Controls.Add(total_box_lbl);
            Controls.Add(total_sum);
            Controls.Add(BtnPrev);
            Controls.Add(BtnNext);
            Controls.Add(LblPage);
            Controls.Add(SearchButton);
            Controls.Add(BtnExport);
            Controls.Add(statusStrip1);
            Controls.Add(TxtPartnumber);
            Controls.Add(LogsTable);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            Name = "Slowmoving";
            Text = "Slowmoving";
            Load += Slowmoving_Load;
            ((System.ComponentModel.ISupportInitialize)LogsTable).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.DataGridView LogsTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label LblPage;
        private System.Windows.Forms.Label total_box_lbl;
        private System.Windows.Forms.Label total_sum;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
    }
}