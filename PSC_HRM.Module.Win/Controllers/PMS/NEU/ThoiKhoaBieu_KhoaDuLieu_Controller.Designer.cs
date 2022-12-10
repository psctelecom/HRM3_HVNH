namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class ThoiKhoaBieu_KhoaDuLieu_Controller
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
            this.popKhoaTKB = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popKhoaTKB
            // 
            this.popKhoaTKB.AcceptButtonCaption = null;
            this.popKhoaTKB.CancelButtonCaption = null;
            this.popKhoaTKB.Caption = "Khóa DL TKB";
            this.popKhoaTKB.ConfirmationMessage = null;
            this.popKhoaTKB.Id = "popKhoaTKB";
            this.popKhoaTKB.ImageName = "Action_Security_ChangePassword";
            this.popKhoaTKB.ToolTip = "Khóa dữ liệu TKB";
            this.popKhoaTKB.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popKhoaTKB_CustomizePopupWindowParams);
            this.popKhoaTKB.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popKhoaTKB_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popKhoaTKB;
    }
}
