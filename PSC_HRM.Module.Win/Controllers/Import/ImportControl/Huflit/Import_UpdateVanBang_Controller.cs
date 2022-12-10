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
using PSC_HRM.Module.Win.Controllers.Import.ImportClass;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class Import_UpdateVanBang_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public Import_UpdateVanBang_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThongTinNhanVien_RutGon_ListView";
        }

        void Import_UpdateVanBang_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong != "HUFLIT")
                btImport_UpdateVanBang.Active["TruyCap"] = false;
        }
        private void btImport_UpdateVanBang_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            View.ObjectSpace.CommitChanges();
            Import_UpdateVanBang.XuLy(_obs);
            _obs.Refresh();
        }
    }
}
