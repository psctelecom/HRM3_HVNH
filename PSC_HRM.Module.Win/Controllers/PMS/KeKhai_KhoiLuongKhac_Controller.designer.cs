namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class KeKhai_KhoiLuongKhac_Controller
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
            this.popKeKhaiSauGiang = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popKeKhaiSauGiang
            // 
            this.popKeKhaiSauGiang.AcceptButtonCaption = null;
            this.popKeKhaiSauGiang.CancelButtonCaption = null;
            this.popKeKhaiSauGiang.Caption = "Kê khai";
            this.popKeKhaiSauGiang.ConfirmationMessage = null;
            this.popKeKhaiSauGiang.Id = "KeKhai_KhoiLuongKhac_Controller";
            this.popKeKhaiSauGiang.ImageName = "Action_Inline_Edit";
            this.popKeKhaiSauGiang.ToolTip = "Kê khai các hoạt động đi học, tham quan";
            this.popKeKhaiSauGiang.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popKeKhaiSauGiang_CustomizePopupWindowParams);
            this.popKeKhaiSauGiang.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popKeKhaiSauGiang_Execute);
            // 
            // KeKhai_KhoiLuongSauGiang
            // 
            this.Activated += KeKhai_KhoiLuongKhac_Controller_Activated;
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popKeKhaiSauGiang;


    }
}
