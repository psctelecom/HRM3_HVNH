namespace PSC_HRM.Module.Controllers.Import.UEL
{
    partial class PMS_Imp_TheoDoiTruTietChuan_Controller
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
            this.btImportTheoDoiTruTietChuan = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImportTheoDoiTruTietChuan
            // 
            this.btImportTheoDoiTruTietChuan.Caption = "Import theo dõi tiết chuẩn";
            this.btImportTheoDoiTruTietChuan.ConfirmationMessage = null;
            this.btImportTheoDoiTruTietChuan.Id = "PMS_Imp_TheoDoiTruTietChuan_Controller";
            this.btImportTheoDoiTruTietChuan.ImageName = "Action_Import";
            this.btImportTheoDoiTruTietChuan.ToolTip = "Import theo dõi trừ tiết chuẩn giảng viên từ file excel";
            this.btImportTheoDoiTruTietChuan.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportTheoDoiTruTietChuan_Execute);
            this.Activated+=PMS_Imp_TheoDoiTruTietChuan_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportTheoDoiTruTietChuan;
    }
}
