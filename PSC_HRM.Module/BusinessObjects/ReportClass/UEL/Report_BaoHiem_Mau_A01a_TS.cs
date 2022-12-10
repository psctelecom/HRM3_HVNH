using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Tờ khai tham gia BHXH, BHYT lần đầu (Mẫu A01a-TS)")]
    public class Report_BaoHiem_Mau_A01a_TS : StoreProcedureReport
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

        public Report_BaoHiem_Mau_A01a_TS(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Dot", Dot);
            param[1] = new SqlParameter("@ThoiGian", ThoiGian);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BaoHiem_Mau_A01a_TS", System.Data.CommandType.StoredProcedure, param);

            return cmd;
        }
    }

}
