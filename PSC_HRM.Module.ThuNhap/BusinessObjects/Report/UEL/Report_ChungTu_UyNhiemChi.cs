using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.ChungTu;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Chứng từ chi tiết ủy nhiệm chi")]
    public class Report_ChungTu_UyNhiemChi : StoreProcedureReport
    {
        private UyNhiemChi _UyNhiemChi;

        [ImmediatePostData]
        [ModelDefault("Caption", "Ủy nhiệm chi")]
        [RuleRequiredField("", DefaultContexts.Save, "Vui lòng chọn lần ủy nhiệm")]
        public UyNhiemChi UyNhiemChi
        {
            get
            {
                return _UyNhiemChi;
            }
            set
            {
                SetPropertyValue("UyNhiemChi", ref _UyNhiemChi, value);
            }
        }

        public Report_ChungTu_UyNhiemChi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@UyNhiemChi", UyNhiemChi.Oid);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ChungTu_UyNhiemChi", System.Data.CommandType.StoredProcedure, parameter);
            cmd.CommandTimeout = 180; 
            return cmd;
        }
    }

}
