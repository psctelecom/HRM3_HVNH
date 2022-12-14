using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.NonPersistentObjects.NEU;
using PSC_HRM.Module.PMS.NonPersistentObjects.HUFLIT;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KhoiLuongGiangDay_XemDuLieuPhanCongTongHop_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        QuanLyXemPhanCongTongHop_HUFLIT _source;
        KhoiLuongGiangDay _TKB;
        Session ses;
        public KhoiLuongGiangDay_XemDuLieuPhanCongTongHop_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_DetailView";
        }

        void KhoiLuongGiangDay_XemDuLieuPhanCongTongHop_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HUFLIT")
                popDongBoTK.Active["TruyCap"] = true;
            else
                popDongBoTK.Active["TruyCap"] = false;
        }


        private void popDongBoTK_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _TKB = View.CurrentObject as KhoiLuongGiangDay;
            if (_TKB != null)
            {
                using (DialogUtil.AutoWait("Đang kiểm tra dữ liệu"))
                {
                    _obs = Application.CreateObjectSpace();
                    ses = ((XPObjectSpace)_obs).Session;

                    if (_TKB != null)
                    {
                        _source = new QuanLyXemPhanCongTongHop_HUFLIT(ses);
                        _source.LoadDuLieu(_TKB.NamHoc, _TKB.HocKy);
                        e.View = Application.CreateDetailView(_obs, _source);
                    }
                }
            }
        }
        private void popDongBoTK_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
           
        }
    }
}