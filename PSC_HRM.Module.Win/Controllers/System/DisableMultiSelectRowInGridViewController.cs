using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class DisableMultiSelectRowInGridViewController : ViewController
    {
        public DisableMultiSelectRowInGridViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DisableMultiSelectRowInGridViewController_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            if (listView != null &&
                listView.ObjectTypeInfo.FullName.StartsWith("PSC_HRM.Module.ThuNhap") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungLuongKy1") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungPhuCapUuDai") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungPhuCapTrachNhiem") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungLuongKy2") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungNangLuongKy1") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungNangLuongKy2") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungPhuCapTienSi") &&
                !listView.ObjectTypeInfo.FullName.Contains("ChiBoSungLuongPhuCapThamNien")
                )
                listView.ControlsCreated += listView_ControlsCreated;
        }

        void listView_ControlsCreated(object sender, EventArgs e)
        {
            GridControl gridControl = View.Control as GridControl;
            if (gridControl != null)
            {
                GridView gridView = gridControl.FocusedView as GridView;
                if (gridView != null)
                {
                    gridView.OptionsSelection.MultiSelect = false;
                }
            }
        }
    }
}
