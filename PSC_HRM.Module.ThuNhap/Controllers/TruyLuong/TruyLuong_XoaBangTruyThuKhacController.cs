using System;
using DevExpress.ExpressApp;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using PSC_HRM.Module.ThuNhap.TruyThu;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class TruyLuong_XoaBangTruyThuKhacController : ViewController
    {
        public TruyLuong_XoaBangTruyThuKhacController()
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
            if (obj.Count == 1 && obj[0] is BangTruyLuong)
            {
                ExecuteCustomDeleteObjects("spd_TaiChinh_TruyLuong_XoaBangTruyThuKhac");
                e.Handled = true;
            }
        }

        private void ExecuteCustomDeleteObjects(string store)
        {
            ListView view = View as ListView;
            BangTruyThuKhac bangTruyLuong = View.CurrentObject as BangTruyThuKhac;
            if (bangTruyLuong != null)
            {
                if (bangTruyLuong.ChungTu == null)
                {
                    DataProvider.ExecuteNonQuery(store,
                        System.Data.CommandType.StoredProcedure, new SqlParameter("@Oid", bangTruyLuong.Oid));
                }
                else
                    DialogUtil.ShowError("Bảng truy thu khác đã lập chứng từ chuyển khoản.");
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
