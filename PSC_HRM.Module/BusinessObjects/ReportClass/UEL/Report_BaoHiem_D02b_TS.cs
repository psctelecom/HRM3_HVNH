using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tính lãi truy thu BHXH, BHYT (Mẫu D02b-TS)")]
    public class Report_BaoHiem_D02b_TS : StoreProcedureReport
    {
        private BaoHiem.QuanLyTruyThuBaoHiem _QuanLyTruyThuBaoHiem;

        [ModelDefault("Caption", "Kỳ báo cáo")]
        //[ModelDefault("EditMask", "MM/yyyy")]
        //[ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public QuanLyTruyThuBaoHiem QuanLyTruyThuBaoHiem
        {
            get
            {
                return _QuanLyTruyThuBaoHiem;
            }
            set
            {
                SetPropertyValue("QuanLyTruyThuBaoHiem", ref _QuanLyTruyThuBaoHiem, value);
            }
        }

        public Report_BaoHiem_D02b_TS(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BaoHiem_D02b_TS", System.Data.CommandType.StoredProcedure, new SqlParameter("@QuanLyTruyThuBaoHiem", QuanLyTruyThuBaoHiem.Oid));
            
            return cmd;
        }
    }

}