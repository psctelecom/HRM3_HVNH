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
    public partial class DongBo_HeSoTNTH_Contronler : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        QuanLyHeSo _QuanLy;
        public DongBo_HeSoTNTH_Contronler()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHeSo_DetailView";
        }

        void DongBo_HeSoTNTH_Contronler_Activated(object sender, System.EventArgs e)
        {
            _QuanLy = View.CurrentObject as QuanLyHeSo;
            if (_QuanLy != null)
                if (_QuanLy.ThongTinTruong.MaQuanLy != "QNU")
                    btDongBo_HeSoTNTH.Active["TruyCap"] = false;
        }

        private void btDongBo_HeSoTNTH_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _QuanLy = View.CurrentObject as QuanLyHeSo;
            if(_QuanLy!=null)
            {
                object kq = 0;
                using(DialogUtil.AutoWait("Đang đồng bộ hệ số TNTH"))
                {
                    SqlParameter[] pDongBo = new SqlParameter[1];
                    pDongBo[0] = new SqlParameter("@QuanLyHeSo", _QuanLy.Oid);
                    kq = DataProvider.GetValueFromDatabase("spd_PMS_DongBoDuLieu_HeSoTNTH", CommandType.StoredProcedure, pDongBo);
                    if (Convert.ToInt32(kq) > 0)
                        XtraMessageBox.Show("Đồng bộ dữ liệu thành công!", "Thông báo");
                    else
                        XtraMessageBox.Show("Đồng bộ dữ liệu thất bại!", "Thông báo");
                    View.Refresh();
                }
            }
        }
    }
}