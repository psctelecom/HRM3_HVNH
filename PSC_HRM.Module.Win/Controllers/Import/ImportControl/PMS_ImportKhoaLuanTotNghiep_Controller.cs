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

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportKhoaLuanTotNghiep_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportKhoaLuanTotNghiep_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "KhoiLuongGiangDay_DetailView";
        }

        private void PMS_ImportKhoaLuanTotNghiep_Controller_Activated(object sender, System.EventArgs e)
        {
            string MaTruong = TruongConfig.MaTruong;
            if (MaTruong == "HVNH" || MaTruong == "DNU" || MaTruong == "UFM" || MaTruong == "HUFLIT")
                btImportKhoiLuong.Active["TruyCap"] = false;
        }
        private void btImportKhoiLuong_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                KhoiLuongGiangDay OidQuanLy = View.CurrentObject as KhoiLuongGiangDay;
                if (OidQuanLy != null)
                {
                    if (OidQuanLy.BangChotThuLao == null)
                    {
                         DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                         if (dialogResult == DialogResult.Yes)
                         {
                             //SqlParameter[] pDongBo = new SqlParameter[0];
                             //DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieuKLTNCD_DH", CommandType.StoredProcedure, pDongBo);
                         }
                        _obs = View.ObjectSpace;
                        View.ObjectSpace.CommitChanges();
                        Imp_KhoaLuanTotNghiep.XuLy(_obs, OidQuanLy);
                        _obs.Refresh();
                    }
                    else
                        XtraMessageBox.Show("Dữ liệu đã chốt - Không thể import", "Thông báo!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
