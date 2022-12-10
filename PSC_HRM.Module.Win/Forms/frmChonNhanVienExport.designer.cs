namespace PSC_HRM.Module.Win.Forms
{
    partial class frmChonNhanVienExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonNhanVienExport));
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miUncheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCheckAll,
            this.miCheckSelected,
            this.toolStripMenuItem1,
            this.miUncheckAll,
            this.miUncheckSelected});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(279, 98);
            // 
            // miCheckAll
            // 
            this.miCheckAll.Image = global::PSC_HRM.Module.Win.Properties.Resources.symbol_add;
            this.miCheckAll.Name = "miCheckAll";
            this.miCheckAll.Size = new System.Drawing.Size(278, 22);
            this.miCheckAll.Text = "Chọn tất cả cán bộ";
            // 
            // miCheckSelected
            // 
            this.miCheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.plus;
            this.miCheckSelected.Name = "miCheckSelected";
            this.miCheckSelected.Size = new System.Drawing.Size(278, 22);
            this.miCheckSelected.Text = "Chọn những cán bộ được đánh dấu";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(275, 6);
            // 
            // miUncheckAll
            // 
            this.miUncheckAll.Image = global::PSC_HRM.Module.Win.Properties.Resources.symbol_delete;
            this.miUncheckAll.Name = "miUncheckAll";
            this.miUncheckAll.Size = new System.Drawing.Size(278, 22);
            this.miUncheckAll.Text = "Bỏ chọn tất cả cán bộ";
            // 
            // miUncheckSelected
            // 
            this.miUncheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.edit_remove;
            this.miUncheckSelected.Name = "miUncheckSelected";
            this.miUncheckSelected.Size = new System.Drawing.Size(278, 22);
            this.miUncheckSelected.Text = "Bỏ chọn những cán bộ được đánh dấu";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnAccept);
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 395);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(474, 37);
            this.panelControl1.TabIndex = 1;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(313, 5);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Chọn";
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Location = new System.Drawing.Point(394, 5);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(474, 395);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // frmDanhSachNhanVien
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 432);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDanhSachNhanVien";
            this.Text = "Chọn cán bộ";
            this.Load += new System.EventHandler(this.frmChonNhanVienExport_Load);
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem miCheckAll;
        private System.Windows.Forms.ToolStripMenuItem miCheckSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miUncheckAll;
        private System.Windows.Forms.ToolStripMenuItem miUncheckSelected;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}