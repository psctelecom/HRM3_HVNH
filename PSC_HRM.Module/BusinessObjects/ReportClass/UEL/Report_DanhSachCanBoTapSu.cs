using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách cán bộ tập sự")]
    public class Report_DanhSachCanBoTapSu : StoreProcedureReport
    {
        private int _TuNam;
        private int _DenNam;

        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int TuNam
        {
            get { return _TuNam; }
            set { SetPropertyValue("TuNam", ref _TuNam, value); }
        }

        [ModelDefault("Caption", "Đến năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int DenNam
        {
            get { return _DenNam; }
            set { SetPropertyValue("DenNam", ref _DenNam, value); }
        }

        public Report_DanhSachCanBoTapSu(Session session) : base(session) { }
               

        public override SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_Report_DanhSachCanBoTapSu");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@TuNam", TuNam);
            cm.Parameters.AddWithValue("@DenNam", DenNam);

            return cm;
        }
    }

}
