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
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BangThuLaoNhanVien_XoaChiTietThuLao_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public BangThuLaoNhanVien_XoaChiTietThuLao_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            //TargetViewId = "BangThuLaoNhanVien_ListChiTietThuLaoNhanVien_ListView";
        }

        private void btXoa_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            string user = "";
            foreach (ChiTietThuLaoNhanVien item in View.SelectedObjects)
            {
                if (item.BangThuLaoNhanVien.ChungTu == null)
                {
                    if (TruongConfig.MaTruong == "HVNH")
                    {
                        if (item.DaThanhToan)
                        {
                            XtraMessageBox.Show("Đã thanh toán thù lao - không  thể xóa!", "Thông báo");
                            return;
                        }
                        else
                        {
                            user = HamDungChung.CurrentUser().UserName.ToString();
                            SqlParameter[] param = new SqlParameter[2]; /*Số parameter trên Store Procedure*/
                            param[0] = new SqlParameter("@OidChiTietThuLaoNhanVien", item.Oid);
                            param[1] = new SqlParameter("@User", user != string.Empty ? user : "");
                            DataProvider.ExecuteNonQuery("spd_PMS_BangThuLaoNhanVien_XoaChiTietThuLao", System.Data.CommandType.StoredProcedure, param);
                        }
                    }
                    else
                    {
                        user = HamDungChung.CurrentUser().UserName.ToString();
                        SqlParameter[] param = new SqlParameter[2]; /*Số parameter trên Store Procedure*/
                        param[0] = new SqlParameter("@OidChiTietThuLaoNhanVien", item.Oid);
                        param[1] = new SqlParameter("@User", user != string.Empty ? user : "");
                        DataProvider.ExecuteNonQuery("spd_PMS_BangThuLaoNhanVien_XoaChiTietThuLao", System.Data.CommandType.StoredProcedure, param);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Thù lao đã lập chứng từ - không thể xóa!", "Thông báo");
                    return;
                }
            }
            XtraMessageBox.Show("Xóa chi tiết thù lao nhân viên thành công", "Thông báo");
            View.ObjectSpace.Refresh();
        }
    }
}