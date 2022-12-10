using PSC_HRM.Module.NghiPhep;
namespace PSC_HRM.Module.Controllers
{
    partial class NghiPhep_ImportSoNgayPhepNamController
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
            this.simpleAction.Caption = "Import số ngày phép năm";
            this.simpleAction.Id = "NghiPhep_ImportSoNgayPhepNamController";
            this.simpleAction.ImageName = "Action_Import";
            this.simpleAction.TargetObjectType = typeof(QuanLyNghiPhep);
            this.simpleAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction.ToolTip = "Import số ngày phép năm từ file excel";
            this.simpleAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;       
    }
}
