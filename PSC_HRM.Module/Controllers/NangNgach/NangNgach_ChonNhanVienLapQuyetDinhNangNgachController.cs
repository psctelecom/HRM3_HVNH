using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangNgach;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class NangNgach_ChonNhanVienLapQuyetDinhNangNgachController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private QuyetDinhNangNgach qd;

        public NangNgach_ChonNhanVienLapQuyetDinhNangNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangNgach_ChonNhanVienLapQuyetDinhNangNgachController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangNgach>()
                && HamDungChung.IsWriteGranted<ChiTietQuyetDinhNangNgach>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qd = View.CurrentObject as QuyetDinhNangNgach;
            if (qd != null && qd.DeNghiNangNgach != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem nhanVien;
                foreach (var item in qd.DeNghiNangNgach.ListChiTietDeNghiNangNgach)
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
                if (!NangNgachHelper.IsExits(qd, item.ThongTinNhanVien))
                {
                    qd.CreateListChiTietQuyetDinhNangNgach(item);
                }
            }
            View.Refresh();
        }
    }
}
