namespace FGScanner
{
    partial class StockEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockEdit));
            BoxTxt = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            boxlbl = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            locationlbl = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            stockslbl = new System.Windows.Forms.Label();
            prodverlbl = new System.Windows.Forms.Label();
            proddatelbl = new System.Windows.Forms.Label();
            customerlbl = new System.Windows.Forms.Label();
            partnumberlbl = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            cancelbtn = new System.Windows.Forms.Button();
            label9 = new System.Windows.Forms.Label();
            reason_txtbox = new System.Windows.Forms.ComboBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // BoxTxt
            // 
            BoxTxt.Location = new System.Drawing.Point(94, 223);
            BoxTxt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BoxTxt.Name = "BoxTxt";
            BoxTxt.Size = new System.Drawing.Size(146, 23);
            BoxTxt.TabIndex = 6;
            BoxTxt.KeyPress += textBox1_KeyPress;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(426, 317);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(88, 27);
            button1.TabIndex = 8;
            button1.Text = "Out";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label5.Location = new System.Drawing.Point(15, 228);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(62, 15);
            label5.TabIndex = 7;
            label5.Text = "No of Box:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(boxlbl);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(locationlbl);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(stockslbl);
            groupBox1.Controls.Add(prodverlbl);
            groupBox1.Controls.Add(proddatelbl);
            groupBox1.Controls.Add(customerlbl);
            groupBox1.Controls.Add(partnumberlbl);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(14, 14);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(594, 202);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Stock Information";
            // 
            // boxlbl
            // 
            boxlbl.AutoSize = true;
            boxlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            boxlbl.Location = new System.Drawing.Point(471, 108);
            boxlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            boxlbl.Name = "boxlbl";
            boxlbl.Size = new System.Drawing.Size(78, 15);
            boxlbl.TabIndex = 30;
            boxlbl.Text = "Part Number:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label8.Location = new System.Drawing.Point(430, 108);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(29, 15);
            label8.TabIndex = 29;
            label8.Text = "Box:";
            // 
            // locationlbl
            // 
            locationlbl.AutoSize = true;
            locationlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            locationlbl.Location = new System.Drawing.Point(471, 73);
            locationlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            locationlbl.Name = "locationlbl";
            locationlbl.Size = new System.Drawing.Size(78, 15);
            locationlbl.TabIndex = 28;
            locationlbl.Text = "Part Number:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label7.Location = new System.Drawing.Point(399, 73);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(56, 15);
            label7.TabIndex = 27;
            label7.Text = "Location:";
            // 
            // stockslbl
            // 
            stockslbl.AutoSize = true;
            stockslbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            stockslbl.Location = new System.Drawing.Point(471, 42);
            stockslbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            stockslbl.Name = "stockslbl";
            stockslbl.Size = new System.Drawing.Size(78, 15);
            stockslbl.TabIndex = 26;
            stockslbl.Text = "Part Number:";
            // 
            // prodverlbl
            // 
            prodverlbl.AutoSize = true;
            prodverlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            prodverlbl.Location = new System.Drawing.Point(141, 143);
            prodverlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            prodverlbl.Name = "prodverlbl";
            prodverlbl.Size = new System.Drawing.Size(78, 15);
            prodverlbl.TabIndex = 25;
            prodverlbl.Text = "Part Number:";
            // 
            // proddatelbl
            // 
            proddatelbl.AutoSize = true;
            proddatelbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            proddatelbl.Location = new System.Drawing.Point(141, 108);
            proddatelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            proddatelbl.Name = "proddatelbl";
            proddatelbl.Size = new System.Drawing.Size(78, 15);
            proddatelbl.TabIndex = 24;
            proddatelbl.Text = "Part Number:";
            // 
            // customerlbl
            // 
            customerlbl.AutoSize = true;
            customerlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            customerlbl.Location = new System.Drawing.Point(140, 73);
            customerlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            customerlbl.Name = "customerlbl";
            customerlbl.Size = new System.Drawing.Size(78, 15);
            customerlbl.TabIndex = 23;
            customerlbl.Text = "Part Number:";
            // 
            // partnumberlbl
            // 
            partnumberlbl.AutoSize = true;
            partnumberlbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            partnumberlbl.Location = new System.Drawing.Point(140, 42);
            partnumberlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            partnumberlbl.Name = "partnumberlbl";
            partnumberlbl.Size = new System.Drawing.Size(78, 15);
            partnumberlbl.TabIndex = 22;
            partnumberlbl.Text = "Part Number:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label6.Location = new System.Drawing.Point(369, 42);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(82, 15);
            label6.TabIndex = 21;
            label6.Text = "Current Stock:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(6, 143);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(110, 15);
            label4.TabIndex = 20;
            label4.Text = "Production Version:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.Location = new System.Drawing.Point(6, 108);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(96, 15);
            label3.TabIndex = 19;
            label3.Text = "Production Date:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(6, 73);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 15);
            label2.TabIndex = 18;
            label2.Text = "Customer:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(6, 42);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(78, 15);
            label1.TabIndex = 17;
            label1.Text = "Part Number:";
            // 
            // cancelbtn
            // 
            cancelbtn.Location = new System.Drawing.Point(520, 317);
            cancelbtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cancelbtn.Name = "cancelbtn";
            cancelbtn.Size = new System.Drawing.Size(88, 27);
            cancelbtn.TabIndex = 10;
            cancelbtn.Text = "Cancel";
            cancelbtn.UseVisualStyleBackColor = true;
            cancelbtn.Click += cancelbtn_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label9.Location = new System.Drawing.Point(15, 268);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(48, 15);
            label9.TabIndex = 11;
            label9.Text = "Reason:";
            // 
            // reason_txtbox
            // 
            reason_txtbox.FormattingEnabled = true;
            reason_txtbox.Items.AddRange(new object[] { "Manual Deduction - Excess Scan", "Manual Deduction - Damaged Goods", "Manual Deduction - Cycle Count Adjustment", "Quality Control Testing - OUT" });
            reason_txtbox.Location = new System.Drawing.Point(94, 268);
            reason_txtbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            reason_txtbox.Name = "reason_txtbox";
            reason_txtbox.Size = new System.Drawing.Size(299, 23);
            reason_txtbox.TabIndex = 12;
            // 
            // StockEdit
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(622, 368);
            Controls.Add(reason_txtbox);
            Controls.Add(label9);
            Controls.Add(cancelbtn);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(BoxTxt);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "StockEdit";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "StockEdit";
            Load += StockEdit_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox BoxTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label locationlbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label stockslbl;
        private System.Windows.Forms.Label prodverlbl;
        private System.Windows.Forms.Label proddatelbl;
        private System.Windows.Forms.Label customerlbl;
        private System.Windows.Forms.Label partnumberlbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label boxlbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox reason_txtbox;
    }
}