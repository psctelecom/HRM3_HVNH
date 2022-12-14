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
    [ModelDefault("Caption", "Báo cáo: Bảng thông tin truy lĩnh lương")]
    public class Report_TruyLinhLuong_BangThongTinTruyLinhLuong : StoreProcedureReport
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

        public Report_TruyLinhLuong_BangThongTinTruyLinhLuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_TruyLinhLuong_BangThongTinTruyLinhLuong", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangTruyLuong", BangTruyLuong.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
