namespace FGScanner
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSignIn = new System.Windows.Forms.Button();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtUserId = new System.Windows.Forms.TextBox();
            this.version_lbl = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(382, 337);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.BtnSignIn);
            this.panel1.Controls.Add(this.TxtPassword);
            this.panel1.Controls.Add(this.TxtUserId);
            this.panel1.Controls.Add(this.version_lbl);
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(382, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(314, 337);
            this.panel1.TabIndex = 1;
            // 
            // BtnSignIn
            // 
            this.BtnSignIn.Location = new System.Drawing.Point(108, 254);
            this.BtnSignIn.Name = "BtnSignIn";
            this.BtnSignIn.Size = new System.Drawing.Size(102, 35);
            this.BtnSignIn.TabIndex = 17;
            this.BtnSignIn.Text = "Login";
            this.BtnSignIn.UseVisualStyleBackColor = true;
            this.BtnSignIn.Click += new System.EventHandler(this.BtnSignIn_Click_1);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(30, 200);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(255, 22);
            this.TxtPassword.TabIndex = 16;
            // 
            // TxtUserId
            // 
            this.TxtUserId.Location = new System.Drawing.Point(30, 115);
            this.TxtUserId.Name = "TxtUserId";
            this.TxtUserId.Size = new System.Drawing.Size(255, 22);
            this.TxtUserId.TabIndex = 15;
            // 
            // version_lbl
            // 
            this.version_lbl.AutoSize = true;
            this.version_lbl.Location = new System.Drawing.Point(9, 7);
            this.version_lbl.Name = "version_lbl";
            this.version_lbl.Size = new System.Drawing.Size(38, 13);
            this.version_lbl.TabIndex = 14;
            this.version_lbl.Text = "label4";
            // 
            // CloseBtn
            // 
            this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
            this.CloseBtn.Location = new System.Drawing.Point(292, 5);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(15, 15);
            this.CloseBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CloseBtn.TabIndex = 13;
            this.CloseBtn.TabStop = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "User ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 30);
            this.label1.TabIndex = 7;
            this.label1.Text = "Login";
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 337);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.Load += new System.EventHandler(this.login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox CloseBtn;
        private System.Windows.Forms.Label version_lbl;
        private System.Windows.Forms.TextBox TxtUserId;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Button BtnSignIn;
    }
}