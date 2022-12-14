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
    [ModelDefault("Caption", "Báo cáo: Thống kê cán bộ, giảng viên, nhân viên")]
    public class Report_ThongKe_CanBoGiangVienNhanVien : StoreProcedureReport
    {
        // Fields...
        private DateTime _NgayGui;
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

        [ModelDefault("Caption", "Ngày gửi")]
        public DateTime NgayGui
        {
            get
            {
                return _NgayGui;
            }
            set
            {
                SetPropertyValue("NgayGui", ref _NgayGui, value);
            }
        }

        public Report_ThongKe_CanBoGiangVienNhanVien(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_CanBoGiangVienNhanVien", System.Data.CommandType.StoredProcedure, new SqlParameter("@NamHoc", NamHoc.Oid)))
            {
                cmd.Connection = (SqlConnection)Session.Connection;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(DataSource);
                }
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayGui = HamDungChung.GetServerTime();
        }
    }

}
