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
            this.TxtPartnumber = new System.Windows.Forms.TextBox();
            this.LogsTable = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.BtnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LogsTable)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtPartnumber
            // 
            this.TxtPartnumber.Location = new System.Drawing.Point(167, 47);
            this.TxtPartnumber.Name = "TxtPartnumber";
            this.TxtPartnumber.Size = new System.Drawing.Size(217, 22);
            this.TxtPartnumber.TabIndex = 19;
            this.TxtPartnumber.TextChanged += new System.EventHandler(this.TxtPartnumber_TextChanged);
            // 
            // LogsTable
            // 
            this.LogsTable.AllowUserToAddRows = false;
            this.LogsTable.AllowUserToDeleteRows = false;
            this.LogsTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.LogsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.LogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogsTable.Location = new System.Drawing.Point(43, 149);
            this.LogsTable.Name = "LogsTable";
            this.LogsTable.RowHeadersWidth = 51;
            this.LogsTable.Size = new System.Drawing.Size(1219, 602);
            this.LogsTable.TabIndex = 18;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 762);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1303, 25);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 19);
            // 
            // BtnExport
            // 
            this.BtnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExport.Location = new System.Drawing.Point(1123, 101);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(139, 37);
            this.BtnExport.TabIndex = 30;
            this.BtnExport.Text = "Export Data";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 28);
            this.label1.TabIndex = 31;
            this.label1.Text = "Part Number:";
            // 
            // Slowmoving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 787);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.TxtPartnumber);
            this.Controls.Add(this.LogsTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Slowmoving";
            this.Text = "Slowmoving";
            this.Load += new System.EventHandler(this.Slowmoving_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogsTable)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.DataGridView LogsTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label1;
    }
}