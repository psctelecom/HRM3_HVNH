namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class KhaoThi_DongBo_Controller
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
            this.btDongBoKhaoThi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btDongBoKhaoThi
            // 
            this.btDongBoKhaoThi.Caption = "Đồng bộ";
            this.btDongBoKhaoThi.ConfirmationMessage = null;
            this.btDongBoKhaoThi.Id = "KhaoThi_DongBo_Controller";
            this.btDongBoKhaoThi.ImageName = "BO_Money1";
            this.btDongBoKhaoThi.ToolTip = null;
            this.btDongBoKhaoThi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btDongBoKhaoThi_Execute);
            // 
            // KhaoThi_DongBo_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Activated += KhaoThi_DongBo_Controller_Activated;
        }



        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btDongBoKhaoThi;

    }
}
