namespace PSC_HRM.Module.Win.QuyTrinh.XepLoaiLaoDong
{
    partial class XepLoaiController<T>
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
            this.deDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.gridDanhSachNghiHuu = new DevExpress.XtraGrid.GridControl();
            this.menuNghiHuu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miLapQDMienNhiem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miLapDeNghiBoNhiemLai = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewDanhSachNghiHuu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDanhSachNghiHuu)).BeginInit();
            this.menuNghiHuu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDanhSachNghiHuu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.deDenNgay);
            this.layoutControl1.Controls.Add(this.btnTimKiem);
            this.layoutControl1.Controls.Add(this.gridDanhSachNghiHuu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(601, 471);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // deDenNgay
            // 
            this.deDenNgay.EditValue = new System.DateTime(2013, 7, 30, 16, 47, 40, 0);
            this.deDenNgay.Location = new System.Drawing.Point(87, 12);
            this.deDenNgay.Name = "deDenNgay";
            this.deDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.deDenNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.deDenNgay.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.deDenNgay.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.deDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDenNgay.Properties.EditFormat.FormatString = "MM/yyyy";
            this.deDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDenNgay.Properties.Mask.EditMask = "MM/yyyy";
            this.deDenNgay.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView;
            this.deDenNgay.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.deDenNgay.Size = new System.Drawing.Size(104, 20);
            this.deDenNgay.StyleController = this.layoutControl1;
            this.deDenNgay.TabIndex = 8;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(195, 12);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(79, 22);
            this.btnTimKiem.StyleController = this.layoutControl1;
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm kiếm";
            
            // 
            // gridDanhSachNghiHuu
            // 
            this.gridDanhSachNghiHuu.ContextMenuStrip = this.menuNghiHuu;
            this.gridDanhSachNghiHuu.Location = new System.Drawing.Point(24, 69);
            this.gridDanhSachNghiHuu.MainView = this.gridViewDanhSachNghiHuu;
            this.gridDanhSachNghiHuu.Name = "gridDanhSachNghiHuu";
            this.gridDanhSachNghiHuu.Size = new System.Drawing.Size(553, 378);
            this.gridDanhSachNghiHuu.TabIndex = 5;
            this.gridDanhSachNghiHuu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDanhSachNghiHuu});
            // 
            // menuNghiHuu
            // 
            this.menuNghiHuu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLapQDMienNhiem,
            this.toolStripMenuItem1,
            this.miLapDeNghiBoNhiemLai});
            this.menuNghiHuu.Name = "menuNghiHuu";
            this.menuNghiHuu.Size = new System.Drawing.Size(206, 76);
            // 
            // miLapQDMienNhiem
            // 
            this.miLapQDMienNhiem.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_QuyetDinh;
            this.miLapQDMienNhiem.Name = "miLapQDMienNhiem";
            this.miLapQDMienNhiem.Size = new System.Drawing.Size(205, 22);
            this.miLapQDMienNhiem.Text = "Lập QĐ miễn nhiệm";
            
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 6);
            // 
            // miLapDeNghiBoNhiemLai
            // 
            this.miLapDeNghiBoNhiemLai.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_BangTheoDoiViPham_32x32;
            this.miLapDeNghiBoNhiemLai.Name = "miLapDeNghiBoNhiemLai";
            this.miLapDeNghiBoNhiemLai.Size = new System.Drawing.Size(205, 22);
            this.miLapDeNghiBoNhiemLai.Text = "Lập đề nghị bổ nhiệm lại";
            
            // 
            // gridViewDanhSachNghiHuu
            // 
            this.gridViewDanhSachNghiHuu.GridControl = this.gridDanhSachNghiHuu;
            this.gridViewDanhSachNghiHuu.Name = "gridViewDanhSachNghiHuu";
            this.gridViewDanhSachNghiHuu.OptionsView.ShowGroupPanel = false;
            
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(601, 471);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Danh sách cán bộ";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(581, 425);
            this.layoutControlGroup2.Text = "Danh sách cán bộ";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridDanhSachNghiHuu;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(557, 382);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnTimKiem;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(183, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(266, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(315, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.deDenNgay;
            this.layoutControlItem5.CustomizationFormText = "Tính đến ngày:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(183, 26);
            this.layoutControlItem5.Text = "Tính đến ngày:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 13);
            // 
            // XepLoaiController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "XepLoaiController";
            this.Size = new System.Drawing.Size(601, 471);
            
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDanhSachNghiHuu)).EndInit();
            this.menuNghiHuu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDanhSachNghiHuu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraGrid.GridControl gridDanhSachNghiHuu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDanhSachNghiHuu;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.ContextMenuStrip menuNghiHuu;
        private System.Windows.Forms.ToolStripMenuItem miLapDeNghiBoNhiemLai;
        private System.Windows.Forms.ToolStripMenuItem miLapQDMienNhiem;
        private DevExpress.XtraEditors.DateEdit deDenNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}
