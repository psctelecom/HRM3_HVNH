using System;

using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.KhauTru;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class KhauTruLuong_XoaBangKhauTruLuongController : ViewController
    {
        public KhauTruLuong_XoaBangKhauTruLuongController()
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
            if (obj.Count == 1 && obj[0] is BangKhauTruLuong)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_KhauTruLuong_XoaBangKhauTruLuong");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            BangKhauTruLuong obj = View.CurrentObject as BangKhauTruLuong;
            if (obj != null)
            {
                if (obj.ChungTu == null)
                {
                    DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure,
                        new SqlParameter("@Oid", obj.Oid));
                }
                else
                    XtraMessageBox.Show("Không thể bảng khấu trừ lương. Bảng này đã được khấu trừ vào lương.", "Thông báo");
            }

            //nếu là listview thì update lại lưới
            if (view != null)
                ObjectSpace.Refresh();
            //ngược lại là detailview thì đóng cửa sổ lại
            else
            {
                View.Close();
            }
        }
    }
}
