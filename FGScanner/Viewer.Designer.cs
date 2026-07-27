namespace FGScanner
{
    partial class Viewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            panel2 = new System.Windows.Forms.Panel();
            minimizedBtn = new System.Windows.Forms.PictureBox();
            MaxBtn = new System.Windows.Forms.PictureBox();
            CloseBtn = new System.Windows.Forms.PictureBox();
            TimeLbl = new System.Windows.Forms.Label();
            DateLbl = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)minimizedBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CloseBtn).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.RoyalBlue;
            panel2.Controls.Add(minimizedBtn);
            panel2.Controls.Add(MaxBtn);
            panel2.Controls.Add(CloseBtn);
            panel2.Controls.Add(TimeLbl);
            panel2.Controls.Add(DateLbl);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1054, 46);
            panel2.TabIndex = 3;
            panel2.MouseDown += panel2_MouseDown;
            // 
            // minimizedBtn
            // 
            minimizedBtn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            minimizedBtn.Image = (System.Drawing.Image)resources.GetObject("minimizedBtn.Image");
            minimizedBtn.Location = new System.Drawing.Point(942, 10);
            minimizedBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            minimizedBtn.Name = "minimizedBtn";
            minimizedBtn.Size = new System.Drawing.Size(23, 23);
            minimizedBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            minimizedBtn.TabIndex = 10;
            minimizedBtn.TabStop = false;
            minimizedBtn.Click += minimizedBtn_Click;
            // 
            // MaxBtn
            // 
            MaxBtn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            MaxBtn.Image = (System.Drawing.Image)resources.GetObject("MaxBtn.Image");
            MaxBtn.Location = new System.Drawing.Point(980, 10);
            MaxBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaxBtn.Name = "MaxBtn";
            MaxBtn.Size = new System.Drawing.Size(23, 23);
            MaxBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            MaxBtn.TabIndex = 9;
            MaxBtn.TabStop = false;
            MaxBtn.Click += MaxBtn_Click;
            // 
            // CloseBtn
            // 
            CloseBtn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            CloseBtn.Image = (System.Drawing.Image)resources.GetObject("CloseBtn.Image");
            CloseBtn.Location = new System.Drawing.Point(1017, 10);
            CloseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new System.Drawing.Size(23, 23);
            CloseBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            CloseBtn.TabIndex = 8;
            CloseBtn.TabStop = false;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // TimeLbl
            // 
            TimeLbl.AutoSize = true;
            TimeLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TimeLbl.ForeColor = System.Drawing.Color.White;
            TimeLbl.Location = new System.Drawing.Point(757, 13);
            TimeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TimeLbl.Name = "TimeLbl";
            TimeLbl.Size = new System.Drawing.Size(23, 17);
            TimeLbl.TabIndex = 7;
            TimeLbl.Text = "---";
            // 
            // DateLbl
            // 
            DateLbl.AutoSize = true;
            DateLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            DateLbl.ForeColor = System.Drawing.Color.White;
            DateLbl.Location = new System.Drawing.Point(559, 13);
            DateLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            DateLbl.Name = "DateLbl";
            DateLbl.Size = new System.Drawing.Size(23, 17);
            DateLbl.TabIndex = 6;
            DateLbl.Text = "---";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label5.ForeColor = System.Drawing.Color.White;
            label5.Location = new System.Drawing.Point(328, 13);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(23, 17);
            label5.TabIndex = 5;
            label5.Text = "---";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.White;
            label4.Location = new System.Drawing.Point(696, 13);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(39, 17);
            label4.TabIndex = 4;
            label4.Text = "Time:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.ForeColor = System.Drawing.Color.White;
            label3.Location = new System.Drawing.Point(499, 13);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(38, 17);
            label3.TabIndex = 3;
            label3.Text = "Date:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.ForeColor = System.Drawing.Color.White;
            label2.Location = new System.Drawing.Point(268, 13);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(38, 17);
            label2.TabIndex = 2;
            label2.Text = "User:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(14, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 20);
            label1.TabIndex = 0;
            label1.Text = "FGMIS";
            // 
            // panel1
            // 
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 46);
            panel1.Margin = new System.Windows.Forms.Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1054, 657);
            panel1.TabIndex = 4;
            // 
            // Viewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1054, 703);
            Controls.Add(panel1);
            Controls.Add(panel2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(2);
            Name = "Viewer";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Viewer";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)minimizedBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)CloseBtn).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label TimeLbl;
        private System.Windows.Forms.Label DateLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox MaxBtn;
        private System.Windows.Forms.PictureBox CloseBtn;
        private System.Windows.Forms.PictureBox minimizedBtn;
    }
}