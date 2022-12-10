using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap;
using DevExpress.Persistent.BaseImpl;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class TruyLuongNew_LayHeSoController: ViewController
    {
        public TruyLuongNew_LayHeSoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TruyLuongNew_LayHeSoController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("NEU"))
                simpleAction1.Active["TruyCap"] = false;
            else
                simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangTruyLuongNew>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu dữ liệu lại
            View.ObjectSpace.CommitChanges();
            //
            BangTruyLuongNew bangTruyLuong = View.CurrentObject as BangTruyLuongNew;
            if (bangTruyLuong != null)
            {
                using (DialogUtil.AutoWait())
                {
                    if (bangTruyLuong.KyTinhLuong.KhoaSo)
                        DialogUtil.ShowWarning(String.Format("Kỳ tính lương '{0}' đã khóa sổ.", bangTruyLuong.KyTinhLuong.TenKy));
                    else if (bangTruyLuong.ChungTu != null)
                        DialogUtil.ShowWarning("Bảng truy lĩnh đã được lập chứng từ chi tiền.");
                    else if (bangTruyLuong.NgayLap < bangTruyLuong.KyTinhLuong.TuNgay || bangTruyLuong.NgayLap > bangTruyLuong.KyTinhLuong.DenNgay)
                    {
                        DialogUtil.ShowWarning("Ngày lập phải nằm trong kỳ tính lương.");
                    }
                    else if (bangTruyLuong.DenNgay > bangTruyLuong.KyTinhLuong.DenNgay)
                    {
                        DialogUtil.ShowWarning("Đến ngày phải nằm trong kỳ tính lương.");
                    }
                    else
                    {
                        //Tính dữ liệu mới
                        XuLy(View.ObjectSpace, bangTruyLuong);

                        View.ObjectSpace.ReloadObject(bangTruyLuong);
                        (View as DetailView).Refresh();  
                    }
                }

                //
                DialogUtil.ShowInfo("Lấy hệ số thành công.");
            }
        }

        public void XuLy(IObjectSpace obs, BaseObject obj)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BangTruyLuong", obj.Oid);

            SqlCommand cmd = new SqlCommand("spd_TaiChinh_TruyLuongNew_LayHeSo");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(param);
            DataProvider.ExecuteNonQuery((SqlConnection)((XPObjectSpace)obs).Session.Connection, cmd);
        }
    }
}
