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
    public partial class TuyenDung_LapHopDongThinhGiangController : ViewController
    {
        public TuyenDung_LapHopDongThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_LapHopDongThinhGiangController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            TrungTuyen trungTuyen = View.CurrentObject as TrungTuyen;
            if (trungTuyen != null
                && trungTuyen.UngVien.NhuCauTuyenDung.ViTriTuyenDung.LoaiTuyenDung.TenLoaiTuyenDung == "Thỉnh giảng")
            {          
                IObjectSpace obs = Application.CreateObjectSpace();
                HopDong_ThinhGiang quyetDinh = obs.FindObject<HopDong_ThinhGiang>(CriteriaOperator.Parse("QuanLyTuyenDung=?", trungTuyen.QuanLyTuyenDung.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<HopDong_ThinhGiang>();
                    quyetDinh.BoPhan = obs.GetObjectByKey<BoPhan>(trungTuyen.UngVien.NhuCauTuyenDung.BoPhan.Oid);
                    quyetDinh.NhanVien = TuyenDungHelper.HoSoGiangVienThinhGiang(obs, trungTuyen);
                }

                Application.ShowView<HopDong_ThinhGiang>(obs, quyetDinh);
            }
            else
                HamDungChung.ShowWarningMessage("Ứng viên này ứng tuyển vị trí giảng viên.");
        }

        private void ChuyenHoSoTuyenDungAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active.SetItemValue("SecurityAllowance", HamDungChung.IsWriteGranted<QuanLyTuyenDung>());
        }
    }
}
