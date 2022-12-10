using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.NghiKhongHuongLuong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TiepNhan_LapQuyetDinhTiepNhanNghiKhongHuongLuongController : ViewController
    {
        public TiepNhan_LapQuyetDinhTiepNhanNghiKhongHuongLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TiepNhan_LapQuyetDinhTiepNhanNghiKhongHuongLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhTiepNhan>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNghiKhongHuongLuong thongTinNghiKhongHuongLuong = View.CurrentObject as ThongTinNghiKhongHuongLuong;
            if (thongTinNghiKhongHuongLuong != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhTiepNhan obj = obs.CreateObject<QuyetDinhTiepNhan>();
                obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTinNghiKhongHuongLuong.ThongTinNhanVien.Oid);
                obj.QuyetDinhNghiKhongHuongLuong = obs.GetObjectByKey<QuyetDinhNghiKhongHuongLuong>(thongTinNghiKhongHuongLuong.QuyetDinhNghiKhongHuongLuong.Oid);
                obj.TuNgay = thongTinNghiKhongHuongLuong.QuyetDinhNghiKhongHuongLuong.DenNgay.AddDays(1);

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, obj);
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                e.ShowViewParameters.CreateAllControllers = true;
            }
        }
    }
}
