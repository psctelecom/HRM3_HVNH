using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoiDuong_ChonNhanVienDuyetBoiDuongController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private DuyetDangKyBoiDuong duyet;

        public BoiDuong_ChonNhanVienDuyetBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoiDuong_ChonNhanVienDuyetBoiDuongController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyBoiDuong>()
                && HamDungChung.IsWriteGranted<DuyetDangKyBoiDuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            duyet = View.CurrentObject as DuyetDangKyBoiDuong;
            if (duyet != null && duyet.DangKyBoiDuong != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem nhanVien;
                foreach (var item in duyet.DangKyBoiDuong.ListChiTietDangKyBoiDuong)
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
                HamDungChung.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đăng ký bồi dưỡng");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            foreach (var item in danhSach.ListNhanVien)
            {
                if (item.Chon)
                {
                    if (!BoiDuongHelper.IsExits(duyet, item.ThongTinNhanVien))
                    {
                        duyet.CreateListChiTietDuyetDangKyBoiDuong(item);
                    }
                }
            }
            //View.ObjectSpace.CommitChanges();
            //View.ObjectSpace.Refresh();
            View.Refresh();
        }
    }
}
