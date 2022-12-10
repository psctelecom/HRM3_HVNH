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
    public partial class ChungTu_DeleteChuyenKhoanLuongNhanVienController : ViewController
    {
        public ChungTu_DeleteChuyenKhoanLuongNhanVienController()
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
            ExecuteCustomDeleteObjects("spd_TaiChinh_ChungTu_XoaChuyenKhoanLuongNhanVien");
            e.Handled = true;     
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            ChuyenKhoanLuongNhanVien chuyenKhoan = View.CurrentObject as ChuyenKhoanLuongNhanVien;
            if (chuyenKhoan != null)
            {
                //
                BangChotTongThuNhapNhanVien bangChotCuoiThang = view.ObjectSpace.FindObject<BangChotTongThuNhapNhanVien>(CriteriaOperator.Parse("KyTinhLuong=?", chuyenKhoan.KyTinhLuong.Oid));
                if (bangChotCuoiThang != null)
                {
                    DialogUtil.ShowError(String.Format("Xóa bảng chốt tổng thu nhập [{0}] trước khi xóa chứng từ.",chuyenKhoan.KyTinhLuong.TenKy));
                    return;
                }
                if(chuyenKhoan.ThanhToan)
                {
                    DialogUtil.ShowError("Chứng từ đã chi tiền.");
                    return;
                }
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
