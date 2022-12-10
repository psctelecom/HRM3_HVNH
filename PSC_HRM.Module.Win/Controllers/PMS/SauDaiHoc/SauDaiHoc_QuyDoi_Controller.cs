using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.ChamCong;
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using ERP.Module.Win.Controllers.Import.ImportClass;
using PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class SauDaiHoc_QuyDoi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public SauDaiHoc_QuyDoi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLySauDaiHoc_DetailView";
        }
        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuanLySauDaiHoc OidQuanLy = View.CurrentObject as QuanLySauDaiHoc;
            if (OidQuanLy != null)
            {
                try
                {
                    using (DialogUtil.Wait("Đang quy đổi giờ giảng dạy!", "Vui lòng đợi"))
                    {
                        View.ObjectSpace.CommitChanges();
                        SqlParameter[] pQuyDoi = new SqlParameter[1];
                        pQuyDoi[0] = new SqlParameter("@QuanLySauDaiHoc", OidQuanLy.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_QuyDoiDuLieu_SauDaiHoc", CommandType.StoredProcedure, pQuyDoi);
                        View.ObjectSpace.Refresh();
                    }
                    MessageBox.Show("Quy đổi thành công", "Thông báo");
                }
                catch (Exception exx)
                {
                    MessageBox.Show(exx.Message, "Thông báo");
                }
            }
        }
    }
}
