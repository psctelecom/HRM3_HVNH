using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách nâng ngạch")]
    [ImageName("BO_Report")]
    public class Report_DanhSachNangNgach : StoreProcedureReport
    {
        private int _Nam = DateTime.Today.Year;

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

        public Report_DanhSachNangNgach(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            base.FillDataSource();

            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_DanhSachCanBoNangNgach", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Nam", Nam);
                da.SelectCommand.Parameters.AddWithValue("@TruongHoc", HamDungChung.ThongTinTruong(Session).Oid);

                da.Fill(DataSource);
            }
        }
    }

}
