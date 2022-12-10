using DevExpress.ExpressApp;
using PSC_HRM.Module.PMS.NonPersistentObjects.TaiChinh;
namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class ThongKe_BaoCao_PhuLuc01_Controller
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
            this.simpleAction1.Id = "ThongKe_BaoCao_PhuLuc01_Controller";
            this.simpleAction1.Caption = "Lấy dữ liệu";
            this.simpleAction1.ImageName = "BO_LocDuLieu";
            this.TargetObjectType = typeof(Pivot_ChiTietPhuLuc01);
            this.simpleAction1.TargetViewType = ViewType.Any;
            this.simpleAction1.TargetViewNesting = Nesting.Any;
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // Kho_CapNhatKhoController
            // 
        this.Actions.Add(this.simpleAction1);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}


