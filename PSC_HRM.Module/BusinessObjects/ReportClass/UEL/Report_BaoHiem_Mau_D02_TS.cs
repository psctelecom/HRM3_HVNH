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
    [ModelDefault("Caption", "Báo cáo: Danh sách lao động tham gia BHXH, BHYT (Mẫu D02-TS)")]
    public class Report_BaoHiem_Mau_D02_TS : StoreProcedureReport
    {
        // Fields...
        private QuanLyBienDong _QuanLyBienDong;

        [ModelDefault("Caption", "Quản lý biến động")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public QuanLyBienDong QuanLyBienDong
        {
            get
            {
                return _QuanLyBienDong;
            }
            set
            {
                SetPropertyValue("QuanLyBienDong", ref _QuanLyBienDong, value);
            }
        }

        public Report_BaoHiem_Mau_D02_TS(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {      
            return null;
        }

        public override void FillDataSource()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BaoHiem_D02_TS", System.Data.CommandType.StoredProcedure, new SqlParameter("@QuanLyBienDong", QuanLyBienDong.Oid));
            
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }
    }

}