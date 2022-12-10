using DevExpress.ExpressApp;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module
{
    public partial class DetailUtil
    {
        /// Enum Format Column
        public enum ColumnFormat { NULL, Date, DateTime, N0, N1, N2, N3, N4 }

        #region Process

        /// Hàm cho phép chỉnh sửa định dạng ngày tháng năm
        public static void FormatItemMonthYear(DetailView dtv, string[] itemNames)
        {
            /// Item Name
            string strItemName = string.Empty;

            for (int i = 0; i < itemNames.Length; i++)
            {
                /// Lấy item Name
                strItemName = itemNames[i];

                /// Set thuộc tính Fix cho các item
                (((DevExpress.XtraEditors.DateEdit)(dtv.FindItem(strItemName).Control)).Properties).EditMask = "MM/yyyy";
                (((DevExpress.XtraEditors.DateEdit)(dtv.FindItem(strItemName).Control)).Properties).DisplayFormat.FormatString = "MM/yyyy";
            }
        }

        /// Hàm cho phép chỉnh sửa caption thuộc tính
        public static void FormatCaption(DetailView dtv, string itemNames,string caption)
        {
            /// Set thuộc tính Fix cho các item
            if (dtv.FindItem(itemNames)!=null)
            dtv.FindItem(itemNames).Caption = caption;
        }

        #endregion
    }
}
