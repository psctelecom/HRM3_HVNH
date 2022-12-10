using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách thi vấn đáp")]
    public class Report_TuyenDung_DanhSachThiVanDap : StoreProcedureReport
    {
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

        public Report_TuyenDung_DanhSachThiVanDap(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_TuyenDung_DanhSachThiVanDap", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLyTuyenDung", QuanLyTuyenDung.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
