namespace PSC_HRM.Module.ThuNhap.Controllers
{
    partial class ThuNhapTangThem_TinhQuyetToanTNTTController
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
            this.simpleAction1.Caption = "Quyết toán";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "ThuNhapTangThem_TinhController";
            this.simpleAction1.ImageName = "BO_NangLuong";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.Shortcut = null;
            this.simpleAction1.Tag = null;
            this.simpleAction1.TargetObjectsCriteria = null;
            this.simpleAction1.TargetObjectType = typeof(PSC_HRM.Module.ThuNhap.ThuNhapTangThem.BangQuyetToanThuNhapTangThem);
            this.simpleAction1.TargetViewId = null;
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Quyết toán thu nhập tăng thêm";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // ThuNhapTangThem_TinhController
            // 
            this.Activated += new System.EventHandler(this.ThuNhapTangThem_TinhQuyetToanTNTTController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
