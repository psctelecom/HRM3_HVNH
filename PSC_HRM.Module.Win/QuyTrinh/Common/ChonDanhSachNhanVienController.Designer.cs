namespace PSC_HRM.Module.Win.QuyTrinh
{
    partial class ChonDanhSachNhanVienController
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridNhanVien = new DevExpress.XtraGrid.GridControl();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miUncheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miUncheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.bsNhanVien = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewNhanVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBoPhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNhanVien)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridNhanVien);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(373, 416);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridNhanVien
            // 
            this.gridNhanVien.ContextMenuStrip = this.menu;
            this.gridNhanVien.DataSource = this.bsNhanVien;
            this.gridNhanVien.Location = new System.Drawing.Point(24, 43);
            this.gridNhanVien.MainView = this.gridViewNhanVien;
            this.gridNhanVien.Name = "gridNhanVien";
            this.gridNhanVien.Size = new System.Drawing.Size(325, 349);
            this.gridNhanVien.TabIndex = 4;
            this.gridNhanVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNhanVien});
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
            this.miCheckAll.Click += new System.EventHandler(this.miCheckAll_Click);
            // 
            // miCheckSelected
            // 
            this.miCheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.plus;
            this.miCheckSelected.Name = "miCheckSelected";
            this.miCheckSelected.Size = new System.Drawing.Size(278, 22);
            this.miCheckSelected.Text = "Chọn những cán bộ được đánh dấu";
            this.miCheckSelected.Click += new System.EventHandler(this.miCheckSelected_Click);
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
            this.miUncheckAll.Click += new System.EventHandler(this.miUncheckAll_Click);
            // 
            // miUncheckSelected
            // 
            this.miUncheckSelected.Image = global::PSC_HRM.Module.Win.Properties.Resources.edit_remove;
            this.miUncheckSelected.Name = "miUncheckSelected";
            this.miUncheckSelected.Size = new System.Drawing.Size(278, 22);
            this.miUncheckSelected.Text = "Bỏ chọn những cán bộ được đánh dấu";
            this.miUncheckSelected.Click += new System.EventHandler(this.miUncheckSelected_Click);
            // 
            // bsNhanVien
            // 
            this.bsNhanVien.DataSource = typeof(PSC_HRM.Module.Win.QuyTrinh.Common.NhanVienItem);
            // 
            // gridViewNhanVien
            // 
            this.gridViewNhanVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colHoTen,
            this.colBoPhan});
            this.gridViewNhanVien.GridControl = this.gridNhanVien;
            this.gridViewNhanVien.GroupCount = 1;
            this.gridViewNhanVien.Name = "gridViewNhanVien";
            this.gridViewNhanVien.OptionsView.ShowGroupPanel = false;
            this.gridViewNhanVien.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBoPhan, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colChon
            // 
            this.colChon.FieldName = "Chon";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            // 
            // colHoTen
            // 
            this.colHoTen.FieldName = "HoTen";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.Visible = true;
            this.colHoTen.VisibleIndex = 1;
            // 
            // colBoPhan
            // 
            this.colBoPhan.FieldName = "BoPhan";
            this.colBoPhan.Name = "colBoPhan";
            this.colBoPhan.Visible = true;
            this.colBoPhan.VisibleIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(373, 416);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Danh sách cán bộ";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(353, 396);
            this.layoutControlGroup2.Text = "Danh sách cán bộ";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridNhanVien;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(329, 353);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // ChonDanhSachNhanVienController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ChonDanhSachNhanVienController";
            this.Size = new System.Drawing.Size(373, 416);
            this.Load += new System.EventHandler(this.ChonDanhSachNhanVienController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNhanVien)).EndInit();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.GridControl gridNhanVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNhanVien;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        protected DevExpress.XtraLayout.LayoutControl layoutControl1;
        protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource bsNhanVien;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraGrid.Columns.GridColumn colHoTen;
        private DevExpress.XtraGrid.Columns.GridColumn colBoPhan;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem miCheckAll;
        private System.Windows.Forms.ToolStripMenuItem miCheckSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miUncheckAll;
        private System.Windows.Forms.ToolStripMenuItem miUncheckSelected;
    }
}
