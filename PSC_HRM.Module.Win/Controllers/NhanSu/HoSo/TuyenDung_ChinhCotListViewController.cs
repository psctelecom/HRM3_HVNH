using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Common;
using PSC_HRM.Module.Win.Forms;
using DevExpress.XtraGrid.Columns;
using PSC_HRM.Module.TuyenDung;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class TuyenDung_ChinhCotListView1Controller : ViewController
    {
        public TuyenDung_ChinhCotListView1Controller()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            GridControl gridControl = View.Control as GridControl;
            if (gridControl != null)
            {
                GridView gridView = gridControl.FocusedView as GridView;
                if (gridView != null)
                {
                    using (frmListViewColumns dialog = new frmListViewColumns(typeof(UngVien)))
                    {
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //hide all columns
                            for (int i = 0; i < gridView.Columns.Count; i++)
                            {
                                gridView.Columns[i].Visible = false;
                            }

                            //show columns choosed
                            List<ObjectProperty> data = dialog.GetData();
                            foreach (ObjectProperty item in data)
                            {
                                foreach (GridColumn col in gridView.Columns)
                                {
                                    if (col.FieldName == item.PropertyName)
                                    {
                                        col.Visible = true;
                                        col.VisibleIndex = item.Index;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            e.ShowViewParameters.Context = TemplateContext.View;
        }
    }
}
