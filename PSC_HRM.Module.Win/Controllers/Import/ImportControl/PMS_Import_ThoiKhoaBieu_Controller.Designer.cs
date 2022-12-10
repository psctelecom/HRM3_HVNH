namespace PSC_HRM.Module.Win.Controllers.Import.ImportControl
{
    partial class PMS_Import_ThoiKhoaBieu_Controller
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
            this.ImportThoiKB = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // ImportThoiKB
            // 
            this.ImportThoiKB.AcceptButtonCaption = null;
            this.ImportThoiKB.CancelButtonCaption = null;
            this.ImportThoiKB.Caption = "Nhập Thời Khóa Biểu";
            this.ImportThoiKB.ImageName = "Action_Import";
            this.ImportThoiKB.ConfirmationMessage = null;
            this.ImportThoiKB.Id = "ImportThoiKB";
            this.ImportThoiKB.ToolTip = null;
            this.ImportThoiKB.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ImportThoiKB_CustomizePopupWindowParams);
            this.ImportThoiKB.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ImportThoiKB_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ImportThoiKB;
    }
}
