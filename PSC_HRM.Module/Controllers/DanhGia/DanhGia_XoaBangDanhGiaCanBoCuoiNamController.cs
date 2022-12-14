using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhGia;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class DanhGia_XoaBangDanhGiaCanBoCuoiNamController : ViewController
    {
        public DanhGia_XoaBangDanhGiaCanBoCuoiNamController()
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
            if (obj.Count == 1 && obj[0] is DanhGiaCanBoCuoiNam)
            {
                
                ExecuteCustomDeleteObjects("spd_DanhGia_XoaBangDanhGiaCanBoCuoiNam");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            DanhGiaCanBoCuoiNam danhGiaCanBoCuoiNam = View.CurrentObject as DanhGiaCanBoCuoiNam;
            if (danhGiaCanBoCuoiNam != null)
            {
                 DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", danhGiaCanBoCuoiNam.Oid));
            }
        }
    }
}
