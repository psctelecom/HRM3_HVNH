namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_ImportKhoaLuanTotNghiep_Controller
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
            this.btImportKhoiLuong = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImportHDKhac
            // 
            this.btImportKhoiLuong.Caption = "Import khóa luận tốt nghiệp";
            this.btImportKhoiLuong.ConfirmationMessage = null;
            this.btImportKhoiLuong.Id = "PMS_ImportKhoaLuanTotNghiep_Controller";
            this.btImportKhoiLuong.ImageName = "Action_Import";
            this.btImportKhoiLuong.ToolTip = "Import khóa luận tốt nghiệp từ file excel";
            this.btImportKhoiLuong.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportKhoiLuong_Execute);
            this.Activated += PMS_ImportKhoaLuanTotNghiep_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportKhoiLuong;
    }
}
