using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace PSC_HRM.Module
{
    public static class LookupEditHelper
    {
        /// <summary>
        /// Init grid lookup edit
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="autoFilter"></param>
        /// <param name="autoPopup"></param>
        /// <param name="textEditStyle"></param>
        public static void InitGridLookUp(LookUpEdit lookup, bool autoPopup, TextEditStyles textEditStyle)
        {
            //Show filter
            lookup.Properties.TextEditStyle = textEditStyle;
            lookup.Properties.ImmediatePopup = autoPopup;
            //grid.Properties.BestFitMode = BestFitMode.BestFit;
            for (int i = 0; i < lookup.Properties.Columns.Count; i++)
                lookup.Properties.Columns[i].Visible = false;
        }

        /// <summary>
        /// Show field with caption, width
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(LookUpEdit lookup, string[] fieldName, string[] caption)
        {
            for (int i = 0; i < fieldName.Length; i++)
            {
                lookup.Properties.Columns[fieldName[i]].Visible = true;
                lookup.Properties.Columns[fieldName[i]].Caption = caption[i];
            }
        }

        /// <summary>
        /// Show field with caption, width
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(LookUpEdit lookup, string[] fieldName, string[] caption, int[] width)
        {
            for (int i = 0; i < fieldName.Length; i++)
            {
                lookup.Properties.Columns[fieldName[i]].Visible = true;
                lookup.Properties.Columns[fieldName[i]].Caption = caption[i];
                lookup.Properties.Columns[fieldName[i]].Width = width[i];
            }
        }
        /// <summary>
        /// Init grid lookup edit
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="autoFilter"></param>
        /// <param name="autoPopup"></param>
        /// <param name="textEditStyle"></param>
        public static void InitGridLookUp(RepositoryItemLookUpEdit lookup, bool autoPopup, TextEditStyles textEditStyle)
        {
            //Show filter
            lookup.TextEditStyle = textEditStyle;
            lookup.ImmediatePopup = autoPopup;
            //grid.Properties.BestFitMode = BestFitMode.BestFit;
            for (int i = 0; i < lookup.Columns.Count; i++)
                lookup.Columns[i].Visible = false;
        }

        /// <summary>
        /// Show field with caption, width
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(RepositoryItemLookUpEdit lookup, string[] fieldName, string[] caption)
        {
            for (int i = 0; i < fieldName.Length; i++)
            {
                lookup.Columns[fieldName[i]].Visible = true;
                lookup.Columns[fieldName[i]].Caption = caption[i];
            }
        }

        /// <summary>
        /// Show field with caption, width
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(RepositoryItemLookUpEdit lookup, string[] fieldName, string[] caption, int[] width)
        {
            for (int i = 0; i < fieldName.Length; i++)
            {
                lookup.Columns[fieldName[i]].Visible = true;
                lookup.Columns[fieldName[i]].Caption = caption[i];
                lookup.Columns[fieldName[i]].Width = width[i];
            }
        }
    }
}
