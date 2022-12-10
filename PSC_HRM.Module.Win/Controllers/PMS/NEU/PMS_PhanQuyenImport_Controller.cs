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
using PSC_HRM.Module.PMS.BusinessObjects.DanhMuc;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.Controllers.PMS.NEU
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class PMS_PhanQuyenImport_Controller : ViewController
    {
        IObjectSpace _obs;
        PhanQuyen_PMS_WEB _PhanQuyen;
        Session _ses;
        ChonNhanVien_PhanQuyenPMS _source;
        CollectionSource collectionSource;
        public PMS_PhanQuyenImport_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewId = "PhanQuyen_PMS_WEB_DetailView";
        }
        private void pop_PhanQuyenImport_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            _PhanQuyen = View.CurrentObject as PhanQuyen_PMS_WEB;
            if (_PhanQuyen != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(ChonNhanVien_PhanQuyenPMS));
                _source = new ChonNhanVien_PhanQuyenPMS(_ses);
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }

        private void pop_PhanQuyenImport_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source != null)
            {
                string NhanVien = _source.NhanVien.Oid.ToString();
                string _Import = _source.BoPhanImport;
                string _XacNhan = _source.BoPhanXacNhan;
                string _User = HamDungChung.CurrentUser().UserName.ToString();

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@PhanQuyen_PMS_WEB", _PhanQuyen.Oid);
                param[1] = new SqlParameter("@NhanVien", NhanVien);
                param[2] = new SqlParameter("@Import", _Import != null ? _Import : "");
                param[3] = new SqlParameter("@XacNhan", _XacNhan != null ? _XacNhan : "");
                param[4] = new SqlParameter("@User", _User);
                DataProvider.ExecuteNonQuery("spd_PMS_PhanQuyenImport_XacNhan", System.Data.CommandType.StoredProcedure, param);
                View.ObjectSpace.Refresh();

            }
        }
    }
}
