namespace FGScanner
{
    partial class Menu
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
            this.BtnWarehousingArea = new System.Windows.Forms.Button();
            this.BtnEcozoneArea = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnWarehousingArea
            // 
            this.BtnWarehousingArea.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnWarehousingArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnWarehousingArea.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnWarehousingArea.Location = new System.Drawing.Point(62, 88);
            this.BtnWarehousingArea.Name = "BtnWarehousingArea";
            this.BtnWarehousingArea.Size = new System.Drawing.Size(174, 57);
            this.BtnWarehousingArea.TabIndex = 0;
            this.BtnWarehousingArea.Text = "Warehouse";
            this.BtnWarehousingArea.UseVisualStyleBackColor = false;
            this.BtnWarehousingArea.Click += new System.EventHandler(this.BtnWarehousingArea_Click);
            // 
            // BtnEcozoneArea
            // 
            this.BtnEcozoneArea.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnEcozoneArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnEcozoneArea.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEcozoneArea.Location = new System.Drawing.Point(62, 183);
            this.BtnEcozoneArea.Name = "BtnEcozoneArea";
            this.BtnEcozoneArea.Size = new System.Drawing.Size(174, 60);
            this.BtnEcozoneArea.TabIndex = 1;
            this.BtnEcozoneArea.Text = "Warehouse (Ecozone)";
            this.BtnEcozoneArea.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(58, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Warehousing Area";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(299, 308);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnEcozoneArea);
            this.Controls.Add(this.BtnWarehousingArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnWarehousingArea;
        private System.Windows.Forms.Button BtnEcozoneArea;
        private System.Windows.Forms.Label label1;
    }
}