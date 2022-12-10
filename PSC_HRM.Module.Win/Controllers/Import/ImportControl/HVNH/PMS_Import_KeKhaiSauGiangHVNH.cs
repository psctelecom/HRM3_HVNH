using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.ChamCong;
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using ERP.Module.Win.Controllers.Import.ImportClass;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using PSC_HRM.Module.PMS.NghiepVu.PhiGiaoVu;
using PSC_HRM.Module.PMS.GioChuan;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_Import_KeKhaiSauGiangHVNH : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_Import_KeKhaiSauGiangHVNH()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKeKhaiSauGiang_DetailView";
        }

        void PMS_Import_KeKhaiSauGiangHVNH_Activated(object sender, System.EventArgs e)
        {

        }
        private void btImport_DinhMucGioChuan_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //try
            //{
            QuanLyKeKhaiSauGiang OidQuanLy = View.CurrentObject as QuanLyKeKhaiSauGiang;
            if (OidQuanLy != null)
            {
                _obs = View.ObjectSpace;

                Imp_KheKhaiSauGiang_HVNH.XuLy(_obs, OidQuanLy);
                SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                param[0] = new SqlParameter("@KeKhaiSauGiang", OidQuanLy.Oid);
                DataProvider.ExecuteNonQuery("spd_PMS_QuyDoi_KeKhaiSauGiang", System.Data.CommandType.StoredProcedure, param);
                _obs.Refresh();
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}