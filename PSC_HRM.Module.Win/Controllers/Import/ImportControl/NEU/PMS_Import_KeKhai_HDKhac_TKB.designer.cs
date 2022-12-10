namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_Import_KeKhai_HDKhac_TKB
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
            this.btImport = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImport
            // 
            this.btImport.Caption = "Import";
            this.btImport.ConfirmationMessage = null;
            this.btImport.Id = "PMS_Import_KeKhai_HDKhac_TKB";
            this.btImport.ImageName = "Action_Import";
            this.btImport.ToolTip = null;
            this.btImport.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImport_Execute);
            this.Activated += PMS_Import_KeKhai_HDKhac_TKB_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btImport;


    }
}
