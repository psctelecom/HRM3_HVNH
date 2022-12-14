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
    public partial class DongBoNV_DinhMucGioChuan_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyGioChuan _QuanLy;
        public DongBoNV_DinhMucGioChuan_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioChuan_DetailView";
        }

        void DongBoNV_DinhMucGioChuan_Contronler_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HVNH" || TruongConfig.MaTruong == "UEL")
                btDongBo_HeSo.Active["TruyCap"] = false;
            else
                btDongBo_HeSo.Active["TruyCap"] = true;
        }

        private void btDongBo_HeSo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            _QuanLy = View.CurrentObject as QuanLyGioChuan;
            QuanLyGioChuan qly=null;
            if (_QuanLy != null)
            {
                qly = _QuanLy.Session.FindObject<QuanLyGioChuan>(CriteriaOperator.Parse("Oid =?", _QuanLy.Oid));

                using (DialogUtil.AutoWait("Đang đồng bộ danh sách nhân viên!"))
                {
                    if (qly == null)
                        View.ObjectSpace.CommitChanges();

                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlParameter[] pXoa = new SqlParameter[2];
                        pXoa[0] = new SqlParameter("@QuanLyGioChuan", _QuanLy.Oid);
                        pXoa[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                        DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieu_QuanLyGioChuan", CommandType.StoredProcedure, pXoa);
                    }
                    SqlParameter[] pDongBo = new SqlParameter[2];
                    pDongBo[0] = new SqlParameter("@QuanLyGioChuan", _QuanLy.Oid);
                    pDongBo[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                    DataProvider.ExecuteNonQueryTimeOut("spd_PMS_DongBoNhanVien_DinhMucChucVu", CommandType.StoredProcedure, pDongBo);

                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }
    }
}