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
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class DongBo_CongTacPhi_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyCongTacPhi _QuanLy;
        public DongBo_CongTacPhi_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            if (TruongConfig.MaTruong =="NEU")
            {
                TargetViewId = "NULL"; // Hiện tại đang ẩn
            }
            else
            {
                TargetViewId = "QuanLyCongTacPhi_DetailView";
            }
        }

        void DongBo_CongTacPhi_Contronler_Activated(object sender, System.EventArgs e)
        {
            //_QuanLy = View.CurrentObject as QuanLyHeSo;
            //if (_QuanLy != null)
            //    if (_QuanLy.ThongTinTruong.MaQuanLy != "DNU")
            //        btDongBo_HeSo.Active["TruyCap"] = false;
        }

        private void btDongBo_HeSo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _QuanLy = View.CurrentObject as QuanLyCongTacPhi;
            if (_QuanLy != null)
            {
                using (DialogUtil.AutoWait("Đang đồng bộ!"))
                {
                    SqlParameter[] pDongBo = new SqlParameter[1];
                    pDongBo[0] = new SqlParameter("@QuanLyCongTacPhi", _QuanLy.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_CongTacPhi", CommandType.StoredProcedure, pDongBo);

                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }
    }
}