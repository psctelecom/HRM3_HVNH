using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Đánh giá cán bộ tổng hợp")]
    [Appearance("Report_TongHopXepLoaiLaoDong.TatCaBoPhan", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Report_TongHopXepLoaiLaoDong : StoreProcedureReport, IBoPhan
    {
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;
        private int _Nam = DateTime.Today.Year;

        public Report_TongHopXepLoaiLaoDong(Session session) : base(session) { }

        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData()]
        public bool TatCaBoPhan
        {
            get
            {
                return _TatCaBoPhan;
            }
            set
            {
                SetPropertyValue("TatCaBoPhan", ref _TatCaBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

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
            SqlCommand cm = new SqlCommand("spd_Report_ThongKeXepLoaiLaoDong");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            if (BoPhan != null)
                cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@BoPhan", DBNull.Value);
            cm.Parameters.AddWithValue("@Nam", Nam);

            return cm;
        }
    }

}
