namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_ImportKhoiLuongGiangDay_Controller
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
            this.pop_Import_KhoiLuongGiangDay = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.btImport_khoiLuongChung = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImportKhoiLuong
            // 
            this.btImportKhoiLuong.Caption = "Import khối lượng giảng dạy";
            this.btImportKhoiLuong.ConfirmationMessage = null;
            this.btImportKhoiLuong.Id = "PMS_ImportKhoiLuongGiangDay_Controller";
            this.btImportKhoiLuong.ImageName = "Action_Import";
            this.btImportKhoiLuong.ToolTip = "Import khối lượng giảng dạy từ file excel";
            this.btImportKhoiLuong.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportKhoiLuong_Execute);
            // 
            // pop_Import_KhoiLuongGiangDay
            // 
            this.pop_Import_KhoiLuongGiangDay.AcceptButtonCaption = null;
            this.pop_Import_KhoiLuongGiangDay.CancelButtonCaption = null;
            this.pop_Import_KhoiLuongGiangDay.Caption = "Import khối lượng giảng dạy";
            this.pop_Import_KhoiLuongGiangDay.ConfirmationMessage = null;
            this.pop_Import_KhoiLuongGiangDay.Id = "pop_Import_KhoiLuongGiangDay";
            this.pop_Import_KhoiLuongGiangDay.ImageName = "Action_Import";
            this.pop_Import_KhoiLuongGiangDay.ToolTip = null;
            this.pop_Import_KhoiLuongGiangDay.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pop_Import_KhoiLuongGiangDay_CustomizePopupWindowParams);
            this.pop_Import_KhoiLuongGiangDay.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pop_Import_KhoiLuongGiangDay_Execute);
            // 
            // btImport_khoiLuongChung
            // 
            this.btImport_khoiLuongChung.Caption = "Import khối lượng giảng chung";
            this.btImport_khoiLuongChung.ConfirmationMessage = null;
            this.btImport_khoiLuongChung.Id = "btImport_khoiLuongChung";
            this.btImport_khoiLuongChung.ImageName = "Action_Import";
            this.btImport_khoiLuongChung.ToolTip = null;
            this.btImport_khoiLuongChung.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImport_khoiLuongChung_Execute);
            this.Activated+=PMS_ImportKhoiLuongGiangDay_Controller_Activated;
        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction btImportKhoiLuong;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pop_Import_KhoiLuongGiangDay;
        private DevExpress.ExpressApp.Actions.SimpleAction btImport_khoiLuongChung;
    }
}
