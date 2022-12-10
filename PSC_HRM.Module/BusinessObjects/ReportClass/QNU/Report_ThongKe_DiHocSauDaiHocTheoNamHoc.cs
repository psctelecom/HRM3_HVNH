using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê đi học sau đại học theo năm học")]
    public class Report_ThongKe_DiHocSauDaiHocTheoNamHoc : StoreProcedureReport
    {
        public Report_ThongKe_DiHocSauDaiHocTheoNamHoc(Session session) : base(session) { }
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);


            SqlCommand cmd = DataProvider.GetCommand("spd_ThongKeDiHocSauDaiHocThemNamHoc",
                System.Data.CommandType.StoredProcedure, param);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }


    }
}
