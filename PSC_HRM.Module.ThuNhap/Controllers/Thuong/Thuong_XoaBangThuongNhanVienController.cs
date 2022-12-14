using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module.ThuNhap.Thuong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class Thuong_XoaBangThuongNhanVienController : ViewController
    {
        public Thuong_XoaBangThuongNhanVienController()
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
            if (obj.Count == 1 && obj[0] is BangThuongNhanVien)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_Thuong_XoaBangThuongNhanVien");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            BangThuongNhanVien obj = View.CurrentObject as BangThuongNhanVien;
            if (obj != null)
            {
                if (obj.ChungTu == null)
                {
                    DataProvider.ExecuteNonQuery(store,
                        System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", obj.Oid));
                }
                else
                    XtraMessageBox.Show("Không thể xóa bảng khen thưởng - phúc lợi. Bảng này đã được chi tiền.", "Thông báo");
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
