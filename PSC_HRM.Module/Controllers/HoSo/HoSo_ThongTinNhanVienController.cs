using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.Persistent.Base.General;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_ThongTinNhanVienController : ViewController
    {
        public HoSo_ThongTinNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(ThongTinNhanVien);
            Activated += FilterCategoryTruongHoc_Activated;
            Deactivated += FilterCategoryTruongHoc_Deactivating;
            HamDungChung.DebugTrace("HoSo_ThongTinNhanVienController");
        }

        void FilterCategoryTruongHoc_Deactivating(object sender, EventArgs e)
        {
            View.ControlsCreated -= View_ControlsCreated;
        }

        void FilterCategoryTruongHoc_Activated(object sender, EventArgs e)
        {
            View.ControlsCreated += View_ControlsCreated;
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            CategorizedListEditor editor = (View as ListView).Editor as CategorizedListEditor;
            if (editor != null)
            {
                ListView listView = editor.CategoriesListView;

                if (listView != null)
                {
                    //hide all columns, only show column TenBoPhan
                    TreeListEditor treeListEditor = listView.Editor as TreeListEditor;
                    if (treeListEditor != null)
                    {
                        DevExpress.XtraTreeList.TreeList treeList = treeListEditor.TreeList;
                        if (treeList != null)
                        {
                            foreach (DevExpress.XtraTreeList.Columns.TreeListColumn item in treeList.Columns)
                            {
                                item.Visible = false;
                            }
                            treeList.Columns["TenBoPhan"].Visible = true;
                        }
                    }
                }
            }
        }

        private void HoSo_ThongTinNhanVienAction_ViewControlsCreated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;
            if (listView != null)
            {
                listView.CurrentObjectChanged += listView_CurrentObjectChanged;
            }
        }

        void listView_CurrentObjectChanged(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView != null)
            {
                ThongTinNhanVien nhanVien = listView.CurrentObject as ThongTinNhanVien;
                if (nhanVien != null)
                {
                    ThongTinNhanVien.NhanVien = nhanVien;
                    HoSo.HoSo.CurrentHoSo = nhanVien;
                }
            }
        }
    }
}
