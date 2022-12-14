using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module.ThuNhap.NgoaiGio;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class NgoaiGio_XoaBangLuongNgoaiGioController : ViewController
    {
        public NgoaiGio_XoaBangLuongNgoaiGioController()
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
            if (obj.Count == 1 && obj[0] is BangLuongNgoaiGio)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_NgoaiGio_XoaBangLuongNgoaiGio");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            BangLuongNgoaiGio obj = View.CurrentObject as BangLuongNgoaiGio;
            if (obj != null)
            {
                if (obj.ChungTu == null)
                {
                    DataProvider.ExecuteNonQuery(store, 
                        System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", obj.Oid));
                }
                else
                    XtraMessageBox.Show("Không thể bảng lương ngoài giờ. Bảng này đã được chi tiền.", "Thông báo");
            }

            //nếu là listview thì update lại lưới
            if (view != null)
                View.Refresh();
            //ngược lại là detailview thì đóng cửa sổ lại
            else
                View.Close();
        }
    }
}
