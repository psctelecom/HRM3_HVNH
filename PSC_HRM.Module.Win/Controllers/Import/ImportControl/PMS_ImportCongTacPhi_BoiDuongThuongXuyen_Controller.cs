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
using PSC_HRM.Module.PMS.NghiepVu.QuanLyBoiDuongThuongXuyen;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportCongTacPhi_BoiDuongThuongXuyen_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportCongTacPhi_BoiDuongThuongXuyen_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyBoiDuongThuongXuyen_DetailView";
        }

        private void btImportThuLao_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                QuanLyBoiDuongThuongXuyen OidQuanLy = View.CurrentObject as QuanLyBoiDuongThuongXuyen;
                if (OidQuanLy != null)
                {
                    _obs = View.ObjectSpace;
                    View.ObjectSpace.CommitChanges();
                    Imp_CongTacPhiBoiDuongThuongXuyen.XuLy(_obs, OidQuanLy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
