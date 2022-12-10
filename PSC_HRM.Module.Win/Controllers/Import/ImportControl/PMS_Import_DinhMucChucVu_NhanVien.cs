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
    public partial class PMS_Import_DinhMucChucVu_NhanVien : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_Import_DinhMucChucVu_NhanVien()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioChuan_DetailView";
        }

        void PMS_Import_DinhMucChucVu_NhanVien_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HUFLIT" || TruongConfig.MaTruong == "HVNH")
                btImport_DinhMucGioChuan.Active["TruyCap"] = false;
        }
        private void btImport_DinhMucGioChuan_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //try
            //{
            QuanLyGioChuan OidQuanLy = View.CurrentObject as QuanLyGioChuan;
            if (OidQuanLy != null)
            {
                _obs = View.ObjectSpace;
                if (TruongConfig.MaTruong != "UFM" && TruongConfig.MaTruong != "UEL")
                {
                    SqlParameter[] param = new SqlParameter[3]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@OidQuanLy", OidQuanLy.Oid);
                    param[1] = new SqlParameter("@NamHoc", OidQuanLy.NamHoc.Oid);
                    param[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                    DataProvider.ExecuteNonQuery("spd_PMS_CapNhatQuanLyGioChuan", System.Data.CommandType.StoredProcedure, param);
                }
                if (TruongConfig.MaTruong != "QNU" && TruongConfig.MaTruong != "UEL")
                {
                    View.ObjectSpace.CommitChanges();
                    SqlParameter[] param = new SqlParameter[2]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@QuanLyGioChuan", OidQuanLy.Oid);
                    param[1] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName.ToString());
                    DataProvider.ExecuteNonQuery("spd_PMS_Xoa_DinhMucGioChuan_NhaNVien", System.Data.CommandType.StoredProcedure, param);
                }
                if (TruongConfig.MaTruong == "UEL")
                {
                    Imp_DinhMucGioChuan_NhanVien.XuLyUEL(_obs, OidQuanLy);
                }
                else
                {
                    Imp_DinhMucGioChuan_NhanVien.XuLy(_obs, OidQuanLy);
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