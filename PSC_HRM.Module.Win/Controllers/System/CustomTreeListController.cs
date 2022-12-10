using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base.General;
using PSC_HRM.Module.Win.Editors;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Columns;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomTreeListController : ViewController
    {
        public CustomTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);

            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(ICategorizedItem);
            Activated += CustomTreeListController_Activated;
            Deactivated += CustomTreeListController_Deactivating;
        }

        private void CustomTreeListController_Activated(object sender, EventArgs e)
        {
            View.ControlsCreated += View_ControlsCreated;
        }

        private void CustomTreeListController_Deactivating(object sender, EventArgs e)
        {
            View.ControlsCreated -= View_ControlsCreated;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            #region Thông tin nhân viên
            CustomCategorizedListEditor editorThongTinNhanVien = (View as ListView).Editor as CustomCategorizedListEditor;
            if (editorThongTinNhanVien != null)
            {
                ListView listView = editorThongTinNhanVien.CategoriesListView;

                if (listView != null)
                {
                    TreeListEditor treeListEditor = listView.Editor as TreeListEditor;
                    if (treeListEditor != null)
                    {
                        TreeList treeList = treeListEditor.TreeList;
                        if (treeList != null)
                        {
                            foreach (TreeListColumn item in treeList.Columns)
                            {
                                if (item.Name != "TenBoPhan")
                                    item.Visible = false;
                            }
                            TreeListColumn column = treeList.Columns["STT"];
                            if (column != null)
                            {
                                if (TruongConfig.MaTruong.Equals("UTE"))
                                {
                                    column.SortIndex = 1;
                                }
                                else
                                {
                                    column.SortIndex = 0;
                                }
                                column.SortOrder = System.Windows.Forms.SortOrder.Ascending;
                            }
                            column = treeList.Columns["TenBoPhan"];
                            if (column != null)
                            {
                                if (TruongConfig.MaTruong.Equals("UTE"))
                                {
                                    column.SortIndex = 0;
                                }
                                else
                                {
                                    column.SortIndex = 1;
                                }
                                column.SortOrder = System.Windows.Forms.SortOrder.Ascending;
                            }
                        }
                    }

                    NguoiSuDung user = SecuritySystem.CurrentUser as NguoiSuDung;
                    if (user != null)
                    {
                        if (editorThongTinNhanVien.Name == "Thông tin cán bộ")
                        {
                            XafGridView gridEditor = editorThongTinNhanVien.GridView;
                            if (gridEditor != null)
                            {
                                gridEditor.Columns[1].SortIndex = 0;
                                gridEditor.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                                gridEditor.Columns[0].SortIndex = 1;
                                gridEditor.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }
                        }
                    }                
                }
            }
            #endregion

            #region Giảng viên thỉnh giảng
            CustomCategorizedGiangVienThinhGiangListEditor editorGiangVienThinhGiang = (View as ListView).Editor as CustomCategorizedGiangVienThinhGiangListEditor;
            if (editorGiangVienThinhGiang != null)
            {
                ListView listView = editorGiangVienThinhGiang.CategoriesListView;

                if (listView != null)
                {
                    TreeListEditor treeListEditor = listView.Editor as TreeListEditor;
                    if (treeListEditor != null)
                    {
                        TreeList treeList = treeListEditor.TreeList;
                        if (treeList != null)
                        {
                            foreach (TreeListColumn item in treeList.Columns)
                            {
                                if (item.Name != "TenBoPhan")
                                    item.Visible = false;
                            }
                            TreeListColumn column = treeList.Columns["STT"];
                            if (column != null)
                            {
                                if (TruongConfig.MaTruong.Equals("UTE"))
                                {
                                    column.SortIndex = 1;
                                }
                                else
                                {
                                    column.SortIndex = 0;
                                }
                                column.SortOrder = System.Windows.Forms.SortOrder.Ascending;
                            }
                            column = treeList.Columns["TenBoPhan"];
                            if (column != null)
                            {
                                if (TruongConfig.MaTruong.Equals("UTE"))
                                {
                                    column.SortIndex = 0;
                                }
                                else
                                {
                                    column.SortIndex = 1;
                                }
                                column.SortOrder = System.Windows.Forms.SortOrder.Ascending;
                            }
                        }
                    }

                    NguoiSuDung user = SecuritySystem.CurrentUser as NguoiSuDung;
                    if (user != null)
                    {
                        if (editorGiangVienThinhGiang.Name == "Giảng viên thỉnh giảng")
                        {
                            XafGridView gridEditor = editorGiangVienThinhGiang.GridView;
                            if (gridEditor != null)
                            {
                                gridEditor.Columns[1].SortIndex = 0;
                                gridEditor.Columns[1].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                                gridEditor.Columns[0].SortIndex = 1;
                                gridEditor.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }
                        }
                    }
                }
            }
            #endregion

        }
    }
}
