namespace PSC_HRM.Module.Win.Controller
{
    partial class NhatKyDuLieu
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
            this.actNhatKy = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // actNhatKy
            // 
            this.actNhatKy.AcceptButtonCaption = null;
            this.actNhatKy.CancelButtonCaption = null;
            this.actNhatKy.Caption = "Nhật ký dữ liệu";
            this.actNhatKy.ConfirmationMessage = null;
            this.actNhatKy.Id = "NhatKyDuLieu";
            this.actNhatKy.ImageName = "Action_NhatKyDuLieu";
            this.actNhatKy.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.actNhatKy.Shortcut = null;
            this.actNhatKy.Tag = null;
            this.actNhatKy.TargetObjectsCriteria = null;
            this.actNhatKy.TargetViewId = null;
            this.actNhatKy.ToolTip = "Xem nhật ký thay đổi của dữ liệu đang chọn";
            this.actNhatKy.TypeOfView = null;
            this.actNhatKy.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.actNhatKy_CustomizePopupWindowParams);
            // 
            // NhatKyDuLieu
            // 
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction actNhatKy;
    }
}
