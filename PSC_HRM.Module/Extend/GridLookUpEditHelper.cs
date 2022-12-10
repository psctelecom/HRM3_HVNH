using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace PSC_HRM.Module
{
    public static class GridLookUpEditHelper
    {
        /// <summary>
        /// Init grid lookup edit
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="autoFilter"></param>
        /// <param name="autoPopup"></param>
        /// <param name="textEditStyle"></param>
        public static void InitGridLookUp(this GridLookUpEdit grid, bool autoFilter, bool autoPopup, TextEditStyles textEditStyle)
        {
            //Show filter
            grid.Properties.View.OptionsView.ShowAutoFilterRow = autoFilter;
            grid.Properties.TextEditStyle = textEditStyle;
            grid.Properties.ImmediatePopup = autoPopup;
            grid.Properties.PopupFilterMode = PopupFilterMode.Contains;
            //grid.Properties.BestFitMode = BestFitMode.BestFit;
            for (int i = 0; i < grid.Properties.View.Columns.Count; i++)
                grid.Properties.View.Columns[i].Visible = false;
        }

        /// <summary>
        /// Init popup form
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void InitPopupFormSize(this GridLookUpEdit grid, int width, int height)
        {
            grid.Properties.PopupFormSize = new Size(width, height);
        }
    }
}
