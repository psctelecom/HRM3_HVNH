using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.XuLyQuyTrinh.HopDong;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module;


namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_GiaHanHopDongController : ViewController
    {
        public HopDong_GiaHanHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_GiaHanHopDongController");
        }

        private void NangLuong_QuyetDinhNangLuongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyGiaHanHopDong>()
                && HamDungChung.IsCreateGranted<ChiTietGiaHanHopDong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinHopDong thongTin = View.CurrentObject as ThongTinHopDong;
            if (thongTin != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                ChiTietGiaHanHopDong obj = obs.CreateObject<ChiTietGiaHanHopDong>();

                obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                obj.HopDongNhanVien = obs.GetObjectByKey<HopDong_NhanVien>(thongTin.HopDong.Oid);
                
                Application.ShowView<ChiTietGiaHanHopDong>(obs, obj);
            }
        }
    }
}
