using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_KhoaHoSoController : ViewController
    {
        public HoSo_KhoaHoSoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_KhoaHoSoController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinNhanVien nhanVien = View.CurrentObject as ThongTinNhanVien;
            if (TruongConfig.MaTruong == "HUFLIT")
            {
                SqlParameter[] pDongBo = new SqlParameter[1];
                pDongBo[0] = new SqlParameter("@Oid", nhanVien.Oid);
                DataProvider.ExecuteNonQuery("spd_NhanSu_HRM_MoKhoaHoSo", CommandType.StoredProcedure, pDongBo);
            }
            else
            {
                nhanVien.KhoaHoSo = !nhanVien.KhoaHoSo;
                View.ObjectSpace.CommitChanges();
            }
            View.ObjectSpace.Refresh();
        }

        private void KhoaHoSoAction_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinNhanVien>();
        }
    }
}
