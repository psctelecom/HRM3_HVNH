namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class Clone_GioChuanPMS_Controller
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
            //this.btClone = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popClone = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // btClone
            // 
            //this.btClone.Caption = "Copy";
            //this.btClone.ConfirmationMessage = null;
            //this.btClone.Id = "Clone_GioChuanPMS_Controller";
            //this.btClone.ToolTip = "Nhân bản dữ liệu";
            //this.btClone.ImageName = "Action_CloneMerge_Clone_Object";
            //this.btClone.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btClone_Execute);
            //// 
            // popClone
            // 
            this.popClone.AcceptButtonCaption = null;
            this.popClone.CancelButtonCaption = null;
            this.popClone.Caption = "Copy";
            this.popClone.ConfirmationMessage = null;
            this.popClone.Id = "popCloneClone_GioChuanPMS_Controller";
            this.popClone.ToolTip = "Nhân bản dữ liệu";
            this.popClone.ImageName = "Action_CloneMerge_Clone_Object";
            this.popClone.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popClone_CustomizePopupWindowParams);
            this.popClone.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popClone_Execute);
            // 
            // Clone_PMS_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Activated += Clone_GioChuanPMS_Controller_Activated;

        }



        #endregion

        //private DevExpress.ExpressApp.Actions.SimpleAction btClone;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popClone;



    }
}
