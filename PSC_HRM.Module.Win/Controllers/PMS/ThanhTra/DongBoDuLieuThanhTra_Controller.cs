using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.ThuNhap.NonPersistentThuNhap;
using PSC_HRM.Module.ReportClass;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class DongBoDuLieuThanhTra_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public DongBoDuLieuThanhTra_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            if(TruongConfig.MaTruong == "UEL")
            {
                TargetViewId = "NULL";
            }
            else
            {
                TargetViewId = "Quanlythanhtra_DetailView";
            }
        }

        void DongBoDuLieuThanhTra_Controller_Activated(object sender, System.EventArgs e)
        {
        }
        private void btDongBoKhoiLuongGiangDay_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            Quanlythanhtra obj = View.CurrentObject as Quanlythanhtra;
            //spd_PMS_DongBo_KhoiLuongGiangDay_ThanhTra
            SqlParameter[] pDongBo = new SqlParameter[1];
            pDongBo[0] = new SqlParameter("@QuanLyThanhTra", obj.Oid);
            DataProvider.ExecuteNonQueryTimeOut("spd_PMS_DongBo_KhoiLuongGiangDay_ThanhTra", CommandType.StoredProcedure, pDongBo);

        }

        private void btDongBoKhoiLuongGiangDay_Execute_1(object sender, SimpleActionExecuteEventArgs e)
        {

        }

    }
}