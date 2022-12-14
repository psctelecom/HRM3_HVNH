using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Report Danh sách cán bộ sắp nghỉ hưu")]
    [ImageName("BO_Report")]
    public class Report_DanhSachSapNghiHuu : StoreProcedureReport
    {
        private int _Thang;
        private int _Nam;

        [ModelDefault("Caption", "Tháng")]
        [RuleRange("", DefaultContexts.Save, 1, 12, "Tháng không hợp lệ")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập tháng")]
        public int Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
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

        public Report_DanhSachSapNghiHuu(Session session) : base(session) { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_DanhSachSapNghiHuu", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Thang", Thang);
            param[1] = new SqlParameter("@Nam", Nam);

            da.SelectCommand.Parameters.AddRange(param);
            da.Fill(DataSource);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Thang = DateTime.Today.Month;
            Nam = DateTime.Today.Year;
        }
    }

}
