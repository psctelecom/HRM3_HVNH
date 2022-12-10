using System;

using DevExpress.ExpressApp;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class OddEvenStyleForListViewController : ViewController
    {
        public OddEvenStyleForListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        void OddEvenStyleForListViewController_ViewControlsCreated(object sender, EventArgs e)
        {
            if(View is ListView)
            {
                GridControl gridControl = View.Control as GridControl;
                if(gridControl != null)
                {
                    GridView gridView = gridControl.FocusedView as GridView;
                    if(gridView != null)
                    {
                        gridView.OptionsView.EnableAppearanceOddRow = true;
                        gridView.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(211, 211, 170);
                        gridView.OptionsPrint.EnableAppearanceOddRow = true;
                        gridView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.FromArgb(211, 211, 170);

                        if (View.AllowEdit.Count > 0)
                            gridView.MouseDown += gridView_MouseDown;
                    }
                }
            }
        }

        private void gridView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) != System.Windows.Forms.Keys.Control)
            {
                GridView view = sender as GridView;
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                if (hi.InRowCell)
                {
                    if (hi.Column.RealColumnEdit.EditorTypeName == "BoolEdit")
                    {
                        view.FocusedRowHandle = hi.RowHandle;
                        view.FocusedColumn = hi.Column;
                        view.ShowEditor();
                        CheckEdit edit = (view.ActiveEditor as CheckEdit);
                        if (edit != null)
                            edit.Toggle();
                        (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
                    }
                }
            }
        }
    }
}
