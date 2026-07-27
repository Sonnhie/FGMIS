namespace FGScanner
{
    partial class ProductMasterlist
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            ppslbl = new System.Windows.Forms.TextBox();
            customercmb = new System.Windows.Forms.ComboBox();
            partnamelbl = new System.Windows.Forms.TextBox();
            partlbl = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ppslbl);
            groupBox1.Controls.Add(customercmb);
            groupBox1.Controls.Add(partnamelbl);
            groupBox1.Controls.Add(partlbl);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(30, 70);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(572, 242);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Product Information";
            // 
            // ppslbl
            // 
            ppslbl.Location = new System.Drawing.Point(182, 173);
            ppslbl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ppslbl.Name = "ppslbl";
            ppslbl.Size = new System.Drawing.Size(140, 23);
            ppslbl.TabIndex = 7;
            ppslbl.KeyPress += ppslbl_KeyPress;
            // 
            // customercmb
            // 
            customercmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            customercmb.FormattingEnabled = true;
            customercmb.Items.AddRange(new object[] { "EPPI", "CBMP", "BIPH", "IMI", "IONICS", "YAZAKI", "JCM", "KOWA EMORI", "IVOCLAR", "ZAMA", "EXCELITAS", "NCFL" });
            customercmb.Location = new System.Drawing.Point(182, 128);
            customercmb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customercmb.Name = "customercmb";
            customercmb.Size = new System.Drawing.Size(140, 23);
            customercmb.TabIndex = 6;
            // 
            // partnamelbl
            // 
            partnamelbl.Location = new System.Drawing.Point(182, 83);
            partnamelbl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            partnamelbl.Name = "partnamelbl";
            partnamelbl.Size = new System.Drawing.Size(356, 23);
            partnamelbl.TabIndex = 5;
            // 
            // partlbl
            // 
            partlbl.Location = new System.Drawing.Point(182, 38);
            partlbl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            partlbl.Name = "partlbl";
            partlbl.Size = new System.Drawing.Size(356, 23);
            partlbl.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(61, 182);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(30, 15);
            label4.TabIndex = 3;
            label4.Text = "PPS:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.Location = new System.Drawing.Point(58, 137);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(62, 15);
            label3.TabIndex = 2;
            label3.Text = "Customer:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(61, 92);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(66, 15);
            label2.TabIndex = 1;
            label2.Text = "Part Name:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(61, 47);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "Part number: ";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(378, 339);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(108, 32);
            button1.TabIndex = 2;
            button1.Text = "Add Item";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(493, 339);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(108, 32);
            button2.TabIndex = 3;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label5.Location = new System.Drawing.Point(24, 10);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(206, 25);
            label5.TabIndex = 4;
            label5.Text = "Register Part Number";
            // 
            // ProductMasterlist
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(636, 396);
            Controls.Add(label5);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ProductMasterlist";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ProductMasterlist";
            MouseDown += ProductMasterlist_MouseDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ppslbl;
        private System.Windows.Forms.ComboBox customercmb;
        private System.Windows.Forms.TextBox partnamelbl;
        private System.Windows.Forms.TextBox partlbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
    }
}