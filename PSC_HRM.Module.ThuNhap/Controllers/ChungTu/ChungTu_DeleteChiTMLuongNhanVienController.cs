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
    public partial class ChungTu_DeleteChiTMLuongNhanVienController : ViewController
    {
        public ChungTu_DeleteChiTMLuongNhanVienController()
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
            ExecuteCustomDeleteObjects("spd_TaiChinh_ChungTu_XoaChiTMLuongNhanVien");
            e.Handled = true;
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            ChiTMLuongNhanVien chungTuTienMat = View.CurrentObject as ChiTMLuongNhanVien;
            if (chungTuTienMat != null)
            {
                //xóa chứng từ khi chưa chi tiền
                if (chungTuTienMat.UyNhiemChi == null)
                {
                    DataProvider.ExecuteNonQuery(store, CommandType.StoredProcedure, new SqlParameter("@Oid", chungTuTienMat.Oid));
                }
                else
                    DialogUtil.ShowWarning("Không thể xóa chứng từ này. Chứng từ đã chi tiền.");
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
