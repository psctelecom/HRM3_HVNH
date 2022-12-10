namespace PSC_HRM.Module.Win.Controllers.Import.ImportControl.DNU
{
    partial class Import_ChamThi_CoiThi_DeThi_Controller
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem1 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem2 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem3 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // btn_Import_ChamThi_CoiThi_DeThi_Controller
            // 
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.Caption = "Import";
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.ConfirmationMessage = null;
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.Id = "btn_Import_ChamThi_CoiThi_DeThi_Controller";
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.ImageName = "Action_Import";
            choiceActionItem1.Caption = "Import Chấm Thi";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "Import Coi Thi";
            choiceActionItem2.ImageName = null;
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            choiceActionItem3.Caption = "Import Đề Thi";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.Items.Add(choiceActionItem1);
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.Items.Add(choiceActionItem2);
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.Items.Add(choiceActionItem3);
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.ToolTip = null;
            this.btn_Import_ChamThi_CoiThi_DeThi_Controller.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.btn_Import_ChamThi_CoiThi_DeThi_Controller_Execute);
            this.Activated += Import_ChamThi_CoiThi_DeThi_Controller_Activated;
        }


        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction btn_Import_ChamThi_CoiThi_DeThi_Controller;
    }
}
