namespace PSC_HRM.Module.Controllers
{
    partial class DaoTao_DuyetDaoTaoController
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
            this.simpleAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction
            // 
            this.simpleAction.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleAction.Caption = "Duyệt đào tạo";
            this.simpleAction.ConfirmationMessage = null;
            this.simpleAction.Id = "DaoTao_DuyetDaoTaoController";
            this.simpleAction.ImageName = "BO_Document";
            this.simpleAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.simpleAction.Shortcut = null;
            this.simpleAction.Tag = null;
            this.simpleAction.TargetObjectsCriteria = null;
            this.simpleAction.TargetObjectType = typeof(PSC_HRM.Module.DaoTao.DangKyDaoTao);
            this.simpleAction.TargetViewId = null;
            this.simpleAction.ToolTip = "Duyệt đăng ký đào tạo";
            this.simpleAction.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.simpleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
            // 
            // DaoTao_DuyetDaoTaoController
            // 
            this.Activated += new System.EventHandler(this.DanhGia_ImportChamCongController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
    }
}
