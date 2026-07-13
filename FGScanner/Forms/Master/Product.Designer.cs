namespace FGScanner
{
    partial class Product
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
            LblPage = new System.Windows.Forms.Label();
            BtnPrev = new System.Windows.Forms.Button();
            BtnNext = new System.Windows.Forms.Button();
            LogsTable = new System.Windows.Forms.DataGridView();
            addbtn = new System.Windows.Forms.Button();
            TxtPartnumber = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)LogsTable).BeginInit();
            SuspendLayout();
            // 
            // LblPage
            // 
            LblPage.AutoSize = true;
            LblPage.Location = new System.Drawing.Point(36, 675);
            LblPage.Name = "LblPage";
            LblPage.Size = new System.Drawing.Size(74, 18);
            LblPage.TabIndex = 27;
            LblPage.Text = "Page 1 of 300";
            // 
            // BtnPrev
            // 
            BtnPrev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnPrev.Location = new System.Drawing.Point(907, 675);
            BtnPrev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            BtnPrev.Name = "BtnPrev";
            BtnPrev.Size = new System.Drawing.Size(88, 36);
            BtnPrev.TabIndex = 26;
            BtnPrev.Text = "Prev Page";
            BtnPrev.UseVisualStyleBackColor = true;
            BtnPrev.Click += BtnPrev_Click;
            // 
            // BtnNext
            // 
            BtnNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnNext.Location = new System.Drawing.Point(1015, 675);
            BtnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            BtnNext.Name = "BtnNext";
            BtnNext.Size = new System.Drawing.Size(88, 36);
            BtnNext.TabIndex = 25;
            BtnNext.Text = "Next Page";
            BtnNext.UseVisualStyleBackColor = true;
            BtnNext.Click += BtnNext_Click;
            // 
            // LogsTable
            // 
            LogsTable.AllowUserToAddRows = false;
            LogsTable.AllowUserToDeleteRows = false;
            LogsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LogsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            LogsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            LogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogsTable.Location = new System.Drawing.Point(36, 82);
            LogsTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            LogsTable.Name = "LogsTable";
            LogsTable.RowHeadersWidth = 51;
            LogsTable.Size = new System.Drawing.Size(1069, 585);
            LogsTable.TabIndex = 22;
            // 
            // addbtn
            // 
            addbtn.Location = new System.Drawing.Point(1000, 36);
            addbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            addbtn.Name = "addbtn";
            addbtn.Size = new System.Drawing.Size(105, 38);
            addbtn.TabIndex = 28;
            addbtn.Text = "Add new item";
            addbtn.UseVisualStyleBackColor = true;
            addbtn.Click += addbtn_Click;
            // 
            // TxtPartnumber
            // 
            TxtPartnumber.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TxtPartnumber.Location = new System.Drawing.Point(36, 41);
            TxtPartnumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            TxtPartnumber.Name = "TxtPartnumber";
            TxtPartnumber.PlaceholderText = "Search Part Number";
            TxtPartnumber.Size = new System.Drawing.Size(215, 27);
            TxtPartnumber.TabIndex = 30;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(269, 41);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 27);
            button1.TabIndex = 31;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = true;
            // 
            // Product
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1140, 737);
            Controls.Add(button1);
            Controls.Add(TxtPartnumber);
            Controls.Add(addbtn);
            Controls.Add(LblPage);
            Controls.Add(BtnPrev);
            Controls.Add(BtnNext);
            Controls.Add(LogsTable);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "Product";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Product";
            Load += Product_Load;
            ((System.ComponentModel.ISupportInitialize)LogsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblPage;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
        private System.Windows.Forms.DataGridView LogsTable;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.Button button1;
    }
}