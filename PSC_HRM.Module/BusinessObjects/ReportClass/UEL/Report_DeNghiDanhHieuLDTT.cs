using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.KhenThuong;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách đề nghị thi đua khen thưởng")]
    public class Report_DeNghiDanhHieuLDTT : StoreProcedureReport
    {
        private DanhHieuKhenThuong _DanhHieu;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DanhHieuKhenThuong DanhHieu
        {
            get
            {
                return _DanhHieu;
            }
            set
            {
                SetPropertyValue("DanhHieu", ref _DanhHieu, value);
            }
        }


        public Report_DeNghiDanhHieuLDTT(Session session) : base(session) { }


        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_DanhSachCaNhanDeNghiThiDuaKhenThuong", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
            cmd.Parameters.AddWithValue("@DanhHieu", DanhHieu.Oid);
            return cmd;
        }
    }
}
