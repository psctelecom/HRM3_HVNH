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
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class PMS_CopyHoatDongKhac_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CopyDanhMucHoatDongKhac _source;
        Session ses;
        public PMS_CopyHoatDongKhac_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "DanhMucHoatDongKhac_ListView";
        }

        void PMS_CopyHoatDongKhac_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "VHU")
                popKeKhaiSauGiang.Active["TruyCap"] = true;
            else
                popKeKhaiSauGiang.Active["TruyCap"] = false;

        }
        private void popKeKhaiSauGiang_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            ses = ((XPObjectSpace)_obs).Session;
            _source = new CopyDanhMucHoatDongKhac(ses);
            e.View = Application.CreateDetailView(_obs, _source);
        }

        private void popKeKhaiSauGiang_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {                    
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TuNamHoc", _source.TuNamHoc.Oid);
            param[1] = new SqlParameter("@DenNamHoc", _source.DenNamHoc.Oid);
            DataProvider.ExecuteNonQuery("spd_PMS_CpoyDanhMucHoatDongKhac", System.Data.CommandType.StoredProcedure, param);
            View.ObjectSpace.Refresh();
        }
    }
}