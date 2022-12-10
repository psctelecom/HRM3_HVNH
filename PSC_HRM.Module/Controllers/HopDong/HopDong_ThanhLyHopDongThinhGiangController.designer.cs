namespace PSC_HRM.Module.Controllers
{
    partial class HopDong_ThanhLyHopDongThinhGiangController
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
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.simpleAction1.Caption = "Thanh lý hợp đồng";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "ThanhLyHopDongThinhGiangController";
            this.simpleAction1.ImageName = "BO_Contract";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.Independent;
            this.simpleAction1.Shortcut = "";
            this.simpleAction1.TargetObjectType = typeof(PSC_HRM.Module.HopDong.HopDong);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction1.ToolTip = "Thanh lý hợp đồng";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // HopDong_ThanhLyHopDongThinhGiangController
            // 
            this.Activated += new System.EventHandler(this.HopDong_ThanhLyHopDongThinhGiangController_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
