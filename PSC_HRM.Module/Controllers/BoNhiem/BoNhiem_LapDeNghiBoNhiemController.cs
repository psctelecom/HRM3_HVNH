using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoNhiem_LapDeNghiBoNhiemController : ViewController
    {
        public BoNhiem_LapDeNghiBoNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoNhiem_LapDeNghiBoNhiemController");
        }

        private void BoNhiem_LapDeNghiBoNhiemController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyBoNhiem>() &&
                HamDungChung.IsCreateGranted<DeNghiBoNhiem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinBoNhiem thongTin = View.CurrentObject as ThongTinBoNhiem;
            if (thongTin != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                DeNghiBoNhiem obj = obs.CreateObject<DeNghiBoNhiem>();
                obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                obj.ChucVu = obs.GetObjectByKey<ChucVu>(thongTin.ChucVu.Oid);
                obj.KiemNhiem = thongTin.KiemNhiem;

                Application.ShowView<DeNghiBoNhiem>(obs, obj);
            }
        }
    }
}
