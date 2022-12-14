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
    public partial class DongBo_HeSo_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyHeSo _QuanLy;
        public DongBo_HeSo_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHeSo_DetailView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            DetailView _DetailView = View as DetailView;
            if (_DetailView != null)
            {
                if (_DetailView.ToString().Contains("QuanLyHeSo_DetailView"))
                {
                    _QuanLy = View.CurrentObject as QuanLyHeSo;
                    if (_QuanLy != null)
                    {
                        _QuanLy.updateHocKyList();
                    }
                }
            }
        }

        void DongBo_HeSo_Contronler_Activated(object sender, System.EventArgs e)
        {
            _QuanLy = View.CurrentObject as QuanLyHeSo;
            if (_QuanLy != null)
            {
                if (_QuanLy.ThongTinTruong.MaQuanLy == "HUFLIT")
                    btDongBo_HeSo.Active["TruyCap"] = false;
            }
        }

        private void btDongBo_HeSo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _QuanLy = View.CurrentObject as QuanLyHeSo;
            if (_QuanLy != null)
            {
                using (DialogUtil.AutoWait("Đang đồng bộ hệ số!"))
                {
                    if (TruongConfig.MaTruong == "HUFLIT")
                    {
                        if(_QuanLy.HocKy==null)
                        {
                            XtraMessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo");
                            return;
                        }
                    }
                    View.ObjectSpace.CommitChanges();
                    SqlParameter[] pDongBo = new SqlParameter[1];
                    pDongBo[0] = new SqlParameter("@QuanLyHeSo", _QuanLy.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoHeSoChucDanh", CommandType.StoredProcedure, pDongBo);

                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }
    }
}