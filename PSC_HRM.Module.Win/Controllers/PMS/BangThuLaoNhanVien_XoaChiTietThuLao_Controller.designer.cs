namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class BangThuLaoNhanVien_XoaChiTietThuLao_Controller
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
            this.btXoa = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btXoa
            // 
            this.btXoa.Caption = "Xóa";
            this.btXoa.ConfirmationMessage = null;
            this.btXoa.Id = "BangThuLaoNhanVien_XoaChiTietThuLao_Controller";
            this.btXoa.ImageName = "Action_Delete";
            this.btXoa.TargetObjectType = typeof(PSC_HRM.Module.ThuNhap.ThuLao.ChiTietThuLaoNhanVien);
            this.btXoa.ToolTip = "Xóa chi tiết thù lao nhân viên - thu nhập khác";
            this.btXoa.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btXoa_Execute);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btXoa;



    }
}
