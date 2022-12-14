using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Security;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_DuyetDangKyTuyenDungController : ViewController
    {
        public TuyenDung_DuyetDangKyTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_DuyetDangKyTuyenDungController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();

            QuanLyTuyenDung quanLyTuyenDung = View.CurrentObject as QuanLyTuyenDung;
            if (quanLyTuyenDung != null)
            {
                NhuCauTuyenDung nhuCau;
                foreach (DangKyTuyenDung dangKy in quanLyTuyenDung.ListDangKyTuyenDung)
                {
                    if (dangKy.BoMon != null)
                        nhuCau = View.ObjectSpace.FindObject<NhuCauTuyenDung>(CriteriaOperator.Parse("ViTriTuyenDung=? and BoPhan=? and BoMon=?", dangKy.ViTriTuyenDung, dangKy.BoPhan, dangKy.BoMon));
                    else
                        nhuCau = View.ObjectSpace.FindObject<NhuCauTuyenDung>(CriteriaOperator.Parse("ViTriTuyenDung=? and BoPhan=?", dangKy.ViTriTuyenDung, dangKy.BoPhan));
                    if (nhuCau == null)
                    {
                        nhuCau = View.ObjectSpace.CreateObject<NhuCauTuyenDung>();
                        nhuCau.ViTriTuyenDung = dangKy.ViTriTuyenDung;
                        nhuCau.BoPhan = dangKy.BoPhan;
                        nhuCau.BoMon = dangKy.BoMon;
                        nhuCau.SoLuongTuyen = dangKy.SoLuongTuyen;

                        quanLyTuyenDung.ListNhuCauTuyenDung.Add(nhuCau);
                    }
                }

                HamDungChung.ShowSuccessMessage("Duyệt đăng ký tuyển dụng thành công.");
                View.Refresh();
            }
        }

        private void ChuyenHoSoTuyenDungAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyTuyenDung>();
        }
    }
}
