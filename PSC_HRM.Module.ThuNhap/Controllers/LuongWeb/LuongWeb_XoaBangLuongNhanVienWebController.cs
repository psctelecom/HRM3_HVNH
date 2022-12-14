using System;
using DevExpress.ExpressApp;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.LuongWeb;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class LuongWeb_XoaBangLuongNhanVienWebController : ViewController
    {
        public LuongWeb_XoaBangLuongNhanVienWebController()
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
            if (obj.Count == 1 && obj[0] is BangLuongNhanVienWeb)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_LuongWeb_XoaBangLuongNhanVienWeb");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            BangLuongNhanVienWeb bangLuong = View.CurrentObject as BangLuongNhanVienWeb;
            if (bangLuong != null)
            {
                if (!bangLuong.KyTinhLuong.KhoaSo)
                {
                    DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", bangLuong.Oid));
                }
                else
                {
                    DialogUtil.ShowWarning("Kỳ tính lương đã khóa sổ. Khổng thể xóa!!!");
                }
            }

            //nếu là listview thì update lại lưới
            if (view != null)
                ObjectSpace.Refresh();
            //ngược lại là detailview thì đóng cửa sổ lại
            else
                View.Close();
        }
    }
}
