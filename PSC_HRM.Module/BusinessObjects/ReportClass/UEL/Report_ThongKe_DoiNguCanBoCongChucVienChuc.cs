using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê đội ngũ cán bộ, công chức, viên chức")]
    public class Report_ThongKe_DoiNguCanBoCongChucVienChuc : StoreProcedureReport
    {
        // Fields...
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

        public Report_ThongKe_DoiNguCanBoCongChucVienChuc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_DoiNguCanBoCongChucVienChuc", System.Data.CommandType.StoredProcedure, new SqlParameter("@NamHoc", NamHoc.Oid));
            return cmd;
        }
    }

}
