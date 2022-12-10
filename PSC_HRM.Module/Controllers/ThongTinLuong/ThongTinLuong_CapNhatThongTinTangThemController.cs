using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module;
using System.Data;
using System.Data.SqlClient;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_CapNhatThongTinTangThemController : ViewController
    {
        public ThongTinLuong_CapNhatThongTinTangThemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_CapNhatThongTinTangThemController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                View.ObjectSpace.CommitChanges();
                //
                BangChotThongTinTinhLuong bangChotCurrrent = View.CurrentObject as BangChotThongTinTinhLuong;
                if (bangChotCurrrent != null)
                {
                    using (DialogUtil.AutoWait())
                    {
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@BangChotThongTinTinhLuong", bangChotCurrrent.Oid);

                        DataProvider.ExecuteNonQuery("spd_ThongTinLuong_CapNhatThongTinTangThem", CommandType.StoredProcedure, param);
                    }
                    DialogUtil.ShowInfo("Cập nhật thông tin tăng thêm thành công!");
                }
            }
            catch (Exception ex)
            {
                DialogUtil.ShowError("Cập nhật thông tin tăng thêm thất bại!");
            }
            View.ObjectSpace.Refresh();
        }

        private void CapNhatThamNienAction_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                BangChotThongTinTinhLuong bangChotCurrrent = View.CurrentObject as BangChotThongTinTinhLuong;
                if (bangChotCurrrent != null && bangChotCurrrent.KhoaSo)
                    simpleAction1.Active["TruyCap"] = false;
                else  
                    simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangChotThongTinTinhLuong>();
            }
            else
            {
                simpleAction1.Active["TruyCap"] = false;
            }
        }
    }
}
