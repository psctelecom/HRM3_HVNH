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
    [ModelDefault("Caption", "Báo cáo: Danh sách cán bộ hết hạn tập sự trong tháng")]
    public class Report_DanhSach_CanBoHetHanTapSuTrongThang : StoreProcedureReport
    {
        private DateTime _ThangNam;

        [ModelDefault("Caption", "Tháng năm")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime ThangNam
        {
            get { return _ThangNam; }
            set { SetPropertyValue("ThangNam", ref _ThangNam, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThangNam = HamDungChung.GetServerTime();
        }
        public Report_DanhSach_CanBoHetHanTapSuTrongThang(Session session) : base(session) { }
        public override SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_Report_DanhSach_CanBoTapSuHetHanTapSuTrongThangNam");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@ThangNam", ThangNam);
            return cm;
        }
    }

}
