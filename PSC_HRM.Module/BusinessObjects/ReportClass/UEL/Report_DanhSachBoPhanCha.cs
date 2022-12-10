using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách phòng ban")]
    public class Report_DanhSachBoPhanCha : StoreProcedureReport
    {
        public Report_DanhSachBoPhanCha(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_DanhSachBoPhanCha", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return cmd;
        }
    }

}
