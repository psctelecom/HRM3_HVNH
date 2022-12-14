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
    [ModelDefault("Caption", "Thống Kê Giảng Viên Thỉnh Giảng Theo Độ Tuổi")]
    public class rptThongKeGiangVienThinhGiangTheoDoTuoi : StoreProcedureReport
    {
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

        public rptThongKeGiangVienThinhGiangTheoDoTuoi(Session session) : 
            base(session) 
        { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter data = new SqlDataAdapter("spd_Report_ThongKeGiangVienThinhGiangTheoDoTuoi", (SqlConnection)Session.Connection);

            data.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            data.SelectCommand.Parameters.AddWithValue("@NamCongTac", Nam);

            data.Fill(DataSource);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = DateTime.Today.Year;
        }
    }

}
