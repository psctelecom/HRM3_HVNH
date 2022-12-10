using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class ChonDuLieuController<T> : BaseController where T : BaseObject
    {
        private XPCollection<T> _DataSource;
        private XafApplication _App;
        private IObjectSpace _Obs;
        private string[] _Properties;
        private string[] _Captions;
        private int[] _Widths;

        public ChonDuLieuController(XafApplication app, IObjectSpace obs, string text, CriteriaOperator filter, string[] properties, string[] captions, int[] widths)
        {
            InitializeComponent();

            _App = app;
            _Obs = obs;
            layoutControlItem2.Text = text;
            _DataSource = new XPCollection<T>(((XPObjectSpace)obs).Session, filter);
            gridObjects.Properties.DataSource = _DataSource;
            _Properties = properties;
            _Captions = captions;
            _Widths = widths;
        }

        public ChonDuLieuController(string text, IEnumerable<T> dataSource, string[] properties, string[] captions, int[] widths)
        {
            InitializeComponent();

            layoutControlItem2.Text = text;
            gridObjects.Properties.DataSource = dataSource;
            _Properties = properties;
            _Captions = captions;
            _Widths = widths;
            gridObjects.Properties.Buttons[1].Visible = false;
        }

        private void CauHinhThoiViecController_Load(object sender, EventArgs e)
        {
            gridObjects.InitGridLookUp(true, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            gridViewObjects.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewObjects.ShowField(_Properties, _Captions, _Widths);
            gridObjects.InitPopupFormSize(gridObjects.Width, 300);
            gridObjects.Properties.DisplayMember = _Properties[0];
            gridObjects.Properties.ValueMember = "This";
        }

        private void gridObjects_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                _Obs = _App.CreateObjectSpace();
                T obj = _Obs.CreateObject<T>();
                _App.ShowModelView<T>(_Obs, obj);

                _Obs.ReloadCollection(_DataSource);
                layoutControl1.Invalidate();
            }
        }

        public T GetData()
        {
            T obj = gridViewObjects.GetFocusedRow() as T;
            return obj;
        }

        private void gridObjects_Resize(object sender, EventArgs e)
        {
            gridObjects.InitPopupFormSize(gridObjects.Width, 300);
        }
    }
}
