namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    partial class ChiTietKhoiLuongThanhToan_Xoa_Controller
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
            this.btnXoa = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = null;
            this.btnXoa.ConfirmationMessage = null;
            this.btnXoa.Id = "abb3bbc0-32c0-491c-a22e-1b6ca931712b";
            this.btnXoa.ImageName = "Action_Delete";
            this.btnXoa.ToolTip = null;
            this.btnXoa.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnXoa_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btnXoa;
    }
}
