using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Thống kê thi đua khen thưởng (tập thể")]
    [ImageName("BO_Report")]
    public class Report_ThongKeThiDuaKhenThuongTapThe : StoreProcedureReport, IBoPhan
    {
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }
        public Report_ThongKeThiDuaKhenThuongTapThe(Session session) : base(session) { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThongKeThiDuaKhenThuongTapThe", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);

            da.Fill(DataSource);
        }
    }

}
