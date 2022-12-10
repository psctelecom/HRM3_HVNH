using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.TreeListEditors.Win;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_GiangVienThinhGiangController : ViewController
    {
        public HoSo_GiangVienThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(GiangVienThinhGiang);
            Activated += FilterCategoryTruongHoc_Activated;
            Deactivated += FilterCategoryTruongHoc_Deactivating;
            HamDungChung.DebugTrace("HoSo_GiangVienThinhGiangController");
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
                GiangVienThinhGiang giangVienThinhGiang = listView.CurrentObject as GiangVienThinhGiang;
                if (giangVienThinhGiang != null)
                {
                    HoSo.HoSo.CurrentHoSo = giangVienThinhGiang;
                }
            }
        }
    }
}
