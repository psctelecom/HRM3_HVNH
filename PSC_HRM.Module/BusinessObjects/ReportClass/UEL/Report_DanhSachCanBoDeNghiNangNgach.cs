using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ đề nghị nâng ngạch")]
    [ImageName("BO_Report")]
    public class Report_DanhSachCanBoDeNghiNangNgach : StoreProcedureReport
    {
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
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

        public Report_DanhSachCanBoDeNghiNangNgach(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            base.FillDataSource();

            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_DanhSachCanBoDeNghiNangNgach", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                //da.SelectCommand.Parameters.AddWithValue("@TruongHoc", HamDungChung.TruongHoc(Session).Oid);

                da.Fill(DataSource);
            }
        }
    }
}
