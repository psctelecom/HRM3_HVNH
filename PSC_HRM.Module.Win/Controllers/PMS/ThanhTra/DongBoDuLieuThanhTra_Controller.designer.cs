namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class DongBoDuLieuThanhTra_Controller
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
            this.btDongBoKhoiLuongGiangDay = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btDongBoKhoiLuongGiangDay
            // 
            this.btDongBoKhoiLuongGiangDay.Caption = "Đổng bộ dữ liệu";
            this.btDongBoKhoiLuongGiangDay.ConfirmationMessage = null;
            this.btDongBoKhoiLuongGiangDay.Id = "btDongBoKhoiLuongGiangDay";
            this.btDongBoKhoiLuongGiangDay.ImageName = "BO_Money_Calculator";
            this.btDongBoKhoiLuongGiangDay.ToolTip = null;
            this.btDongBoKhoiLuongGiangDay.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btDongBoKhoiLuongGiangDay_Execute_1);

        }



        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btDongBoKhoiLuongGiangDay;

    }
}
