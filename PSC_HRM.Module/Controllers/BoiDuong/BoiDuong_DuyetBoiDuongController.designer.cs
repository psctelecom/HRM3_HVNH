namespace PSC_HRM.Module.Controllers
{
    partial class BoiDuong_DuyetBoiDuongController
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
            this.simpleAction.Caption = "Duyệt bồi dưỡng";
            this.simpleAction.ConfirmationMessage = null;
            this.simpleAction.Id = "BoiDuong_DuyetBoiDuongController";
            this.simpleAction.ImageName = "BO_Document";
            this.simpleAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction.TargetObjectType = typeof(PSC_HRM.Module.BoiDuong.DangKyBoiDuong);
            this.simpleAction.TargetViewId = "";
            this.simpleAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction.ToolTip = "Duyệt đăng ký bồi dưỡng";
            this.simpleAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
            // 
            // BoiDuong_DuyetBoiDuongController
            // 
            this.Activated += new System.EventHandler(this.DanhGia_ImportChamCongController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
    }
}
