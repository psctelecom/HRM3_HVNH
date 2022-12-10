using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using PSC_HRM.Module.PMS.NonPersistentObjects;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class PMS_tkb_LayThongTinChiTietController
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
            // 
            // btSearch
            // 
            this.btSearch.Caption = "Lấy Thông tin";
            this.btSearch.ConfirmationMessage = null;
            this.btSearch.Id = "PMS_tkb_LayThongTinChiTietController";
            this.btSearch.ImageName = "BO_ChiTietLuong";
            this.btSearch.TargetObjectType = typeof(DanhSachThongTinThoiKhoaBieuGiangVien);
            this.btSearch.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.btSearch.ToolTip = null;
            this.btSearch.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.btSearch.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
            // 
            // BaoCao_HopDong_VHU_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btSearch;
    }
}
