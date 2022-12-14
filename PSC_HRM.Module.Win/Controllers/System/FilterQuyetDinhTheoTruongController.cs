using System;

using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class FilterQuyetDinhTheoTruongController : ViewController
    {
        public FilterQuyetDinhTheoTruongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void FilterQuyetDinhTheoTruongController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;

            if (listView != null && !listView.Id.Contains("LookupListView") && (listView.ObjectTypeInfo.Base.Type == typeof(QuyetDinh.QuyetDinh) || listView.ObjectTypeInfo.Base.Type == typeof(QuyetDinh.QuyetDinhCaNhan)))
            {
                NguoiSuDung user = SecuritySystem.CurrentUser as NguoiSuDung;

                if (user != null && user.ThongTinTruong != null)
                {
                    listView.CollectionSource.Criteria.Clear();
                    listView.CollectionSource.Criteria["FilterQuyetDinh"] = CriteriaOperator.Parse("ThongTinTruong.Oid=?", user.ThongTinTruong.Oid);
                    listView.ObjectSpace.Refresh();
                }
            }
        }
    }
}
