using System;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.ChamCong;

namespace PSC_HRM.Module.Controllers
{
    public partial class ChamCong_XoaBangChamCongNhanVienController : ViewController
    {
        public ChamCong_XoaBangChamCongNhanVienController()
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
            if (obj.Count == 1 && obj[0] is QuanLyChamCongNhanVien)
            {
                ExecuteCustomDeleteObjects("spd_ChamCong_XoaBangQuanLyChamCongNhanVien");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            QuanLyChamCongNhanVien quanLyChamCongNhanVien = View.CurrentObject as QuanLyChamCongNhanVien;
            if (quanLyChamCongNhanVien != null)
            {
                if (!quanLyChamCongNhanVien.KyTinhLuong.KhoaSo)
                {
                    DataProvider.ExecuteNonQuery(store, System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", quanLyChamCongNhanVien.Oid));
                }
                else
                    XtraMessageBox.Show("Không thể xóa bảng lương. Bảng lương đã khóa sổ.", "Thông báo");
            }
            //Refesh lại lưới
            View.Refresh();
        }
    }
}
