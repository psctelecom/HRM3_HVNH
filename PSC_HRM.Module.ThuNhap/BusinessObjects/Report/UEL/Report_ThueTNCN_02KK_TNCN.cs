using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.ThuNhap.Thue;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: 02/KK-TNCN")]
    public class Report_02KK_TNCN : StoreProcedureReport
    {
        // Fields...
        private ToKhaiKhauTruThueTNCN _ToKhaiKhauTruThueTNCN;

        [ModelDefault("Caption", "Tờ khai khấu trừ thuế TNCN")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ToKhaiKhauTruThueTNCN ToKhaiKhauTruThueTNCN
        {
            get
            {
                return _ToKhaiKhauTruThueTNCN;
            }
            set
            {
                SetPropertyValue("ToKhaiKhauTruThueTNCN", ref _ToKhaiKhauTruThueTNCN, value);
            }
        }

        public Report_02KK_TNCN(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThueTNCN_02KK_TNCN", System.Data.CommandType.StoredProcedure, new SqlParameter("@ToKhai", ToKhaiKhauTruThueTNCN.Oid));
            return cmd;
            //return null;
        }
    }

}
