using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module;
using PSC_HRM.Module.BoiDuong;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoiDuong_ChonNhanVienLapQuyetDinhBoiDuongController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private QuyetDinhBoiDuong qd;

        public BoiDuong_ChonNhanVienLapQuyetDinhBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoiDuong_ChonNhanVienLapQuyetDinhBoiDuongController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhBoiDuong>()
                && HamDungChung.IsWriteGranted<ChiTietBoiDuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qd = View.CurrentObject as QuyetDinhBoiDuong;
            if (qd != null && qd.DuyetDangKyBoiDuong != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem nhanVien;
                foreach (var item in qd.DuyetDangKyBoiDuong.ListChiTietDuyetDangKyBoiDuong)
                {
                    nhanVien = obs.CreateObject<HoSo_NhanVienItem>();
                    nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    danhSach.ListNhanVien.Add(nhanVien);
                }
                e.View = Application.CreateDetailView(obs, danhSach);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đăng ký bồi dưỡng");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            foreach (var item in danhSach.ListNhanVien)
            {
                if (!BoiDuongHelper.IsExits(qd, item.ThongTinNhanVien))
                {
                    qd.CreateListChiTietBoiDuong(item);
                }
            }
           // View.ObjectSpace.CommitChanges();
           // View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
