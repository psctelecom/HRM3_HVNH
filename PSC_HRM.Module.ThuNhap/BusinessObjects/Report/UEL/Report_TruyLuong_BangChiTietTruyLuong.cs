using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng chi tiết truy lương")]
    public class Report_TruyLuong_BangChiTietTruyLuong : StoreProcedureReport
    {
        // Fields...
        private BangTruyLuong _BangTruyLuong;

        [ModelDefault("Caption", "Bảng truy lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BangTruyLuong BangTruyLuong
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

        public Report_TruyLuong_BangChiTietTruyLuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TruyLuong_BangChiTietTruyLuong", System.Data.CommandType.StoredProcedure, new SqlParameter("@BangTruyLuong", BangTruyLuong.Oid));
            return cmd;
        }
    }

}
