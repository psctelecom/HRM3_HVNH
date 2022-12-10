using System;
using System.Collections.Generic;
using PSC_HRM.Module.ThuViec;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThuViec_QuyetDinhXepLuongController : ViewController
    {
        private IObjectSpace obs;
        private ChiTietDeNghiXepLuong chiTiet;
        private QuyetDinhXepLuong quyetDinh;

        public ThuViec_QuyetDinhXepLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThuViec_QuyetDinhXepLuongController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();
            chiTiet = View.CurrentObject as ChiTietDeNghiXepLuong;
            if (chiTiet != null)
            {
                quyetDinh = obs.CreateObject<QuyetDinhXepLuong>();
                quyetDinh.BoPhan = obs.GetObjectByKey<BoPhan>(chiTiet.BoPhan.Oid);
                quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(chiTiet.ThongTinNhanVien.Oid);
                quyetDinh.NgachLuong = obs.GetObjectByKey<NgachLuong>(chiTiet.NgachLuong.Oid);
                quyetDinh.BacLuong = obs.GetObjectByKey<BacLuong>(chiTiet.BacLuong.Oid);
                quyetDinh.HeSoLuong = chiTiet.HeSoLuong;
                quyetDinh.NgayHuongLuong = chiTiet.NgayHuongLuong;
                
                e.Context = TemplateContext.View;
                e.View = Application.CreateDetailView(obs, quyetDinh);
                obs.Committed += obs_Committed;
            }
        }

        void obs_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void ThuViec_QuyetDinhXepLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhXepLuong>();
        }

    }
}
