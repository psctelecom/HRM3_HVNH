using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo danh sách và tiền lương công chức, viên chức")]
    [Appearance("RptBangLuongCanBo.TatCaBoPhan", TargetItems =  "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class RptBangLuongCanBo : StoreProcedureReport, IBoPhan
    {
        public RptBangLuongCanBo(Session session) : base(session) { }
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

        private DateTime _TinhDenNgay = DateTime.Today;
        [ModelDefault("Caption", "Tính đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TinhDenNgay
        {
            get
            {
                return _TinhDenNgay;
            }
            set
            {
                SetPropertyValue("TinhDenNgay", ref _TinhDenNgay, value);
            }
        }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_Report_BangLuongNhanVien");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            if (BoPhan != null)
                cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@BoPhan", DBNull.Value);
            return cm;
        }
    }

}
