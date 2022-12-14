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
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class DongBo_QuanLyDoHoanThanhGiangDay_Controller : ViewController
    {
        IObjectSpace _obs = null;
        QuanLyDoHoanThanhGiangDay giogiang;
        Session ses = null;
        public DongBo_QuanLyDoHoanThanhGiangDay_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyDoHoanThanhGiangDay_DetailView";
        }

        private void btQuiDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            giogiang = View.CurrentObject as QuanLyDoHoanThanhGiangDay;
            if (giogiang != null && !giogiang.Khoa)
            {
                using (DialogUtil.AutoWait("Hệ thống đang đồng bộ dữ liệu"))
                {
                    SqlParameter[] pQuyDoi = new SqlParameter[1];
                    pQuyDoi[0] = new SqlParameter("@QuanLyDoHoanThanhGiangDay", giogiang.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBo_QuanLyDoHoanThanhGiangDay", CommandType.StoredProcedure, pQuyDoi);
                    View.ObjectSpace.Refresh();
                    XtraMessageBox.Show("Đồng bộ dữ liệu thành công!", "Thông báo");
                }

            }
            else
            {
                XtraMessageBox.Show("Dữ liệu đã khóa không thể đồng bộ!", "Thông báo");
            }
        }

    }
}