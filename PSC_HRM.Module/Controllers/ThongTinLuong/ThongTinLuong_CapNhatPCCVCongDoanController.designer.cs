namespace PSC_HRM.Module.Controllers
{
    partial class ThongTinLuong_CapNhatPCCVCongDoanController
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
            this.simpleAction2 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction2
            // 
            this.simpleAction2.Caption = "Cập nhật PCCV Công đoàn";
            this.simpleAction2.ConfirmationMessage = "Bạn có muốn cập nhật phụ cấp chức vụ Công đoàn vào hồ sơ không?";
            this.simpleAction2.Id = "ThongTinLuong_CapNhatPCCVCongDoanController";
            this.simpleAction2.ImageName = "edit1_32x32";
            this.simpleAction2.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction2.Tag = null;
            this.simpleAction2.TargetObjectType = typeof(PSC_HRM.Module.DanhMuc.ChucVuDoanThe);
            this.simpleAction2.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction2.ToolTip = "Cập nhật phụ cấp chức vụ Công đoàn";
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;
    }
}
