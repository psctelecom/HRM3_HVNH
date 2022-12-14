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
using PSC_HRM.Module.PMS.ThoiKhoaBieu;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class DongBo_KeKhaiCacHoatDong_TheoThoiKhoaBieu_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        KeKhai_CacHoatDong_ThoiKhoaBieu _QuanLy;
        public DongBo_KeKhaiCacHoatDong_TheoThoiKhoaBieu_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KeKhai_CacHoatDong_ThoiKhoaBieu_DetailView";
        }

        void DongBo_HeSo_Contronler_Activated(object sender, System.EventArgs e)
        {
            //_QuanLy = View.CurrentObject as QuanLyHeSo;
            //if (_QuanLy != null)
            //    if (_QuanLy.ThongTinTruong.MaQuanLy != "DNU")
            //        btDongBo_HeSo.Active["TruyCap"] = false;
        }

        private void btDongBo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _QuanLy = View.CurrentObject as KeKhai_CacHoatDong_ThoiKhoaBieu;
            if (_QuanLy != null)
            {
                using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu!"))
                {
                    View.ObjectSpace.CommitChanges();
                    SqlParameter[] pDongBo = new SqlParameter[1];
                    pDongBo[0] = new SqlParameter("@KeKhai", _QuanLy.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_KeKhai_CacHoatDong_ThoiKhoaBieu", CommandType.StoredProcedure, pDongBo);

                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }
    }
}