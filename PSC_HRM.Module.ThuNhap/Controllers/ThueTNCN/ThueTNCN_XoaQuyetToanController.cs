using System;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using PSC_HRM.Module.ThuNhap.TamUng;
using PSC_HRM.Module.ThuNhap.Thue;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThueTNCN_XoaQuyetToanController : ViewController
    {
        public ThueTNCN_XoaQuyetToanController()
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
            if (obj.Count == 1 && obj[0] is BangKhauTruTamUng)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_ThueTNCN_XoaToKhaiQuyetToanThueTNCN");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            ToKhaiQuyetToanThueTNCN bang = View.CurrentObject as ToKhaiQuyetToanThueTNCN;
            if (bang != null)
            {
                DataProvider.ExecuteNonQuery(store,
                    System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", bang.Oid));
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
