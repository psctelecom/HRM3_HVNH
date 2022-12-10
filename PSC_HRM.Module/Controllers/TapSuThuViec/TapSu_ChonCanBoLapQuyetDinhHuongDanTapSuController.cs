using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.TapSu;

namespace PSC_HRM.Module.Controllers
{
    public partial class TapSu_ChonCanBoLapQuyetDinhHuongDanTapSuController : ViewController
    {
        private IObjectSpace obs;
        private QuyetDinhHuongDanTapSu quyetDinh;
        private HoSo_ChonNhanVien list;

        public TapSu_ChonCanBoLapQuyetDinhHuongDanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TapSu_ChonCanBoLapQuyetDinhHuongDanTapSuController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();

            quyetDinh = View.CurrentObject as QuyetDinhHuongDanTapSu;
            if (quyetDinh != null
                && quyetDinh.QuyetDinhTuyenDung != null)
            {
                list = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem chiTiet;
                foreach (ChiTietQuyetDinhTuyenDung item in quyetDinh.QuyetDinhTuyenDung.ListChiTietQuyetDinhTuyenDung)
                {
                    chiTiet = obs.CreateObject<HoSo_NhanVienItem>();
                    chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    list.ListNhanVien.Add(chiTiet);
                }

                e.View = Application.CreateDetailView(obs, list);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa chọn quyết định đào tạo");
        }

        private void DanhGia_CopyBangQuyDoiThangController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhHuongDanTapSu>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = View.ObjectSpace;
            foreach (var item in list.ListNhanVien)
            {
                if (item.Chon)
                {
                    if (!TapSuHelper.IsExits(quyetDinh, item.ThongTinNhanVien))
                    {
                        quyetDinh.CreateListChiTietQuyetDinhHuongDanTapSu(item);
                    }
                }
            }
            View.Refresh();
        }
    }
}
