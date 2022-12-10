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
    [ModelDefault("Caption", "Báo cáo: Danh sách cán bộ theo đơn vị công tác")]
    public class Report_DanhSach_NhanVienTheoDonViCongTac : StoreProcedureReport
    {
        public Report_DanhSach_NhanVienTheoDonViCongTac(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_DanhSach_NhanVienTheoDonViCongTac", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return cmd;
        }
    }

}
