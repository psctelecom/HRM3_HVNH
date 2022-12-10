namespace PSC_HRM.Module.Win.QuyTrinh.TuyenDung
{
    partial class DuyetDangKyTuyenDungController
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
            this.gridObjects = new DevExpress.XtraGrid.GridControl();
            this.gridViewObjects = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObjects)).BeginInit();
            this.SuspendLayout();
            // 
            // gridObjects
            // 
            this.gridObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridObjects.Location = new System.Drawing.Point(0, 0);
            this.gridObjects.MainView = this.gridViewObjects;
            this.gridObjects.Name = "gridObjects";
            this.gridObjects.Size = new System.Drawing.Size(491, 379);
            this.gridObjects.TabIndex = 0;
            this.gridObjects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewObjects});
            // 
            // gridViewObjects
            // 
            this.gridViewObjects.GridControl = this.gridObjects;
            this.gridViewObjects.Name = "gridViewObjects";
            // 
            // DuyetDangKyTuyenDungController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridObjects);
            this.Name = "DuyetDangKyTuyenDungController";
            this.Size = new System.Drawing.Size(491, 379);
            this.Load += new System.EventHandler(this.DuyetDangKyTuyenDungController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewObjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridObjects;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewObjects;
    }
}
