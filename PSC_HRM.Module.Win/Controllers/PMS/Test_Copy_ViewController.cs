using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Test_Copy_ViewController : ViewController
    {
        public Test_Copy_ViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewId = "ChiTietKhoiLuongGiangDay_ListView";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            View.ControlsCreated += View_ControlsCreated;
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            if (View.Id == "ChiTietKhoiLuongGiangDay_ListView")
            {
                GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
                if (listEditor != null)
                {
                    listEditor.GridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
                }
            }  
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            if (View.Id == "ChiTietKhoiLuongGiangDay_ListView")
            {
                GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
                if (listEditor != null)
                {
                    listEditor.GridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
                }
            }  
        }
        #region Paste Rows
        private void PasteRowAction_Execute(object sender, SimpleActionExecuteEventArgs e)  
        {  
            string[][] copiedValues = ClipboardDataToJaggedArray();  
            if (copiedValues == null) return;  
            PasteRowValues(copiedValues);  
        }  
  
        private void PasteRowValues(string[][] copiedValues)  
        {  
            GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;  
  
            if (listEditor != null)  
            {  
                XafGridView gridView = listEditor.GridView;  
                Session session = ((XPObjectSpace)ObjectSpace).Session;  
  
                // paste rows  
                if ((gridView.IsNewItemRow(gridView.FocusedRowHandle)))  
                {  
                    // note that the new row must be focused for this to work  
                    PasteValuesToNewRows(copiedValues, gridView, session, gridView.FocusedRowHandle);  
                }  
                else  
                {  
                    // paste to selected rows  
                    int[] selectedRowHandles = gridView.GetSelectedRows();  
                    PasteValuesToExistingRows(copiedValues, gridView, session, selectedRowHandles);  
                }  
            }  
        }  
  
        private void PasteValuesToExistingRows(string[][] copiedValues, XafGridView gridView, Session session, int[] selectedRowHandles)  
        {  
            string keyName = GetKeyName();  
            int focusedRowHandle = gridView.FocusedRowHandle;  
  
            // paste rows  
            for (int r = 0; r < copiedValues.Length; r++)  
            {  
                // ignore row with empty string  
                if (copiedValues[r].Length == 1 && string.IsNullOrWhiteSpace(copiedValues[r][0]))  
                    continue;  
  
                // get next selected row  
                if (r < selectedRowHandles.Length)  
                    focusedRowHandle = selectedRowHandles[r];  
                else  
                    return;  
  
                // paste cells  
                PasteColumnsToRow(copiedValues[r], gridView, session, focusedRowHandle, keyName);  
                gridView.UpdateCurrentRow();  
            }  
        }  
  
        // newRowHandle: must be new row handle.  
        private void PasteValuesToNewRows(string[][] copiedValues, XafGridView gridView, Session session, int newRowHandle)  
        {  
            string keyName = GetKeyName();  
  
            // paste rows  
            for (int r = 0; r < copiedValues.Length; r++)  
            {  
                // ignore row with empty string  
                if (copiedValues[r].Length == 1 && string.IsNullOrWhiteSpace(copiedValues[r][0]))  
                    continue;  
  
                // add new row in gridview  
                gridView.FocusedRowHandle = newRowHandle;  
                gridView.AddNewRow();  
  
                // paste cells  
                PasteColumnsToRow(copiedValues[r], gridView, session, newRowHandle, keyName);  
                gridView.UpdateCurrentRow();  
            }  
        }  
  
        private void PasteColumnsToRow(string[] copiedRowValues, XafGridView gridView, Session session, int focusedRowHandle, string keyName)  
        {  
            for (int c = 0; c < gridView.Columns.Count && c < copiedRowValues.Length; c++)  
            {  
                var gridViewColumn = (XafGridColumn)gridView.Columns[c];  
  
                // skip non-editable column  
                bool allowEdit = gridViewColumn.Model.AllowEdit;  
                if (!allowEdit)  
                    continue;  
  
                // skip Key  
                if (gridViewColumn.PropertyName == keyName)  
                    continue;  
  
                string lookupProperty = gridViewColumn.Model.LookupProperty;  
                object pasteValue;  
                // set object to paste based on lookup property  
                if (string.IsNullOrEmpty(lookupProperty))  
                    pasteValue = copiedRowValues[c];  
                else  
                    pasteValue = session.FindObject(gridView.Columns[c].ColumnType,  
                        CriteriaOperator.Parse(string.Format("{0} LIKE '{1}'", lookupProperty, copiedRowValues[c])));  
                // paste object to new cell  
                gridView.SetRowCellValue(focusedRowHandle, gridView.Columns[c], pasteValue);  
            }  
        }  
  
        #endregion  
  
        #region Paste Cells  
  
        // ensure that you set the below:  
        // gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;  
        private void PasteCellValueAction_Execute(object sender, SimpleActionExecuteEventArgs e)  
        {  
            string[] copiedValues = ClipboardDataColumnToArray(0);  
            if (copiedValues == null) return;  
            PasteCellValues(copiedValues);  
        }  
  
        // paste single value into selected rows in the focused column  
        // if the property editor is a lookup, then a lookup is performed on the paste value  
        private void PasteCellValues(string copiedValue)  
        {  
            string[] copiedValues = new string[1];  
            copiedValues[0] = copiedValue;  
            PasteCellValues(copiedValues);  
        }  
        private void PasteCellValues(string[] copiedValues)  
        {  
            GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;  
  
            if (listEditor != null)  
            {  
                XafGridView gridView = listEditor.GridView;  
                bool allowEdit = ((XafGridColumn)gridView.FocusedColumn).Model.AllowEdit;  
                if (!allowEdit)  
                    throw new InvalidOperationException("Column '" + gridView.FocusedColumn.Caption + "' is not editable.");  
  
                Session session = ((XPObjectSpace)ObjectSpace).Session;  
  
                string lookupProperty = ((XafGridColumn)gridView.FocusedColumn).Model.LookupProperty;  
                int[] selectedRows = gridView.GetSelectedRows();  
  
                int copyIndex = 0;  
                foreach (int rowHandle in selectedRows)  
                {  
                    // set object to paste based on lookup property  
                    string copiedValue = copiedValues[copyIndex];  
                    object pasteValue;  
                    if (string.IsNullOrEmpty(lookupProperty))  
                        pasteValue = copiedValue;  
                    else  
                        pasteValue = session.FindObject(gridView.FocusedColumn.ColumnType,  
                            CriteriaOperator.Parse(string.Format("{0} LIKE '{1}'", lookupProperty, copiedValue)));  
  
                    // paste object to focused cell  
                    gridView.SetRowCellValue(rowHandle, gridView.FocusedColumn, pasteValue);  
  
                    // go to next copied value  
                    copyIndex++;  
                    // reset copied row counter to the beginning after the last row is reached  
                    if (copyIndex > copiedValues.Length - 1)  
                        copyIndex = 0;  
                }  
            }  
        }  
  
        #endregion  
  
        #region XAF  
  
        public string GetKeyName()  
        {  
            return View.Model.AsObjectView.ModelClass.KeyProperty;  
        }  
  
        #endregion  
  
        #region Clipboard  
        // split tab-delimited string into array  
        string[] GetRowData(string data)  
        {  
            string[] rowData = data.Split(new char[] { '\r', '\x09' });  
            return rowData;  
        }  
        string ClipboardData  
        {  
            get  
            {  
                System.Windows.Forms.IDataObject iData = System.Windows.Forms.Clipboard.GetDataObject();  
                if (iData == null) return "";  
  
                if (iData.GetDataPresent(System.Windows.Forms.DataFormats.Text))  
                    return (string)iData.GetData(System.Windows.Forms.DataFormats.Text);  
                return "";  
            }  
            set { System.Windows.Forms.Clipboard.SetDataObject(value); }  
        }  
        private string[] ClipboardDataColumnToArray(int column = 0)  
        {  
            string[][] data = ClipboardDataToJaggedArray();  
            if (data == null) return null;  
            string[] result = new string[data.Length];  
            for (int i = 0; i < result.Length; i++)  
            {  
                result[i] = data[i][column];  
            }  
            return result;  
        }  
  
        private string[][] ClipboardDataToJaggedArray()  
        {  
            string[] data = ClipboardData.Split('\n');  
            if (data.Length < 1) return null;  
            string[][] parsed = new string[data.Length][];  
            for (int i = 0; i < parsed.Length; i++)  
            {  
                parsed[i] = GetRowData(data[i]);  
            }  
            return parsed;  
        }  
        #endregion  
    }  
}
