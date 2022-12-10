using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ nghỉ phép")]
    [ImageName("BO_Report")]
    public class Report_DanhSachCanBoNghiPhepTheoNam : StoreProcedureReport
    {
        private int nam = DateTime.Today.Year;

        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập năm ")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Nam
        {
            get { return nam; }
            set { SetPropertyValue("Nam", ref nam, value); }
        }


        public Report_DanhSachCanBoNghiPhepTheoNam(Session session) : base(session) { }


        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_DanhSachCanBoNghiPhep", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@Nam", Nam);

                da.Fill(DataSource);
            }
            
        }
    }
}
