using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_ChonUngVienLapQuyetDinhTuyenDungController : ViewController
    {
        private IObjectSpace obs;
        private TuyenDung_DanhSachThi danhSach;
        private QuyetDinhTuyenDung quyetDinh;

        public TuyenDung_ChonUngVienLapQuyetDinhTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_ChonUngVienLapQuyetDinhTuyenDungController");
        }

        private void ChuyenHoSoTuyenDungAction_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhTuyenDung>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            quyetDinh = View.CurrentObject as QuyetDinhTuyenDung;
            if (quyetDinh != null
                && quyetDinh.QuanLyTuyenDung != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<TuyenDung_DanhSachThi>();
                TuyenDung_ThiSinh thiSinh;
                foreach (TrungTuyen item in quyetDinh.QuanLyTuyenDung.ListTrungTuyen)
                {
                    if (item.UngVien.NhuCauTuyenDung.ViTriTuyenDung.LoaiTuyenDung.TenLoaiTuyenDung != "Thỉnh giảng")
                    {
                        thiSinh = obs.CreateObject<TuyenDung_ThiSinh>();
                        thiSinh.UngVien = obs.GetObjectByKey<UngVien>(item.UngVien.Oid);
                        danhSach.ListUngVien.Add(thiSinh);
                    }
                }

                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa chọn quản lý tuyển dụng.");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = View.ObjectSpace;
            ChiTietQuyetDinhTuyenDung chiTiet;
            TrungTuyen trungTuyen;
            ThongTinNhanVien nhanVien;
            foreach (TuyenDung_ThiSinh item in danhSach.ListUngVien)
            {
                if (item.Chon)
                {
                    trungTuyen = obs.FindObject<TrungTuyen>(CriteriaOperator.Parse("UngVien=?", item.UngVien.Oid));
                    if (trungTuyen != null)
                    {
                        nhanVien = TuyenDungHelper.HoSoNhanVien(obs, trungTuyen);
                        chiTiet = obs.FindObject<ChiTietQuyetDinhTuyenDung>(CriteriaOperator.Parse("QuyetDinhTuyenDung=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid));
                        if (chiTiet == null)
                        {
                            quyetDinh.CreateListChiTietQuyetDinhTuyenDung(nhanVien);
                        }
                    }
                }
            }
            View.Refresh();
        }
    }
}
