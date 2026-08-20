namespace FGScanner.Forms.Master
{
    partial class MasterList
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
            button1 = new System.Windows.Forms.Button();
            TxtPartnumber = new System.Windows.Forms.TextBox();
            LogsTable = new System.Windows.Forms.DataGridView();
            addbtn = new System.Windows.Forms.Button();
            LblPage = new System.Windows.Forms.Label();
            BtnPrev = new System.Windows.Forms.Button();
            BtnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)LogsTable).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            button1.Location = new System.Drawing.Point(244, 19);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 27);
            button1.TabIndex = 33;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = true;
            // 
            // TxtPartnumber
            // 
            TxtPartnumber.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TxtPartnumber.Location = new System.Drawing.Point(23, 19);
            TxtPartnumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            TxtPartnumber.Name = "TxtPartnumber";
            TxtPartnumber.PlaceholderText = "Search Part Number";
            TxtPartnumber.Size = new System.Drawing.Size(215, 27);
            TxtPartnumber.TabIndex = 32;
            // 
            // LogsTable
            // 
            LogsTable.AllowUserToAddRows = false;
            LogsTable.AllowUserToDeleteRows = false;
            LogsTable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LogsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            LogsTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            LogsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogsTable.Location = new System.Drawing.Point(23, 59);
            LogsTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            LogsTable.Name = "LogsTable";
            LogsTable.RowHeadersWidth = 51;
            LogsTable.Size = new System.Drawing.Size(935, 429);
            LogsTable.TabIndex = 34;
            LogsTable.CellContentClick += LogsTable_CellContentClick;
            // 
            // addbtn
            // 
            addbtn.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            addbtn.Location = new System.Drawing.Point(853, 13);
            addbtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            addbtn.Name = "addbtn";
            addbtn.Size = new System.Drawing.Size(105, 38);
            addbtn.TabIndex = 35;
            addbtn.Text = "Add new item";
            addbtn.UseVisualStyleBackColor = true;
            addbtn.Click += addbtn_Click;
            // 
            // LblPage
            // 
            LblPage.AutoSize = true;
            LblPage.Font = new System.Drawing.Font("Bahnschrift Condensed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            LblPage.Location = new System.Drawing.Point(23, 496);
            LblPage.Name = "LblPage";
            LblPage.Size = new System.Drawing.Size(74, 18);
            LblPage.TabIndex = 36;
            LblPage.Text = "Page 1 of 300";
            // 
            // BtnPrev
            // 
            BtnPrev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnPrev.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F);
            BtnPrev.Location = new System.Drawing.Point(762, 496);
            BtnPrev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            BtnPrev.Name = "BtnPrev";
            BtnPrev.Size = new System.Drawing.Size(88, 36);
            BtnPrev.TabIndex = 38;
            BtnPrev.Text = "Prev Page";
            BtnPrev.UseVisualStyleBackColor = true;
            BtnPrev.Click += BtnPrev_Click;
            // 
            // BtnNext
            // 
            BtnNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            BtnNext.Font = new System.Drawing.Font("Bahnschrift Condensed", 9.75F);
            BtnNext.Location = new System.Drawing.Point(870, 496);
            BtnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            BtnNext.Name = "BtnNext";
            BtnNext.Size = new System.Drawing.Size(88, 36);
            BtnNext.TabIndex = 37;
            BtnNext.Text = "Next Page";
            BtnNext.UseVisualStyleBackColor = true;
            BtnNext.Click += BtnNext_Click;
            // 
            // MasterList
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(BtnPrev);
            Controls.Add(BtnNext);
            Controls.Add(LblPage);
            Controls.Add(addbtn);
            Controls.Add(LogsTable);
            Controls.Add(button1);
            Controls.Add(TxtPartnumber);
            Font = new System.Drawing.Font("Bahnschrift Condensed", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Name = "MasterList";
            Size = new System.Drawing.Size(980, 594);
            Load += MasterList_Load;
            ((System.ComponentModel.ISupportInitialize)LogsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TxtPartnumber;
        private System.Windows.Forms.DataGridView LogsTable;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.Label LblPage;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.Button BtnNext;
    }
}
