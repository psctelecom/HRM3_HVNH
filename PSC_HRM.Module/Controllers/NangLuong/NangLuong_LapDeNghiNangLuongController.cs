using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using PSC_HRM.Module;


namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_LapDeNghiNangLuongController : ViewController
    {
        public NangLuong_LapDeNghiNangLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangLuong_LapDeNghiNangLuongController");
        }

        private void NangLuong_LapDeNghiNangLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyNangLuong>() &&
                HamDungChung.IsCreateGranted<ChiTietDeNghiNangLuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNangLuong nangLuong = View.CurrentObject as ThongTinNangLuong;
            if (nangLuong != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                ChiTietDeNghiNangLuong obj = obs.CreateObject<ChiTietDeNghiNangLuong>();
                
                obj.BoPhan = obs.GetObjectByKey<BoPhan>(nangLuong.BoPhan.Oid);
                obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nangLuong.ThongTinNhanVien.Oid);
                obj.NgachLuong = obs.GetObjectByKey<NgachLuong>(nangLuong.NgachLuong.Oid);
                obj.BacLuongCu = obs.GetObjectByKey<BacLuong>(nangLuong.BacLuongCu.Oid);
                obj.HeSoLuongCu = nangLuong.HeSoLuongCu;
                obj.VuotKhungCu = nangLuong.VuotKhungCu;
                obj.MocNangLuongCu = nangLuong.MocNangLuongCu;
                obj.NgayHuongLuongCu = nangLuong.NgayHuongLuongCu;
                obj.BacLuongMoi = obs.GetObjectByKey<BacLuong>(nangLuong.BacLuongMoi.Oid);
                obj.HeSoLuongMoi = nangLuong.HeSoLuongMoi;
                obj.VuotKhungMoi = nangLuong.VuotKhungMoi;
                obj.MocNangLuongMoi = nangLuong.Ngay;

                Application.ShowView<ChiTietDeNghiNangLuong>(obs, obj);
            }

        }
    }
}
