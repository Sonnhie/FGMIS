namespace FGScanner
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dataEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            transferLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            bPPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            incomingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            outgoingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            warehouseReturnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            rackViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            warehouseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ecozoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            stockListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            slowMovingListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            iNOUTLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            masterListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            repoertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            inventorySummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            packingListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            warehouseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            panel2 = new System.Windows.Forms.Panel();
            TimeLbl = new System.Windows.Forms.Label();
            DateLbl = new System.Windows.Forms.Label();
            LblUser = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1 = new System.Windows.Forms.Panel();
            menuStrip1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.Color.White;
            menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { menuToolStripMenuItem, inventoryToolStripMenuItem, repoertToolStripMenuItem, documentsToolStripMenuItem });
            menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            menuStrip1.Location = new System.Drawing.Point(0, 42);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1452, 23);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.MouseDown += menuStrip1_MouseDown;
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { logoutToolStripMenuItem });
            menuToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new System.Drawing.Size(50, 19);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            logoutToolStripMenuItem.Text = "Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            // 
            // inventoryToolStripMenuItem
            // 
            inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { dataEntryToolStripMenuItem, rackViewerToolStripMenuItem, stockListToolStripMenuItem, slowMovingListToolStripMenuItem, iNOUTLedgerToolStripMenuItem, masterListToolStripMenuItem });
            inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            inventoryToolStripMenuItem.Size = new System.Drawing.Size(70, 19);
            inventoryToolStripMenuItem.Text = "Inventory";
            // 
            // dataEntryToolStripMenuItem
            // 
            dataEntryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { transferLocationToolStripMenuItem, bPPSToolStripMenuItem, incomingToolStripMenuItem, outgoingToolStripMenuItem, warehouseReturnToolStripMenuItem1 });
            dataEntryToolStripMenuItem.Name = "dataEntryToolStripMenuItem";
            dataEntryToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            dataEntryToolStripMenuItem.Text = "Data Entry";
            dataEntryToolStripMenuItem.Click += dataEntryToolStripMenuItem_Click;
            // 
            // transferLocationToolStripMenuItem
            // 
            transferLocationToolStripMenuItem.Name = "transferLocationToolStripMenuItem";
            transferLocationToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            transferLocationToolStripMenuItem.Text = "Transfer Location";
            transferLocationToolStripMenuItem.Click += transferLocationToolStripMenuItem_Click;
            // 
            // bPPSToolStripMenuItem
            // 
            bPPSToolStripMenuItem.Name = "bPPSToolStripMenuItem";
            bPPSToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            bPPSToolStripMenuItem.Text = "BPPS";
            bPPSToolStripMenuItem.Click += bPPSToolStripMenuItem_Click;
            // 
            // incomingToolStripMenuItem
            // 
            incomingToolStripMenuItem.Name = "incomingToolStripMenuItem";
            incomingToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            incomingToolStripMenuItem.Text = "Incoming";
            incomingToolStripMenuItem.Click += incomingToolStripMenuItem_Click;
            // 
            // outgoingToolStripMenuItem
            // 
            outgoingToolStripMenuItem.Name = "outgoingToolStripMenuItem";
            outgoingToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            outgoingToolStripMenuItem.Text = "Outgoing";
            outgoingToolStripMenuItem.Click += outgoingToolStripMenuItem_Click;
            // 
            // warehouseReturnToolStripMenuItem1
            // 
            warehouseReturnToolStripMenuItem1.Name = "warehouseReturnToolStripMenuItem1";
            warehouseReturnToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            warehouseReturnToolStripMenuItem1.Text = "Warehouse Return";
            warehouseReturnToolStripMenuItem1.Click += warehouseReturnToolStripMenuItem1_Click;
            // 
            // rackViewerToolStripMenuItem
            // 
            rackViewerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { warehouseToolStripMenuItem1, ecozoneToolStripMenuItem });
            rackViewerToolStripMenuItem.Name = "rackViewerToolStripMenuItem";
            rackViewerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            rackViewerToolStripMenuItem.Text = "Rack Viewer";
            rackViewerToolStripMenuItem.Click += rackViewerToolStripMenuItem_Click;
            // 
            // warehouseToolStripMenuItem1
            // 
            warehouseToolStripMenuItem1.Name = "warehouseToolStripMenuItem1";
            warehouseToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            warehouseToolStripMenuItem1.Text = "Warehouse";
            warehouseToolStripMenuItem1.Click += warehouseToolStripMenuItem1_Click;
            // 
            // ecozoneToolStripMenuItem
            // 
            ecozoneToolStripMenuItem.Name = "ecozoneToolStripMenuItem";
            ecozoneToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            ecozoneToolStripMenuItem.Text = "Ecozone";
            ecozoneToolStripMenuItem.Click += ecozoneToolStripMenuItem_Click;
            // 
            // stockListToolStripMenuItem
            // 
            stockListToolStripMenuItem.Name = "stockListToolStripMenuItem";
            stockListToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            stockListToolStripMenuItem.Text = "Stock List";
            stockListToolStripMenuItem.Click += stockListToolStripMenuItem_Click;
            // 
            // slowMovingListToolStripMenuItem
            // 
            slowMovingListToolStripMenuItem.Name = "slowMovingListToolStripMenuItem";
            slowMovingListToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            slowMovingListToolStripMenuItem.Text = "Slow Moving List";
            slowMovingListToolStripMenuItem.Click += slowMovingListToolStripMenuItem_Click;
            // 
            // iNOUTLedgerToolStripMenuItem
            // 
            iNOUTLedgerToolStripMenuItem.Name = "iNOUTLedgerToolStripMenuItem";
            iNOUTLedgerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            iNOUTLedgerToolStripMenuItem.Text = "IN & OUT Ledger";
            iNOUTLedgerToolStripMenuItem.Click += iNOUTLedgerToolStripMenuItem_Click;
            // 
            // masterListToolStripMenuItem
            // 
            masterListToolStripMenuItem.Name = "masterListToolStripMenuItem";
            masterListToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            masterListToolStripMenuItem.Text = "Master List";
            masterListToolStripMenuItem.Click += masterListToolStripMenuItem_Click;
            // 
            // repoertToolStripMenuItem
            // 
            repoertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { inventorySummaryToolStripMenuItem });
            repoertToolStripMenuItem.Name = "repoertToolStripMenuItem";
            repoertToolStripMenuItem.Size = new System.Drawing.Size(59, 19);
            repoertToolStripMenuItem.Text = "Reports";
            // 
            // inventorySummaryToolStripMenuItem
            // 
            inventorySummaryToolStripMenuItem.Name = "inventorySummaryToolStripMenuItem";
            inventorySummaryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            inventorySummaryToolStripMenuItem.Text = "Inventory Summary";
            inventorySummaryToolStripMenuItem.Click += inventorySummaryToolStripMenuItem_Click;
            // 
            // documentsToolStripMenuItem
            // 
            documentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { packingListToolStripMenuItem, warehouseToolStripMenuItem });
            documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
            documentsToolStripMenuItem.Size = new System.Drawing.Size(81, 19);
            documentsToolStripMenuItem.Text = "Documents";
            // 
            // packingListToolStripMenuItem
            // 
            packingListToolStripMenuItem.Name = "packingListToolStripMenuItem";
            packingListToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            packingListToolStripMenuItem.Text = "Packing List";
            packingListToolStripMenuItem.Click += packingListToolStripMenuItem_Click;
            // 
            // warehouseToolStripMenuItem
            // 
            warehouseToolStripMenuItem.Name = "warehouseToolStripMenuItem";
            warehouseToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            warehouseToolStripMenuItem.Text = "Warehouse Return Slip";
            warehouseToolStripMenuItem.Click += warehouseToolStripMenuItem_Click;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.RoyalBlue;
            panel2.Controls.Add(TimeLbl);
            panel2.Controls.Add(DateLbl);
            panel2.Controls.Add(LblUser);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1452, 42);
            panel2.TabIndex = 2;
            panel2.MouseDown += panel2_MouseDown;
            // 
            // TimeLbl
            // 
            TimeLbl.AutoSize = true;
            TimeLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TimeLbl.ForeColor = System.Drawing.Color.White;
            TimeLbl.Location = new System.Drawing.Point(950, 13);
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
            DateLbl.Location = new System.Drawing.Point(751, 13);
            DateLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            DateLbl.Name = "DateLbl";
            DateLbl.Size = new System.Drawing.Size(23, 17);
            DateLbl.TabIndex = 6;
            DateLbl.Text = "---";
            // 
            // LblUser
            // 
            LblUser.AutoSize = true;
            LblUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            LblUser.ForeColor = System.Drawing.Color.White;
            LblUser.Location = new System.Drawing.Point(551, 13);
            LblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LblUser.Name = "LblUser";
            LblUser.Size = new System.Drawing.Size(23, 17);
            LblUser.TabIndex = 5;
            LblUser.Text = "---";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.White;
            label4.Location = new System.Drawing.Point(889, 13);
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
            label3.Location = new System.Drawing.Point(692, 13);
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
            label2.Location = new System.Drawing.Point(485, 12);
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
            label1.Text = "FGIMS";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 65);
            panel1.Margin = new System.Windows.Forms.Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1452, 838);
            panel1.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1452, 903);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            Controls.Add(panel2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FGMIS";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repoertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rackViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventorySummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packingListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warehouseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockListToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeLbl;
        private System.Windows.Forms.Label DateLbl;
        private System.Windows.Forms.Label LblUser;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem slowMovingListToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem warehouseToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ecozoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iNOUTLedgerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bPPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incomingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outgoingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warehouseReturnToolStripMenuItem1;
    }
}