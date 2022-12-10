namespace PSC_HRM.Module.ThuNhap.Controllers
{
    partial class ThueTNCN_MienGiamThueTNCNController
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
            this.actCopy = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // actCopy
            // 
            this.actCopy.AcceptButtonCaption = null;
            this.actCopy.CancelButtonCaption = null;
            this.actCopy.Caption = "Miễn giảm thuế TNCN";
            this.actCopy.ConfirmationMessage = null;
            this.actCopy.Id = "ThueTNCN_MienGiamThueTNCNController";
            this.actCopy.ImageName = "BO_NangLuong";
            this.actCopy.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.actCopy.Shortcut = null;
            this.actCopy.Tag = null;
            this.actCopy.TargetObjectsCriteria = null;
            this.actCopy.TargetObjectType = typeof(PSC_HRM.Module.ThuNhap.Thue.ToKhaiQuyetToanThueTNCN);
            this.actCopy.TargetViewId = null;
            this.actCopy.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.actCopy.ToolTip = "Tính miễn giảm thuế TNCN";
            this.actCopy.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.actCopy.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.actCopy_CustomizePopupWindowParams);
            this.actCopy.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.actCopy_Execute);
            // 
            // ThueTNCN_MienGiamThueTNCNController
            // 
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.Activated += new System.EventHandler(this.KhauTruLuongCopyController_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction actCopy;
    }
}
