using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Thống kê giảng viên thỉnh giảng theo năm")]    
    public class rptThongKeGiangVienThinhGiangTheoNam : StoreProcedureReport
    {
        public rptThongKeGiangVienThinhGiangTheoNam(Session session) : base(session) { }

        private int _Nam;
        [ModelDefault("Caption", "Năm Cộng Tác")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập Năm Cộng Tác")]
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

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeGiaoVienThinhGiangTheoNam", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
             
            da.SelectCommand.Parameters.AddWithValue("@NamCongTac", Nam);

            da.Fill(DataSource);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = DateTime.Today.Year;
        }
    }

}
