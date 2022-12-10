namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class BangChotThuLao_KhoaKhoiLuong_Controller
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
            this.btnKhoaBangChot = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btnKhoaBangChot
            // 
            this.btnKhoaBangChot.Caption = "Khóa_Khối lượng";
            this.btnKhoaBangChot.ConfirmationMessage = null;
            this.btnKhoaBangChot.Id = "BangChotThuLao_BangChot_Controller";
            this.btnKhoaBangChot.ImageName = "Action_Security_ChangePassword";
            this.btnKhoaBangChot.ToolTip = "PSC sử dụng - Khóa các table khối lượng ";
            this.btnKhoaBangChot.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnKhoaBangChot_Execute);
            this.Activated += BangChotThuLao_KhoaKhoiLuong_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btnKhoaBangChot;
    }
}
