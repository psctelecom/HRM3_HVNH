namespace PSC_HRM.Module.Win.Controllers.PMS.HVNH
{
    partial class DongBoCoSoGiangDayController
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
            this.btQuyDoi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btQuyDoi
            // 
            this.btQuyDoi.Caption = "Đồng Bộ dữ liệu";
            this.btQuyDoi.ConfirmationMessage = null;
            this.btQuyDoi.Id = "DongBoCoSoGiangDayController";
            this.btQuyDoi.ToolTip = "Đồng bộ";
            this.btQuyDoi.ImageName = "BO_QuyDoi";
            this.btQuyDoi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btQuyDoi_Execute);
            // 
            // QuyDoi_NgoaiGiangDay_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.Activated += DongBoCoSoGiangDayController_Activated;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btQuyDoi;



    }
}
