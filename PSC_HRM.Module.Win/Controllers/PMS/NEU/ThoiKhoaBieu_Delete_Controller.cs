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
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS;
using System.Data;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Win.Controllers.PMS.NEU
{
    public partial class ThoiKhoaBieu_Delete_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        QuanLyTKB_Delete _source;
        ThoiKhoaBieu_KhoiLuongGiangDay _qlyTKB;
        ThongTinChungPMS _thongtinPMS;
        
        public ThoiKhoaBieu_Delete_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThoiKhoaBieu_KhoiLuongGiangDay_DetailView";
        }
        void ThoiKhoaBieu_Delete_Controller_Activated(object sender, System.EventArgs e)
        {
            //if (TruongConfig.MaTruong != "NEU")
            //    btn_Delete_TKB.Active["TruyCap"] = false;
        }
        //Kh?i t?o 1 b?ng Popup
        private void btn_Delete_TKB_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _qlyTKB = View.CurrentObject as ThoiKhoaBieu_KhoiLuongGiangDay;
            if (_qlyTKB != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(dsChiTietTKB));
                _source = new QuanLyTKB_Delete(_ses);
                _source.OidQuanLyTKB = _qlyTKB.Oid;
                _source.NamHoc = _qlyTKB.NamHoc;
                _source.HocKy = _qlyTKB.HocKy;
                _source.Load();
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }
        //
        private void btn_Delete_TKB_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_qlyTKB != null)
            {
                View.ObjectSpace.CommitChanges();
                using (DialogUtil.AutoWait("?ang xóa d? li?u"))
                {
                    foreach (dsChiTietTKB item in _source.listTKB)
                    {
                        if (item.Chon)
                        {
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@OidTKB_ChiTietKhoiLuongGiangDay", item.OidTKB_ChiTietKhoiLuongGiangDay);
                            param[1] = new SqlParameter("@OidTKB_KhoiLuongGiangDay", item.OidTKB_KhoiLuongGiangDay);
                            
                            DataProvider.ExecuteNonQuery("spd_PMS_Xoa_ChiTietTKB", CommandType.StoredProcedure, param);
                        }
                    }
                    View.ObjectSpace.Refresh();
                }
            }
            View.ObjectSpace.Refresh();
        }
        
    }
}
