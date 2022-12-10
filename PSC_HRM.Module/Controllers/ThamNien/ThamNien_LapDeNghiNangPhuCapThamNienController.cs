using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNien;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNien_LapDeNghiNangPhuCapThamNienController : ViewController
    {
        public ThamNien_LapDeNghiNangPhuCapThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNien_LapDeNghiNangPhuCapThamNienController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyNangPhuCapThamNien>() &&
                HamDungChung.IsCreateGranted<ChiTietDeNghiNangPhuCapThamNien>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNangThamNien thamNien = View.CurrentObject as ThongTinNangThamNien;
            if (thamNien != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                ChiTietDeNghiNangPhuCapThamNien obj = obs.CreateObject<ChiTietDeNghiNangPhuCapThamNien>();
                obj.BoPhan = obs.GetObjectByKey<BoPhan>(thamNien.BoPhan.Oid);
                obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thamNien.ThongTinNhanVien.Oid);
                obj.ThamNienCu = thamNien.ThamNienCu;
                obj.NgayHuongThamNienCu = thamNien.NgayHuongThamNienCu;
                obj.ThamNienMoi = thamNien.ThamNienMoi;

                Application.ShowView<ChiTietDeNghiNangPhuCapThamNien>(obs, obj);                
            }
        }
    }
}
