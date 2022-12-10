using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Win.Controllers
{
    partial class HoSo_TaoHoSoNhanVienGiangVienCoHuuController
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
            // simpleAction1
            // 
            this.simpleAction.Caption = "Tạo hồ sơ cơ hữu";
            this.simpleAction.Id = "HoSo_TaoHoSoNhanVienGiangVienCoHuuController";
            this.simpleAction.ImageName = "BO_Group3";
            this.simpleAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction.Tag = null;
            this.simpleAction.TargetObjectType = typeof(GiangVienThinhGiang);
            this.simpleAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction.ToolTip = "Tạo hồ sơ giảng viên cơ hữu";
            this.simpleAction.TypeOfView = null;
            this.simpleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
            // 
            // KhoaHoSoAction
            // 
            this.Activated += new System.EventHandler(this.HoSo_TaoHoSoNhanVienGiangVienCoHuuController_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
    }
}
