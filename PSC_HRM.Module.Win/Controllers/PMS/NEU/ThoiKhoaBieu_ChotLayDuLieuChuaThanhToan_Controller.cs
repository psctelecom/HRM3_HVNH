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
using PSC_HRM.Module.PMS.ThoiKhoaBieu;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ThoiKhoaBieu_ChotLayDuLieuChuaThanhToan_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public ThoiKhoaBieu_ChotLayDuLieuChuaThanhToan_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }

        void KhaoThi_DongBo_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "DNU")
                btDongBoKhaoThi.Active["TruyCap"] = false;
            else
                btDongBoKhaoThi.Active["TruyCap"] = true;
        }

        private void btDongBoKhaoThi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            session = ((XPObjectSpace)_obs).Session;
            ThoiKhoaBieu_KhoiLuongGiangDay obj = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (obj != null)
            {
                using (DialogUtil.Wait("Đang lưu dữ liệu", "Vui lòng đợi"))
                {
                    int kq;
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@ThoiKhoaBieu_KhoiLuongGiangDay", obj.Oid);
                    kq = DataProvider.ExecuteNonQuery("spd_PMS_LuuDuLieuChuaThanhToan", CommandType.StoredProcedure, param);
                    if (kq ==  1)
                    {
                        XtraMessageBox.Show("Đã lưu dữ liệu chưa thanh toán thành công", "Thông báo");
                    }
                }
            }
        }
    }
}