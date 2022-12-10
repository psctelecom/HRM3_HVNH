using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using PSC_HRM.Module.ThuNhap.ChungTu;
using DevExpress.XtraEditors;
using System.Data;
using PSC_HRM.Module.ThuNhap.Thue;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ChungTu_DeleteChuyenKhoanLuongNhanVien_TamGiuLuongController : ViewController
    {
        public ChungTu_DeleteChuyenKhoanLuongNhanVien_TamGiuLuongController()
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
            ExecuteCustomDeleteObjects("spd_TaiChinh_ChungTu_XoaChuyenKhoanLuongNhanVien_TamGiuLuong");
            e.Handled = true;     
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            ChungTu_TamGiuLuong chuyenKhoan = View.CurrentObject as ChungTu_TamGiuLuong;
            if (chuyenKhoan != null)
            {
                //
                DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, new SqlParameter("@Oid", chuyenKhoan.Oid));
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
