using System;
using System.ComponentModel;

namespace PSC_HRM.Module.Win.Controllers
{
    partial class FilterNamHocController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.singleChoiceAction1 = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // singleChoiceAction1
            // 
            this.singleChoiceAction1.Caption = " ";
            this.singleChoiceAction1.Category = "Filters";
            this.singleChoiceAction1.ConfirmationMessage = null;
            this.singleChoiceAction1.Id = "HeThong_FilterNamHocContrller";
            this.singleChoiceAction1.ImageName = "Action_Import";
            this.singleChoiceAction1.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Caption;
            this.singleChoiceAction1.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.singleChoiceAction1.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.singleChoiceAction1.ToolTip = null;
            this.singleChoiceAction1.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.singleChoiceAction1.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceAction1_Execute);
            // 
            // HeThong_FilterNamHocContrller
            // 
            this.Actions.Add(this.singleChoiceAction1);
            this.ViewControlsCreated += new System.EventHandler(this.HeThong_FilterNamHocContrller_ViewControlsCreated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceAction1;
    }
}
