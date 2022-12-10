using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.XuLyQuyTrinh.NangLuong;
using PSC_HRM.Module;
using PSC_HRM.Module.NangLuong;


namespace PSC_HRM.Module.Controllers
{
    public partial class NangLuong_QuyetDinhNangLuongSomController : ViewController
    {
        private DanhSachNangLuongSom ds;

        public NangLuong_QuyetDinhNangLuongSomController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangLuong_QuyetDinhNangLuongSomController");
        }

        private void NangLuong_QuyetDinhNangLuongSomController_Activated(object sender, System.EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangLuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DanhSachNangLuongSom nangLuong = View.CurrentObject as DanhSachNangLuongSom;
            if (nangLuong != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhNangLuong obj = obs.CreateObject<QuyetDinhNangLuong>();
                obj.NoiDung = String.Format("nâng bậc lương năm {0} cho cán bộ", obj.NgayHieuLuc.Year);
                obj.QuyetDinhMoi = true;
                obj.Imporrt = true;

                foreach(var item in nangLuong.DanhSachNhanVien)
                {
                    if(item.Chon)
                    {
                        ChiTietQuyetDinhNangLuong chiTiet = obs.CreateObject<ChiTietQuyetDinhNangLuong>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        chiTiet.NgachLuong = obs.GetObjectByKey<NgachLuong>(item.NgachLuong.Oid);
                        chiTiet.BacLuongCu = obs.GetObjectByKey<BacLuong>(item.BacLuongCu.Oid);
                        chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                        chiTiet.VuotKhungCu = item.PhanTramVuotKhungCu;
                        chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                        //chiTiet.NgayHuongLuongCu = item.MocNangLuongCu;
                        chiTiet.BacLuongMoi = obs.GetObjectByKey<BacLuong>(item.BacLuongMoi.Oid);
                        chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                        chiTiet.VuotKhungMoi = item.PhanTramVuotKhungMoi;
                        chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                        chiTiet.NangLuongTruocHan = true;
                        //
                        obj.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
                    }

                }

                Application.ShowView<QuyetDinhNangLuong>(obs, obj);
            }
        }
    }
}
