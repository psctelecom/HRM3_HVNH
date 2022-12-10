namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class CapNhat_DaTinhTien_Controller
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
            this.btnCapNhat = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.btnCapNhatNCKH = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Caption = "Cập Nhật Đã Tính Tiền";
            this.btnCapNhat.ConfirmationMessage = null;
            this.btnCapNhat.Id = "CapNhat_DaTinhTien_Controller";
            this.btnCapNhat.ImageName = "Action_UpdateProcess";
            this.btnCapNhat.ToolTip = null;
            this.btnCapNhat.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnCapNhat_Execute);
            // 
            // btnCapNhatNCKH
            // 
            this.btnCapNhatNCKH.Caption = "Cập Nhật NCKH";
            this.btnCapNhatNCKH.ConfirmationMessage = null;
            this.btnCapNhatNCKH.Id = "CapNhat_NCKH_Controller";
            this.btnCapNhatNCKH.ImageName = "Action_UpdateProcess";
            this.btnCapNhatNCKH.ToolTip = null;
            this.btnCapNhatNCKH.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnCapNhatNCKH_Execute);
            this.Activated += CapNhat_DaTinhTien_Controller_Activated;
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btnCapNhat;
        private DevExpress.ExpressApp.Actions.SimpleAction btnCapNhatNCKH;
    }
}
