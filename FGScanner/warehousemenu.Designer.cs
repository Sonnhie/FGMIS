namespace FGScanner
{
    partial class warehousemenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(warehousemenu));
            this.BtnDataEntryIn = new System.Windows.Forms.Button();
            this.BtnDataEntryOut = new System.Windows.Forms.Button();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.BtnReport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BackBtn = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnDataEntryIn
            // 
            this.BtnDataEntryIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDataEntryIn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDataEntryIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDataEntryIn.Location = new System.Drawing.Point(60, 57);
            this.BtnDataEntryIn.Name = "BtnDataEntryIn";
            this.BtnDataEntryIn.Size = new System.Drawing.Size(143, 44);
            this.BtnDataEntryIn.TabIndex = 0;
            this.BtnDataEntryIn.Text = "Data Entry (IN)";
            this.BtnDataEntryIn.UseVisualStyleBackColor = true;
            this.BtnDataEntryIn.Click += new System.EventHandler(this.BtnDataEntryIn_Click);
            // 
            // BtnDataEntryOut
            // 
            this.BtnDataEntryOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDataEntryOut.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDataEntryOut.Location = new System.Drawing.Point(60, 118);
            this.BtnDataEntryOut.Name = "BtnDataEntryOut";
            this.BtnDataEntryOut.Size = new System.Drawing.Size(143, 44);
            this.BtnDataEntryOut.TabIndex = 1;
            this.BtnDataEntryOut.Text = "Data Entry (OUT)";
            this.BtnDataEntryOut.UseVisualStyleBackColor = true;
            this.BtnDataEntryOut.Click += new System.EventHandler(this.BtnDataEntryOut_Click);
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGenerate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerate.Location = new System.Drawing.Point(60, 181);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(143, 44);
            this.BtnGenerate.TabIndex = 2;
            this.BtnGenerate.Text = "Generate Documents";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            // 
            // BtnReport
            // 
            this.BtnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnReport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReport.Location = new System.Drawing.Point(60, 244);
            this.BtnReport.Name = "BtnReport";
            this.BtnReport.Size = new System.Drawing.Size(143, 44);
            this.BtnReport.TabIndex = 3;
            this.BtnReport.Text = "Inventory";
            this.BtnReport.UseVisualStyleBackColor = true;
            this.BtnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.BackBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 25);
            this.panel1.TabIndex = 5;
            // 
            // BackBtn
            // 
            this.BackBtn.BackColor = System.Drawing.Color.Transparent;
            this.BackBtn.Image = ((System.Drawing.Image)(resources.GetObject("BackBtn.Image")));
            this.BackBtn.Location = new System.Drawing.Point(237, 4);
            this.BackBtn.Margin = new System.Windows.Forms.Padding(2);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(13, 15);
            this.BackBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackBtn.TabIndex = 2;
            this.BackBtn.TabStop = false;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(60, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 44);
            this.button1.TabIndex = 6;
            this.button1.Text = "QR Generation Tool";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // warehousemenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 397);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnReport);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.BtnDataEntryOut);
            this.Controls.Add(this.BtnDataEntryIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "warehousemenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "warehousemenu";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BackBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnDataEntryIn;
        private System.Windows.Forms.Button BtnDataEntryOut;
        private System.Windows.Forms.Button BtnGenerate;
        private System.Windows.Forms.Button BtnReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox BackBtn;
        private System.Windows.Forms.Button button1;
    }
}