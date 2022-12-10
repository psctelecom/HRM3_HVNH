using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_TrinhDoChuyenMonCaoNhatAction : ViewController
    {
        public HoSo_TrinhDoChuyenMonCaoNhatAction()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_TrinhDoChuyenMonCaoNhatAction");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            VanBang trinhDo = View.CurrentObject as VanBang;

            if (trinhDo != null)
            {
                if (trinhDo.HoSo is UngVien)
                {
                    UngVien ungVien = View.ObjectSpace.GetObjectByKey<UngVien>(trinhDo.HoSo.Oid);
                    if (ungVien != null)
                    {
                        ungVien.TrinhDoChuyenMon = trinhDo.TrinhDoChuyenMon;
                        ungVien.HinhThucDaoTao = trinhDo.HinhThucDaoTao;
                        ungVien.TruongDaoTao = trinhDo.TruongDaoTao;
                        ungVien.ChuyenMonDaoTao = trinhDo.ChuyenMonDaoTao;
                        ungVien.NamTotNghiep = trinhDo.NamTotNghiep;
                    }
                }
                else
                {
                    NhanVien nhanVien = View.ObjectSpace.GetObjectByKey<NhanVien>(trinhDo.HoSo.Oid);
                    if (nhanVien != null)
                    {
                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo.TrinhDoChuyenMon;
                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = trinhDo.HinhThucDaoTao;
                        nhanVien.NhanVienTrinhDo.TruongDaoTao = trinhDo.TruongDaoTao;
                        nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = trinhDo.ChuyenMonDaoTao;
                        nhanVien.NhanVienTrinhDo.NamTotNghiep = trinhDo.NamTotNghiep;
                        nhanVien.NhanVienTrinhDo.NgayCapBang = trinhDo.NgayCapBang;
                    }
                }

                View.Refresh();
            }
        }
    }
}
