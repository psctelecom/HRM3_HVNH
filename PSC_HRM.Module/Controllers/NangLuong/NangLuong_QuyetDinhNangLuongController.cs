using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using PSC_HRM.Module;


namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_QuyetDinhNangLuongController : ViewController
    {
        public NangLuong_QuyetDinhNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangLuong_QuyetDinhNangLuongController");
        }

        private void NangLuong_QuyetDinhNangLuongController_Activated(object sender, System.EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangLuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNangLuong nangLuong = View.CurrentObject as ThongTinNangLuong;
            if (nangLuong != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhNangLuong obj = obs.CreateObject<QuyetDinhNangLuong>();

                obj.QuyetDinhMoi = true;

                ChiTietQuyetDinhNangLuong chiTiet = obs.CreateObject<ChiTietQuyetDinhNangLuong>();
                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(nangLuong.BoPhan.Oid);
                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nangLuong.ThongTinNhanVien.Oid);
                chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(nangLuong.NgachLuong.Oid);
                chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(nangLuong.BacLuongCu.Oid);
                chiTiet.HeSoLuongCu = nangLuong.HeSoLuongCu;
                chiTiet.VuotKhungCu = nangLuong.VuotKhungCu;
                chiTiet.MocNangLuongCu = nangLuong.MocNangLuongCu;
                chiTiet.NgayHuongLuongCu = nangLuong.NgayHuongLuongCu;
                chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(nangLuong.BacLuongMoi.Oid);
                chiTiet.HeSoLuongMoi = nangLuong.HeSoLuongMoi;
                chiTiet.VuotKhungMoi = nangLuong.VuotKhungMoi;
                chiTiet.MocNangLuongMoi = nangLuong.Ngay;
                obj.ListChiTietQuyetDinhNangLuong.Add(chiTiet);

                Application.ShowView<QuyetDinhNangLuong>(obs, obj);
            }
        }
    }
}
