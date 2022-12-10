namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class DongBo_BangChotThuLao_Contronler
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
            this.btDongBo_HeSo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.btn_MoKhoa = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btDongBo_HeSo
            // 
            this.btDongBo_HeSo.Caption = "Đồng bộ bảng chốt";
            this.btDongBo_HeSo.ConfirmationMessage = null;
            this.btDongBo_HeSo.Id = "DongBo_BangChotThuLao_Contronler";
            this.btDongBo_HeSo.ImageName = "Action_Reload";
            this.btDongBo_HeSo.ToolTip = null;
            this.btDongBo_HeSo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btDongBo_HeSo_Execute);
            // 
            // btn_MoKhoa
            // 
            this.btn_MoKhoa.Caption = "Mở khóa";
            this.btn_MoKhoa.ConfirmationMessage = null;
            this.btn_MoKhoa.Id = "DongBo_BangChotThuLao_MoKhoa_Contronler";
            this.btn_MoKhoa.ToolTip = null;
            this.btn_MoKhoa.ImageName = "Action_ResetPassword";
            this.btn_MoKhoa.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btn_MoKhoa_Execute);
            // 
            // DongBo_BangChotThuLao_Contronler
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btDongBo_HeSo;
        private DevExpress.ExpressApp.Actions.SimpleAction btn_MoKhoa;

    }
}
