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
    [ModelDefault("Caption", "Báo cáo: Thống kê số lượng giảng viên theo khoa chuyên môn")]
    public class Report_ThongKe_SoLuongGiangVienTheoKhoaChuyenMon : StoreProcedureReport
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

        public Report_ThongKe_SoLuongGiangVienTheoKhoaChuyenMon(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Ngay = HamDungChung.GetServerTime();
        }

        public override void FillDataSource()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@Ngay", Ngay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_SoLuongGiangVienTheoKhoaChuyenMon", System.Data.CommandType.StoredProcedure, parameter);
            //
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
    }

}
