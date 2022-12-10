using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ đang đi học")]
    [ImageName("BO_Report")]
    public class Report_DanhSachCanBoDangDiHoc : StoreProcedureReport
    {
        public Report_DanhSachCanBoDangDiHoc(Session session) : base(session) { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_DanhSachCanBoDangDiHoc", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            da.Fill(DataSource);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
