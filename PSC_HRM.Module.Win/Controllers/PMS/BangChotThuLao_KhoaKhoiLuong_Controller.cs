using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.ThuNhap.NonPersistentThuNhap;
using PSC_HRM.Module.ReportClass;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BangChotThuLao_KhoaKhoiLuong_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public BangChotThuLao_KhoaKhoiLuong_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_DetailView";
        }

        void BangChotThuLao_KhoaKhoiLuong_Controller_Activated(object sender, System.EventArgs e)
        {
            if (HamDungChung.CurrentUser().UserName != "psc")
                btnKhoaBangChot.Active["TruyCap"] = false;
        }

        private void btnKhoaBangChot_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BangChotThuLao bangChot;
            bangChot = View.CurrentObject as BangChotThuLao;
            if (bangChot != null)
            {
                try
                {
                    if (TruongConfig.MaTruong == "HVNH" || TruongConfig.MaTruong == "VHU")
                    {
                        SqlParameter[] pTinhTien = new SqlParameter[1];
                        pTinhTien[0] = new SqlParameter("@BangChot", bangChot.Oid);
                        DataProvider.ExecuteNonQuery("spd_PMS_Khoa_ChotDuLieu", CommandType.StoredProcedure, pTinhTien);
                    }
                    else
                    {
                        DataProvider.ExecuteNonQuery("spd_PMS_Khoa_ChotDuLieu", CommandType.StoredProcedure);
                    }
                }
                catch (Exception)
                {
                    string sql = "UPDATE dbo.KhoiLuongGiangDay"
                                    + " SET Khoa=1"
                                    + " WHERE ISNULL(Khoa,0)=0"

                                    + " UPDATE dbo.QuanLyHoatDongKhac"
                                    + " SET Khoa=1"
                                    + " WHERE ISNULL(Khoa,0)=0"

                                    + " UPDATE dbo.ThoiKhoaBieu_KhoiLuongGiangDay"
                                    + " SET Khoa=1"
                                    + " WHERE ISNULL(Khoa,0)=0"

                                    + " UPDATE dbo.KeKhai_CacHoatDong_ThoiKhoaBieu"
                                    + " SET Khoa=1"
                                    + " WHERE ISNULL(Khoa,0)=0"

                                    + " UPDATE dbo.QuanLySauDaiHoc"
                                    + " SET Khoa=1"
                                    + " WHERE ISNULL(Khoa,0)=0"

                                    + " UPDATE dbo.QuanLyBoiDuongThuongXuyen"
                                    + " SET Khoa=1"
                                    + " WHERE ISNULL(Khoa,0)=0"

                                    + " UPDATE dbo.QuanLyKhaoThi"
                                    + " SET Khoa=1"
                                    + " WHERE ISNULL(Khoa,0)=0";
                    DataProvider.ExecuteNonQuery(sql, CommandType.Text);
                }
                XtraMessageBox.Show("Khóa khối lượng giảng dạy _ khối lượng khác thành công!", "Thông báo");
            }
        }
    }
}
