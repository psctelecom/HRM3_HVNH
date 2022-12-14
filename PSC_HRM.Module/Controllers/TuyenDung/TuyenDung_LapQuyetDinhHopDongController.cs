using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_LapQuyetDinhHopDongController : ViewController
    {
        public TuyenDung_LapQuyetDinhHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_LapQuyetDinhHopDongController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            TrungTuyen trungTuyen = View.CurrentObject as TrungTuyen;
            if (trungTuyen != null
                && trungTuyen.UngVien.NhuCauTuyenDung.ViTriTuyenDung.LoaiTuyenDung.TenLoaiTuyenDung != "Thỉnh giảng")
            {          
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhHopDong quyetDinh = obs.FindObject<QuyetDinhHopDong>(CriteriaOperator.Parse("QuanLyTuyenDung=?", trungTuyen.QuanLyTuyenDung.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<QuyetDinhHopDong>();
                    quyetDinh.QuanLyTuyenDung = obs.GetObjectByKey<QuanLyTuyenDung>(trungTuyen.QuanLyTuyenDung.Oid);
                    quyetDinh.BoPhan = obs.GetObjectByKey<BoPhan>(trungTuyen.UngVien.NhuCauTuyenDung.BoPhan.Oid);
                    quyetDinh.ThongTinNhanVien = TuyenDungHelper.HoSoNhanVien(obs, trungTuyen);
                }

                Application.ShowView<QuyetDinhHopDong>(obs, quyetDinh);
            }
            else
                HamDungChung.ShowWarningMessage("Ứng viên này ứng tuyển vị trí giảng viên thỉnh giảng.");
        }

        private void ChuyenHoSoTuyenDungAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active.SetItemValue("SecurityAllowance", HamDungChung.IsWriteGranted<QuanLyTuyenDung>());
        }
    }
}
