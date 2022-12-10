namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class BaoCaoTong_VHU_Controller
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
            this.popupWindowShowAction1 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowAction2 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popDongBo
            // 
            this.popDongBo.AcceptButtonCaption = null;
            this.popDongBo.CancelButtonCaption = null;
            this.popDongBo.Caption = "Báo cáo tổng";
            this.popDongBo.ConfirmationMessage = null;
            this.popDongBo.Id = "BaoCaoTong_VHU_Controller";
            this.popDongBo.ImageName = "BO_XepLoaiLaoDong";
            this.popDongBo.ToolTip = null;
            this.popDongBo.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popDongBo_CustomizePopupWindowParams);
            this.popDongBo.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popDongBo_Execute);
            // 
            // popupWindowShowAction1
            // 
            this.popupWindowShowAction1.AcceptButtonCaption = null;
            this.popupWindowShowAction1.CancelButtonCaption = null;
            this.popupWindowShowAction1.Caption = "Báo cáo chi tiết";
            this.popupWindowShowAction1.ConfirmationMessage = null;
            this.popupWindowShowAction1.Id = "BaoCaoChitiet_VHU_Controller";
            this.popupWindowShowAction1.ImageName = "BO_XepLoaiLaoDong";
            this.popupWindowShowAction1.ToolTip = null;
            this.popupWindowShowAction1.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction1_CustomizePopupWindowParams);
            this.popupWindowShowAction1.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction1_Execute);
            // 
            // popupWindowShowAction2
            // 
            this.popupWindowShowAction2.AcceptButtonCaption = null;
            this.popupWindowShowAction2.CancelButtonCaption = null;
            this.popupWindowShowAction2.Caption = "Hợp đồng giảng dạy";
            this.popupWindowShowAction2.ConfirmationMessage = null;
            this.popupWindowShowAction2.Id = "HopDongGiangDay_VHU_Controller";
            this.popupWindowShowAction2.ImageName = "BO_XepLoaiLaoDong";
            this.popupWindowShowAction2.ToolTip = null;
            this.popupWindowShowAction2.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction2_CustomizePopupWindowParams);
            this.popupWindowShowAction2.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction2_Execute);
            // 
            // BaoCaoTong_VHU_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popDongBo;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction1;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction2;
    }
}
