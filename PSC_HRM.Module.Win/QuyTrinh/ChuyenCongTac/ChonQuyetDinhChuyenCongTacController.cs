using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class ChonQuyetDinhChuyenCongTacController : BaseController
    {
        private XafApplication _App;
        private IObjectSpace _Obs;

        public ChonQuyetDinhChuyenCongTacController(XafApplication app, IObjectSpace obs)
        {
            InitializeComponent();

            _App = app;
            _Obs = obs;
            unitOfWork = new DevExpress.Xpo.UnitOfWork(((XPObjectSpace)obs).Session.DataLayer);
            listObjects.Session = unitOfWork;
        }

        private void CauHinhThoiViecController_Load(object sender, EventArgs e)
        {
            gridObjects.InitGridLookUp(true, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            gridViewObjects.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewObjects.ShowField(new string[] { "SoQuyetDinh", "NgayHieuLuc", "ThongTinNhanVien.HoTen" }, 
                new string[] { "Số quyết định", "Ngày hiệu lực", "Cán bộ" },
                new int[] { 80, 80, 150 });
        }

        private void gridObjects_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                _Obs = _App.CreateObjectSpace();
                QuyetDinhChuyenCongTac obj = _Obs.CreateObject<QuyetDinhChuyenCongTac>();
                _App.ShowModelView<QuyetDinhChuyenCongTac>(_Obs, obj);

                unitOfWork.DropIdentityMap();
                listObjects.Reload();
                layoutControl1.Invalidate();
            }
        }

        public QuyetDinhChuyenCongTac GetData()
        {
            QuyetDinhChuyenCongTac obj = gridViewObjects.GetFocusedRow() as QuyetDinhChuyenCongTac;
            return obj;
        }
    }
}
