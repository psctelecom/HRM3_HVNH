using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DiNuocNgoai;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DiNuocNgoai_ChonNhanVienLapQuyetDinhDiNuocNgoaiController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private QuyetDinhDiNuocNgoai qd;

        public DiNuocNgoai_ChonNhanVienLapQuyetDinhDiNuocNgoaiController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DiNuocNgoai_ChonNhanVienLapQuyetDinhDiNuocNgoaiController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhDiNuocNgoai>()
                && HamDungChung.IsWriteGranted<ChiTietQuyetDinhDiNuocNgoai>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qd = View.CurrentObject as QuyetDinhDiNuocNgoai;
            if (qd != null && qd.DangKyDiNuocNgoai != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem nhanVien;
                foreach (var item in qd.DangKyDiNuocNgoai.ListChiTietDangKyDiNuocNgoai)
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
                HamDungChung.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đăng ký đào tạo");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            foreach (var item in danhSach.ListNhanVien)
            {
                if (!DiNuocNgoaiHelper.IsExits(qd, item.ThongTinNhanVien))
                {
                    qd.CreateListChiTietQuyetDinhDiNuocNgoai(item);
                }
            }
            View.Refresh();
        }
    }
}
