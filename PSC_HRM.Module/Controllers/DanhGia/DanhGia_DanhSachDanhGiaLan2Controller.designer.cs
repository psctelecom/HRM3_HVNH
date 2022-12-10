namespace PSC_HRM.Module.Controllers
{
    partial class DanhGia_DanhSachDanhGiaLan2Controller
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
            this.simpleAction2.Caption = "Chốt đánh giá cán bộ";
            this.simpleAction2.ConfirmationMessage = null;
            this.simpleAction2.Id = "DanhGia_DanhSachDanhGiaLan2Controller";
            this.simpleAction2.ImageName = "BO_LapQD";
            this.simpleAction2.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.simpleAction2.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.simpleAction2.TargetObjectType = typeof(PSC_HRM.Module.DanhGia.ChiTietDanhGiaCanBoCuoiNamLan2);
            this.simpleAction2.TargetViewNesting = DevExpress.ExpressApp.Nesting.Nested;
            this.simpleAction2.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.simpleAction2.ToolTip = "Chốt đánh giá cán bộ";
            this.simpleAction2.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.simpleAction2.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction2_Execute);
            // 
            // DanhGia_DanhSachDanhGiaLan3Controller
            // 
            this.Activated += new System.EventHandler(this.DanhGia_DanhSachDanhGiaLan2Controller_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction2;


    }
}
