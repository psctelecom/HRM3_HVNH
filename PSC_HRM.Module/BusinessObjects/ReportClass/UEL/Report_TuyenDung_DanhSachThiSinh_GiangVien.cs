using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using System.Data;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TuyenDung;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách thí sinh (giảng viên)")]
    public class Report_TuyenDung_DanhSachThiSinh : StoreProcedureReport
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

        public Report_TuyenDung_DanhSachThiSinh(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TuyenDung_DanhSachThiSinh_GiangVien",
                   CommandType.StoredProcedure, new SqlParameter("@QuanLyTuyenDung", QuanLyTuyenDung.Oid));
            
            return cmd;
        }
    }

}
