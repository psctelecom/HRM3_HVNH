namespace PSC_HRM.Module.Win.QuyTrinh
{
    partial class ChonQuyetDinhChuyenCongTacController
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridObjects = new DevExpress.XtraEditors.GridLookUpEdit();
            this.listObjects = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork = new DevExpress.Xpo.UnitOfWork(this.components);
            this.gridViewObjects = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridObjects.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridObjects);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(281, 187);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridObjects
            // 
            this.gridObjects.Location = new System.Drawing.Point(154, 12);
            this.gridObjects.Name = "gridObjects";
            this.gridObjects.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Chọn", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::PSC_HRM.Module.Win.Properties.Resources.Action_New, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Tạo mới", null, null, true)});
            this.gridObjects.Properties.DataSource = this.listObjects;
            this.gridObjects.Properties.DisplayMember = "NamHoc.TenNamHoc";
            this.gridObjects.Properties.NullText = "";
            this.gridObjects.Properties.ValueMember = "This";
            this.gridObjects.Properties.View = this.gridViewObjects;
            this.gridObjects.Size = new System.Drawing.Size(115, 22);
            this.gridObjects.StyleController = this.layoutControl1;
            this.gridObjects.TabIndex = 5;
            this.gridObjects.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.gridObjects_ButtonClick);
            // 
            // listObjects
            // 
            this.listObjects.ObjectType = typeof(PSC_HRM.Module.QuyetDinh.QuyetDinhThoiViec);
            this.listObjects.Session = this.unitOfWork;
            // 
            // unitOfWork
            // 
            this.unitOfWork.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork.TrackPropertiesModifications = false;
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
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(281, 187);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridObjects;
            this.layoutControlItem2.CustomizationFormText = "Quản lý chuyển công tác:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(261, 167);
            this.layoutControlItem2.Text = "Quyết định chuyển công tác:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(139, 13);
            // 
            // ChonQuyetDinhChuyenCongTacController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ChonQuyetDinhChuyenCongTacController";
            this.Size = new System.Drawing.Size(281, 187);
            this.Load += new System.EventHandler(this.CauHinhThoiViecController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridObjects.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.GridLookUpEdit gridObjects;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewObjects;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.Xpo.UnitOfWork unitOfWork;
        private DevExpress.Xpo.XPCollection listObjects;
    }
}
