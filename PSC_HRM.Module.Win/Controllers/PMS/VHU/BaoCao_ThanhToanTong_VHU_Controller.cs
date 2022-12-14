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
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.ThuNhap.ThuLao;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BaoCao_ThanhToanTong_VHU_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao _HoatDong;
        public BaoCao_ThanhToanTong_VHU_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao_DetailView";
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string query = "";
            object kq;
            _HoatDong = View.CurrentObject as PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao;
            if (_HoatDong != null)
            {
                foreach (var chiTiet in _HoatDong.ListDanhSach)
                {
                    if (chiTiet.Chon)
                    {
                        query += " UNION ALL " + "SELECT '"
                                               + chiTiet.MaGV.Replace("'", "''") + "' as MaGV,N'"
                                               + chiTiet.HoTen.Replace("'", "''") + "' as HoTen,CAST("
                                               + chiTiet.DonGia.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as DonGia, CAST("
                                               + chiTiet.ThanhTien.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as ThanhTienTong, CAST("
                                               + chiTiet.ThueTNCN.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as ThueTNCN, CAST("
                                               + chiTiet.ThucNhan.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as ThucNhan, N'"
                                               + chiTiet.MaSoThue.Replace("'", "''") + "' as MaSoThue, N'"
                                               + chiTiet.SoTaiKhoan.Replace("'", "''") + "' as SoTaiKhoan, N'"
                                               + chiTiet.TenNamhoc.Replace("'", "''") + "' as TenNamhoc, N'"
                                               + chiTiet.NganHang.Replace("'", "''") + "' as TenNganHang";
                    }
                }

                if (query == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần in báo cáo!");
                }
                else
                {
                    DevExpress.ExpressApp.DC.ITypeInfo type;
                    //Bắt đầu xuất report
                    int OIDReport = 4;

                    type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BaoCao.Report_PMS_ThanhToanThuLaoGiangDay_Tong");
                    if (type != null)
                    {
                        _obs = Application.CreateObjectSpace();
                        StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);

                        HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));
                        if (report != null)
                        {
                            //Truyền parameter
                            ((Report_PMS_ThanhToanThuLaoGiangDay_Tong)rpt).SQL = query != "" ? query.Substring(11).ToString() : "";
                            //
                            StoreProcedureReport.Param = rpt;
                            Frame.GetController<ReportServiceController>().ShowPreview(report);
                        }
                    }
                }
            }
        }
    }
}