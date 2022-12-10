using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.XuLyQuyTrinh.NghiHuu;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using PSC_HRM.Module.NghiPhep;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Controllers
{
    public partial class NghiPhep_ChotPhepNamController : ViewController
    {
        public NghiPhep_ChotPhepNamController()
        {
            InitializeComponent();
            RegisterActions(components);
            //HamDungChung.DebugTrace("NghiHuu_LapQuyetDinhKeoDaiThoiGianCongTacController");
        }

        private void NghiPhep_ChotPhepNamController_Activated(object sender, EventArgs e)
        {
            //simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNghiHuu>();
            if (TruongConfig.MaTruong.Equals("NEU"))
            {
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyNghiPhep>();
            }
            else
            {
                simpleAction1.Active["TruyCap"] = false;
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            QuanLyNghiPhep quanLyNghiPhep = View.CurrentObject as QuanLyNghiPhep;
            using (DialogUtil.AutoWait())
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Nam", quanLyNghiPhep.Nam);
                DataProvider.ExecuteNonQuery("spd_WebChamCong_QuanLyNghiPhep_TaoMoiChiTietNghiPhep", CommandType.StoredProcedure, param);
                //
                View.ObjectSpace.Refresh();
            }
            DialogUtil.ShowInfo("Chốt phép năm thành công.");
        }
    }
}
