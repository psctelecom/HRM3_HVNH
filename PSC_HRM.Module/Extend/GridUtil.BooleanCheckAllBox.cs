using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
namespace PSC_HRM.Module
{
    public partial class GridUtil
    {  
        public class BooleanCheckAllBox
        {
            #region Static method
            public static BooleanCheckAllBox SetCheckAllBoxToBooleanGridColumn(GridView gridView, DevExpress.XtraGrid.Columns.GridColumn booleanColumn)
            {
                return new BooleanCheckAllBox(gridView, booleanColumn);
            }
            public static BooleanCheckAllBox SetCheckAllBoxToBooleanGridColumn(GridView gridView, DevExpress.XtraGrid.Columns.GridColumn booleanColumn, BooleanCheckAllBox.ColumnChecking delegateFn)
            {
                BooleanCheckAllBox chk = new BooleanCheckAllBox(gridView, booleanColumn);
                chk.ColumnCheckingEvent += delegateFn;
                return chk;
            }
            public static BooleanCheckAllBox SetCheckAllBoxToBooleanGridColumn(GridView gridView, DevExpress.XtraGrid.Columns.GridColumn booleanColumn, DevExpress.Utils.HorzAlignment horzAlignment)
            {
                return new BooleanCheckAllBox(gridView, booleanColumn, horzAlignment);
            }
            public static BooleanCheckAllBox SetCheckAllBoxToBooleanGridColumn(GridView gridView, DevExpress.XtraGrid.Columns.GridColumn booleanColumn, DevExpress.Utils.HorzAlignment horzAlignment, BooleanCheckAllBox.ColumnChecking delegateFn)
            {
                BooleanCheckAllBox chk = new BooleanCheckAllBox(gridView, booleanColumn, horzAlignment);
                chk.ColumnCheckingEvent += delegateFn;
                return chk;
            }



            #endregion
            #region Inner Type
            public delegate void ColumnChecking(object sender, CheckAllColumnCheckingEventArgs e);
            #region CheckAllColumnCheckingEventArgs
            public class CheckAllColumnCheckingEventArgs : EventArgs
            {
                private bool checking;
                private bool allow = true;
                private DevExpress.XtraGrid.Columns.GridColumn gridColumn;
                private int rowHandle;

                public bool Checking
                {
                    get { return checking; }
                }

                public bool Allow
                {
                    get { return allow; }
                    set { allow = value; }
                }

                public DevExpress.XtraGrid.Columns.GridColumn GridColumn
                {
                    get { return gridColumn; }
                }

                public int RowHandle
                {
                    get { return rowHandle; }
                }

                public CheckAllColumnCheckingEventArgs(bool checking, DevExpress.XtraGrid.Columns.GridColumn gridColumn, int rowHandle)
                {
                    this.checking = checking;
                    this.gridColumn = gridColumn;
                    this.rowHandle = rowHandle;
                }
            }

            #endregion
            #endregion
            private DevExpress.XtraGrid.Columns.GridColumn booleanColumn;
            private GridView gridView;
            private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit edit;
            private bool checkAllState = false;
            public event ColumnChecking ColumnCheckingEvent;
            private DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo _checkEditViewInfo;
            public BooleanCheckAllBox(GridView gridView, DevExpress.XtraGrid.Columns.GridColumn booleanColumn, DevExpress.Utils.HorzAlignment horzAlignment)
            {
                SetupColumn(gridView, booleanColumn, horzAlignment);
            }

            public BooleanCheckAllBox(GridView gridView, DevExpress.XtraGrid.Columns.GridColumn booleanColumn)
            {
                SetupColumn(gridView, booleanColumn, DevExpress.Utils.HorzAlignment.Default);
            }

            private void SetupColumn(GridView gridView, DevExpress.XtraGrid.Columns.GridColumn booleanColumn, DevExpress.Utils.HorzAlignment horzAlignment)
            {
                if (booleanColumn.UnboundType == DevExpress.Data.UnboundColumnType.Boolean || booleanColumn.UnboundType == DevExpress.Data.UnboundColumnType.Bound)
                {
                    this.booleanColumn = booleanColumn;
                    this.gridView = gridView;

                    //Create a repository item check edit to use in the column header
                    edit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                    //Set the caption to the same as the column header
                    edit.Caption = " " + booleanColumn.GetCaption();
                    edit.GlyphAlignment = horzAlignment;

                    //Turn off sorting and the column caption
                    booleanColumn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    booleanColumn.OptionsColumn.ShowCaption = false;

                    //Wire up the grid view events
                    gridView.Click += new EventHandler(gridView_Click);
                    gridView.CustomDrawColumnHeader += new ColumnHeaderCustomDrawEventHandler(gridView_CustomDrawColumnHeader);
                }
            }

