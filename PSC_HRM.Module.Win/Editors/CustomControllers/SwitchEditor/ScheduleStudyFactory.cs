using DevExpress.XtraEditors;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PSC_HRM.Module.Win.CustomControllers.SwitchEditor
{
    public class ScheduleStudyFactory
    {
        public static IEditor CreateControl()
        {
            IEditor control;
            string pms = ConfigurationManager.AppSettings.Get("PMS").ToLower();
            if (pms.Equals("true"))
            {
                control = EditorFactory.GetEditor(EditorTypeEnum.GridLookupEditor);
                GridLookUpEdit grid = control.Control as GridLookUpEdit;
                if (grid != null)
                {
                    ScheduleStudyList dataSource = new ScheduleStudyList();
                    grid.Properties.DataSource = dataSource;
                    grid.Properties.DisplayMember = "Alias";
                    grid.Properties.ValueMember = "ID";
                    grid.Properties.NullText = string.Empty;
                    grid.InitGridLookUp(false, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
                    grid.InitPopupFormSize(grid.Width, 200);
                    grid.Properties.View.InitGridView(false, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
                }
            }
            else
                control = EditorFactory.GetEditor(EditorTypeEnum.TextEditor);
            return control;
        }
    }
}
