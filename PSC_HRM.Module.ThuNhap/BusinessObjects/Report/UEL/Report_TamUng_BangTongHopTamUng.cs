using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp tạm ứng")]
    public class Report_TamUng_BangTongHopTamUng : StoreProcedureReport
    {

        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat","####")]
        [ModelDefault("EditMask", "####")]
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


        public Report_TamUng_BangTongHopTamUng(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Nam = DateTime.Now.Year;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Nam", Nam);


            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TamUng_BangTongHopTamUng", 
                System.Data.CommandType.StoredProcedure, 
                param);
            return cmd;
        }
    }

}
