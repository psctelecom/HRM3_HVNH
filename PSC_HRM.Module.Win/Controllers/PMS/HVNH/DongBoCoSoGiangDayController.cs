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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.NghiepVu.HVNH;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS.HVNH
{
    public partial class DongBoCoSoGiangDayController : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        HinhThucThi _KeKhai;
        public DongBoCoSoGiangDayController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "CoSoGiangDay_ListView";
        }

        private void DongBoCoSoGiangDayController_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HVNH")
            {
                btQuyDoi.Active["TruyCap"] = true;
            }
            else
            {
                btQuyDoi.Active["TruyCap"] = false;
            }
        }

        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _KeKhai = View.CurrentObject as HinhThucThi;

            using (DialogUtil.AutoWait("Đang đồng bộ dữ liệu"))
            {
                SqlParameter[] pQuyDoi = new SqlParameter[0];
                DataProvider.GetValueFromDatabase("spd_PMS_DongBoCoSoGiangDay", CommandType.StoredProcedure, pQuyDoi);
                View.ObjectSpace.Refresh();
            }
            XtraMessageBox.Show("Đồng bộ thành công!", "Thông báo!");
        }

    }
}