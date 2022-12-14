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
    public partial class DanhGiaKPI_XoaDanhGiaKPIController : ViewController
    {
        public DanhGiaKPI_XoaDanhGiaKPIController()
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
            if (obj.Count == 1 && obj[0] is DanhGiaKPI.DanhGiaKPI)
            {
                //ExecuteCustomDeleteObjects("spd_DanhGiaKPI_DeleteDanhGiaKPI");
                //e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            DevExpress.ExpressApp.ListView view = View as DevExpress.ExpressApp.ListView;
            DanhGiaKPI.DanhGiaKPI danhGiaKPI = View.CurrentObject as DanhGiaKPI.DanhGiaKPI;

            if (danhGiaKPI != null)
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Oid", danhGiaKPI.Oid);
                DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure, param);
            }
        }
    }
}
