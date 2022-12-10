namespace PSC_HRM.Module.Win.QuyTrinh.BoiDuong
{
    partial class DangKyBoiDuongController
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridObjects = new DevExpress.XtraEditors.GridLookUpEdit();
            this.listChuongTrinhBoiDuong = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork1 = new DevExpress.Xpo.UnitOfWork(this.components);
            this.gridViewObjects = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chonDanhSachNhanVienController1 = new PSC_HRM.Module.Win.QuyTrinh.ChonDanhSachNhanVienController();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridObjects.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listChuongTrinhBoiDuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridObjects);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(287, 44);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridObjects
            // 
            this.gridObjects.Location = new System.Drawing.Point(101, 12);
            this.gridObjects.Name = "gridObjects";
            this.gridObjects.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridObjects.Properties.DataSource = this.listChuongTrinhBoiDuong;
            this.gridObjects.Properties.NullText = "";
            this.gridObjects.Properties.View = this.gridViewObjects;
            this.gridObjects.Size = new System.Drawing.Size(174, 20);
            this.gridObjects.StyleController = this.layoutControl1;
            this.gridObjects.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Chưa chọn \"Chương trình đào tạo\"";
            this.dxValidationProvider1.SetValidationRule(this.gridObjects, conditionValidationRule1);
            this.gridObjects.EditValueChanged += new System.EventHandler(this.gridObjects_EditValueChanged);
            this.gridObjects.Resize += new System.EventHandler(this.gridObjects_Resize);
            // 
            // listChuongTrinhBoiDuong
            // 
            this.listChuongTrinhBoiDuong.ObjectType = typeof(PSC_HRM.Module.DanhMuc.ChuongTrinhBoiDuong);
            this.listChuongTrinhBoiDuong.Session = this.unitOfWork1;
            // 
            // unitOfWork1
            // 
            this.unitOfWork1.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork1.TrackPropertiesModifications = false;
            // 
            // gridViewObjects
            // 
            this.gridViewObjects.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewObjects.Name = "gridViewObjects";
            this.gridViewObjects.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewObjects.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(287, 44);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridObjects;
            this.layoutControlItem1.CustomizationFormText = "Danh hiệu:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(267, 24);
            this.layoutControlItem1.Text = "Tên chương trình:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(86, 13);
            // 
            // chonDanhSachNhanVienController1
            // 
            this.chonDanhSachNhanVienController1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chonDanhSachNhanVienController1.Location = new System.Drawing.Point(0, 44);
            this.chonDanhSachNhanVienController1.Name = "chonDanhSachNhanVienController1";
            this.chonDanhSachNhanVienController1.Session = null;
            this.chonDanhSachNhanVienController1.Size = new System.Drawing.Size(287, 412);
            this.chonDanhSachNhanVienController1.TabIndex = 1;
            // 
            // DangKyBoiDuongController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chonDanhSachNhanVienController1);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DangKyBoiDuongController";
            this.Size = new System.Drawing.Size(287, 456);
            this.Load += new System.EventHandler(this.DangKyThiDuaController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridObjects.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listChuongTrinhBoiDuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.GridLookUpEdit gridObjects;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewObjects;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private ChonDanhSachNhanVienController chonDanhSachNhanVienController1;
        private DevExpress.Xpo.XPCollection listChuongTrinhBoiDuong;
        private DevExpress.Xpo.UnitOfWork unitOfWork1;
    }
}
