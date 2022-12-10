using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.XuLyQuyTrinh.BoNhiem;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoNhiem_TaoQuyetDinhBoNhiemController : ViewController
    {
        public BoNhiem_TaoQuyetDinhBoNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoNhiem_TaoQuyetDinhBoNhiemController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<QuyetDinhBoNhiem>() &&
                HamDungChung.IsCreateGranted<QuyetDinhBoNhiemKiemNhiem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinBoNhiem thongTin = View.CurrentObject as ThongTinBoNhiem;
            if (thongTin != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                if (thongTin.KiemNhiem)
                {
                    QuyetDinhBoNhiemKiemNhiem obj = obs.CreateObject<QuyetDinhBoNhiemKiemNhiem>();
                    obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                    obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                    obj.ChucVuKiemNhiemMoi = obs.GetObjectByKey<ChucVu>(thongTin.ChucVu.Oid);
                    obj.BoPhanMoi = obs.GetObjectByKey<BoPhan>(thongTin.TaiBoPhan.Oid);

                    Application.ShowView<QuyetDinhBoNhiemKiemNhiem>(obs, obj);
                }
                else
                {
                    QuyetDinhBoNhiem obj = obs.CreateObject<QuyetDinhBoNhiem>();
                    obj = obs.CreateObject<QuyetDinhBoNhiem>();
                    obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                    obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                    obj.ChucVuCu = obs.GetObjectByKey<ChucVu>(thongTin.ChucVu.Oid);
                    obj.ChucVuMoi = obs.GetObjectByKey<ChucVu>(thongTin.ChucVu.Oid);

                    Application.ShowView<QuyetDinhBoNhiem>(obs, obj);
                }
            }
        }
    }
}
