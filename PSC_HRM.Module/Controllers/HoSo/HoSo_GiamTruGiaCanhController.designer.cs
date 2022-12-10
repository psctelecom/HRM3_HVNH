namespace PSC_HRM.Module.Controllers
{
    partial class HoSo_GiamTruGiaCanhController
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
            this.simpleAction.Caption = "Giảm trừ gia cảnh";
            this.simpleAction.Id = "GiamTruGiaCanh";
            this.simpleAction.ImageName = "BO_GiaDinh";
            this.simpleAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.simpleAction.Tag = null;
            this.simpleAction.TargetObjectType = typeof(PSC_HRM.Module.HoSo.QuanHeGiaDinh);
            this.simpleAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction.ToolTip = "Khai báo giảm trừ gia cảnh";
            this.simpleAction.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
            // 
            // HoSo_GiamTruGiaCanhController
            // 
            this.Activated += new System.EventHandler(this.HoSo_GiamTruGiaCanhController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
    }
}
