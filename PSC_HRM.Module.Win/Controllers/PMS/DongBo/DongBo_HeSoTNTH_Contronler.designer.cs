namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class DongBo_HeSoTNTH_Contronler
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
            this.btDongBo_HeSoTNTH = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btDongBo_HeSoTNTH
            // 
            this.btDongBo_HeSoTNTH.Caption = "Đồng bộ hệ số TNTH";
            this.btDongBo_HeSoTNTH.ConfirmationMessage = null;
            this.btDongBo_HeSoTNTH.Id = "DongBo_HeSoTNTH_Contronler";
            this.btDongBo_HeSoTNTH.ImageName = "Action_Reload";
            this.btDongBo_HeSoTNTH.ToolTip = null;
            this.btDongBo_HeSoTNTH.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btDongBo_HeSoTNTH_Execute);
            // 
            // DongBo_HeSoTNTH_Contronler
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Activated += DongBo_HeSoTNTH_Contronler_Activated;

        }


        #endregion
        

        private DevExpress.ExpressApp.Actions.SimpleAction btDongBo_HeSoTNTH;

    }
}
