using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.Controllers.NhanSu
{
    partial class DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController
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
            this.simpleAction1.Caption = "Chọn cán bộ từ hệ thống";
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "DaoTao_ChonNhanVienLapQuyetDinhDaoTaoTuHeThongController";
            this.simpleAction1.ImageName = "BO_QuyetDinh";
            this.simpleAction1.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction1.TargetObjectType = typeof(QuyetDinhDaoTao);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction1.ToolTip = "Chọn cán bộ lập quyết định đào tạo";
            this.simpleAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // KhenThuong_ChonNhanVienLapQuyetDinhKhenThuongController
            // 
            this.Activated += new System.EventHandler(this.DaoTao_ChonNhanVienLapQuyetDinhDaoTaoController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
