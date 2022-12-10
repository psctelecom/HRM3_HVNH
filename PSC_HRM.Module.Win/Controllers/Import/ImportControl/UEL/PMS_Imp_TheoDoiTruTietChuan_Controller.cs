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
using PSC_HRM.Module.ThuNhap.ThuLao;
using ERP.Module.Win.Controllers.Import.ImportClass.UEL;

namespace PSC_HRM.Module.Controllers.Import.UEL
{
    public partial class PMS_Imp_TheoDoiTruTietChuan_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_Imp_TheoDoiTruTietChuan_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangThuLaoNhanVien_DetailView";
        }

        void PMS_Imp_TheoDoiTruTietChuan_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "UEL")
                btImportTheoDoiTruTietChuan.Active["TruyCap"] = true;
            else
                btImportTheoDoiTruTietChuan.Active["TruyCap"] = false;
        }

        private void btImportTheoDoiTruTietChuan_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                BangThuLaoNhanVien bangThuLaoNhanVien = View.CurrentObject as BangThuLaoNhanVien;
                if (bangThuLaoNhanVien != null)
                {
                    _obs = View.ObjectSpace;
                    //View.ObjectSpace.CommitChanges();
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlParameter[] pXoa = new SqlParameter[1];
                        pXoa[0] = new SqlParameter("@bangThuLaoNhanVien", bangThuLaoNhanVien.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_XoaDuLieuTheoDoiTruTietChuan", CommandType.StoredProcedure, pXoa);
                    }
                    Imp_TheoDoiTruTietChuan.XuLy(_obs, bangThuLaoNhanVien);
                    _obs.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}