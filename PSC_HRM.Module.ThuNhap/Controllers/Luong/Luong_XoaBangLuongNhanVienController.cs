using System;
using DevExpress.ExpressApp;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class Luong_XoaBangLuongNhanVienController : ViewController
    {
        public Luong_XoaBangLuongNhanVienController()
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
            if (obj.Count == 1 && obj[0] is BangLuongNhanVien)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_Luong_XoaBangLuongNhanVien");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            BangLuongNhanVien bangLuong = View.CurrentObject as BangLuongNhanVien;
            if (bangLuong != null)
            {
                if (bangLuong.ChungTu == null)
                {
                    DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", bangLuong.Oid));
                }
                else
                    XtraMessageBox.Show("Không thể xóa bảng lương. Bảng lương đã được chi tiền.", "Thông báo");
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
