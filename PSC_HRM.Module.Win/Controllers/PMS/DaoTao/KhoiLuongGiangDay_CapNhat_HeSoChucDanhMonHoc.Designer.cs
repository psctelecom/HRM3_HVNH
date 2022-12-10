namespace PSC_HRM.Module.Win.Controllers.PMS.DaoTao
{
    partial class KhoiLuongGiangDay_CapNhat_HeSoChucDanhMonHoc
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
            this.popCapNhat = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.btnMoKhoa_KhoiLuong = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // popCapNhat
            // 
            this.popCapNhat.AcceptButtonCaption = null;
            this.popCapNhat.CancelButtonCaption = null;
            this.popCapNhat.Caption = "Cập nhật Hệ số";
            this.popCapNhat.ConfirmationMessage = null;
            this.popCapNhat.Id = "popCapNhat";
            this.popCapNhat.ImageName = "Action_Inline_Edit";
            this.popCapNhat.ToolTip = "Cập nhật Hệ số _ Chức danh theo môn học";
            this.popCapNhat.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popCapNhat_CustomizePopupWindowParams);
            this.popCapNhat.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popCapNhat_Execute);
            // 
            // btnMoKhoa_KhoiLuong
            // 
            this.btnMoKhoa_KhoiLuong.Caption = "Khóa/Mở khóa";
            this.btnMoKhoa_KhoiLuong.ConfirmationMessage = null;
            this.btnMoKhoa_KhoiLuong.Id = "btnMoKhoa_KhoiLuong";
            this.btnMoKhoa_KhoiLuong.ImageName = "Action_ResetPassword";
            this.btnMoKhoa_KhoiLuong.ToolTip = null;
            this.btnMoKhoa_KhoiLuong.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnMoKhoa_KhoiLuong_Execute);

            this.Activated += KhoiLuongGiangDay_CapNhat_HeSoChucDanhMonHoc_Activated;
        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popCapNhat;
        private DevExpress.ExpressApp.Actions.SimpleAction btnMoKhoa_KhoiLuong;
        
    }
}
