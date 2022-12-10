using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Controllers.NangPhuCap
{
    public partial class NangPhuCap_TimKiemQuyetDinhNangPhuCapThamNienController : ViewController
    {
        private IObjectSpace _obs;
        QuyetDinh_TimNhanVien _view;

        public NangPhuCap_TimKiemQuyetDinhNangPhuCapThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NangPhuCap_TimKiemQuyetDinhNangPhuCapThamNienController");
        }
        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();

            _view = _obs.CreateObject<QuyetDinh_TimNhanVien>();
            //
            e.View = Application.CreateDetailView(_obs, _view);
        }

        private void NangPhuCap_TimKiemQuyetDinhNangPhuCapThamNienController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangPhuCapThamNienNhaGiao>();
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_view != null && _view.ThongTinNhanVien != null)
            {
                ListView listView = View as ListView;
                CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[ThongTinNhanVien.Oid=?]", _view.ThongTinNhanVien.Oid);
                listView.CollectionSource.Criteria["Filter"] = filter;
            }
            else
            {
                ListView listView = View as ListView;
                listView.CollectionSource.Criteria["Filter"] = null;
            }


        }
    }
}
