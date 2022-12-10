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
    [ModelDefault("Caption", "Báo cáo: Thống kê công khai thông tin cán bộ")]
    public class Report_ThongKeCongKhaiThongTinCanBo : StoreProcedureReport
    {
        // Fields...
        private DateTime _Ngay;

        [ModelDefault("Caption", "Tính đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
            }
        }
        public Report_ThongKeCongKhaiThongTinCanBo(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Ngay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@Nam", Ngay.Year);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKeCongKhaiThongTinCanBo", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
