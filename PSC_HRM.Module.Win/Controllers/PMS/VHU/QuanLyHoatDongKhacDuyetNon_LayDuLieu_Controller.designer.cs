using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class QuanLyHoatDongKhacDuyetNon_LayDuLieu_Controller
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
            this.btSearch = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleAction2 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btSearch
            // 
            this.btSearch.Caption = "Tìm kiếm";
            this.btSearch.ConfirmationMessage = null;
            this.btSearch.Id = "QuanLyHoatDongKhacDuyetNon_LayDuLieu_Controller";
            this.btSearch.ImageName = "Action_Filter";
            this.btSearch.TargetObjectType = typeof(QuanLyHDK_Duyet_Non);
            this.btSearch.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.btSearch.ToolTip = null;
            this.btSearch.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.btSearch.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btSearch_Execute);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Xác nhận";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "QuanLyHoatDongKhacDuyetNon_XacNhan_Controller";
            this.simpleAction1.ImageName = "Action_Grant";
            this.simpleAction1.TargetObjectType = typeof(QuanLyHDK_Duyet_Non);
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // simpleAction2
            // 
            this.simpleAction2.Caption = "Hủy xác nhận";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "QuanLyHoatDongKhacDuyetNon_HuyXacNhan_Controller";
            this.simpleAction2.ImageName = "Action_Deny";
            this.simpleAction2.TargetObjectType = typeof(QuanLyHDK_Duyet_Non);
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction2.ToolTip = null;
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // HoatDongQuanLyNon_LayDuLieu_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btSearch;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
    }
}
