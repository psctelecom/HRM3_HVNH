namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class Clone_CauHinhQuyDoiPMS_VHU_Controller
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
            this.popCloneCH = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popCloneCH
            // 
            this.popCloneCH.AcceptButtonCaption = null;
            this.popCloneCH.CancelButtonCaption = null;
            this.popCloneCH.Caption = "Copy";
            this.popCloneCH.ConfirmationMessage = null;
            this.popCloneCH.Id = "Clone_CauHinhQuyDoiPMS_VHU_Controller";
            this.popCloneCH.ImageName = "Action_CloneMerge_Clone_Object";
            this.popCloneCH.ToolTip = "Nhân bản dữ liệu";
            this.popCloneCH.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popClone_CustomizePopupWindowParams);
            this.popCloneCH.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popClone_Execute);
            // 
            // Clone_CauHinhQuyDoiPMS_VHU_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }



        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popCloneCH;



    }
}
