namespace PSC_HRM.Module.Controllers
{
    partial class BoNhiemNgach_ImportBoNhiemNgachController
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
            this.simpleAction1.Caption = "Import cán bộ";
            this.simpleAction1.Id = "BoNhiemNgach_ImportBoNhiemNgachController";
            this.simpleAction1.ImageName = "Action_Import";
            this.simpleAction1.TargetObjectType = typeof(PSC_HRM.Module.QuyetDinh.QuyetDinhBoNhiemNgach);
            this.simpleAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.simpleAction1.TargetViewType = DevExpress.ExpressApp.ViewType.Any;
            this.simpleAction1.ToolTip = "Import bổ nhiệm ngạch từ file excel";          
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
