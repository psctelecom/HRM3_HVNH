namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_Import_SiSoChuyenNganh_SauDaiHoc_Controller
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
            this.btImportSiSoChuyenNganh = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImportSiSoChuyenNganh
            // 
            this.btImportSiSoChuyenNganh.Caption = "Import";
            this.btImportSiSoChuyenNganh.ConfirmationMessage = null;
            this.btImportSiSoChuyenNganh.Id = "PMS_Import_SiSoChuyenNganh_SauDaiHoc_Controller";
            this.btImportSiSoChuyenNganh.ImageName = "Action_Import";
            this.btImportSiSoChuyenNganh.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.btImportSiSoChuyenNganh.ToolTip = "Import sau đại học từ file excel";
            this.btImportSiSoChuyenNganh.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.btImportSiSoChuyenNganh.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportSiSoChuyenNganh_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportSiSoChuyenNganh;
    }
}
