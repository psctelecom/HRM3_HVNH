using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê trình độ chuyên môn")]
    public class Report_ThongKe_TrinhDoChuyenMon : StoreProcedureReport
    {
        public Report_ThongKe_TrinhDoChuyenMon(Session session) : base(session) { }
        private int _Nam = DateTime.Today.Year; 

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_ThongKeTrinhDoChuyenMon", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Nam", Nam);
                da.Fill(DataSource);
            }
        }

        
    }
}
