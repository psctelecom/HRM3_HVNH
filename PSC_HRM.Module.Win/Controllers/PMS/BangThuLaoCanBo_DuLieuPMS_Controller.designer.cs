namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class BangThuLaoCanBo_DuLieuPMS_Controller
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
            this.popDongBo = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popCheckThanhToan = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popDongBo
            // 
            this.popDongBo.AcceptButtonCaption = null;
            this.popDongBo.CancelButtonCaption = null;
            this.popDongBo.Caption = "Lấy dữ liệu PMS";
            this.popDongBo.ConfirmationMessage = null;
            this.popDongBo.Id = "BangThuLaoCanBo_DuLieuPMS";
            this.popDongBo.ImageName = "BO_XepLoaiLaoDong";
            this.popDongBo.ToolTip = null;
            this.popDongBo.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popDongBo_CustomizePopupWindowParams);
            this.popDongBo.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popDongBo_Execute);
            // 
            // popCheckThanhToan
            // 
            this.popCheckThanhToan.AcceptButtonCaption = null;
            this.popCheckThanhToan.CancelButtonCaption = null;
            this.popCheckThanhToan.Caption = "Check thanh toán";
            this.popCheckThanhToan.ConfirmationMessage = null;
            this.popCheckThanhToan.ImageName = "Action_Grant";
            this.popCheckThanhToan.Id = "BangThuLaoCanBo_Check_DaThanhToan";
            this.popCheckThanhToan.ToolTip = null;
            this.popCheckThanhToan.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popCheckThanhToan_CustomizePopupWindowParams);
            this.popCheckThanhToan.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popCheckThanhToan_Execute);
            // 
            // BangThuLaoCanBo_DuLieuPMS_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popDongBo;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popCheckThanhToan;


    }
}
