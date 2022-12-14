using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Đội ngũ giảng viên (Biểu mẫu 23)")]
    [ImageName("BO_Report")]
    public class Report_ThongKe_BieuMau23 : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private DateTime _NgayGui;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Ngày gửi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayGui
        {
            get
            {
                return _NgayGui;
            }
            set
            {
                SetPropertyValue("NgayGui", ref _NgayGui, value);
            }
        }

        public Report_ThongKe_BieuMau23(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_BieuMau23", System.Data.CommandType.StoredProcedure, new SqlParameter("@NamHoc", NamHoc.Oid));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.Connection = (SqlConnection)Session.Connection;

            da.Fill(DataSource);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayGui = HamDungChung.GetServerTime();
        }
    }

}
