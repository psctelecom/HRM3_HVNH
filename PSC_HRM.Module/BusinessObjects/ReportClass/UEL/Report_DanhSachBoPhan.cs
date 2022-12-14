using System;
using DevExpress.Xpo;
using System.Data.SqlClient;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách phòng ban và bộ môn")]
    public class Report_DanhSachBoPhan : StoreProcedureReport
    {
        public Report_DanhSachBoPhan(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_DanhSachBoPhan", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return cmd;
        }
    }

}
