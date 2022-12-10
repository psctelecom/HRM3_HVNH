namespace PSC_HRM.Module.Controllers.Import
{
    partial class PMS_Import_KeKhai_TuXa_Controller
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
            this.btImport = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.btnXuatBaoCao = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.btnXoa = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btImport
            // 
            this.btImport.Caption = "Import kê khai";
            this.btImport.ConfirmationMessage = null;
            this.btImport.Id = "PMS_Import_KeKhai_TuXa_Controller";
            this.btImport.ImageName = "Action_Import";
            this.btImport.ToolTip = null;
            this.btImport.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btImport_Execute);
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.Caption = "Hướng dẫn chuyên đề";
            this.btnXuatBaoCao.Category = "View";
            this.btnXuatBaoCao.ConfirmationMessage = null;
            this.btnXuatBaoCao.Id = "Export_Data_XemThongTin";
            this.btnXuatBaoCao.ImageName = "Action_Export_ToExcel";
            this.btnXuatBaoCao.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.btnXuatBaoCao.ToolTip = null;
            this.btnXuatBaoCao.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.btnXuatBaoCao_Execute);
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xóa kê khai";
            this.btnXoa.ConfirmationMessage = null;
            this.btnXoa.Id = "9be7cb27-d893-4177-bacf-bc2fae36ff45";
            this.btnXoa.ImageName = "Action_Delete";
            this.btnXoa.ToolTip = null;
            this.btnXoa.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnXoa_Execute);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btImport;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction btnXuatBaoCao;
        private DevExpress.ExpressApp.Actions.SimpleAction btnXoa;


    }
}
