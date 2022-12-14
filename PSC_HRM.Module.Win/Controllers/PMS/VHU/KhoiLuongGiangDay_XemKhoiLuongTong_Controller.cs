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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KhoiLuongGiangDay_XemKhoiLuongTong_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CollectionSource collectionSource;
        DanhSachDuLieu_KhoiLuongGiangDay_Non _source;
        KhoiLuongGiangDay _TKB;
        Session ses;
        public KhoiLuongGiangDay_XemKhoiLuongTong_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_ListView";
        }

        void KhoiLuongGiangDay_XemKhoiLuongTong_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "VHU")
                popDongBoTK.Active["TruyCap"] = true;
            else
                popDongBoTK.Active["TruyCap"] = false;
        }

        private void KhoiLuongGiangDay_XemKhoiLuongTong_Controller_ViewControlsCreated(object sender, System.EventArgs e)
        {
            
        }


        private void popDongBoTK_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _TKB = View.CurrentObject as KhoiLuongGiangDay;
            using (DialogUtil.AutoWait("Đang load danh sách dữ liệu"))
            {
                _obs = Application.CreateObjectSpace();
                ses = ((XPObjectSpace)_obs).Session;
                //collectionSource = new CollectionSource(_obs, typeof(dsThongTinNhanVien));


                //collectionSource = new CollectionSource(_obs, typeof(ChonNhanVien));
                _source = new DanhSachDuLieu_KhoiLuongGiangDay_Non(ses);
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }
        private void popDongBoTK_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
        }
    }
}