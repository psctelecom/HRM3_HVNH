using System;
using DevExpress.ExpressApp;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class BangThuLaoNhanVien_XoaController : ViewController
    {
        public BangThuLaoNhanVien_XoaController()
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
            if (obj.Count == 1 && obj[0] is BangThuLaoNhanVien)
            {
                ExecuteCustomDeleteObjects("spd_PMS_Xoa_BangThuLaoNhanVien");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            using (DialogUtil.AutoWait("Đang xóa dữ liệu"))
            {
                ListView view = View as ListView;
                BangThuLaoNhanVien bangThuLaoNhanVien = View.CurrentObject as BangThuLaoNhanVien;
                if (bangThuLaoNhanVien != null)
                {
                    if (bangThuLaoNhanVien.ChungTu==null)
                    {
                        object kq = "";
                        SqlParameter[] pXoaChot = new SqlParameter[2];
                        pXoaChot[0] = new SqlParameter("@BangThuLaoNhanVien", bangThuLaoNhanVien.Oid);
                        pXoaChot[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                        kq = DataProvider.GetValueFromDatabase(store, CommandType.StoredProcedure, pXoaChot);
                        XtraMessageBox.Show(kq.ToString(), "Thông báo");
                    }
                    else
                    {
                        XtraMessageBox.Show("Đã lập chứng từ - Không thể xóa!", "Thông báo");
                    }
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
}