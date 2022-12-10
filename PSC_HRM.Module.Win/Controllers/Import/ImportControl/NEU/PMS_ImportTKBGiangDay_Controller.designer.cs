namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_ImportTKBGiangDay_Controller
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
            this.pop_Import_TKB = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.btImportTKB = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // pop_Import_TKB
            // 
            this.pop_Import_TKB.AcceptButtonCaption = null;
            this.pop_Import_TKB.CancelButtonCaption = null;
            this.pop_Import_TKB.Caption = "Import TKB";
            this.pop_Import_TKB.ConfirmationMessage = null;
            this.pop_Import_TKB.Id = "pop_Import_TKB";
            this.pop_Import_TKB.ImageName = "Action_Import";
            this.pop_Import_TKB.ToolTip = "Chọn đơn vị, bậc đào tạo, hệ đào tạo để import";
            this.pop_Import_TKB.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pop_Import_TKB_CustomizePopupWindowParams);
            this.pop_Import_TKB.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pop_Import_TKB_Execute);
            // 
            // btImportTKB
            // 
            this.btImportTKB.Caption = "Import";
            this.btImportTKB.ConfirmationMessage = null;
            this.btImportTKB.Id = "btImportTKB";
            this.btImportTKB.ImageName = "Action_Import";
            this.btImportTKB.ToolTip = null;
            this.btImportTKB.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportTKB_Execute);
            this.Activated += PMS_ImportTKBGiangDay_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pop_Import_TKB;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportTKB;

    }
}
