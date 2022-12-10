namespace PSC_HRM.Module.Win.Controllers.PMS.NEU
{
    partial class ThoiKhoaBieu_Delete_Controller
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
            this.btn_Delete_TKB = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // btn_Delete_TKB
            // 
            this.btn_Delete_TKB.AcceptButtonCaption = null;
            this.btn_Delete_TKB.CancelButtonCaption = null;
            this.btn_Delete_TKB.Caption = "Xóa DS TKB";
            this.btn_Delete_TKB.ConfirmationMessage = "Xóa TKB";
            this.btn_Delete_TKB.Id = "btn_Delete_TKB";
            this.btn_Delete_TKB.ImageName = "Action_Delete";
            this.btn_Delete_TKB.ToolTip = null;
            this.btn_Delete_TKB.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.btn_Delete_TKB_CustomizePopupWindowParams);
            this.btn_Delete_TKB.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.btn_Delete_TKB_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction btn_Delete_TKB;
    }
}
