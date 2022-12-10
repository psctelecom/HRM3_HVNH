namespace PSC_HRM.Module.Win.Controllers.PMS
{
    partial class BaoCaoHopDong_ThanhLy_ThinhGiang_Controller
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
            this.btTinhThuLao = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // btTinhThuLao
            // 
            this.btTinhThuLao.Caption = "Hợp đồng thỉnh giảng";
            this.btTinhThuLao.ConfirmationMessage = null;
            this.btTinhThuLao.Id = "BaoCaoHopDong_ThanhLy_ThinhGiang_Controller";
            this.btTinhThuLao.ImageName = "BO_QuanLyBoiDuong";
            this.btTinhThuLao.ToolTip = "In báo cáo hợp đồng và thanh lý thỉnh giảng";
            this.btTinhThuLao.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.btTinhThuLao_Execute);
            this.Activated+= BaoCaoHopDong_ThanhLy_ThinhGiang_Controller_Activated;
            // 
            // TinhThuLaoGiangDay_Controller
            // 
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction btTinhThuLao;

    }
}
