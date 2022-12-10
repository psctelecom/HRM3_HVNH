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
using PSC_HRM.Module.PMS.NonPersistent;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_Import_KeKhai_HDKhac_TKB : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        KeKhai_CacHoatDong_ThoiKhoaBieu KeKhai;
        public PMS_Import_KeKhai_HDKhac_TKB()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KeKhai_CacHoatDong_ThoiKhoaBieu_DetailView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        void PMS_Import_KeKhai_HDKhac_TKB_Activated(object sender, System.EventArgs e)
        {
            //if (HamDungChung.CurrentUser().UserName != "psc")
                btImport.Active["TruyCap"] = false;
        }
        private void btImport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            KeKhai = View.CurrentObject as KeKhai_CacHoatDong_ThoiKhoaBieu;
            
            using (DialogUtil.AutoWait("Đang import kê khai" ))
            {
                _obs = View.ObjectSpace;
                Import_KeKhai_HDKhac_TKB.XuLy(_obs, KeKhai.Oid);
                _obs.Refresh();
            }
        }
    }
}