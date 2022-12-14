using System;
using DevExpress.ExpressApp;
using PSC_HRM.Module.DanhMuc;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhGiaKPI;
using System.Windows.Forms;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class DanhGiaKPI_XoaQuanLyDanhGiaKPIController : ViewController
    {
        public DanhGiaKPI_XoaQuanLyDanhGiaKPIController()
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
            if (obj.Count == 1 && obj[0] is QuanLyDanhGiaKPI)
            {
                ExecuteCustomDeleteObjects("spd_DanhGiaKPI_DeleteQuanLyDanhGiaKPI");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            DevExpress.ExpressApp.ListView view = View as DevExpress.ExpressApp.ListView;
            QuanLyDanhGiaKPI qlDanhGiaKPI = View.CurrentObject as QuanLyDanhGiaKPI;

            if (qlDanhGiaKPI != null)
            {
                if (!qlDanhGiaKPI.KyTinhLuong.KhoaSo)
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@Oid", qlDanhGiaKPI.Oid);
                    DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure, param);
                }
                else
                    XtraMessageBox.Show("Không thể xóa Quản lý đánh giá KPI vì đã được dùng để tính lương.", "Thông báo");
            }
        }
    }
}
