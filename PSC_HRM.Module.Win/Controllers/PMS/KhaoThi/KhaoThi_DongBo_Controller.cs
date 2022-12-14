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
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KhaoThi_DongBo_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public KhaoThi_DongBo_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKhaoThi_DetailView";
        }

        void KhaoThi_DongBo_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "DNU")
                btDongBoKhaoThi.Active["TruyCap"] = false;
        }

        private void btDongBoKhaoThi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            QuanLyKhaoThi obj = View.CurrentObject as QuanLyKhaoThi;
            if(obj!=null)
            {
                if (obj.Khoa || obj.BangChotThuLao != null)
                {
                    XtraMessageBox.Show("Đã khóa dữ liệu","Thông báo");
                }
                else
                {
                    View.ObjectSpace.CommitChanges();
                    using (DialogUtil.Wait("Đang dồng bộ dữ liệu", "Vui lòng đợi"))
                    {
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@QuanLyKhaoThi", obj.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieuKhaoThi", CommandType.StoredProcedure, param);
                    }
                    View.ObjectSpace.Refresh();
                }
            }
        }
    }
}