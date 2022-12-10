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
    [ModelDefault("Caption", "Báo cáo: Thống kê chất lượng viên chức trong các đơn vị sự nghiệp công lập")]
    public class Report_ThongKe_ChatLuongVienChuc : StoreProcedureReport
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
        public Report_ThongKe_ChatLuongVienChuc(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Ngay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@Ngay", Ngay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_ChatLuongVienChuc", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
