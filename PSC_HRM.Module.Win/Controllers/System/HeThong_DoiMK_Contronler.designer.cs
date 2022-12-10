namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class HeThong_DoiMK_Contronler
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
            this.btThongTinTaiKhoan = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btThongTinTaiKhoan
            // 
            this.btThongTinTaiKhoan.Caption = "Đổi mật khẩu";
            this.btThongTinTaiKhoan.ConfirmationMessage = null;
            this.btThongTinTaiKhoan.Id = "btThongTinTaiKhoan_DoiMK";
            this.btThongTinTaiKhoan.ImageName = "Action_ResetPassword";
            this.btThongTinTaiKhoan.ToolTip = null;
            this.btThongTinTaiKhoan.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btThongTinTaiKhoan_Execute);
            // 
            // HeThong_DoiMK_Contronler
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btThongTinTaiKhoan;

    }
}
