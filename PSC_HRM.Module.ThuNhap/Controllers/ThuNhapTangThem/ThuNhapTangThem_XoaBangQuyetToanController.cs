using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThuNhapTangThem_XoaBangQuyetToanController : ViewController
    {
        public ThuNhapTangThem_XoaBangQuyetToanController()
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
            if (obj.Count == 1 && obj[0] is BangThuNhapTangThem)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_ThuNhapTangThem_XoaBangQuyetToanThuNhapTangThem");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            BangThuNhapTangThem bang = View.CurrentObject as BangThuNhapTangThem;
            if (bang != null)
            {
                if (bang.ChungTu == null)
                {
                    DataProvider.ExecuteNonQuery(store,
                        System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", bang.Oid));
                }
                else
                    XtraMessageBox.Show("Không thể xóa bảng quyết toán thu nhập tăng thêm. Bảng quyết toán thu nhập tăng thêm đã được chi tiền.", "Thông báo");
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
