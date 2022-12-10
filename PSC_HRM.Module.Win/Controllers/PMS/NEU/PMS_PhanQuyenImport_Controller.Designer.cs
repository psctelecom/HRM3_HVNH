namespace PSC_HRM.Module.Win.Controllers.PMS.NEU
{
    partial class PMS_PhanQuyenImport_Controller
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
            this.pop_PhanQuyenImport = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pop_PhanQuyenImport
            // 
            this.pop_PhanQuyenImport.AcceptButtonCaption = null;
            this.pop_PhanQuyenImport.CancelButtonCaption = null;
            this.pop_PhanQuyenImport.Caption = "Chọn cán bộ";
            this.pop_PhanQuyenImport.ConfirmationMessage = null;
            this.pop_PhanQuyenImport.Id = "PMS_PhanQuyenImport_Controller";
            this.pop_PhanQuyenImport.ToolTip = null;
            this.pop_PhanQuyenImport.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pop_PhanQuyenImport_CustomizePopupWindowParams);
            this.pop_PhanQuyenImport.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pop_PhanQuyenImport_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pop_PhanQuyenImport;
    }
}
