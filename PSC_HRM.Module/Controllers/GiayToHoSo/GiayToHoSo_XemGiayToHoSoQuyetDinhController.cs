using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Controllers
{
    public partial class GiayToHoSo_XemGiayToHoSoQuyetDinhController : ViewController
    {
        private IObjectSpace obs;
        public GiayToHoSo_XemGiayToHoSoQuyetDinhController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("GiayToHoSo_XemGiayToHoSoQuyetDinhController");
        }
        protected override void OnActivated()
        {
            base.OnActivated();          
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();          
        }
        protected override void OnDeactivated()
        {          
            base.OnDeactivated();
        }

        private void HoSo_ImportQuanHeGiaDinhController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong == "NEU")
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinh.QuyetDinh>();
            else
                simpleAction1.Active["TruyCap"] = false;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuyetDinh.QuyetDinh quyetDinh = View.CurrentObject as QuyetDinh.QuyetDinh;
            if (quyetDinh != null)
            {
                using (DialogUtil.AutoWait())
                {
                    try
                    {
                        byte[] data = FptProvider.DownloadFile(quyetDinh.GiayToHoSo.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                        if (data != null)
                        {
                            string strTenFile = "TempFile.pdf";
                            //Lưu file vào thư mục bin\Debug
                            HamDungChung.SaveFilePDF(data, strTenFile);
                            //Đọc file pdf
                            frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                            viewer.ShowDialog();
                        }
                        else
                            XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }            
        }
    }
}