            protected void DrawCheckBox(System.Drawing.Graphics g, DevExpress.Utils.AppearanceObject a, System.Drawing.Rectangle r, bool Checked)
            {

                DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
                DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
                _checkEditViewInfo = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
                painter = edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
                _checkEditViewInfo.EditValue = Checked;
                _checkEditViewInfo.Bounds = r;
                //Manually set the forecolor and font
                _checkEditViewInfo.PaintAppearance.ForeColor = a.ForeColor;
                _checkEditViewInfo.PaintAppearance.Font = a.Font;
                _checkEditViewInfo.CalcViewInfo(g);
                args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(_checkEditViewInfo, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
                painter.Draw(args);
                args.Cache.Dispose();
            }

            void gridView_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
            {
                //If drawing the boolean column header, draw the checkbox
                if (object.ReferenceEquals(e.Column, booleanColumn))
                {
                    e.Info.InnerElements.Clear();
                    e.Painter.DrawObject(e.Info);
                    DrawCheckBox(e.Graphics, e.Appearance, e.Bounds, checkAllState);
                    e.Handled = true;
                }
            }

            void gridView_Click(object sender, EventArgs e)
            {

                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo;
                System.Drawing.Point pt = gridView.GridControl.PointToClient(Control.MousePosition);
                hitInfo = gridView.CalcHitInfo(pt);

                if (hitInfo.InColumn && Object.ReferenceEquals(hitInfo.Column, booleanColumn))
                {
                    int toaDoXCuaCheckBox = _checkEditViewInfo.CheckInfo.GlyphRect.X;
                    int checkBoxWidth = _checkEditViewInfo.CheckInfo.GlyphRect.Width;
                    int checkBoxHeight = _checkEditViewInfo.CheckInfo.GlyphRect.Height;
                    int toaDoYCuaCheckBox = _checkEditViewInfo.CheckInfo.GlyphRect.Y;
                    if (pt.X >= toaDoXCuaCheckBox && pt.X <= toaDoXCuaCheckBox + checkBoxWidth)
                    {
                        if (pt.Y >= toaDoYCuaCheckBox && pt.Y <= toaDoYCuaCheckBox + checkBoxHeight)
                        {

                            if (checkAllState)
                            {
                                CheckNone();
                                checkAllState = false;
                            }
                            else
                            {
                                CheckAll();
                                checkAllState = true;
                            }
                        }
                    }

                }
            }

            private void CheckNone()
            {
                Cursor cursor = gridView.GridControl.Cursor;
                gridView.GridControl.Cursor = Cursors.WaitCursor;

                try
                {
                    int[] selectedRows = gridView.GetSelectedRows();
                    gridView.BeginDataUpdate();
                    if (selectedRows.Length < 2)
                    {
                        //Uncheck all rows
                        for (int i = 0; i < gridView.RowCount; i++)
                        {

                            CheckAllColumnCheckingEventArgs e = new CheckAllColumnCheckingEventArgs(false, booleanColumn, i);
                            if (ColumnCheckingEvent != null)
                            {
                                ColumnCheckingEvent(this, e);
                            }
                            if (e.Allow)
                            {
                                gridView.SetRowCellValue(i, booleanColumn, false);
                            }
                        }
                    }
                    else
                    {
                        //Uncheck item which is choosed
                        for (int i = 0; i < gridView.RowCount; i++)
                        {
                            for (int j = 0; j < selectedRows.Length; j++)
                            {
                                if (selectedRows[j] == i)
                                {
                                    CheckAllColumnCheckingEventArgs e = new CheckAllColumnCheckingEventArgs(false, booleanColumn, i);
                                    if (ColumnCheckingEvent != null)
                                    {
                                        ColumnCheckingEvent(this, e);
                                    }
                                    if (e.Allow)
                                    {
                                        gridView.SetRowCellValue(i, booleanColumn, false);
                                    }
                                }
                            }
                        }
                    }
                    gridView.EndDataUpdate();
                }
                finally
                {
                    gridView.GridControl.Cursor = cursor;
                }
            }

            private void CheckAll()
            {
                Cursor cursor = gridView.GridControl.Cursor;
                gridView.GridControl.Cursor = Cursors.WaitCursor;

                try
                {
                    int[] selectedRows = gridView.GetSelectedRows();
                    gridView.BeginDataUpdate();

                    if (selectedRows.Length < 2)
                    {
                        //Check all rows
                        for (int i = 0; i < gridView.RowCount; i++)
                        {
                            CheckAllColumnCheckingEventArgs e = new CheckAllColumnCheckingEventArgs(false, booleanColumn, i);
                            if (ColumnCheckingEvent != null)
                            {
                                ColumnCheckingEvent(this, e);
                            }
                            if (e.Allow)
                            {
                                gridView.SetRowCellValue(i, booleanColumn, true);
                            }
                        }
                    }
                    else
                    {
                        //Check item which is choosed
                        for (int i = 0; i < gridView.RowCount; i++)
                        {
                            for (int j = 0; j < selectedRows.Length; j++)
                            {
                                if (selectedRows[j] == i)
                                {

                                    CheckAllColumnCheckingEventArgs e = new CheckAllColumnCheckingEventArgs(false, booleanColumn, i);
                                    if (ColumnCheckingEvent != null)
                                    {
                                        ColumnCheckingEvent(this, e);
                                    }
                                    if (e.Allow)
                                    {
                                        gridView.SetRowCellValue(i, booleanColumn, true);
                                    }
                                }
                            }
                        }
                    }
                    gridView.EndDataUpdate();
                }
                finally
                {
                    gridView.GridControl.Cursor = cursor;
                }
            }
        }



    }
}
