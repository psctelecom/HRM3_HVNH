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
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.ThuNhap.ThuLao;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class BaoCaoTong_VHU_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session ses;
        CollectionSource collectionSource;
        PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao _source;
        PMS_ThanhToanThuLaoGiangDay_ChiTiet_NonBaoCao _source1;
        PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao _source2;
        public BaoCaoTong_VHU_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_ListView";
        }


        private void BaoCaoTong_VHU_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong == "VHU")
            {
                popDongBo.Active["TruyCap"] = true;
                popupWindowShowAction1.Active["TruyCap"] = true;
            }
            else
            {
                popDongBo.Active["TruyCap"] = false;
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
        }
        private void popDongBo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;

            View.ObjectSpace.CommitChanges();
            using (DialogUtil.AutoWait("Load danh sách"))
            {
                collectionSource = new CollectionSource(_obs, typeof(PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao));
                _source = new PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao(ses);
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }
        private void popDongBo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source != null)
            {
                if (_source.ListDanhSach.Count > 0)
                {
                    string query = "";
                    foreach (Report_PMS_ThanhToanThuLaoGiangDay_Tong_Non chiTiet in _source.ListDanhSach)
                    {
                        if (chiTiet.Chon == true)
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

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;

            View.ObjectSpace.CommitChanges();
            using (DialogUtil.AutoWait("Load danh sách"))
            {
                collectionSource = new CollectionSource(_obs, typeof(PMS_ThanhToanThuLaoGiangDay_ChiTiet_NonBaoCao));
                _source1 = new PMS_ThanhToanThuLaoGiangDay_ChiTiet_NonBaoCao(ses);
                e.View = Application.CreateDetailView(_obs, _source1);
            }
        }
            
        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source1 != null)
            {
                if (_source1.ListDanhSach.Count > 0)
                {
                    string query = "";
                    foreach (Report_PMS_ThanhToanThuLaoGiangDay_ChiTiet_Non chiTiet in _source1.ListDanhSach)
                    {
                        if (chiTiet.Chon == true)
                        {

                            query += " UNION ALL " + "SELECT '"
                                                + chiTiet.MaGV.Replace("'", "''") + "' as MaNhanVien,N'"
                                                + chiTiet.HoTen.Replace("'", "''") + "' as HoTen,N'"
                                                + chiTiet.ChucDanh.Replace("'", "''") + "' as ChucDanh,N'"
                                                + chiTiet.HocHam.Replace("'", "''") + "' as HocHam,N'"
                                                + chiTiet.HocVi.Replace("'", "''") + "' as HocVi,N'"
                                                + chiTiet.LoaiGV.Replace("'", "''") + "' as LoaiGV,N'"
                                                + chiTiet.MaHocPhan.Replace("'", "''") + "' as MaHocPhan,N'"
                                                + chiTiet.TenHocPhan.Replace("'", "''") + "' as TenHocPhan,N'"
                                                + chiTiet.LoaiHocPhan.Replace("'", "''") + "' as LoaiHocPhan,CAST("
                                                + chiTiet.SiSo.ToString().Replace(",", ".") + " AS INT) as SiSo, CAST("
                                                + chiTiet.SoTietThucDay.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as SoTietThucDay, CAST("
                                                + chiTiet.TietQuyDoi.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as TietQuyDoi, CAST("
                                                + chiTiet.DonGia.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as DonGia, CAST("
                                                + chiTiet.ThanhTienChiTiet.ToString().Replace(",", ".") + " AS DECIMAL(18,0)) as ThanhTienChiTiet, N'"
                                                + chiTiet.TenBoPhan.Replace("'", "''") + "' as TenBoPhan, N'"
                                                + chiTiet.TenNamhoc.Replace("'", "''") + "' as TenNamHoc";
                        }
                    }


                    DevExpress.ExpressApp.DC.ITypeInfo type;
                    //Bắt đầu xuất report
                    int OIDReport = 3;

                    type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BaoCao.Report_PMS_ThanhToanThuLaoGiangDay_CT");
                    if (type != null)
                    {
                        _obs = Application.CreateObjectSpace();
                        StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);

                        HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));
                        if (report != null)
                        {
                            //Truyền parameter
                            ((Report_PMS_ThanhToanThuLaoGiangDay_CT)rpt).SQL = query != "" ? query.Substring(11).ToString() : "";
                            //
                            StoreProcedureReport.Param = rpt;
                            Frame.GetController<ReportServiceController>().ShowPreview(report);
                        }
                    }
                }
            }
        }

        private void popupWindowShowAction2_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;

            View.ObjectSpace.CommitChanges();
            using (DialogUtil.AutoWait("Load danh sách"))
            {
                collectionSource = new CollectionSource(_obs, typeof(PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao));
                _source2 = new PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao(ses);
                e.View = Application.CreateDetailView(_obs, _source2);
            }
        }


        private void popupWindowShowAction2_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source2 != null)
            {
                if (_source2.ListDanhSach.Count > 0)
                {
                    string query = "";
                    foreach (Report_PMS_ThanhToanThuLaoGiangDay_HopDong_Non chiTiet in _source2.ListDanhSach)
                    {
                        if (chiTiet.Chon == true)
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
                                                + chiTiet.ThoiGiangDay.Replace("'", "''") + "' as ThoiGiangDay, N'"
                                                + chiTiet.DiaDiemDay.Replace("'", "''") + "' as DiaDiemDay";
                        }
                    }


                    DevExpress.ExpressApp.DC.ITypeInfo type;
                    //Bắt đầu xuất report
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