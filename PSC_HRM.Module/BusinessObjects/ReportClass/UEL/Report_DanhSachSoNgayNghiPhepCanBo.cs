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
    [ModelDefault("Caption", "Danh sách số ngày nghỉ phép của cán bộ")]
    [ImageName("BO_Report")]
    public class Report_DanhSachSoNgayNghiPhepCanBo : StoreProcedureReport
    {
        public Report_DanhSachSoNgayNghiPhepCanBo(Session session) : base(session) { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_DanhSachSoNgayNghiPhepCanBo", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            da.Fill(DataSource);
        }
    }
}
