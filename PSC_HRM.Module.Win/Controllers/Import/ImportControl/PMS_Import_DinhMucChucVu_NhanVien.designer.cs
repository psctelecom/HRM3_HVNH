namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_Import_DinhMucChucVu_NhanVien
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
            this.btImport_DinhMucGioChuan = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImport_DinhMucGioChuan
            // 
            this.btImport_DinhMucGioChuan.Caption = "Import";
            this.btImport_DinhMucGioChuan.ConfirmationMessage = null;
            this.btImport_DinhMucGioChuan.Id = "PMS_Import_DinhMucChucVu_NhanVien";
            this.btImport_DinhMucGioChuan.ImageName = "Action_Import";
            this.btImport_DinhMucGioChuan.ToolTip = "Import định mức chức vụ từ file excel";
            this.btImport_DinhMucGioChuan.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImport_DinhMucGioChuan_Execute);
            this.Activated += PMS_Import_DinhMucChucVu_NhanVien_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImport_DinhMucGioChuan;
    }
}
