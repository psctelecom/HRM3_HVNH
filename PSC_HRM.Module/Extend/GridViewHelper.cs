using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;

namespace PSC_HRM.Module
{
    public static class GridViewHelper
    {
        /// <summary>
        /// Init GridView
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="autoFilter"></param>
        /// <param name="multiSelect"></param>
        /// <param name="selectMode"></param>
        /// <param name="detailButton"></param>
        /// <param name="groupPanel"></param>
        public static void InitGridView(this GridView grid, bool autoFilter, bool multiSelect, GridMultiSelectMode selectMode, bool detailButton, bool groupPanel)
        {
            //Show filter
            grid.OptionsView.ShowAutoFilterRow = autoFilter;
            //Show multi select
            grid.OptionsSelection.MultiSelect = multiSelect;
            //Show multi select mode
            grid.OptionsSelection.MultiSelectMode = selectMode;
            //Show detail button
            grid.OptionsView.ShowDetailButtons = detailButton;
            grid.OptionsView.ShowChildrenInGroupPanel = detailButton;
            grid.OptionsDetail.ShowDetailTabs = detailButton;
            //Show group panel
            grid.OptionsView.ShowGroupPanel = groupPanel;

            for (int i = 0; i < grid.Columns.Count; i++)
                grid.Columns[i].Visible = false;
        }

        /// <summary>
        /// Show field with caption, width
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(this GridView grid, string[] fieldName, string[] caption)
        {
            grid.OptionsView.ColumnAutoWidth = true;
            for (int i = 0; i < fieldName.Length; i++)
            {
                grid.Columns.AddField(fieldName[i]);
                grid.Columns[fieldName[i]].Visible = true;
                grid.Columns[fieldName[i]].Caption = caption[i];
                grid.Columns[fieldName[i]].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            }
        }

        /// <summary>
        /// Show field with caption, width
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(this GridView grid, string[] fieldName, string[] caption, int[] width)
        {
            grid.OptionsView.ColumnAutoWidth = false;
            for (int i = 0; i < fieldName.Length; i++)
            {
                grid.Columns.AddField(fieldName[i]);
                grid.Columns[fieldName[i]].Visible = true;
                grid.Columns[fieldName[i]].Caption = caption[i];
                grid.Columns[fieldName[i]].Width = width[i];
                grid.Columns[fieldName[i]].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            }
        }

        /// <summary>
        /// Readonly all columns
        /// </summary>
        /// <param name="grid"></param>
        public static void ReadOnlyGridView(this GridView grid)
        {
            grid.OptionsBehavior.Editable = false;
        }

        /// <summary>
        /// Readonly field
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        public static void ReadOnlyColumns(this GridView grid, params string[] fieldName)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].OptionsColumn.AllowEdit = false;
        }

        /// <summary>
        /// Sort field
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="sortOrder"></param>
        /// <param name="fieldNames"></param>
        public static void SortField(this GridView gridView, ColumnSortOrder sortOrder, params string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
            {
                gridView.Columns[fieldNames[i]].SortIndex = i;
                gridView.Columns[fieldNames[i]].SortOrder = sortOrder;
            }
        }

        /// <summary>
        /// Group field
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="fieldNames"></param>
        public static void GroupField(this GridView gridView, params string[] fieldNames)
        {
            gridView.OptionsBehavior.AutoExpandAllGroups = true;
            for (int i = 0; i < fieldNames.Length; i++)
            {
                gridView.Columns[fieldNames[i]].GroupIndex = i;
            }
        }

        /// <summary>
        /// Display format grid column
        /// </summary>
        /// <param name="property"></param>
        /// <param name="column"></param>
        public static void DisplayFormat(this GridView gridView, string columnName, DevExpress.Utils.FormatType type, string format)
        {
            gridView.Columns[columnName].DisplayFormat.FormatType = type;
            gridView.Columns[columnName].DisplayFormat.FormatString = format;
        }

    }
}
