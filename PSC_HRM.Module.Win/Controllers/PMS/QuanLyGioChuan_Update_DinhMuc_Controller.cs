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
using PSC_HRM.Module.PMS.GioChuan;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers.PMS
{  
    public partial class QuanLyGioChuan_Update_DinhMuc_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        QLyGC_Update_DM _source;
        QuanLyGioChuan _qlyGC;
        public QuanLyGioChuan_Update_DinhMuc_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioChuan_DetailView";
        }

        void QuanLyGioChuan_Update_DinhMuc_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong != "DNU")
                btn_Update_DM.Active["TruyCap"] = false;
        }
        //Khởi tạo 1 bảng Popup
        private void btn_Update_DM_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _qlyGC = View.CurrentObject as QuanLyGioChuan;
            if (_qlyGC != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(dsDinhMucChucVu));
                _source = new QLyGC_Update_DM(_ses);
                _source.NamHoc = _qlyGC.NamHoc;
                _source.Load();
                e.View = Application.CreateDetailView(_obs, _source);
            }         
        }
        //Thực thi khi bấm OK 
        private void btn_Update_DM_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_qlyGC != null)
            {
                View.ObjectSpace.CommitChanges();
                using (DialogUtil.AutoWait("Đang cập nhật dữ liệu"))
                {
                    decimal SoGioDinhMuc_NCKH = Convert.ToDecimal(_source.SoGioDinhMuc_NCKH);
                    decimal SoGioDinhMuc_Khac = Convert.ToDecimal(_source.SoGioDinhMuc_Khac);
                    foreach (dsDinhMucChucVu item in _source.listDMucChucVu)
                    {
                        if (item.Chon)
                        {
                            SqlParameter[] param = new SqlParameter[4];
                            param[0] = new SqlParameter("@OidDinhMucChucVu", item.OidDinhMucChucVu);
                            param[1] = new SqlParameter("@OidQLyGioChuan", item.OidQLyGioChuan);
                            param[2] = new SqlParameter("@SoGioDinhMuc_NCKH", SoGioDinhMuc_NCKH);
                            param[3] = new SqlParameter("@SoGioDinhMuc_Khac", SoGioDinhMuc_Khac);
                            
                            DataProvider.ExecuteNonQuery("spd_pms_KeKhaiDinhMucChucVu ", CommandType.StoredProcedure, param);
                        }
                    }
                    View.ObjectSpace.Refresh();
                }
            }
            View.ObjectSpace.Refresh();
        }


    }
}
