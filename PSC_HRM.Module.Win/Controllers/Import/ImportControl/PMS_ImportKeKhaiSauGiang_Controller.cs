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
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportKeKhaiSauGiang_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportKeKhaiSauGiang_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKeKhaiSauGiang_DetailView";
        }

        void PMS_ImportKeKhaiSauGiang_Controller_Activated(object sender, System.EventArgs e)
        {
            btImportKhoiLuong.Active["TruyCap"] = false;
        }
        private void btImportKhoiLuong_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                QuanLyKeKhaiSauGiang OidQuanLy = View.CurrentObject as QuanLyKeKhaiSauGiang;
                if (OidQuanLy != null)
                {
                    if (OidQuanLy.BangChotThuLao == null)
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataProvider.ExecuteNonQuery("DELETE FROM dbo.ChiTietKeKhaiSauGiang WHERE QuanLyKeKhaiSauGiang = '"+ OidQuanLy.Oid + "'", CommandType.Text);

                        }                   
                        _obs = View.ObjectSpace;
                        View.ObjectSpace.CommitChanges();
                        Imp_KeKhaiSauGiangDay.XuLy(_obs, OidQuanLy);
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
