using System;
using DevExpress.ExpressApp;
using PSC_HRM.Module.DanhMuc;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using System.Windows.Forms;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThongTinLuong_XoaBangChotThongTinTinhLuongController : ViewController
    {
        public ThongTinLuong_XoaBangChotThongTinTinhLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            View.ObjectSpace.CustomDeleteObjects += ObjectSpace_CustomDeleteObjects;
        }

        protected override void OnDeactivated()
        {
            View.ObjectSpace.CustomDeleteObjects -= ObjectSpace_CustomDeleteObjects;
            base.OnDeactivated();
        }

        private void ObjectSpace_CustomDeleteObjects(object sender, CustomDeleteObjectsEventArgs e)
        {
            System.Collections.ArrayList obj = (System.Collections.ArrayList)e.Objects;
            if (obj.Count == 1 && obj[0] is BangChotThongTinTinhLuong)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_DeleteBangChotThongTinTinhLuong");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            DevExpress.ExpressApp.ListView view = View as DevExpress.ExpressApp.ListView;
            BangChotThongTinTinhLuong bangChotThongTinTinhLuong = View.CurrentObject as BangChotThongTinTinhLuong;

            if (bangChotThongTinTinhLuong != null)
            {
                try
                {
                        using (DialogUtil.AutoWaitForDelete())
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@BangChotThongTinTinhLuong", bangChotThongTinTinhLuong.Oid);
                            param[1] = new SqlParameter("@Type", true);

                            DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure, param);
                        }
                        //DialogUtil.ShowInfo("Xóa bảng chốt thông tin tính lương thành công.");
                }
                catch (Exception ex)
                {
                    DialogUtil.ShowError("Xóa bảng chốt thông tin tính lương không thành công." + ex.Message);
                }
                 
            }

        }
    }
}
