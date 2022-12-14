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
using PSC_HRM.Module.ThuNhap.ThuLao;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BangThuLaoNhanVien_XoaFull : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public BangThuLaoNhanVien_XoaFull()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangThuLaoNhanVien_DetailView";
        }

        private void btXoaDuLieu_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BangThuLaoNhanVien BangThuLao = View.CurrentObject as BangThuLaoNhanVien;
            if(BangThuLao!=null)
            {
                string user = "";
                if(BangThuLao.ChungTu!=null)
                {
                    XtraMessageBox.Show("Bảng thù lao đã được lập chứng từ - Không thể xóa!", "Thông báo");
                }
                else
                {
                    using(DialogUtil.AutoWait("Đang xóa dữ liệu"))
                    {
                        user = HamDungChung.CurrentUser().UserName.ToString();
                        SqlParameter[] param = new SqlParameter[2]; /*Số parameter trên Store Procedure*/
                        param[0] = new SqlParameter("@BangThuLao", BangThuLao.Oid);
                        param[1] = new SqlParameter("@User", user != string.Empty ? user : "");
                        DataProvider.ExecuteNonQuery("spd_PMS_BangThuLaoNhanVien_XoaFullChiTietThuLao", System.Data.CommandType.StoredProcedure, param);
                    }
                    XtraMessageBox.Show("Xóa dữ liệu thành công", "Thông báo");
                    View.ObjectSpace.Refresh();
                }
            }
        }
    }
}