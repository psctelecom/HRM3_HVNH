using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê biến động CBVC")]
    public class Report_ThongKe_BienDongCBVC : StoreProcedureReport
    {
        // Fields...
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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
 
        public Report_ThongKe_BienDongCBVC(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuNgay = HamDungChung.GetServerTime();
            DenNgay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@TuNgay", TuNgay);
            parameter[1] = new SqlParameter("@DenNgay", DenNgay);
            
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_BienDongCBVC", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
