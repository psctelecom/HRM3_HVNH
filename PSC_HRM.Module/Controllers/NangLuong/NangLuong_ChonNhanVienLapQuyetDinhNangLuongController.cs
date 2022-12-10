using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_ChonNhanVienLapQuyetDinhNangLuongController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private QuyetDinhNangLuong qd;

        public NangLuong_ChonNhanVienLapQuyetDinhNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangLuong_ChonNhanVienLapQuyetDinhNangLuongController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangLuong>()
                && HamDungChung.IsWriteGranted<ChiTietQuyetDinhNangLuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qd = View.CurrentObject as QuyetDinhNangLuong;
            if (qd != null && qd.DeNghiNangLuong != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem nhanVien;
                foreach (var item in qd.DeNghiNangLuong.ListChiTietDeNghiNangLuong)
                {
                    nhanVien = obs.CreateObject<HoSo_NhanVienItem>();
                    nhanVien.Chon = true;
                    nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    danhSach.ListNhanVien.Add(nhanVien);
                }
                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đề nghị nâng ngạch");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = View.ObjectSpace;
            foreach (var item in danhSach.ListNhanVien)
            {
                if (!NangLuongHelper.IsExits(qd, item.ThongTinNhanVien))
                {
                    qd.CreateListChiTietQuyetDinhNangLuong(item);
                }
            }
            View.Refresh();
        }
    }
}
