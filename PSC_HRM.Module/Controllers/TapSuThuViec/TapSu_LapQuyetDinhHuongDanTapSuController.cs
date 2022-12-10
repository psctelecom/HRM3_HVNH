using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.XuLyQuyTrinh.TapSu;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TapSu_LapQuyetDinhHuongDanTapSuController : ViewController
    {
        private IObjectSpace obs;
        private ThongTinHetHanTapSu hetHanTapSu;
        private QuyetDinhBoNhiemNgach quyetDinh;

        public TapSu_LapQuyetDinhHuongDanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TapSu_LapQuyetDinhHuongDanTapSuController");
        }

        private void TapSu_QuyetDinhCongNhanHetHanTapSuController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinHetHanTapSu>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            quyetDinh = obs.CreateObject<QuyetDinhBoNhiemNgach>();
            quyetDinh.QuyetDinhMoi = true;
            hetHanTapSu = View.CurrentObject as ThongTinHetHanTapSu;
            if (hetHanTapSu != null)
            {
                ChiTietQuyetDinhBoNhiemNgach chiTiet = obs.CreateObject<ChiTietQuyetDinhBoNhiemNgach>();
                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(hetHanTapSu.BoPhan.Oid);
                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(hetHanTapSu.ThongTinNhanVien.Oid);
                if (hetHanTapSu.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong != null)
                    chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(hetHanTapSu.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong.Oid);
                if (hetHanTapSu.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong != null)
                    chiTiet.BacLuong = obs.GetObjectByKey<BacLuong>(hetHanTapSu.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong.Oid);
                chiTiet.HeSoLuong = hetHanTapSu.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
                quyetDinh.ListChiTietQuyetDinhBoNhiemNgach.Add(chiTiet);
            }

            e.Context = TemplateContext.View;
            e.View = Application.CreateDetailView(obs, quyetDinh);
            e.DialogController.AcceptAction.Caption = "Lưu";
        }
    }
}
