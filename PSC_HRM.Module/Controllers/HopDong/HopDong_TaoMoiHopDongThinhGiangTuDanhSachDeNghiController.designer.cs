namespace PSC_HRM.Module.Controllers
{
    partial class HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController
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
            this.popupWindowShowAction2 = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWindowShowAction2
            // 
            this.popupWindowShowAction2.AcceptButtonCaption = null;
            this.popupWindowShowAction2.CancelButtonCaption = null;
            this.popupWindowShowAction2.Caption = "Tạo hợp đồng mới";
            this.popupWindowShowAction2.ConfirmationMessage = null;
            this.popupWindowShowAction2.Id = "HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController";
            this.popupWindowShowAction2.ImageName = "BO_QuyetDinh";
            this.popupWindowShowAction2.TargetObjectType = typeof(PSC_HRM.Module.HopDong.QuanLyDeNghiMoiGiang);
            this.popupWindowShowAction2.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.popupWindowShowAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popupWindowShowAction2.ToolTip = "Tạo hợp đồng mới";
            this.popupWindowShowAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popupWindowShowAction2.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowAction2_CustomizePopupWindowParams);
            this.popupWindowShowAction2.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowAction2_Execute);
            // 
            // HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController
            // 
            this.Activated += new System.EventHandler(this.HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowAction2;
    }
}
