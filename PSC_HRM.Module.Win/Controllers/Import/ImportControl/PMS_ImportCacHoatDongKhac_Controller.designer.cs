namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_ImportCacHoatDongKhac_Controller
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
            this.btImportHDKhac = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImportHDKhac
            // 
            this.btImportHDKhac.Caption = "Import các HD khác";
            this.btImportHDKhac.ConfirmationMessage = null;
            this.btImportHDKhac.Id = "PMS_ImportCacHoatDongKhac_Controller";
            this.btImportHDKhac.ImageName = "Action_Import";
            this.btImportHDKhac.ToolTip = "Import các HD khác từ file excel";
            this.btImportHDKhac.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportHDKhac_Execute);
            this.Activated += PMS_ImportCacHoatDongKhac_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportHDKhac;
    }
}
