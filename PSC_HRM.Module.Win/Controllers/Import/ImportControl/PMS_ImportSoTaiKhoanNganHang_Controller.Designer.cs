namespace PSC_HRM.Module.Win.Controllers.Import.ImportControl
{
    partial class PMS_ImportSoTaiKhoanNganHang_Controller
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
            this.PMS_ImportSoTaiKhoanNganHang = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // PMS_ImportSoTaiKhoanNganHang
            // 
            this.PMS_ImportSoTaiKhoanNganHang.Caption = "Import Tài khoản NH";
            this.PMS_ImportSoTaiKhoanNganHang.ConfirmationMessage = null;
            this.PMS_ImportSoTaiKhoanNganHang.Id = "PMS_ImportSoTaiKhoanNganHang_Controller";
            this.PMS_ImportSoTaiKhoanNganHang.ImageName = "Action_Import";
            this.PMS_ImportSoTaiKhoanNganHang.ToolTip = null;
            this.PMS_ImportSoTaiKhoanNganHang.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PMS_ImportSoTaiKhoanNganHang_Execute);
            this.Activated += PMS_ImportSoTaiKhoanNganHang_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction PMS_ImportSoTaiKhoanNganHang;
    }
}
