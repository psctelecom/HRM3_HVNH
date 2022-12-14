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
    public partial class BangChotThuLao_XoaController : ViewController
    {
        public BangChotThuLao_XoaController()
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
            if (obj.Count == 1 && obj[0] is BangChotThuLao)
            {
                ExecuteCustomDeleteObjects("spd_PMS_Xoa_BangChotThuLao");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            using (DialogUtil.AutoWait("Đang xóa dữ liệu"))
            {
                ListView view = View as ListView;
                BangChotThuLao bangChotThuLao = View.CurrentObject as BangChotThuLao;
                if (bangChotThuLao != null)
                {
                    if (!bangChotThuLao.Khoa)
                    {
                        object kq = "";
                        SqlParameter[] pXoaChot = new SqlParameter[2];
                        pXoaChot[0] = new SqlParameter("@BangChotThuLao", bangChotThuLao.Oid);
                        pXoaChot[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                        kq = DataProvider.GetValueFromDatabase("spd_PMS_Xoa_BangChotThuLao", CommandType.StoredProcedure, pXoaChot);
                        XtraMessageBox.Show(kq.ToString(), "Thông báo");
                    }
                    else
                    {
                        XtraMessageBox.Show("Đã khóa bảng chốt - Không thể xóa!", "Thông báo");
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