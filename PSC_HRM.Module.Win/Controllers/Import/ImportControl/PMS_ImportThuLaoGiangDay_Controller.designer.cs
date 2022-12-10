namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_ImportThuLaoGiangDay_Controller
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
            this.btImportThuLao = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImportThuLao
            // 
            this.btImportThuLao.Caption = "Import Thù lao giảng dạy";
            this.btImportThuLao.ConfirmationMessage = null;
            this.btImportThuLao.Id = "PMS_ImportThuLaoGiangDay_Controller";
            this.btImportThuLao.ImageName = "Action_Import";
            this.btImportThuLao.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.btImportThuLao.ToolTip = "Import thù lao giảng dạy từ file excel";
            this.btImportThuLao.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.btImportThuLao.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportThuLao_Execute);
            this.Activated += PMS_ImportThuLaoGiangDay_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportThuLao;
    }
}
