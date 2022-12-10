namespace PSC_HRM.Module.Controllers
{
    partial class XepLoaiLaoDong_DanhGiaLan2Controller
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
            this.simpleAction2.Caption = "Đánh giá lần 2";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "XepLoaiLaoDong_DanhGiaLan2Controller";
            this.simpleAction2.ImageName = "BO_LapQD";
            this.simpleAction2.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.simpleAction2.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction2.TargetObjectType = typeof(PSC_HRM.Module.DanhGia.XepLoaiLaoDong);
            this.simpleAction2.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // DanhGia_DanhSachDanhGiaLan3Controller
            // 
            this.Activated += new System.EventHandler(this.XepLoaiLaoDong_DanhGiaLan2Controller_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;


    }
}
