using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.ThuNhap.TruyLuong;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp truy lĩnh")]
    public class Report_TruyLuong_BangTongHopTruyLinh : StoreProcedureReport
    {
        // Fields...
        private BangTruyLuongNew _BangTruyLuong;

        [ModelDefault("Caption", "Bảng truy lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BangTruyLuongNew BangTruyLuong
        {
            get
            {
                return _BangTruyLuong;
            }
            set
            {
                SetPropertyValue("BangTruyLuong", ref _BangTruyLuong, value);
            }
        }

        public Report_TruyLuong_BangTongHopTruyLinh(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_TruyLuong_BangTongHopTruyLinh", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangTruyLuong", BangTruyLuong.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
