using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Thống kê hợp đồng theo loại")]
    [ImageName("BO_Report")]
    public class Report_ThongKeHopDongTheoLoai : StoreProcedureReport
    {
        // Fields...
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        public Report_ThongKeHopDongTheoLoai(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKe_HopDongTheoLoai", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DenNgay", DenNgay);
            da.SelectCommand.Parameters.AddRange(param);

            da.Fill(DataSource);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DenNgay = HamDungChung.GetServerTime();
        }
    }

}
