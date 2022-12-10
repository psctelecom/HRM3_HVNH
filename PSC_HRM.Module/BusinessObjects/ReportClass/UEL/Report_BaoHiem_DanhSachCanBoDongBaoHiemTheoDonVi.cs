using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Chi tiết danh sách cán bộ đóng bảo hiểm")]
    public class Report_BaoHiem_DanhSachCanBoDongBaoHiemTheoDonVi : StoreProcedureReport
    {
        // Fields...
        private DateTime _ThoiGian = DateTime.Today;

        [ModelDefault("Caption", "Tháng năm")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime ThoiGian
        {
            get
            {
                return _ThoiGian;
            }
            set
            {
                SetPropertyValue("ThoiGian", ref _ThoiGian, value);
            }
        }

        public Report_BaoHiem_DanhSachCanBoDongBaoHiemTheoDonVi(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BaoHiem_ChiTietDanhSachCanBoDongBaoHiem", System.Data.CommandType.StoredProcedure, new SqlParameter("@Thang", ThoiGian));

            return cmd;
        }
    }
}
