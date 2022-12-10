namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class QuanLyGioChuan_Update_DinhMuc_Controller
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
            this.btn_Update_DM = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // btn_Update_DM
            // 
            this.btn_Update_DM.AcceptButtonCaption = null;
            this.btn_Update_DM.CancelButtonCaption = null;
            this.btn_Update_DM.Caption = "Kê Khai DM";
            this.btn_Update_DM.ConfirmationMessage = null;
            this.btn_Update_DM.Id = "btn_Update_DM";
            this.btn_Update_DM.ImageName = "Action_Inline_Edit";
            this.btn_Update_DM.ToolTip = null;
            this.btn_Update_DM.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.btn_Update_DM_CustomizePopupWindowParams);
            this.btn_Update_DM.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.btn_Update_DM_Execute);
            this.Activated += QuanLyGioChuan_Update_DinhMuc_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction btn_Update_DM;
    }
}
