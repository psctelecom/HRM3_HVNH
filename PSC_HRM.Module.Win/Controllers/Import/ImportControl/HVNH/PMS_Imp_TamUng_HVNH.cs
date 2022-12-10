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
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_Imp_TamUng_HVNH : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_Imp_TamUng_HVNH()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioGiang_DetailView";
        }

        void PMS_Imp_TamUng_HVNH_Activated(object sender, System.EventArgs e)
        {

        }
        private void btImport_DinhMucGioChuan_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //try
            //{
            QuanLyGioGiang OidQuanLy = View.CurrentObject as QuanLyGioGiang;
            if (OidQuanLy != null)
            {
                _obs = View.ObjectSpace;
                Imp_TamUng_HVNH.XuLy(_obs, OidQuanLy);             
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