using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách ứng viên được thi tuyển")]
    public class Report_TuyenDung_DanhSachDuThiTuyen : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private int _Dot = 1;

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

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        public Report_TuyenDung_DanhSachDuThiTuyen(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[1] = new SqlParameter("@Dot", Dot);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TuyenDung_DanhSachDuThiTuyen",
                System.Data.CommandType.StoredProcedure, param);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                da.Fill(DataSource);
            }
        }
    }

}
