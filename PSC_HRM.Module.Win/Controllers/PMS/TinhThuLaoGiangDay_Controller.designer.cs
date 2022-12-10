namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class TinhThuLaoGiangDay_Controller
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
            this.btTinhThuLao = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btTinhThuLao
            // 
            this.btTinhThuLao.Caption = "Tính thù lao giảng dạy";
            this.btTinhThuLao.ConfirmationMessage = null;
            this.btTinhThuLao.Id = "btTinhThuLao";
            this.btTinhThuLao.ImageName = "BO_Money1";
            this.btTinhThuLao.ToolTip = null;
            this.btTinhThuLao.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btTinhThuLao_Execute);
            this.Activated+=TinhThuLaoGiangDay_Controller_Activated;
            // 
            // TinhThuLaoGiangDay_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btTinhThuLao;

    }
}
