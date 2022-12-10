namespace PSC_HRM.Module.Win.QuyTrinh.KhenThuong
{
    partial class DangKyThiDuaController
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
            this.gridDanhHieuKhenThuong = new DevExpress.XtraEditors.GridLookUpEdit();
            this.listDanhHieuKhenThuong = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork1 = new DevExpress.Xpo.UnitOfWork(this.components);
            this.gridViewDanhHieuKhenThuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chonDanhSachNhanVienController1 = new PSC_HRM.Module.Win.QuyTrinh.ChonDanhSachNhanVienController();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.chonDanhSachBoPhanController1 = new PSC_HRM.Module.Win.QuyTrinh.ChonDanhSachBoPhanController();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDanhHieuKhenThuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDanhHieuKhenThuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDanhHieuKhenThuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridDanhHieuKhenThuong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(629, 44);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridDanhHieuKhenThuong
            // 
            this.gridDanhHieuKhenThuong.Location = new System.Drawing.Point(67, 12);
            this.gridDanhHieuKhenThuong.Name = "gridDanhHieuKhenThuong";
            this.gridDanhHieuKhenThuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridDanhHieuKhenThuong.Properties.DataSource = this.listDanhHieuKhenThuong;
            this.gridDanhHieuKhenThuong.Properties.DisplayMember = "TenDanhHieu";
            this.gridDanhHieuKhenThuong.Properties.NullText = "";
            this.gridDanhHieuKhenThuong.Properties.ValueMember = "This";
            this.gridDanhHieuKhenThuong.Properties.View = this.gridViewDanhHieuKhenThuong;
            this.gridDanhHieuKhenThuong.Size = new System.Drawing.Size(550, 20);
            this.gridDanhHieuKhenThuong.StyleController = this.layoutControl1;
            this.gridDanhHieuKhenThuong.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Chưa chọn \"Danh hiệu\"";
            this.dxValidationProvider1.SetValidationRule(this.gridDanhHieuKhenThuong, conditionValidationRule1);
            this.gridDanhHieuKhenThuong.EditValueChanged += new System.EventHandler(this.gridDanhHieuKhenThuong_EditValueChanged);
            this.gridDanhHieuKhenThuong.Resize += new System.EventHandler(this.gridDanhHieuKhenThuong_Resize);
            // 
            // listDanhHieuKhenThuong
            // 
            this.listDanhHieuKhenThuong.ObjectType = typeof(PSC_HRM.Module.DanhMuc.DanhHieuKhenThuong);
            this.listDanhHieuKhenThuong.Session = this.unitOfWork1;
            // 
            // unitOfWork1
            // 
            this.unitOfWork1.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork1.TrackPropertiesModifications = false;
            // 
            // gridViewDanhHieuKhenThuong
            // 
            this.gridViewDanhHieuKhenThuong.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewDanhHieuKhenThuong.Name = "gridViewDanhHieuKhenThuong";
            this.gridViewDanhHieuKhenThuong.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDanhHieuKhenThuong.OptionsView.ShowGroupPanel = false;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(629, 44);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridDanhHieuKhenThuong;
            this.layoutControlItem1.CustomizationFormText = "Danh hiệu:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(609, 24);
            this.layoutControlItem1.Text = "Danh hiệu:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(52, 13);
            // 
            // chonDanhSachNhanVienController1
            // 
            this.chonDanhSachNhanVienController1.Dock = System.Windows.Forms.DockStyle.Left;
            this.chonDanhSachNhanVienController1.Location = new System.Drawing.Point(0, 44);
            this.chonDanhSachNhanVienController1.Name = "chonDanhSachNhanVienController1";
            this.chonDanhSachNhanVienController1.Session = null;
            this.chonDanhSachNhanVienController1.Size = new System.Drawing.Size(288, 412);
            this.chonDanhSachNhanVienController1.TabIndex = 1;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(288, 44);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 412);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // chonDanhSachBoPhanController1
            // 
            this.chonDanhSachBoPhanController1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chonDanhSachBoPhanController1.Location = new System.Drawing.Point(293, 44);
            this.chonDanhSachBoPhanController1.Name = "chonDanhSachBoPhanController1";
            this.chonDanhSachBoPhanController1.Session = null;
            this.chonDanhSachBoPhanController1.Size = new System.Drawing.Size(336, 412);
            this.chonDanhSachBoPhanController1.TabIndex = 3;
            // 
            // DangKyThiDuaController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chonDanhSachBoPhanController1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.chonDanhSachNhanVienController1);
            this.Controls.Add(this.layoutControl1);
            this.Name = "DangKyThiDuaController";
            this.Size = new System.Drawing.Size(629, 456);
            this.Load += new System.EventHandler(this.DangKyThiDuaController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDanhHieuKhenThuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDanhHieuKhenThuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDanhHieuKhenThuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.GridLookUpEdit gridDanhHieuKhenThuong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDanhHieuKhenThuong;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private ChonDanhSachNhanVienController chonDanhSachNhanVienController1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private ChonDanhSachBoPhanController chonDanhSachBoPhanController1;
        private DevExpress.Xpo.XPCollection listDanhHieuKhenThuong;
        private DevExpress.Xpo.UnitOfWork unitOfWork1;
    }
}
