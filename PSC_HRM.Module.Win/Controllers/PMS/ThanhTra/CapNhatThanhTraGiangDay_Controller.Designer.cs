namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class CapNhatThanhTraGiangDay_Controller
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
            this.popCapNhatThanhTra = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popCapNhatThanhTra
            // 
            this.popCapNhatThanhTra.AcceptButtonCaption = null;
            this.popCapNhatThanhTra.CancelButtonCaption = null;
            this.popCapNhatThanhTra.Caption = "Cập nhật thanh tra";
            this.popCapNhatThanhTra.ConfirmationMessage = null;
            this.popCapNhatThanhTra.Id = "popCapNhatThanhTra";
            this.popCapNhatThanhTra.ImageName = "Action_Inline_Edit";
            this.popCapNhatThanhTra.ToolTip = null;
            this.popCapNhatThanhTra.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popCapNhatThanhTra_CustomizePopupWindowParams);
            this.popCapNhatThanhTra.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popCapNhatThanhTra_Execute);
            this.popCapNhatThanhTra.ProcessCreatedView += popCapNhatThanhTra_ProcessCreatedView;
        }

        void popCapNhatThanhTra_ProcessCreatedView(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
        {
            
        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popCapNhatThanhTra;
    }
}
