using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo số lượng công chức giữ chức vụ lãnh đạo")]
    [Appearance("RptSoLuongCBGiuChucVuBoNhiem.TatCaBoPhan", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class RptSoLuongCBGiuChucVuBoNhiem : StoreProcedureReport, IBoPhan
    {
        public RptSoLuongCBGiuChucVuBoNhiem(Session session) : base(session) { }

        private bool _TatCaBoPhan = true;
        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData()] //Load dữ liệu ngay lập tức
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

        private BoPhan _BoPhan;
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

        private DateTime _TuNgay = DateTime.Today;
        [ModelDefault("Caption", "Kỳ báo cáo từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        private DateTime _DenNgay = DateTime.Today;
        [ModelDefault("Caption", "Kỳ báo cáo đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_Report_SoLuongCongChucDuocBoNhiem");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            if (BoPhan != null)
                cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@BoPhan", DBNull.Value);
            cm.Parameters.AddWithValue("@TuNgay", TuNgay);
            cm.Parameters.AddWithValue("@DenNgay", DenNgay);
            return cm;
        }
    }

}
