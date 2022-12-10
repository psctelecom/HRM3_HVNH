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
using PSC_HRM.Module.PMS.NonPersistent;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa;
using DevExpress.ExpressApp.Utils;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_Import_KeKhai_TuXa_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _Session;
        QuanLyXemKeKhaiTuXa_Non KeKhainon;
        private ChoiceActionItem XemThongTin, XuatHuongDanChuyenDe, ImportHuongDanChuyenDe;
       
        public PMS_Import_KeKhai_TuXa_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyXemKeKhaiTuXa_Non_DetailView";


            btnXuatBaoCao.Items.Clear();
            XemThongTin = new ChoiceActionItem("Xem danh sách đơn vị và hoạt động", null);
            XuatHuongDanChuyenDe = new ChoiceActionItem("Xuất báo cáo hướng dẫn chuyên đề", null);
            ImportHuongDanChuyenDe = new ChoiceActionItem("Import hướng dẫn chuyên đề", null);

            btnXuatBaoCao.Items.Add(XemThongTin);
            btnXuatBaoCao.Items.Add(XuatHuongDanChuyenDe);
            btnXuatBaoCao.Items.Add(ImportHuongDanChuyenDe);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        void PMS_Import_KeKhai_TuXa_Controller_Activated(object sender, System.EventArgs e)
        {
            //if (HamDungChung.CurrentUser().UserName != "psc")
                //btImport.Active["TruyCap"] = false;
        }
        private void btImport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            _Session = ((XPObjectSpace)_obs).Session;
            KeKhainon = View.CurrentObject as QuanLyXemKeKhaiTuXa_Non;
            KeKhai_CacHoatDong_ThoiKhoaBieu KeKhai = null;
            KeKhai = _Session.FindObject<KeKhai_CacHoatDong_ThoiKhoaBieu>(CriteriaOperator.Parse("NamHoc = ? and HocKy = ?", KeKhainon.NamHoc.Oid, KeKhainon.HocKy.Oid));

            if (KeKhai != null && !KeKhai.Khoa)
            {
                Imp_KeKhaiHeTuXa.XuLy(_obs, KeKhai);
                KeKhainon.LoadData();
            }
        }

        private void btnXuatBaoCao_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            DevExpress.ExpressApp.DC.ITypeInfo type;
            KeKhainon = View.CurrentObject as QuanLyXemKeKhaiTuXa_Non;
            if (e.SelectedChoiceActionItem == XemThongTin)
            {
                int OIDReport = 3579;
                _obs = Application.CreateObjectSpace();
                _Session = ((XPObjectSpace)_obs).Session;
                type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM.Report_BaoCaoXemThongTin");
                if (type != null)
                {                                       
                    StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);
                    HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));

                    if (report != null)
                    {
                        ((Report_BaoCaoXemThongTin)rpt).ThongTinTruong = HamDungChung.ThongTinTruong(_Session);
                        StoreProcedureReport.Param = rpt;
                        Frame.GetController<ReportServiceController>().ShowPreview(report);
                    }                                       
                }
            }

            if (e.SelectedChoiceActionItem == XuatHuongDanChuyenDe)
            {
                if (KeKhainon != null)
                {
                    int OIDReport = 3580;
                    _obs = Application.CreateObjectSpace();
                    _Session = ((XPObjectSpace)_obs).Session;
                    type = ObjectSpace.TypesInfo.FindTypeInfo("PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM.Report_PMS_XuatDuLieuHuongDanChuyenDe");
                    if (type != null)
                    {                      
                        StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type.Type);
                        HRMReport report = _obs.FindObject<HRMReport>(CriteriaOperator.Parse("Oid =?", OIDReport));

                        if (report != null)
                        {
                            ((Report_PMS_XuatDuLieuHuongDanChuyenDe)rpt).NamHoc = _Session.GetObjectByKey<NamHoc>(KeKhainon.NamHoc.Oid);
                            ((Report_PMS_XuatDuLieuHuongDanChuyenDe)rpt).HocKy = _Session.GetObjectByKey<HocKy>(KeKhainon.HocKy.Oid);
                            StoreProcedureReport.Param = rpt;
                            Frame.GetController<ReportServiceController>().ShowPreview(report);
                        }
                    }
                }
            }

            if (e.SelectedChoiceActionItem == ImportHuongDanChuyenDe)
            {
                if (KeKhainon != null)
                {
                    _obs = View.ObjectSpace;
                    _Session = ((XPObjectSpace)_obs).Session;                    
                    KeKhai_CacHoatDong_ThoiKhoaBieu KeKhai = null;
                    KeKhai = _Session.FindObject<KeKhai_CacHoatDong_ThoiKhoaBieu>(CriteriaOperator.Parse("NamHoc = ? and HocKy = ?", KeKhainon.NamHoc.Oid, KeKhainon.HocKy.Oid));

                    if (KeKhai != null && !KeKhai.Khoa)
                    {
                        Imp_KeKhaiHeTuXa.XuLyNew(_obs, KeKhai);
                        KeKhainon.LoadData();
                    }
                }
            }
        }

        private void btnXoa_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (DialogUtil.ShowYesNo("Bạn thật sự muốn xóa những dòng kê khai đã chọn?") == DialogResult.Yes)
            {
                _obs = View.ObjectSpace;
                _Session = ((XPObjectSpace)_obs).Session;
                KeKhainon = View.CurrentObject as QuanLyXemKeKhaiTuXa_Non;
                KeKhai_CacHoatDong_ThoiKhoaBieu KeKhai = null;
                KeKhai = _Session.FindObject<KeKhai_CacHoatDong_ThoiKhoaBieu>(CriteriaOperator.Parse("NamHoc = ? and HocKy = ?", KeKhainon.NamHoc.Oid, KeKhainon.HocKy.Oid));

                if (KeKhai != null && !KeKhai.Khoa)
                {
                    foreach (ChiTietXemKeKhaiTuXa_Non ct in KeKhainon.ListChiTietKeKhai)
                    {
                        if (ct.Chon == true)
                        {
                            ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu ct_kekhai = _Session.FindObject<ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu>(CriteriaOperator.Parse("Oid = ? ", ct.OidChiTiet));
                            if (ct_kekhai != null)
                            {
                                string sql = "UPDATE dbo.ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu SET GCRecord = cast(CONVERT(varchar(20),getdate(),112) as INT)  WHERE Oid = '" + ct_kekhai.Oid + "'";
                                DataProvider.ExecuteNonQuery(sql, CommandType.Text);
                            }
                        }
                    }
                    KeKhainon.LoadData();
                    XtraMessageBox.Show("Xóa thành công", "Thông báo");
                }
            }
        }
    }
}