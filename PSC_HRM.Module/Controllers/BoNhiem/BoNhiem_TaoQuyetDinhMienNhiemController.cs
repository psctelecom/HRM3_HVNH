using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoNhiem_TaoQuyetDinhMienNhiemController : ViewController
    {
        public BoNhiem_TaoQuyetDinhMienNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoNhiem_TaoQuyetDinhMienNhiemController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<QuyetDinhMienNhiem>() &&
                HamDungChung.IsCreateGranted<QuyetDinhMienNhiemKiemNhiem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinBoNhiem thongTin = View.CurrentObject as ThongTinBoNhiem;
            if (thongTin != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                if (thongTin.KiemNhiem)
                {
                    QuyetDinhMienNhiemKiemNhiem obj = obs.CreateObject<QuyetDinhMienNhiemKiemNhiem>();
                    obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                    obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                    obj.QuyetDinhBoNhiemKiemNhiem = obs.GetObjectByKey<QuyetDinhBoNhiemKiemNhiem>(thongTin.QuyetDinh.Oid);
                    obj.LyDo = "hết nhiệm kỳ chức vụ";

                    Application.ShowView<QuyetDinhMienNhiemKiemNhiem>(obs, obj);
                }
                else
                {
                    QuyetDinhMienNhiem obj = obs.CreateObject<QuyetDinhMienNhiem>();
                    obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                    obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                    obj.QuyetDinhBoNhiem = obs.GetObjectByKey<QuyetDinhBoNhiem>(thongTin.QuyetDinh.Oid);
                    obj.LyDo = "hết nhiệm kỳ chức vụ";

                    Application.ShowView<QuyetDinhMienNhiem>(obs, obj);
                }
            }
        }
    }
}
