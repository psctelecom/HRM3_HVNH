namespace PSC_HRM.Module.Win.QuyTrinh
{
    partial class ChonNhanVienController
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
            this.ceTatCaNhanVien = new DevExpress.XtraEditors.CheckEdit();
            this.gridThongTinNhanVien = new DevExpress.XtraEditors.GridLookUpEdit();
            this.listThongTinNhanVien = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork = new DevExpress.Xpo.UnitOfWork(this.components);
            this.gridViewThongTinNhanVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceTatCaNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridThongTinNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listThongTinNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThongTinNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceTatCaNhanVien);
            this.layoutControl1.Controls.Add(this.gridThongTinNhanVien);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(295, 140);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceTatCaNhanVien
            // 
            this.ceTatCaNhanVien.Location = new System.Drawing.Point(53, 12);
            this.ceTatCaNhanVien.Name = "ceTatCaNhanVien";
            this.ceTatCaNhanVien.Properties.Caption = "Hiển thị tất cả cán bộ";
            this.ceTatCaNhanVien.Size = new System.Drawing.Size(230, 19);
            this.ceTatCaNhanVien.StyleController = this.layoutControl1;
            this.ceTatCaNhanVien.TabIndex = 5;
            this.ceTatCaNhanVien.CheckedChanged += new System.EventHandler(this.ceTatCaNhanVien_CheckedChanged);
            // 
            // gridThongTinNhanVien
            // 
            this.gridThongTinNhanVien.Location = new System.Drawing.Point(53, 35);
            this.gridThongTinNhanVien.Name = "gridThongTinNhanVien";
            this.gridThongTinNhanVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridThongTinNhanVien.Properties.DataSource = this.listThongTinNhanVien;
            this.gridThongTinNhanVien.Properties.DisplayMember = "HoTen";
            this.gridThongTinNhanVien.Properties.NullText = "";
            this.gridThongTinNhanVien.Properties.ValueMember = "This";
            this.gridThongTinNhanVien.Properties.View = this.gridViewThongTinNhanVien;
            this.gridThongTinNhanVien.Size = new System.Drawing.Size(230, 20);
            this.gridThongTinNhanVien.StyleController = this.layoutControl1;
            this.gridThongTinNhanVien.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Chưa chọn \"Cán bộ\"";
            this.dxValidationProvider1.SetValidationRule(this.gridThongTinNhanVien, conditionValidationRule1);
            this.gridThongTinNhanVien.Resize += new System.EventHandler(this.gridThongTinNhanVien_Resize);
            // 
            // listThongTinNhanVien
            // 
            this.listThongTinNhanVien.ObjectType = typeof(PSC_HRM.Module.HoSo.ThongTinNhanVien);
            this.listThongTinNhanVien.Session = this.unitOfWork;
            // 
            // unitOfWork
            // 
            this.unitOfWork.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork.TrackPropertiesModifications = false;
            // 
            // gridViewThongTinNhanVien
            // 
            this.gridViewThongTinNhanVien.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewThongTinNhanVien.Name = "gridViewThongTinNhanVien";
            this.gridViewThongTinNhanVien.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewThongTinNhanVien.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(295, 140);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridThongTinNhanVien;
            this.layoutControlItem1.CustomizationFormText = "Cán bộ:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(275, 97);
            this.layoutControlItem1.Text = "Cán bộ:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(38, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ceTatCaNhanVien;
            this.layoutControlItem2.CustomizationFormText = " ";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(275, 23);
            this.layoutControlItem2.Text = " ";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(38, 13);
            // 
            // ChonNhanVienController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ChonNhanVienController";
            this.Size = new System.Drawing.Size(295, 140);
            this.Load += new System.EventHandler(this.ChonNhanVienController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceTatCaNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridThongTinNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listThongTinNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThongTinNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridViewThongTinNhanVien;
        protected DevExpress.XtraEditors.GridLookUpEdit gridThongTinNhanVien;
        protected DevExpress.Xpo.XPCollection listThongTinNhanVien;
        protected DevExpress.Xpo.UnitOfWork unitOfWork;
        protected DevExpress.XtraEditors.CheckEdit ceTatCaNhanVien;
        protected DevExpress.XtraLayout.LayoutControl layoutControl1;
        protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        protected DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        protected DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
