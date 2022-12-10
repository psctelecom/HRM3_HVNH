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
namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportCacHoatDongKhac_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportCacHoatDongKhac_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHoatDongKhac_DetailView";
        }

        void PMS_ImportCacHoatDongKhac_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "UEL" || TruongConfig.MaTruong == "HUFLIT" || TruongConfig.MaTruong == "NEU")
                btImportHDKhac.Active["TruyCap"] = false;
        }
        private void btImportHDKhac_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //try
            //{
            QuanLyHoatDongKhac OidQuanLy = View.CurrentObject as QuanLyHoatDongKhac;
            if (OidQuanLy != null)
            {
                _obs = View.ObjectSpace;
                View.ObjectSpace.CommitChanges();
                if (TruongConfig.MaTruong == "UFM")
                {
                    Imp_CacHoatDongKhac.XuLy_UFM(_obs, OidQuanLy);
                }
                else
                {
                    Imp_CacHoatDongKhac.XuLy(_obs, OidQuanLy);
                }
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
