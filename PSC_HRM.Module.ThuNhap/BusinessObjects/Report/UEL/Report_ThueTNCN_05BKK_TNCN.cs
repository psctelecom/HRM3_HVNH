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
    [ModelDefault("Caption", "Báo cáo: 05B/BK-TNCN")]
    public class Report_05BKK_TNCN : StoreProcedureReport
    {
        // Fields...
        private ToKhaiQuyetToanThueTNCN _ToKhaiQuyetToanThueTNCN;

        [ModelDefault("Caption", "Tờ khai quyết toán thuế TNCN")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ToKhaiQuyetToanThueTNCN ToKhaiQuyetToanThueTNCN
        {
            get
            {
                return _ToKhaiQuyetToanThueTNCN;
            }
            set
            {
                SetPropertyValue("ToKhaiQuyetToanThueTNCN", ref _ToKhaiQuyetToanThueTNCN, value);
            }
        }

        public Report_05BKK_TNCN(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThueTNCN_05BKK_TNCN", System.Data.CommandType.StoredProcedure, new SqlParameter("@ToKhai", ToKhaiQuyetToanThueTNCN.Oid));
            return cmd;
            //return null;
        }


    }
}
