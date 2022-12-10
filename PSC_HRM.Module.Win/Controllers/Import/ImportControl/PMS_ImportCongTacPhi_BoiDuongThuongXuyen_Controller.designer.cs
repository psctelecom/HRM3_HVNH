namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_ImportCongTacPhi_BoiDuongThuongXuyen_Controller
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
            this.btImportThuLao.Caption = "Import công tác phí";
            this.btImportThuLao.ConfirmationMessage = null;
            this.btImportThuLao.Id = "PMS_ImportCongTacPhi_BoiDuongThuongXuyen_Controller";
            this.btImportThuLao.ImageName = "Action_Import";
            this.btImportThuLao.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.btImportThuLao.ToolTip = "Import công tác phí bồi dưỡng thường xuyên từ file excel";
            this.btImportThuLao.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.btImportThuLao.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImportThuLao_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImportThuLao;
    }
}
