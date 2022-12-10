using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraTreeList;
using DevExpress.ExpressApp.TreeListEditors.Win;
using PSC_HRM.Module.Win.Common;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraPrinting;
using DevExpress.Xpf.Printing;
using DevExpress.XtraEditors.Repository;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomizeExportExcelController : ViewController
    {
        private ExportController exportController;


        protected override void OnActivated()
        {
            base.OnActivated();
            exportController = Frame.GetController<ExportController>();
            exportController.CustomExport += exportController_CustomExport;
        }

        void exportController_CustomExport(object sender, CustomExportEventArgs e)
        {
            GridListEditor gridListEditor = ((ListView)View).Editor as GridListEditor;

            if (gridListEditor != null)
            {
                //Set format tạm cho cột trước khi xuất ra excel để bỏ kiểu currency
                foreach (GridColumn item in gridListEditor.GridView.Columns)
                {
                    if (item.ColumnType == typeof(decimal))
                        item.DisplayFormat.FormatString = "g";
                    else if (item.ColumnType == typeof(Int32))
                        item.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                }

                XlsExportOptions options = new XlsExportOptions();
                options.SheetName = "Sheet1";
                options.ShowGridLines = true;
                options.ExportMode = XlsExportMode.SingleFile;
                options.TextExportMode = TextExportMode.Value;
                gridListEditor.GridView.ExportToXls(e.Stream, options);
                e.Handled = true;

                //Khôi phụ lại format thật cho cột
                foreach (GridColumn item in gridListEditor.GridView.Columns)
                {
                    if (item.ColumnType == typeof(decimal) || item.ColumnType == typeof(Int32))
                        item.DisplayFormat.FormatString = item.RealColumnEdit.DisplayFormat.FormatString;
                }
            }
        }

        public CustomizeExportExcelController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnDeactivated()
        {
            exportController.CustomExport -= exportController_CustomExport;
            base.OnDeactivated();
        }
    }
}
