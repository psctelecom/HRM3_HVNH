namespace PSC_HRM.Module.Controllers
{
    partial class TapSu_QuyetDinhBoNhiemNgachController
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
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction();
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "QĐ bổ nhiệm ngạch";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "TapSu_QuyetDinhBoNhiemNgachController";
            this.simpleAction1.ImageName = "BO_QuyetDinh";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Shortcut = null;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectsCriteria = null;
            this.simpleAction1.TargetObjectType = typeof(PSC_HRM.Module.XuLyQuyTrinh.TapSu.ThongTinHetHanTapSu);
            this.simpleAction1.TargetViewId = null;
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Lập QĐ bổ nhiệm ngạch";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // TapSu_QuyetDinhBoNhiemNgachController
            // 
            this.Activated += new System.EventHandler(this.TapSu_QuyetDinhCongNhanHetHanTapSuController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;

    }
}
