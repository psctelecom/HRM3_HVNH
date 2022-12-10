namespace PSC_HRM.Module.Win.Controllers.PMS.ThoiKhoaBieu
{
    partial class ThoiKhoaBieu_PhanCongGiangDay_Controller
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
            this.popPhanCongGiangDay = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popPhanCongGiangDay
            // 
            this.popPhanCongGiangDay.AcceptButtonCaption = null;
            this.popPhanCongGiangDay.CancelButtonCaption = null;
            this.popPhanCongGiangDay.Caption = "Phân công giảng dạy";
            this.popPhanCongGiangDay.ConfirmationMessage = null;
            this.popPhanCongGiangDay.Id = "ThoiKhoaBieu_PhanCongGiangDay_Controller";
            this.popPhanCongGiangDay.ImageName = "BO_CongViec";
            this.popPhanCongGiangDay.ToolTip = "Phân công giảng dạy cho những môn học chưa có giảng viên";
            this.popPhanCongGiangDay.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popPhanCongGiangDay_CustomizePopupWindowParams);
            this.popPhanCongGiangDay.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popPhanCongGiangDay_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popPhanCongGiangDay;
    }
}
