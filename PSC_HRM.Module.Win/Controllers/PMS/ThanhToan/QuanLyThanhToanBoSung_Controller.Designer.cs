namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class QuanLyThanhToanBoSung_Controller
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
            this.btnChinhSua = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // btnChinhSua
            // 
            this.btnChinhSua.AcceptButtonCaption = null;
            this.btnChinhSua.CancelButtonCaption = null;
            this.btnChinhSua.Caption = "Danh sách";
            this.btnChinhSua.ConfirmationMessage = null;
            this.btnChinhSua.Id = "QuanLyThanhToanBoSung_Controller";
            this.btnChinhSua.ImageName = "Action_Inline_Edit";
            this.btnChinhSua.ToolTip = null;
            this.btnChinhSua.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.btnChinhSua_CustomizePopupWindowParams_1);
            this.btnChinhSua.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.btnChinhSua_Execute);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction btnChinhSua;
    }
}
