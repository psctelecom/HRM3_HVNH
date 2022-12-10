namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class QuanLyHDQL_DongBoExamination_Controller
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
            this.btDongBo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btXoa
            // 
            this.btDongBo.Caption = "Đồng bộ Examination";
            this.btDongBo.ConfirmationMessage = null;
            this.btDongBo.Id = "QuanLyHDQL_DongBoExamination_Controller";
            this.btDongBo.ImageName = "Action_Reload";
            this.btDongBo.ToolTip = null;
            this.btDongBo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btDongBo_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btDongBo;
    }
}
