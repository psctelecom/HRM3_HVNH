using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNien_ChonNhanVienLapQuyetDinhNangThamNienController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private QuyetDinhNangPhuCapThamNienNhaGiao qd;

        public ThamNien_ChonNhanVienLapQuyetDinhNangThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNien_ChonNhanVienLapQuyetDinhNangThamNienController");
        }

        private void ThamNien_ChonNhanVienLapQuyetDinhNangThamNienController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangPhuCapThamNienNhaGiao>()
                && HamDungChung.IsWriteGranted<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qd = View.CurrentObject as QuyetDinhNangPhuCapThamNienNhaGiao;
            if (qd != null && qd.DeNghiNangPhuCapThamNien != null)
            {
                obs = Application.CreateObjectSpace();
                danhSach = obs.CreateObject<HoSo_ChonNhanVien>();
                HoSo_NhanVienItem nhanVien;
                foreach (var item in qd.DeNghiNangPhuCapThamNien.ListChiTietDeNghiNangPhuCapThamNien)
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
                HamDungChung.ShowWarningMessage("Chưa nhập dữ liệu cho mục Đề nghị nâng PC thâm niên");
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet;
            obs = View.ObjectSpace;
            foreach (var item in danhSach.ListNhanVien)
            {
                if (!NangThamNienHelper.IsExits(qd, item.ThongTinNhanVien))
                {
                    qd.CreateListChiTietQuyetDinhNangPhuCapThamNienNhaGiao(item);
                }
            }
            View.Refresh();
        }
    }
}
