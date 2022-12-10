namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    partial class LoadDuLieu_TinhThuLao_Controller
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
            this.popupTinhThuLao = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupTinhThuLao
            // 
            this.popupTinhThuLao.AcceptButtonCaption = null;
            this.popupTinhThuLao.CancelButtonCaption = null;
            this.popupTinhThuLao.Caption = "Tính thù lao";
            this.popupTinhThuLao.ConfirmationMessage = null;
            this.popupTinhThuLao.Id = "PMS_TinhThuLao_Controller";
            this.popupTinhThuLao.ImageName = "BO_Money1";
            this.popupTinhThuLao.ToolTip = null;
            this.popupTinhThuLao.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupTinhThuLao_CustomizePopupWindowParams);
            this.popupTinhThuLao.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupTinhThuLao_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupTinhThuLao;

    }
}
