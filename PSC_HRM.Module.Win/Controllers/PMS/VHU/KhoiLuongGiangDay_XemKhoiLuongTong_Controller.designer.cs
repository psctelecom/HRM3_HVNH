namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class KhoiLuongGiangDay_XemKhoiLuongTong_Controller
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
            this.popDongBoTK = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popDongBoTK
            // 
            this.popDongBoTK.AcceptButtonCaption = null;
            this.popDongBoTK.CancelButtonCaption = null;
            this.popDongBoTK.Caption = "Tổng hợp khối lượng giảng dạy";
            this.popDongBoTK.ConfirmationMessage = null;
            this.popDongBoTK.Id = "KhoiLuongGiangDay_XemKhoiLuongTong_Controller";
            this.popDongBoTK.ImageName = "BO_List";
            this.popDongBoTK.ToolTip = "Xem dữ liệu tổng hợp giảng dạy";
            this.popDongBoTK.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popDongBoTK_CustomizePopupWindowParams);
            this.popDongBoTK.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popDongBoTK_Execute);
            // 
            // ThoiKhoaBieu_DongBoDuLieu_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.Activated+= KhoiLuongGiangDay_XemKhoiLuongTong_Controller_Activated;
            this.ViewControlsCreated += KhoiLuongGiangDay_XemKhoiLuongTong_Controller_ViewControlsCreated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popDongBoTK;


    }
}
