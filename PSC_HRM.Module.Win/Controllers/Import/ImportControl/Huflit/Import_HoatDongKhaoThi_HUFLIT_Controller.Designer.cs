namespace PSC_HRM.Module.Controllers.Import
{
    partial class Import_HoatDongKhaoThi_HUFLIT_Controller
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
            this.btn_ImportDanhSachKhaoThi_HUFLIT = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btn_ImportDanhSachKhaoThi_HUFLIT
            // 
            this.btn_ImportDanhSachKhaoThi_HUFLIT.Caption = "Import khảo thí";
            this.btn_ImportDanhSachKhaoThi_HUFLIT.ConfirmationMessage = null;
            this.btn_ImportDanhSachKhaoThi_HUFLIT.Id = "btn_ImportDanhSachKhaoThi_HUFLIT";
            this.btn_ImportDanhSachKhaoThi_HUFLIT.ImageName = "Action_Import";
            this.btn_ImportDanhSachKhaoThi_HUFLIT.ToolTip = null;
            this.btn_ImportDanhSachKhaoThi_HUFLIT.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btn_ImportDanhSachKhaoThi_HUFLIT_Execute);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btn_ImportDanhSachKhaoThi_HUFLIT;

    }
}
