using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNien;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNien_TaoQuyetDinhNangPhuCapThamNienNhaGiaoController : ViewController
    {
        public ThamNien_TaoQuyetDinhNangPhuCapThamNienNhaGiaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNien_TaoQuyetDinhNangPhuCapThamNienNhaGiaoController");
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangPhuCapThamNienNhaGiao>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNangThamNien thamNien = View.CurrentObject as ThongTinNangThamNien;
            if (thamNien != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhNangPhuCapThamNienNhaGiao obj = obs.CreateObject<QuyetDinhNangPhuCapThamNienNhaGiao>();
                obj.QuyetDinhMoi = true;
                obj.Imporrt = true;

                ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet = obs.CreateObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>();
                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(thamNien.BoPhan.Oid);
                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thamNien.ThongTinNhanVien.Oid);
                chiTiet.ThamNienCu = thamNien.ThamNienCu;
                chiTiet.NgayHuongThamNienCu = thamNien.NgayHuongThamNienCu;
                chiTiet.ThamNienMoi = thamNien.ThamNienMoi;
                obj.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);

                Application.ShowView<QuyetDinhNangPhuCapThamNienNhaGiao>(obs, obj);
            }
        }
    }
}
