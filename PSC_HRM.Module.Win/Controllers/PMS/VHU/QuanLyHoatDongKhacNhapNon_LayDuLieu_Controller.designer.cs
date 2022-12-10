using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class QuanLyHoatDongKhacNhapNon_LayDuLieu_Controller
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
            this.simpleAction2 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popKeKhaiSauGiang = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // btSearch
            // 
            this.btSearch.Caption = "Tìm kiếm";
            this.btSearch.ConfirmationMessage = null;
            this.btSearch.Id = "QuanLyHoatDongKhacNhapNon_LayDuLieu_Controller";
            this.btSearch.ImageName = "Action_Filter";
            this.btSearch.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyHDK_Nhap_Non);
            this.btSearch.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.btSearch.ToolTip = null;
            this.btSearch.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.btSearch.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btSearch_Execute);
            // 
            // simpleAction2
            // 
            this.simpleAction2.Caption = "Xóa";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "QuanLyHoatDongKhacNhapNon_Xoa_Controller";
            this.simpleAction2.ImageName = "Action_Delete";
            this.simpleAction2.TargetObjectType = typeof(PSC_HRM.Module.PMS.NghiepVu.NCKH.QuanLyHDK_Nhap_Non);
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction2.ToolTip = null;
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // popKeKhaiSauGiang
            // 
            this.popKeKhaiSauGiang.AcceptButtonCaption = null;
            this.popKeKhaiSauGiang.CancelButtonCaption = null;
            this.popKeKhaiSauGiang.Caption = "Thêm mới";
            this.popKeKhaiSauGiang.ConfirmationMessage = null;
            this.popKeKhaiSauGiang.Id = "QuanLyHoatDongKhacNhapNon_Themmoi_Controller";
            this.popKeKhaiSauGiang.ImageName = "Action_Inline_Edit";
            this.popKeKhaiSauGiang.ToolTip = null;
            this.popKeKhaiSauGiang.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popKeKhaiSauGiang_Execute);
            this.popKeKhaiSauGiang.CustomizePopupWindowParams += PopKeKhaiSauGiang_CustomizePopupWindowParams;
            // 
            // QuanLyHoatDongKhacNhapNon_LayDuLieu_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btSearch;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popKeKhaiSauGiang;
    }
}
