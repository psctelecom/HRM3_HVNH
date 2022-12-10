namespace PSC_HRM.Module.Controllers.Import
{
    partial class Import_HoatDongKhac_HUFLIT_Controller
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
            this.btImport_UpdateVanBang = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImport_UpdateVanBang
            // 
            this.btImport_UpdateVanBang.Caption = "Import";
            this.btImport_UpdateVanBang.ConfirmationMessage = null;
            this.btImport_UpdateVanBang.Id = "Import_HoatDongKhac_HUFLIT_Controller";
            this.btImport_UpdateVanBang.ImageName = "Action_Import";
            this.btImport_UpdateVanBang.ToolTip = "Import - Hệ số chức danh";
            this.btImport_UpdateVanBang.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.btImport_UpdateVanBang.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImport_UpdateVanBang_Execute);
            this.Activated += Import_HoatDongKhac_HUFLIT_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
        private DevExpress.ExpressApp.Actions.SimpleAction btImport_UpdateVanBang;
    }
}
