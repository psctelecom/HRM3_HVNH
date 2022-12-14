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

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class DongBo_BangChotThuLao_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanlyThanhToan _QuanLy;
        public DongBo_BangChotThuLao_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanlyThanhToan_DetailView";
        }

        void DongBo_BangChotThuLao_Contronler_Activated(object sender, System.EventArgs e)
        {
            //_QuanLy = View.CurrentObject as QuanLyHeSo;
            //if (_QuanLy != null)
            //    if (_QuanLy.ThongTinTruong.MaQuanLy != "DNU")
            //        btDongBo_HeSo.Active["TruyCap"] = false;
        }

        private void btDongBo_HeSo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _QuanLy = View.CurrentObject as QuanlyThanhToan;
            if (_QuanLy != null)
            {
                using (DialogUtil.AutoWait("Đang đồng bộ!"))
                {
                    View.ObjectSpace.CommitChanges();
                    SqlParameter[] pDongBo = new SqlParameter[2];
                    pDongBo[0] = new SqlParameter("@QuanlyThanhToan", _QuanLy.Oid);
                    pDongBo[1] = new SqlParameter("@NamHoc", _QuanLy.NamHoc.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_TinhTienPhuTroi_HopDong", CommandType.StoredProcedure, pDongBo);

                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }

        private void btn_MoKhoa_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _QuanLy = View.CurrentObject as QuanlyThanhToan;
            if (_QuanLy != null)
            {
                using (DialogUtil.AutoWait("Đang thực thi!"))
                {
                    SqlParameter[] pDongBo = new SqlParameter[1];
                    pDongBo[0] = new SqlParameter("@NamHoc", _QuanLy.NamHoc.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_TinhTienPhuTroi_HopDong_MoKhoaBangChot", CommandType.StoredProcedure, pDongBo);

                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }
    }
}