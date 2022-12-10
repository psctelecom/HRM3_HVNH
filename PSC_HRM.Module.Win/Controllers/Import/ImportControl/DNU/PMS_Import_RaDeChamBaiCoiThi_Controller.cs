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
using PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc;
using DevExpress.XtraEditors;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_Import_RaDeChamBaiCoiThi_Controller : ViewController
    {
        QuanLyKhaoThi qly;
        IObjectSpace _obs = null;
        public PMS_Import_RaDeChamBaiCoiThi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKhaoThi_DetailView";
        }

        void PMS_Import_RaDeChamBaiCoiThi_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "DNU" || TruongConfig.MaTruong == "HUFLIT")
                btImportChamBaiCoiThi.Active["TruyCap"] = false;
        }
        private void btImportChamBaiCoiThi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            qly = View.CurrentObject as QuanLyKhaoThi;
            using (DialogUtil.AutoWait("Đang import dữ liệu chấm bài, coi thi, ra đề!"))
            {
                _obs = View.ObjectSpace;
                Imp_ChamBaiCoiThiRaDe.XuLy(_obs, qly);
                _obs.Refresh();
            }
        }
    }
}
