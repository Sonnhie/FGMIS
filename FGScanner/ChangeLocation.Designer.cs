namespace FGScanner
{
    partial class ChangeLocation
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.logstable = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.curr_loc = new System.Windows.Forms.ComboBox();
            this.wh_id = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nex_loc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.box_qty = new System.Windows.Forms.TextBox();
            this.part_number = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.total_box_lbl = new System.Windows.Forms.Label();
            this.total_sum = new System.Windows.Forms.Label();
            this.qty_text = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.prod_lot = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.logstable)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // logstable
            // 
            this.logstable.AllowUserToAddRows = false;
            this.logstable.AllowUserToDeleteRows = false;
            this.logstable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logstable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.logstable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.logstable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logstable.Location = new System.Drawing.Point(13, 311);
            this.logstable.Name = "logstable";
            this.logstable.Size = new System.Drawing.Size(1221, 324);
            this.logstable.TabIndex = 1;
            this.logstable.SelectionChanged += new System.EventHandler(this.logstable_SelectionChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(13, 278);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 21);
            this.label13.TabIndex = 4;
            this.label13.Text = "Rack Details:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.curr_loc);
            this.groupBox1.Controls.Add(this.wh_id);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 242);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rack Input";
            // 
            // curr_loc
            // 
            this.curr_loc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curr_loc.FormattingEnabled = true;
            this.curr_loc.Items.AddRange(new object[] {
            "WH1",
            "WH2"});
            this.curr_loc.Location = new System.Drawing.Point(150, 123);
            this.curr_loc.Name = "curr_loc";
            this.curr_loc.Size = new System.Drawing.Size(225, 25);
            this.curr_loc.TabIndex = 15;
            this.curr_loc.SelectedIndexChanged += new System.EventHandler(this.curr_loc_SelectedIndexChanged);
            // 
            // wh_id
            // 
            this.wh_id.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wh_id.FormattingEnabled = true;
            this.wh_id.Items.AddRange(new object[] {
            "WH1",
            "WH2"});
            this.wh_id.Location = new System.Drawing.Point(150, 76);
            this.wh_id.Name = "wh_id";
            this.wh_id.Size = new System.Drawing.Size(225, 25);
            this.wh_id.TabIndex = 3;
            this.wh_id.SelectedIndexChanged += new System.EventHandler(this.wh_id_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "WH ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location:";
            // 
            // nex_loc
            // 
            this.nex_loc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nex_loc.FormattingEnabled = true;
            this.nex_loc.Location = new System.Drawing.Point(125, 157);
            this.nex_loc.Name = "nex_loc";
            this.nex_loc.Size = new System.Drawing.Size(242, 25);
            this.nex_loc.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(30, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Transfer to:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(125, 194);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Update Location";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 738);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1245, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 16);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.prod_lot);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.qty_text);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.box_qty);
            this.groupBox2.Controls.Add(this.part_number);
            this.groupBox2.Controls.Add(this.nex_loc);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(511, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(700, 242);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Entry";
            // 
            // box_qty
            // 
            this.box_qty.Location = new System.Drawing.Point(125, 76);
            this.box_qty.Name = "box_qty";
            this.box_qty.Size = new System.Drawing.Size(242, 25);
            this.box_qty.TabIndex = 19;
            this.box_qty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // part_number
            // 
            this.part_number.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.part_number.FormattingEnabled = true;
            this.part_number.Location = new System.Drawing.Point(125, 39);
            this.part_number.Name = "part_number";
            this.part_number.Size = new System.Drawing.Size(242, 25);
            this.part_number.TabIndex = 17;
            this.part_number.SelectedIndexChanged += new System.EventHandler(this.part_number_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "No of Box:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Part Number:";
            // 
            // total_box_lbl
            // 
            this.total_box_lbl.AutoSize = true;
            this.total_box_lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_box_lbl.Location = new System.Drawing.Point(14, 638);
            this.total_box_lbl.Name = "total_box_lbl";
            this.total_box_lbl.Size = new System.Drawing.Size(67, 17);
            this.total_box_lbl.TabIndex = 32;
            this.total_box_lbl.Text = "Total Box:";
            // 
            // total_sum
            // 
            this.total_sum.AutoSize = true;
            this.total_sum.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_sum.Location = new System.Drawing.Point(160, 638);
            this.total_sum.Name = "total_sum";
            this.total_sum.Size = new System.Drawing.Size(97, 17);
            this.total_sum.TabIndex = 31;
            this.total_sum.Text = "Total Quantity:";
            // 
            // qty_text
            // 
            this.qty_text.Location = new System.Drawing.Point(125, 117);
            this.qty_text.Name = "qty_text";
            this.qty_text.ReadOnly = true;
            this.qty_text.Size = new System.Drawing.Size(242, 25);
            this.qty_text.TabIndex = 21;
            this.qty_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(68, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "PPS:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(400, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Production Lot:";
            // 
            // prod_lot
            // 
            this.prod_lot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prod_lot.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.prod_lot.Location = new System.Drawing.Point(502, 39);
            this.prod_lot.Name = "prod_lot";
            this.prod_lot.Size = new System.Drawing.Size(173, 25);
            this.prod_lot.TabIndex = 23;
            // 
            // ChangeLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 760);
            this.Controls.Add(this.total_box_lbl);
            this.Controls.Add(this.total_sum);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.logstable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChangeLocation";
            this.Text = "ChangeLocation";
            ((System.ComponentModel.ISupportInitialize)(this.logstable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView logstable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox wh_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox curr_loc;
        private System.Windows.Forms.ComboBox nex_loc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label total_box_lbl;
        private System.Windows.Forms.Label total_sum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox box_qty;
        private System.Windows.Forms.ComboBox part_number;
        private System.Windows.Forms.TextBox qty_text;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker prod_lot;
        private System.Windows.Forms.Label label6;
    }
}