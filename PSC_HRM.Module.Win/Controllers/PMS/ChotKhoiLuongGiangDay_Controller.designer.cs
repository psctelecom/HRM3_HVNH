namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class ChotKhoiLuongGiangDay_Controller
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
            this.btChotKhoiLuongGiangDay = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.btMoKhoa_BangChot = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.btQuyDoi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btChotKhoiLuongGiangDay
            // 
            this.btChotKhoiLuongGiangDay.Caption = "Chốt khối lượng - Tính thù lao";
            this.btChotKhoiLuongGiangDay.ConfirmationMessage = null;
            this.btChotKhoiLuongGiangDay.Id = "btChotKhoiLuongGiangDay";
            this.btChotKhoiLuongGiangDay.ImageName = "BO_Money_Calculator";
            this.btChotKhoiLuongGiangDay.ToolTip = null;
            this.btChotKhoiLuongGiangDay.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btChotKhoiLuongGiangDay_Execute);
            // 
            // btMoKhoa_BangChot
            // 
            this.btMoKhoa_BangChot.Caption = "Mở khóa";
            this.btMoKhoa_BangChot.ConfirmationMessage = null;
            this.btMoKhoa_BangChot.Id = "btMoKhoa_BangChot";
            this.btMoKhoa_BangChot.ImageName = "Action_ResetPassword";
            this.btMoKhoa_BangChot.ToolTip = null;
            this.btMoKhoa_BangChot.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btMoKhoa_BangChot_Execute);
            // 
            // btQuyDoi
            // 
            this.btQuyDoi.Caption = "Quy đổi";
            this.btQuyDoi.ConfirmationMessage = null;
            this.btQuyDoi.Id = "btQuyDoiBangChotThuLao";
            this.btQuyDoi.ToolTip = null;
            this.btQuyDoi.ImageName = "BO_DeNghiCapSo";
            this.btQuyDoi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btQuyDoi_Execute);
            // 
            // ChotKhoiLuongGiangDay_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Activated += ChotKhoiLuongGiangDay_Controller_Activated;
        }



        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btChotKhoiLuongGiangDay;
        private DevExpress.ExpressApp.Actions.SimpleAction btMoKhoa_BangChot;
        private DevExpress.ExpressApp.Actions.SimpleAction btQuyDoi;

    }
}
