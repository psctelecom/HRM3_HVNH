namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class PMS_DanhMucNhieuHeSoChucDanh_ChayStore_Controller
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
            // 
            // popDongBo
            // 
            this.popDongBo.AcceptButtonCaption = null;
            this.popDongBo.CancelButtonCaption = null;
            this.popDongBo.Caption = "Thêm hệ số chức danh";
            this.popDongBo.ConfirmationMessage = null;
            this.popDongBo.Id = "PMS_DanhMucNhieuHeSoChucDanh_ChayStore_Controller";
            this.popDongBo.ImageName = "BO_XepLoaiLaoDong";
            this.popDongBo.ToolTip = null;
            this.popDongBo.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popDongBo_CustomizePopupWindowParams);
            this.popDongBo.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popDongBo_Execute);
            // 
            // PMS_DanhMucNhieuHeSoChucDanh_ChạyStore_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Activated += PMS_DanhMucNhieuHeSoChucDanh_ChayStore_Controller_Activated;


        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popDongBo;
    }
}
