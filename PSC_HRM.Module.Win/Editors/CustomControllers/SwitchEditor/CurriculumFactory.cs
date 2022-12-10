using DevExpress.XtraEditors;
using PSC_HRM.Module.Win.CustomControllers.Editor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC_HRM.Module.Win.CustomControllers.SwitchEditor
{
    public class CurriculumFactory
    {
        public static IEditor CreateControl()
        {
            IEditor control;
            string uis = ConfigurationManager.AppSettings.Get("UIS").ToLower();
            if (uis.Equals("true"))
            {
                control = EditorFactory.GetEditor(EditorTypeEnum.GridLookupEditor);
                GridLookUpEdit grid = control.Control as GridLookUpEdit;
                if (grid != null)
                {
                    CurriculumList dataSource = new CurriculumList();
                    grid.Properties.DataSource = dataSource;
                    grid.Properties.DisplayMember = "Name";
                    grid.Properties.ValueMember = "Name";
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
