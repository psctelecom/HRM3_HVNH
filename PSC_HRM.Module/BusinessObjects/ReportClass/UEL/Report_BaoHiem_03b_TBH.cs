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
    [ModelDefault("Caption", "Báo cáo: Điều chỉnh hồ sơ BHXH, BHYT (Mẫu 03b-TBH)")]
    public class Report_BaoHiem_03b_TBH : StoreProcedureReport
    {
        // Fields...
        private int _Dot = 1;
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

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        public Report_BaoHiem_03b_TBH(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_BaoHiem_03b_TBH", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Thang", ThoiGian.Month);
            cmd.Parameters.AddWithValue("@Nam", ThoiGian.Year);
            cmd.Parameters.AddWithValue("@Dot", Dot);

            return cmd;
        }
    }

}
