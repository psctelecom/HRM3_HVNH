using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.PivotGrid.Win;
using DevExpress.XtraPivotGrid;
using PSC_HRM.Module.PMS.NonPersistentObjects.TaiChinh;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ThongKe_BaoCao_PhuLuc01_Controller : ViewController
    {
        IObjectSpace iob;
        Pivot_PhuLuc01 tk;
        DevExpress.ExpressApp.ListView listView;
        static PivotGridListEditor PivotEditor;

        public ThongKe_BaoCao_PhuLuc01_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            //TargetObjectType = typeof(Pivot_PhuLuc01);
            //TargetViewId = "Pivot_PhuLuc01_DetailView;Pivot_PhuLuc01_listChiTiet_ListView;";
            //TargetViewId = "Pivot_PhuLuc01_listChiTiet_ListView;";
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            View.ControlsCreated += View_ControlsCreated;
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            listView = View as DevExpress.ExpressApp.ListView;
            DetailView detailView = View as DetailView;

            if (listView != null)
            {
                PivotEditor = listView.Editor as PivotGridListEditor;
                //PivotEditor.PivotGridControl.OptionsView.ShowColumnGrandTotalHeader = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowColumnGrandTotals = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowColumnTotals = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowCustomTotalsForSingleValues = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowGrandTotalsForSingleValues = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowRowGrandTotalHeader = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowRowGrandTotals = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowRowTotals = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowTotalsForSingleValues = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowFilterHeaders = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowColumnHeaders = false;
                //PivotEditor.PivotGridControl.OptionsView.ShowDataHeaders = false;
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            tk = View.CurrentObject as Pivot_PhuLuc01;
            if (tk != null)
            {
                tk.Loaddata();
            }
            if (PivotEditor != null)
            {
                PivotEditor.PivotGridControl.RefreshData();
            }
        }
    }
}
