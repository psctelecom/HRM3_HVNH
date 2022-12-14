namespace PSC_HRM.Module.Win.Controllers
{
    partial class ReportManagerController
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem1 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem2 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.actReportManager = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // actReportManager
            // 
            this.actReportManager.Caption = "Mẫu báo cáo";
            this.actReportManager.Category = "Tools";
            this.actReportManager.ConfirmationMessage = null;
            this.actReportManager.Id = "ReportManager";
            this.actReportManager.ImageName = "Action_ReportTemplate";
            choiceActionItem1.Caption = "Export";
            choiceActionItem1.ImageName = "Action_Export";
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "Import";
            choiceActionItem2.ImageName = "Action_Import";
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            this.actReportManager.Items.Add(choiceActionItem1);
            this.actReportManager.Items.Add(choiceActionItem2);
            this.actReportManager.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.actReportManager.Shortcut = null;
            this.actReportManager.Tag = null;
            this.actReportManager.TargetObjectsCriteria = null;
            this.actReportManager.TargetObjectType = typeof(PSC_HRM.Module.Report.HRMReport);
            this.actReportManager.TargetViewId = null;
            this.actReportManager.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.actReportManager.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.actReportManager.ToolTip = "Export và Import các mẫu báo cáo trong chương trình, mặc định";
            this.actReportManager.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.actReportManager.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.actReportManager_Execute);
            // 
            // ReportManagerController
            // 
            this.TargetWindowType = DevExpress.ExpressApp.WindowType.Main;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction actReportManager;
    }
}
