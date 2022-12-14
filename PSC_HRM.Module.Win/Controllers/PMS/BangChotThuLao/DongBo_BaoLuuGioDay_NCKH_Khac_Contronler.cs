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
using PSC_HRM.Module.PMS.GioChuan;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class DongBo_BaoLuuGioDay_NCKH_Khac_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        BangChotThuLao _QuanLy;
        public DongBo_BaoLuuGioDay_NCKH_Khac_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_DetailView";
        }

        void DongBo_BaoLuuGioDay_NCKH_Khac_Contronler_Activated(object sender, System.EventArgs e)
        {
            _QuanLy = View.CurrentObject as BangChotThuLao;
            if (_QuanLy != null)
                if (_QuanLy.ThongTinTruong.MaQuanLy == "HUFLIT")
                    btDongBo_HeSo.Active["TruyCap"] = true;
        }

        private void btDongBo_HeSo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            _QuanLy = View.CurrentObject as BangChotThuLao;
            QuanLyGioChuan qly=null;
            if (_QuanLy != null)
            {
                qly = _QuanLy.Session.FindObject<QuanLyGioChuan>(CriteriaOperator.Parse("Oid =?", _QuanLy.Oid));

                using (DialogUtil.AutoWait("Đang chạy, vui lòng đợi!"))
                {
                    if (qly == null)
                        View.ObjectSpace.CommitChanges();

                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn bảo lưu năm học không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlParameter[] pXoa = new SqlParameter[1];
                        pXoa[0] = new SqlParameter("@BangChotThuLao", _QuanLy.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_BaoLuuNamHoc_GD_NCKH_Khac", CommandType.StoredProcedure, pXoa);
                    }
                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }
    }
}