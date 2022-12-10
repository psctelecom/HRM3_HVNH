using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.XuLyQuyTrinh.NangThamNien;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNien_LapQuyetDinhNangPhuCapThamNienController : ViewController
    {
        public ThamNien_LapQuyetDinhNangPhuCapThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DanhSachDenHanNangPhuCapThamNien thamNien = View.CurrentObject as DanhSachDenHanNangPhuCapThamNien;
            if (thamNien != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh = obs.CreateObject<QuyetDinhNangPhuCapThamNienNhaGiao>();
                quyetDinh.QuyetDinhMoi = true;
                quyetDinh.Imporrt = true;

                ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet;
                foreach (DenHanNangPhuCapThamNien item in thamNien.ThamNienGiangVienList)
                {
                    if (item.Chon)
                    {
                        chiTiet = obs.CreateObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>();
                        chiTiet.QuyetDinhNangPhuCapThamNienNhaGiao = quyetDinh;
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        chiTiet.ThamNienCu = item.ThamNienCu;
                        chiTiet.NgayHuongThamNienCu = item.NgayHuongThamNienCu;
                        chiTiet.ThamNienMoi = item.ThamNienMoi;
                        chiTiet.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi;
                    }
                }

                Application.ShowView<QuyetDinhNangPhuCapThamNienNhaGiao>(obs, quyetDinh);                
            }
        }
    }
}
