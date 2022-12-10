namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    partial class KhoiLuongGiangDay_Xoa_Controller
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
            this.btnXoaKhoiLuongGiangDay = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btnXoaKhoiLuongGiangDay
            // 
            this.btnXoaKhoiLuongGiangDay.Caption = "Xóa dữ liệu đồng bộ";
            this.btnXoaKhoiLuongGiangDay.ConfirmationMessage = null;
            this.btnXoaKhoiLuongGiangDay.Id = "btnXoaKhoiLuongGiangDayController";
            this.btnXoaKhoiLuongGiangDay.ImageName = "Action_Delete";
            this.btnXoaKhoiLuongGiangDay.ToolTip = "Xóa dữ liệu đồng bộ";
            this.btnXoaKhoiLuongGiangDay.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnXoaKhoiLuongGiangDay_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btnXoaKhoiLuongGiangDay;
    }
}
