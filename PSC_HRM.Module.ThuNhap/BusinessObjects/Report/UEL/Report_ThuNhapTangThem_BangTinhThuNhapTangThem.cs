using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Chi tiết thu nhập tăng thêm")]
    public class Report_ThuNhapTangThem_BangTinhThuNhapTangThem : StoreProcedureReport
    {
        private BangThuNhapTangThem _BangThuNhapTangThem;

        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BangThuNhapTangThem BangThuNhapTangThem
        {
            get
            {
                return _BangThuNhapTangThem;
            }
            set
            {
                SetPropertyValue("BangThuNhapTangThem", ref _BangThuNhapTangThem, value);
            }
        }

        public Report_ThuNhapTangThem_BangTinhThuNhapTangThem(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThuNhapTangThem_BangTinhThuNhapTangThem", 
                System.Data.CommandType.StoredProcedure,
                new SqlParameter("@BangThuNhapTangThem", BangThuNhapTangThem.Oid));
            return cmd;
        }
    }

}
