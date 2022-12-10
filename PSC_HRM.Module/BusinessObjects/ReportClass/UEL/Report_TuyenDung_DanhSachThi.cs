using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách thi")]
    public class Report_TuyenDung_DanhSachThi : StoreProcedureReport
    {
        // Fields...
        private MonThi _MonThi;
        private ViTriTuyenDung _ViTriTuyenDung;
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

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListViTriTuyenDung")]
        public ViTriTuyenDung ViTriTuyenDung
        {
            get
            {
                return _ViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("ViTriTuyenDung", ref _ViTriTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Môn thi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public MonThi MonThi
        {
            get
            {
                return _MonThi;
            }
            set
            {
                SetPropertyValue("MonThi", ref _MonThi, value);
            }
        }

        public Report_TuyenDung_DanhSachThi(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_TuyenDung_DanhSachThi", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLyTuyenDung", QuanLyTuyenDung.Oid);
                da.SelectCommand.Parameters.AddWithValue("@ViTriTuyenDung", ViTriTuyenDung.Oid);
                da.SelectCommand.Parameters.AddWithValue("@MonThi", MonThi.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
