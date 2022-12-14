using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.Win
{
    public partial class ShowOneGroupReportController : ViewController
    {
        public ShowOneGroupReportController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(Report.HRMReport);
            Activated += ShowOneGroupReportController_Activated;
            Deactivated += ShowOneGroupReportController_Deactivated;
        }

        void ShowOneGroupReportController_Deactivated(object sender, EventArgs e)
        {
            View.ControlsCreated -= View_ControlsCreated;
        }

        void ShowOneGroupReportController_Activated(object sender, EventArgs e)
        {
            View.ControlsCreated += View_ControlsCreated;
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            CategorizedListEditor cat = (View as ListView).Editor as CategorizedListEditor;
            if (View.Id == "BaoCaoTaiChinh_ListView")
                cat.CategoriesListView.CollectionSource.Criteria["ByBaoCaoThuNhap"] = CriteriaOperator.Parse("TenNhom='Báo cáo Tài chính'");
        }
    }
}
