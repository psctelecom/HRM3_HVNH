namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    partial class MailMerge_ThongBaoKeoDaiThoiGianCongTacController
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
            this.simpleAction1.Caption = "Thông báo kéo dài thời gian công tác";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "MailMerge_ThongBaoKeoDaiThoiGianCongTacController";
            this.simpleAction1.ImageName = "Action_Printing_Print";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.simpleAction1.TargetObjectType = typeof(PSC_HRM.Module.QuyetDinh.QuyetDinhKeoDaiThoiGianCongTac);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.ToolTip = "In thông báo kéo dài thời gian công tác";
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // MailMerge_ThongBaoNghiHuuController
            // 
            this.Activated += new System.EventHandler(this.MailMerge_ThongBaoKeoDaiThoiGianCongTacController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
