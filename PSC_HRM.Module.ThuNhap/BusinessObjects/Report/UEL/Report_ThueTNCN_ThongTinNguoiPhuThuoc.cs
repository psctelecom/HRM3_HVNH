using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thông tin người phụ thuộc")]
    public class Report_DanhSachNguoiPhuThuoc : StoreProcedureReport
    {
        public Report_DanhSachNguoiPhuThuoc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThueTNCN_ThongTinNguoiPhuThuoc", CommandType.StoredProcedure);
            return cmd;
        }
    }
}
