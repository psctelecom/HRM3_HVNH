using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_LapQuyetDinhTuyenDungController : ViewController
    {
        public TuyenDung_LapQuyetDinhTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_LapQuyetDinhTuyenDungController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            QuanLyTuyenDung qlTuyenDung = View.CurrentObject as QuanLyTuyenDung;
            if (qlTuyenDung != null)
            {          
                IObjectSpace obs = Application.CreateObjectSpace();                
                QuyetDinhTuyenDung quyetDinh = obs.FindObject<QuyetDinhTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=?", qlTuyenDung.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<QuyetDinhTuyenDung>();
                    quyetDinh.QuanLyTuyenDung = obs.GetObjectByKey<QuanLyTuyenDung>(qlTuyenDung.Oid);
                }
                ChiTietQuyetDinhTuyenDung chiTiet;
                foreach (TrungTuyen item in qlTuyenDung.ListTrungTuyen)
                {
                    ThongTinNhanVien nhanVien = TuyenDungHelper.HoSoNhanVien(obs, item);
                    if (nhanVien != null)
                    {
                        chiTiet = obs.FindObject<ChiTietQuyetDinhTuyenDung>(CriteriaOperator.Parse("QuyetDinhTuyenDung=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = obs.CreateObject<ChiTietQuyetDinhTuyenDung>();
                            chiTiet.BoPhan = nhanVien.BoPhan;
                            chiTiet.ThongTinNhanVien = nhanVien;
                            quyetDinh.ListChiTietQuyetDinhTuyenDung.Add(chiTiet);
                        }
                    }
                }

                Application.ShowView<QuyetDinhTuyenDung>(obs, quyetDinh);
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
