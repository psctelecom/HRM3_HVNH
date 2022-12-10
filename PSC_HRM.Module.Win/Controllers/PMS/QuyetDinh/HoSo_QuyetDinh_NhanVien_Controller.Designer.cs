using PSC_HRM.Module.HoSo;
using System;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class HoSo_QuyetDinh_NhanVien_Controller
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
            this.SingleChoiceAction1 = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // SingleChoiceAction
            //
            this.SingleChoiceAction1.Caption = "Quyết định";
            this.SingleChoiceAction1.ConfirmationMessage = null;
            this.SingleChoiceAction1.Id = "HoSo_QuyetDinh_NhanVien_Controller";
            this.SingleChoiceAction1.ImageName = "BO_List";
            this.SingleChoiceAction1.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.SingleChoiceAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.SingleChoiceAction1.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.SingleChoiceAction1.TargetObjectType = typeof(ThongTinNhanVien);
            this.SingleChoiceAction1.ToolTip = null;
            this.SingleChoiceAction1.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.SingleChoiceAction1.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceAction1_Execute);
            this.ViewControlsCreated += new EventHandler(this.HoSo_QuyetDinh_NhanVien_Controller_ViewControlsCreated);
            this.Activated += HoSo_QuyetDinh_NhanVien_Controller_Activated;
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction SingleChoiceAction1;
    }
}
