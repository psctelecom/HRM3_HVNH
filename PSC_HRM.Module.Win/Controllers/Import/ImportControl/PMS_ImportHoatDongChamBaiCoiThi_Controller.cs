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
using System.Data;
using DevExpress.XtraEditors;
namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportHoatDongChamBaiCoiThi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportHoatDongChamBaiCoiThi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyHoatDongKhac_DetailView";
        }

        void PMS_ImportHoatDongChamBaiCoiThi_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "HUFLIT" || TruongConfig.MaTruong == "NEU")
            {
                btImportChamBaiCoiThi.Active["TruyCap"] = false;
            }
        }
        private void btImportChamBaiCoiThi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                QuanLyHoatDongKhac OidQuanLy = View.CurrentObject as QuanLyHoatDongKhac;
                if (OidQuanLy != null)
                {
                    if (OidQuanLy.Khoa || OidQuanLy.BangChotThuLao != null)
                    {
                        XtraMessageBox.Show("Dữ liệu đã khóa - không thể import!", "Thông báo");
                    }
                    else
                    {
                        if (TruongConfig.MaTruong == "UEL")
                        {
                            if (OidQuanLy.KyTinhPMS == null)
                            {
                                XtraMessageBox.Show("Vui lòng chọn đợt chi trả thù lao!", "Thông báo");
                                return;
                            }
                        } 
                        DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa dữ liệu cũ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string sql = "DELETE ChiTietThuLaoChamBaiCoiThi";
                            sql += " FROM QuanLyHoatDongKhac";
                            sql += " JOIN dbo.ChiTietThuLaoChamBaiCoiThi ON ChiTietThuLaoChamBaiCoiThi.QuanLyHoatDongKhac = QuanLyHoatDongKhac.Oid";
                            sql += " WHERE QuanLyHoatDongKhac.Oid='" + OidQuanLy.Oid.ToString() + "'";
                            DataProvider.ExecuteNonQuery(sql, CommandType.Text);
                        }
                        
                        _obs = View.ObjectSpace;
                        View.ObjectSpace.CommitChanges();
                        Imp_HoatDongChamBaiCoiThi.XuLy(_obs, OidQuanLy);
                        _obs.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}