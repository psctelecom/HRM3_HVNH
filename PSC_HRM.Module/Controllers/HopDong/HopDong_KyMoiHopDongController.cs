using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.XuLyQuyTrinh.HopDong;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_KyMoiHopDongController : ViewController
    {
        public HopDong_KyMoiHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_KyMoiHopDongController");
        }

        private void NangLuong_QuyetDinhNangLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyHopDong>()
                && HamDungChung.IsCreateGranted<HopDong_NhanVien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinHopDong thongTin = View.CurrentObject as ThongTinHopDong;
            if (thongTin != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                if (thongTin.HopDong is HopDong_LamViec)
                {
                    HopDong_LamViec hopDong = thongTin.HopDong as HopDong_LamViec;
                    if (hopDong != null)
                    {
                        HopDong_LamViec obj = obs.CreateObject<HopDong_LamViec>();
                        obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                        obj.NhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                        obj.HinhThucHopDong = obs.GetObjectByKey<HinhThucHopDong>(hopDong.HinhThucHopDong.Oid);
                        obj.TuNgay = hopDong.DenNgay.AddDays(1);
                        obj.PhanLoai = (hopDong.PhanLoai == HopDongLamViecEnum.HopDongLanDau) ? HopDongLamViecEnum.CoThoiHan : HopDongLamViecEnum.CoThoiHan;
                        
                        Application.ShowView<HopDong_LamViec>(obs, obj);
                    }
                }
                else if (thongTin.HopDong is HopDong_LaoDong)
                {
                    HopDong_LaoDong hopDong = thongTin.HopDong as HopDong_LaoDong;
                    if (hopDong != null)
                    {
                        HopDong_LaoDong obj = obs.CreateObject<HopDong_LaoDong>();
                        obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                        obj.NhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                        obj.HinhThucHopDong = obs.GetObjectByKey<HinhThucHopDong>(hopDong.HinhThucHopDong.Oid);
                        obj.TuNgay = hopDong.DenNgay.AddDays(1);
                        obj.HinhThucThanhToan = hopDong.HinhThucThanhToan;
                        obj.PhanLoai = (hopDong.PhanLoai == HopDongLaoDongEnum.TapSuThuViec) ? HopDongLaoDongEnum.CoThoiHan : HopDongLaoDongEnum.CoThoiHan;
                        
                        Application.ShowView<HopDong_LaoDong>(obs, obj);
                    }
                }
                else if (thongTin.HopDong is HopDong_Khoan)
                {
                    HopDong_Khoan hopDong = thongTin.HopDong as HopDong_Khoan;
                    if (hopDong != null)
                    {
                        HopDong_Khoan obj = obs.CreateObject<HopDong_Khoan>();
                        obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                        obj.NhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                        obj.HinhThucHopDong = obs.GetObjectByKey<HinhThucHopDong>(hopDong.HinhThucHopDong.Oid);
                        obj.TuNgay = hopDong.DenNgay.AddDays(1);
                        obj.HinhThucThanhToan = hopDong.HinhThucThanhToan;
                        obj.TienLuong = hopDong.TienLuong;

                        Application.ShowView<HopDong_Khoan>(obs, obj);
                    }
                }
            }
        }
    }
}
