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
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.GioChuan;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class ChonBoPhan_SoGio_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        ChonBoPhan_SoGio _source;
        QuanLyGioChuan _qlyGC;
        public ChonBoPhan_SoGio_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioChuan_DetailView";
        }
        void ChonBoPhan_SoGio_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "UFM")
                btnChonBoPhan_SoGio.Active["TruyCap"] = true;
            else
                btnChonBoPhan_SoGio.Active["TruyCap"] = false;
        }

        private void btnChonBoPhan_SoGio_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _qlyGC = View.CurrentObject as QuanLyGioChuan;
            if (_qlyGC != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(ChonBoPhan_SoGio));

                _source = new ChonBoPhan_SoGio(_ses);
                e.View = Application.CreateDetailView(_obs, _source);
            }         
        }

        private void btnChonBoPhan_SoGio_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            string dsBoPhan = _source.BoPhan.ToString();
            decimal soGioDM = _source.SoGioDinhMuc;
            _qlyGC = View.CurrentObject as QuanLyGioChuan;
            if (_qlyGC != null)
            {
                using (DialogUtil.AutoWait("Đang lấy dữ liệu bảng NCKH !"))
                {
                    SqlParameter[] pLayDuLieu = new SqlParameter[3];
                    pLayDuLieu[0] = new SqlParameter("@QuanLyGioChuan", _qlyGC.Oid);
                    pLayDuLieu[1] = new SqlParameter("@DsBoPhan", dsBoPhan);
                    pLayDuLieu[2] = new SqlParameter("@GioDinhMuc", soGioDM);
                    DataProvider.ExecuteNonQuery("spd_PMS_LayDuLieuBoPhan", CommandType.StoredProcedure, pLayDuLieu);

                    View.ObjectSpace.Refresh();//Load lại view nhìn
                }
            }
        }
    }
}
