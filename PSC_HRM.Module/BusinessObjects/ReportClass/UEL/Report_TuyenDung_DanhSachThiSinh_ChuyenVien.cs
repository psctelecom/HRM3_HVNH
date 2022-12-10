using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.TuyenDung;
using System.Data;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách thí sinh (chuyên viên)")]
    public class Report_TuyenDung_DanhSachThiSinh_ChuyenVien : StoreProcedureReport
    {
        // Fields...
        private QuanLyTuyenDung _QuanLyTuyenDung;

        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        public Report_TuyenDung_DanhSachThiSinh_ChuyenVien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TuyenDung_DanhSachThiSinh_ChuyenVien",
                   CommandType.StoredProcedure, new SqlParameter("@QuanLyTuyenDung", QuanLyTuyenDung.Oid));

            return cmd;
        }
    }

}
