using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Danh sách đề nghị hưởng trợ cấp nghỉ DS-PHSK sau điều trị thương tật, bệnh tật do TNLĐ, BNN (Mẫu C70a-HD)")]
    public class Report_BaoHiem_C70a_HD : StoreProcedureReport
    {
        // Fields...
        private DateTime _ThoiGian = DateTime.Today;

        [ModelDefault("Caption", "Kỳ báo cáo")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
            }
        }

        public Report_BaoHiem_C70a_HD(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Thang", ThoiGian.Month);
            param[1] = new SqlParameter("@Nam", ThoiGian.Year);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BaoHiem_C70a_HD",
                System.Data.CommandType.StoredProcedure, param);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }
    }

}
