using PSC_HRM.Module.BusinessObjects.MayChamCong;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Win.Controllers
{
    partial class KetNoiMayChamCongController
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
            this.btnKetNoiMCC = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btnKetNoiMCC
            // 
            this.btnKetNoiMCC.Caption = "Kết nối máy chấm công";
            this.btnKetNoiMCC.ConfirmationMessage = null;
            this.btnKetNoiMCC.Id = "KetNoiMayChamCongController";
            this.btnKetNoiMCC.ImageName = "BO_Group3";
            this.btnKetNoiMCC.TargetObjectType = typeof(PSC_HRM.Module.BusinessObjects.MayChamCong.MayChamCong);
            this.btnKetNoiMCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.btnKetNoiMCC.ToolTip = "Kết nối máy chấm công";
            this.btnKetNoiMCC.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btnKetNoiMCC_Execute);
            // 
            // KetNoiMayChamCongController
            // 
            this.Activated += new System.EventHandler(this.KetNoiMayChamCongController_Activated);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction btnKetNoiMCC;
    }
}
