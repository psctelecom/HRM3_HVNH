namespace PSC_HRM.Module.Win.QuyTrinh.NangLuong
{
    partial class TheoDoiNangLuongController
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
            this.gridNangLuong = new DevExpress.XtraGrid.GridControl();
            this.menuNghiHuu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miLapDeNghiNangLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.miLapQDNangLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewNangLuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.deTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridNangLuong)).BeginInit();
            this.menuNghiHuu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNangLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.deTuNgay);
            this.layoutControl1.Controls.Add(this.deDenNgay);
            this.layoutControl1.Controls.Add(this.btnTimKiem);
            this.layoutControl1.Controls.Add(this.gridNangLuong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(845, 471);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // deDenNgay
            // 
            this.deDenNgay.EditValue = new System.DateTime(2013, 7, 30, 16, 47, 40, 0);
            this.deDenNgay.Location = new System.Drawing.Point(66, 36);
            this.deDenNgay.Name = "deDenNgay";
            this.deDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.deDenNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.deDenNgay.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.deDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.deDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.deDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.deDenNgay.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView;
            this.deDenNgay.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.deDenNgay.Size = new System.Drawing.Size(120, 20);
            this.deDenNgay.StyleController = this.layoutControl1;
            this.deDenNgay.TabIndex = 8;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(190, 12);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(76, 22);
            this.btnTimKiem.StyleController = this.layoutControl1;
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // gridNangLuong
            // 
            this.gridNangLuong.ContextMenuStrip = this.menuNghiHuu;
            this.gridNangLuong.Location = new System.Drawing.Point(24, 91);
            this.gridNangLuong.MainView = this.gridViewNangLuong;
            this.gridNangLuong.Name = "gridNangLuong";
            this.gridNangLuong.Size = new System.Drawing.Size(797, 356);
            this.gridNangLuong.TabIndex = 5;
            this.gridNangLuong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNangLuong});
            // 
            // menuNghiHuu
            // 
            this.menuNghiHuu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLapDeNghiNangLuong,
            this.miLapQDNangLuong});
            this.menuNghiHuu.Name = "menuNghiHuu";
            this.menuNghiHuu.Size = new System.Drawing.Size(201, 48);
            // 
            // miLapDeNghiNangLuong
            // 
            this.miLapDeNghiNangLuong.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_BangTheoDoiViPham_32x32;
            this.miLapDeNghiNangLuong.Name = "miLapDeNghiNangLuong";
            this.miLapDeNghiNangLuong.Size = new System.Drawing.Size(200, 22);
            this.miLapDeNghiNangLuong.Text = "Lập đề nghị nâng lương";
            this.miLapDeNghiNangLuong.Click += new System.EventHandler(this.btnLapDeNghiNangLuong_Click);
            // 
            // miLapQDNangLuong
            // 
            this.miLapQDNangLuong.Image = global::PSC_HRM.Module.Win.Properties.Resources.BO_QuyetDinh;
            this.miLapQDNangLuong.Name = "miLapQDNangLuong";
            this.miLapQDNangLuong.Size = new System.Drawing.Size(200, 22);
            this.miLapQDNangLuong.Text = "Lập QĐ nâng lương";
            this.miLapQDNangLuong.Click += new System.EventHandler(this.btnQDNangLuong_Click);
            // 
            // gridViewNangLuong
            // 
            this.gridViewNangLuong.GridControl = this.gridNangLuong;
            this.gridViewNangLuong.Name = "gridViewNangLuong";
            this.gridViewNangLuong.OptionsView.ShowGroupPanel = false;
            this.gridViewNangLuong.DoubleClick += new System.EventHandler(this.gridViewNangLuong_DoubleClick);
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
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(845, 471);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Danh sách cán bộ";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(825, 403);
            this.layoutControlGroup2.Text = "Danh sách cán bộ";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridNangLuong;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(801, 360);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnTimKiem;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(178, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(80, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(80, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(80, 48);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(258, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(567, 48);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.deDenNgay;
            this.layoutControlItem5.CustomizationFormText = "Tính đến ngày:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(178, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(178, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(178, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Đến ngày:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(51, 13);
            // 
            // deTuNgay
            // 
            this.deTuNgay.EditValue = null;
            this.deTuNgay.Location = new System.Drawing.Point(66, 12);
            this.deTuNgay.Name = "deTuNgay";
            this.deTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.deTuNgay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.deTuNgay.Size = new System.Drawing.Size(120, 20);
            this.deTuNgay.StyleController = this.layoutControl1;
            this.deTuNgay.TabIndex = 9;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.deTuNgay;
            this.layoutControlItem1.CustomizationFormText = "Từ ngày";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(178, 24);
            this.layoutControlItem1.Text = "Từ ngày";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(51, 13);
            // 
            // TheoDoiNangLuongController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "TheoDoiNangLuongController";
            this.Size = new System.Drawing.Size(845, 471);
            this.Load += new System.EventHandler(this.TheoDoiNghiHuuController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridNangLuong)).EndInit();
            this.menuNghiHuu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNangLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraGrid.GridControl gridNangLuong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNangLuong;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.ContextMenuStrip menuNghiHuu;
        private System.Windows.Forms.ToolStripMenuItem miLapQDNangLuong;
        private DevExpress.XtraEditors.DateEdit deDenNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.ToolStripMenuItem miLapDeNghiNangLuong;
        private DevExpress.XtraEditors.DateEdit deTuNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
