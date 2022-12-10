namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class ChonBoPhan_SoGio_Controller
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
            this.btnChonBoPhan_SoGio = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // btnChonBoPhan_SoGio
            // 
            this.btnChonBoPhan_SoGio.AcceptButtonCaption = null;
            this.btnChonBoPhan_SoGio.CancelButtonCaption = null;
            this.btnChonBoPhan_SoGio.Caption = "Kê khai NCKH";
            this.btnChonBoPhan_SoGio.ConfirmationMessage = null;
            this.btnChonBoPhan_SoGio.Id = "ChonBoPhan_SoGio_Controller";
            this.btnChonBoPhan_SoGio.ImageName = "Action_Inline_Edit";
            this.btnChonBoPhan_SoGio.ToolTip = null;
            this.btnChonBoPhan_SoGio.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.btnChonBoPhan_SoGio_CustomizePopupWindowParams);
            this.btnChonBoPhan_SoGio.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.btnChonBoPhan_SoGio_Execute);
            this.Activated+=ChonBoPhan_SoGio_Controller_Activated;
        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction btnChonBoPhan_SoGio;
    }
}
