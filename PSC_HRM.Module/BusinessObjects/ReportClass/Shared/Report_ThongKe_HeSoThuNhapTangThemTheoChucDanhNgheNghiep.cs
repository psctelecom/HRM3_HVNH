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
    [ModelDefault("Caption", "Báo cáo: Thống kê hệ số thu nhập tăng thêm theo chức danh nghề nghiệp")]
    public class Report_ThongKe_HeSoThuNhapTangThemTheoChucDanhNgheNghiep : StoreProcedureReport
    {
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
        public Report_ThongKe_HeSoThuNhapTangThemTheoChucDanhNgheNghiep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Ngay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@Ngay", Ngay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_HeSoThuNhapTangThemTheoChucDanhNgheNghiep", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }
}
