using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC_HRM.Module.BusinessObjects.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Thống kê chức vụ công đoàn")]
    public class Report_ThongKe_ChucVuTrongDoanThe : StoreProcedureReport
    {
        public Report_ThongKe_ChucVuTrongDoanThe(Session session) : 
            base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_ThongKe_ChucVuTrongDoanThe");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return cmd;
        }
    }
}
