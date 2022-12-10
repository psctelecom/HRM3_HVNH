namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class KhoiLuongGiangDay_QuyDoi_Controller
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
            this.btQuiDoi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btQuiDoi
            // 
            this.btQuiDoi.Caption = "Qui đổi";
            this.btQuiDoi.ConfirmationMessage = null;
            this.btQuiDoi.Id = "KhoiLuongGiangDay_QuyDoi_Controller";
            this.btQuiDoi.ToolTip = null;
            this.btQuiDoi.ImageName = "ModelEditor_ModelMerge";
            this.btQuiDoi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btQuiDoi_Execute);
            // 
            // KhoiLuongGiangDay_QuyDoi_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Activated += KhoiLuongGiangDay_QuyDoi_Controller_Activated;
        }



        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btQuiDoi;



    }
}
