namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class BaoCao_TongHopDuLieuTuXa_Controller
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
            this.btnSearch = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btnSearch
            // 
            this.btnSearch.Caption = "Báo cáo bảng tổng hợp dữ liệu từ xa";
            this.btnSearch.ConfirmationMessage = null;
            this.btnSearch.Id = "4812dc4b-dad3-4c9f-bfa3-6f3e2002732b";
            this.btnSearch.ImageName = "BO_ChiTietLuong";
            this.btnSearch.TargetObjectType = typeof(PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa.QuanLyXemKeKhaiTuXa_Non);
            this.btnSearch.ToolTip = null;
            this.btnSearch.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnSearch_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btnSearch;
    }
}
