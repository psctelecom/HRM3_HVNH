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

namespace PSC_HRM.Module.Win.Controllers.PMS.HVNH
{
    public partial class QuyDoiKeKhaiSauGiang_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        QuanLyKeKhaiSauGiang _KeKhai;
        public QuyDoiKeKhaiSauGiang_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKeKhaiSauGiang_DetailView";
        }

        private void btQuyDoi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _KeKhai = View.CurrentObject as QuanLyKeKhaiSauGiang;
            if (_KeKhai != null)
            {
                if (_KeKhai.Khoa == false)
                {
                    using (DialogUtil.AutoWait("Đang quy đổi liệu"))
                    {
                        SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                        param[0] = new SqlParameter("@KeKhaiSauGiang", _KeKhai.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_QuyDoi_KeKhaiSauGiang", System.Data.CommandType.StoredProcedure, param);
                        View.ObjectSpace.Refresh();
                    }
                    XtraMessageBox.Show("Quy Đổi thành công!", "Thông báo!");
                }
                else
                {
                    XtraMessageBox.Show("Đã khóa dữ liệu không thể quy đổi lại!", "Thông báo!");
                }
            }

        }
    }
}