using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class NghienCuuKHQuanLyNon_LayDuLieu_Controllery
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
            this.simpleAction3 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleAction4 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleAction5 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleAction6 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btSearch
            // 
            this.btSearch.Caption = "Tìm kiếm";
            this.btSearch.ConfirmationMessage = null;
            this.btSearch.Id = "NghienCuuKHQuanLyNon_LayDuLieu_Controllery";
            this.btSearch.ImageName = "Action_Filter";
            this.btSearch.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyNCKH_Non);
            this.btSearch.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.btSearch.ToolTip = null;
            this.btSearch.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.btSearch.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btSearch_Execute);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Xác nhận";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "NghienCuuKHQuanLyNon_XacNhan_Controller";
            this.simpleAction1.ImageName = "Action_Grant";
            this.simpleAction1.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyNCKH_Non);
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // simpleAction2
            // 
            this.simpleAction2.Caption = "Hủy xác nhận";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "NghienCuuKHQuanLyNon_HuyXacNhan_Controller";
            this.simpleAction2.ImageName = "Action_Deny";
            this.simpleAction2.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyNCKH_Non);
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction2.ToolTip = null;
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // simpleAction3
            // 
            this.simpleAction3.Caption = "Từ chối";
            this.simpleAction3.ConfirmationMessage = null;
            this.simpleAction3.Id = "NghienCuuKHQuanLyNon_TuChoi_Controller";
            this.simpleAction3.ImageName = "Action_Grant";
            this.simpleAction3.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyNCKH_Non);
            this.simpleAction3.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction3.ToolTip = null;
            this.simpleAction3.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction3.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction3_Execute);
            // 
            // simpleAction4
            // 
            this.simpleAction4.Caption = "Hủy từ chối";
            this.simpleAction4.ConfirmationMessage = null;
            this.simpleAction4.Id = "NghienCuuKHQuanLyNon_HuyTuChoi_Controller";
            this.simpleAction4.ImageName = "Action_Deny";
            this.simpleAction4.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyNCKH_Non);
            this.simpleAction4.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction4.ToolTip = null;
            this.simpleAction4.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction4.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction5_Execute);
            // 
            // simpleAction5
            // 
            this.simpleAction5.Caption = "Đã thanh toán";
            this.simpleAction5.ConfirmationMessage = null;
            this.simpleAction5.Id = "NghienCuuKHQuanLyNon_DaThanhToan_Controller";
            this.simpleAction5.ImageName = "Action_Grant";
            this.simpleAction5.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyNCKH_Non);
            this.simpleAction5.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction5.ToolTip = null;
            this.simpleAction5.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // simpleAction6
            // 
            this.simpleAction6.Caption = "Hủy Đã thanh toán";
            this.simpleAction6.ConfirmationMessage = null;
            this.simpleAction6.Id = "NghienCuuKHQuanLyNon_HuyDaThanhToan_Controller";
            this.simpleAction6.ImageName = "Action_Deny";
            this.simpleAction6.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyNCKH_Non);
            this.simpleAction6.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction6.ToolTip = null;
            this.simpleAction6.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction6.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction6_Execute);
            // 
            // NghienCuuKHQuanLyNon_LayDuLieu_Controllery
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btSearch;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction3;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction4;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction5;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction6;
    }
}
