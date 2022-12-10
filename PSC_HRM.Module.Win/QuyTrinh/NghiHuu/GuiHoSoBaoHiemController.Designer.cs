namespace PSC_HRM.Module.Win.QuyTrinh.NghiHuu
{
    partial class GuiHoSoBaoHiemController
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.gridQuanLyBienDong = new DevExpress.XtraEditors.GridLookUpEdit();
            this.listQuanLyBienDong = new DevExpress.Xpo.XPCollection(this.components);
            this.gridViewQuanLyBienDong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridThongTinNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTatCaNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridQuanLyBienDong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listQuanLyBienDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewQuanLyBienDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridThongTinNhanVien
            // 
            this.gridThongTinNhanVien.Location = new System.Drawing.Point(107, 35);
            this.gridThongTinNhanVien.Size = new System.Drawing.Size(423, 20);
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Chưa chọn \"Cán bộ\"";
            this.dxValidationProvider1.SetValidationRule(this.gridThongTinNhanVien, conditionValidationRule1);
            // 
            // ceTatCaNhanVien
            // 
            this.ceTatCaNhanVien.Location = new System.Drawing.Point(107, 12);
            this.ceTatCaNhanVien.Size = new System.Drawing.Size(423, 19);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridQuanLyBienDong);
            this.layoutControl1.Size = new System.Drawing.Size(542, 393);
            this.layoutControl1.Controls.SetChildIndex(this.gridThongTinNhanVien, 0);
            this.layoutControl1.Controls.SetChildIndex(this.ceTatCaNhanVien, 0);
            this.layoutControl1.Controls.SetChildIndex(this.gridQuanLyBienDong, 0);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup1.Size = new System.Drawing.Size(542, 393);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Size = new System.Drawing.Size(522, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(91, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Size = new System.Drawing.Size(522, 23);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(91, 13);
            // 
            // gridQuanLyBienDong
            // 
            this.gridQuanLyBienDong.Location = new System.Drawing.Point(107, 59);
            this.gridQuanLyBienDong.Name = "gridQuanLyBienDong";
            this.gridQuanLyBienDong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridQuanLyBienDong.Properties.DataSource = this.listQuanLyBienDong;
            this.gridQuanLyBienDong.Properties.DisplayMember = "Caption";
            this.gridQuanLyBienDong.Properties.NullText = "";
            this.gridQuanLyBienDong.Properties.ValueMember = "This";
            this.gridQuanLyBienDong.Properties.View = this.gridViewQuanLyBienDong;
            this.gridQuanLyBienDong.Size = new System.Drawing.Size(423, 20);
            this.gridQuanLyBienDong.StyleController = this.layoutControl1;
            this.gridQuanLyBienDong.TabIndex = 6;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Chưa chọn \"Quản lý biến động\"";
            this.dxValidationProvider1.SetValidationRule(this.gridQuanLyBienDong, conditionValidationRule2);
            this.gridQuanLyBienDong.Resize += new System.EventHandler(this.gridQuanLyBienDong_Resize);
            // 
            // listQuanLyBienDong
            // 
            this.listQuanLyBienDong.ObjectType = typeof(PSC_HRM.Module.BaoHiem.QuanLyBienDong);
            this.listQuanLyBienDong.Session = this.unitOfWork;
            // 
            // gridViewQuanLyBienDong
            // 
            this.gridViewQuanLyBienDong.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewQuanLyBienDong.Name = "gridViewQuanLyBienDong";
            this.gridViewQuanLyBienDong.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewQuanLyBienDong.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridQuanLyBienDong;
            this.layoutControlItem3.CustomizationFormText = "Quản lý biến động:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 47);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(522, 326);
            this.layoutControlItem3.Text = "Quản lý biến động:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(91, 13);
            // 
            // GuiHoSoBaoHiemController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "GuiHoSoBaoHiemController";
            this.Size = new System.Drawing.Size(542, 393);
            this.Load += new System.EventHandler(this.GuiHoSoBaoHiemController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridThongTinNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceTatCaNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridQuanLyBienDong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listQuanLyBienDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewQuanLyBienDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GridLookUpEdit gridQuanLyBienDong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewQuanLyBienDong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.Xpo.XPCollection listQuanLyBienDong;
    }
}
