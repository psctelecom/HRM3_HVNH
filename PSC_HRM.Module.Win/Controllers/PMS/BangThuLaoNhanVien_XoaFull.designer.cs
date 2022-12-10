namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class BangThuLaoNhanVien_XoaFull
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
            this.btXoaDuLieu = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btXoaDuLieu
            // 
            this.btXoaDuLieu.Caption = "Xóa chi tiết thù lao";
            this.btXoaDuLieu.ConfirmationMessage = null;
            this.btXoaDuLieu.Id = "BangThuLaoNhanVien_XoaFull";
            this.btXoaDuLieu.ImageName = "Action_Delete";
            this.btXoaDuLieu.ToolTip = null;
            this.btXoaDuLieu.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btXoaDuLieu_Execute);
            // 
            // BangThuLaoNhanVien_XoaFull
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btXoaDuLieu;

    }
}
