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
    public partial class BaoCao_HopDong_VHU_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao _HoatDong;
        public BaoCao_HopDong_VHU_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao_1_DetailView";
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string query = "";
            object kq;
            _HoatDong = View.CurrentObject as PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao;
            if (_HoatDong != null)
            {
                foreach (var chiTiet in _HoatDong.ListDanhSach)
                {
                    if (chiTiet.Chon)
                    {
                        query += " UNION ALL " + "SELECT '"
                                               + chiTiet.MaGV.Replace("'", "''") + "' as MaNhanVien,N'"
                                               + chiTiet.HoTen.Replace("'", "''") + "' as HoTen,N'"
                                               + chiTiet.ChucDanh.Replace("'", "''") + "' as ChucDanh,N'"
                                               + chiTiet.HocHam.Replace("'", "''") + "' as HocHam,N'"
                                               + chiTiet.HocVi.Replace("'", "''") + "' as HocVi,N'"
                                               + chiTiet.LoaiGV.Replace("'", "''") + "' as LoaiGV,N'"
                                               + chiTiet.LoaiHocPhan.Replace("'", "''") + "' as LoaiHocPhan,N'"
                                               + chiTiet.MaHocPhan.Replace("'", "''") + "' as MaHocPhan,N'"
                                               + chiTiet.TenHocPhan.Replace("'", "''") + "' as TenHocPhan,CAST("
                                               + chiTiet.SoTietLT.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietLT, CAST("
                                               + chiTiet.SoTietTH.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietTH, CAST("
                                               + chiTiet.SoTietKhac.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietKhac, CAST("
                                               + chiTiet.SiSo.ToString().Replace(",", ".") + " AS INT) as SiSo, CAST("
                                               + chiTiet.SoTietThucDay.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietThucDay, CAST("
                                               + chiTiet.TietQuyDoi.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as TietQuyDoi, CAST("
                                               + chiTiet.DonGia.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as DonGia, CAST("
                                               + chiTiet.ThanhTienChiTiet.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as ThanhTienChiTiet, N'"
                                               + chiTiet.TenBoPhan.Replace("'", "''") + "' as TenBoPhan, N'"
                                               + chiTiet.TenNamhoc.Replace("'", "''") + "' as TenNamHoc, N'"
                                               + chiTiet.TenHocKy.Replace("'", "''") + "' as TenHocKy, N'"
                                               + chiTiet.ThoiGiangDay.Replace("'", "''") + "' as ThoiGiangDay, N'"
                                               + chiTiet.DiaDiemDay.Replace("'", "''") + "' as DiaDiemDay";
                    }
                }

                if (query == "")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xác nhận!");
                }
                else
                {
                    DevExpress.ExpressApp.DC.ITypeInfo type;
                    int OIDReport = 5;

                    type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BaoCao.Report_PMS_ThanhToanThuLaoGiangDay_HopDong");
                    if (type != null)
                    {
                        _obs = Application.CreateObjectSpace();
                        StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);

                        HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));
                        if (report != null)
                        {
                            //Truyền parameter
                            ((Report_PMS_ThanhToanThuLaoGiangDay_HopDong)rpt).SQL = query != "" ? query.Substring(11).ToString() : "";
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