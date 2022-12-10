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

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_Import_DinhMucChucVu_NhanVienHVNH : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_Import_DinhMucChucVu_NhanVienHVNH()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioChuan_DetailView";
        }

        void PMS_Import_DinhMucChucVu_NhanVienHVNH_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HVNH")//TruongConfig.MaTruong == "UFM" || 
            {
                btImport_DinhMucGioChuan.Active["TruyCap"] = true;
                btImport_GioTruKhac.Active["TruyCap"] = true;
            }
            else
            {
                btImport_DinhMucGioChuan.Active["TruyCap"] = false;
                btImport_GioTruKhac.Active["TruyCap"] = false;
            }
        }
        private void btImport_DinhMucGioChuan_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //try
            //{
            QuanLyGioChuan OidQuanLy = View.CurrentObject as QuanLyGioChuan;
            if (OidQuanLy != null)
            {
                _obs = View.ObjectSpace;
                Imp_DinhMucGioChuan_NhanVien_HVNH.XuLy(_obs, OidQuanLy);
                _obs.Refresh();
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btImport_GioTruKhac_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //try
            //{
            QuanLyGioChuan OidQuanLy = View.CurrentObject as QuanLyGioChuan;
            if (OidQuanLy != null)
            {
                _obs = View.ObjectSpace;
                Imp_DinhMucGioChuan_NhanVien_HVNH.XuLyGioTruKhac(_obs, OidQuanLy);
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