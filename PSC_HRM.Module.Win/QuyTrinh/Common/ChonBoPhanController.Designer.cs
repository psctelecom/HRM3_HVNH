namespace PSC_HRM.Module.Win.QuyTrinh
{
    partial class ChonBoPhanController
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceTatCaNhanVien = new DevExpress.XtraEditors.CheckEdit();
            this.gridBoPhan = new DevExpress.XtraEditors.GridLookUpEdit();
            this.listDonVi = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork = new DevExpress.Xpo.UnitOfWork(this.components);
            this.gridViewBoPhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceTatCaNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBoPhan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBoPhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceTatCaNhanVien);
            this.layoutControl1.Controls.Add(this.gridBoPhan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(46, 121, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(295, 140);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceTatCaNhanVien
            // 
            this.ceTatCaNhanVien.Location = new System.Drawing.Point(50, 12);
            this.ceTatCaNhanVien.Name = "ceTatCaNhanVien";
            this.ceTatCaNhanVien.Properties.Caption = "Hiển thị tất cả đơn vị";
            this.ceTatCaNhanVien.Size = new System.Drawing.Size(233, 19);
            this.ceTatCaNhanVien.StyleController = this.layoutControl1;
            this.ceTatCaNhanVien.TabIndex = 5;
            this.ceTatCaNhanVien.CheckedChanged += new System.EventHandler(this.ceTatCaBoPhan_CheckedChanged);
            // 
            // gridBoPhan
            // 
            this.gridBoPhan.Location = new System.Drawing.Point(50, 35);
            this.gridBoPhan.Name = "gridBoPhan";
            this.gridBoPhan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridBoPhan.Properties.DataSource = this.listDonVi;
            this.gridBoPhan.Properties.DisplayMember = "TenBoPhan";
            this.gridBoPhan.Properties.NullText = "";
            this.gridBoPhan.Properties.ValueMember = "This";
            this.gridBoPhan.Properties.View = this.gridViewBoPhan;
            this.gridBoPhan.Size = new System.Drawing.Size(233, 20);
            this.gridBoPhan.StyleController = this.layoutControl1;
            this.gridBoPhan.TabIndex = 4;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Chưa chọn \"Cán bộ\"";
            this.dxValidationProvider1.SetValidationRule(this.gridBoPhan, conditionValidationRule2);
            this.gridBoPhan.Resize += new System.EventHandler(this.gridBoPhan_Resize);
            // 
            // listDonVi
            // 
            this.listDonVi.ObjectType = typeof(PSC_HRM.Module.BaoMat.BoPhan);
            this.listDonVi.Session = this.unitOfWork;
            // 
            // unitOfWork
            // 
            this.unitOfWork.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork.TrackPropertiesModifications = false;
            // 
            // gridViewBoPhan
            // 
            this.gridViewBoPhan.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewBoPhan.Name = "gridViewBoPhan";
            this.gridViewBoPhan.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewBoPhan.OptionsView.ShowGroupPanel = false;
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
            this.layoutControlItem1.Control = this.gridBoPhan;
            this.layoutControlItem1.CustomizationFormText = "Cán bộ:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(275, 97);
            this.layoutControlItem1.Text = "Đơn vị:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(35, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ceTatCaNhanVien;
            this.layoutControlItem2.CustomizationFormText = " ";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(275, 23);
            this.layoutControlItem2.Text = " ";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(35, 13);
            // 
            // ChonBoPhanController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ChonBoPhanController";
            this.Size = new System.Drawing.Size(295, 140);
            this.Load += new System.EventHandler(this.ChonNhanVienController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceTatCaNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBoPhan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDonVi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBoPhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBoPhan;
        protected DevExpress.XtraEditors.GridLookUpEdit gridBoPhan;
        protected DevExpress.Xpo.XPCollection listDonVi;
        protected DevExpress.Xpo.UnitOfWork unitOfWork;
        protected DevExpress.XtraEditors.CheckEdit ceTatCaNhanVien;
        protected DevExpress.XtraLayout.LayoutControl layoutControl1;
        protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        protected DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        protected DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
