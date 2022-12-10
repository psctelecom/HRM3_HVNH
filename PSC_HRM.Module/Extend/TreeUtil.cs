using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module
{
    public static class TreeUtil
    {
        /// Enum Format Column
        public enum ColumnFormat { NULL, Date, DateTime, N0, N1, N2, N3, N4 }
        #region Process
        /// Hàm dùng để khởi tạo cấu hình giao diện cho grid view.
        public static void InitTreeView(TreeList tree)
        {
            #region Step 1: Set các thuộc tính cơ bản
            /// Filter Row
            //tree.ShowFindPanel();
            tree.OptionsView.ShowAutoFilterRow = true;
            tree.OptionsBehavior.EnableFiltering = true;
            tree.OptionsFilter.FilterMode = FilterMode.Smart;

            /// Wrap Header
            tree.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Default;

            /// HorzAlignment Text
            tree.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

            /// Độ rộng của cột tự co dãn 
            tree.OptionsView.AutoWidth = true;

            /// Tắt mở hệ thông Panel Filter
            tree.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Default;

            //Cố định các cột
            tree.BestFitColumns();

            foreach (TreeListColumn item in tree.Columns)
            {
                //Canh trái phải các cột trong cây
                if (item.ColumnType == typeof(Int32) || item.ColumnType == typeof(DateTime))
                {
                    item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
                //if (item.ColumnType == typeof(string) || item.ColumnType == typeof(DateTime))
                {
                    item.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;
                }
            }
            #endregion

            #region Step 2: Bổ sung các sự kiện trên Cây
            /// Sự kiện cho việc đánh số thứ tự
            ShowRowNumber(tree);
            #endregion
        }

        /// Bật cột số thứ tự trên TreeView
        public static void ShowRowNumber(TreeList tree)
        {
            tree.IndicatorWidth = 40;
            tree.CustomDrawNodeIndicator -= tree_CustomDrawNodeIndicator;
            tree.CustomDrawNodeIndicator += tree_CustomDrawNodeIndicator;
        }

        /// Hàm đánh số thứ tự của từng dòng bằng sự kiện vẽ cột Indication
        public static void CreateNumberRow(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            TreeList tree = sender as TreeList;

            /// Lấy thông tin từng dòng đang vẽ
            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = default(DevExpress.Utils.Drawing.IndicatorObjectInfoArgs);
            args = (DevExpress.Utils.Drawing.IndicatorObjectInfoArgs)e.ObjectArgs;

            /// Đánh số thứ tự cho dòng
            if (tree.GetVisibleIndexByNode(e.Node) >= 0)
                args.DisplayText = (tree.GetVisibleIndexByNode(e.Node) + 1).ToString();
        }

        /// Hàm Fix thứ tự các các cột về bên trai hoặc bên tay phải
        public static void FixColumn(TreeList tree, string[] fieldNames, DevExpress.XtraTreeList.Columns.FixedStyle fixType = DevExpress.XtraTreeList.Columns.FixedStyle.Left)
        {
            /// Field Name
            string strFieldName = string.Empty;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                /// Lấy field Name
                strFieldName = fieldNames[i];

                /// Set thuộc tính Fix cho các column
                tree.Columns[strFieldName].Fixed = fixType;
            }
        }

        /// Hàm cho phép Ẩn một số cột khi đang chạy
        public static void InvisibleColumn(TreeList tree, string[] fieldNames)
        {
            /// Field Name
            string strFieldName = string.Empty;

            /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
            for (int i = 0; i < fieldNames.Length; i++)
            {
                /// Lấy field Name
                strFieldName = fieldNames[i];

                /// Set thuộc tính Fix cho các column
                tree.Columns[strFieldName].Visible = false;
            }
        }

        /// Hàm cho phép Edit hoặc không cho edit một số cột
        public static void AllowEditColumn(TreeList tree, string[] fieldNames = null, bool bEditColum = false)
        {
            /// Field Name
            string strFieldName = string.Empty;

            /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
            if (fieldNames == null)
            {
                foreach (TreeListColumn col in tree.Columns)
                {
                    col.OptionsColumn.AllowEdit = bEditColum;
                }
            }
            else
            {
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    /// Lấy field Name
                    strFieldName = fieldNames[i];

                    /// Set thuộc tính Fix cho các column
                    tree.Columns[strFieldName].OptionsColumn.AllowEdit = bEditColum;
                }
            }
        }

        /// Hàm cho phép Sort hoặc không cho Sort một số cột
        public static void AllowSortColumn(TreeList tree, string[] fieldNames = null, bool bSortColumn = false)
        {
            /// Field Name
            string strFieldName = string.Empty;

            /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
            if (fieldNames == null)
            {
                foreach (TreeListColumn col in tree.Columns)
                {
                    col.OptionsColumn.AllowSort = bSortColumn;
                }
            }
            else
            {
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    /// Lấy field Name
                    strFieldName = fieldNames[i];

                    /// Set thuộc tính Fix cho các column
                    tree.Columns[strFieldName].OptionsColumn.AllowSort = bSortColumn;
                    tree.Columns[strFieldName].SortOrder = System.Windows.Forms.SortOrder.Ascending;
                }
            }
        }

        /// Hàm Set trạng thái Read Only của Cây
        public static void SetReadOnly(TreeList tree, bool bEdit = false)
        {
            tree.OptionsBehavior.Editable = bEdit;
        }

        /// Hàm Set độ rộng của Cây
        public static void SetWidthOfColumn(TreeList tree, string[] fieldNames, int iwidth)
        {
            /// Field Name
            string strFieldName = string.Empty;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                /// Lấy field Name
                strFieldName = fieldNames[i];

                /// Set thuộc tính Fix cho các column
                tree.Columns[strFieldName].Width = iwidth;
            }
        }
        #endregion

        #region Event
        /// Sự kiện vẽ lại cột Indicator
        static void tree_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            CreateNumberRow(sender, e);
        }
        #endregion
    }
}
