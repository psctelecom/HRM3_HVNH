namespace PSC_HRM.Module.Win.Controllers.Import.ImportControl
{
    partial class PMS_ImportThanhToanPhuTroi_Controller
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
            this.btnImportThanhToanPhuTroi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btnImportThanhToanPhuTroi
            // 
            this.btnImportThanhToanPhuTroi.Caption = "Import";
            this.btnImportThanhToanPhuTroi.ConfirmationMessage = null;
            this.btnImportThanhToanPhuTroi.Id = "PMS_ImportThanhToanPhuTroi_Controller";
            this.btnImportThanhToanPhuTroi.ImageName = "Action_Import";
            this.btnImportThanhToanPhuTroi.ToolTip = null;
            this.btnImportThanhToanPhuTroi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnImportThanhToanPhuTroi_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btnImportThanhToanPhuTroi;
    }
}
