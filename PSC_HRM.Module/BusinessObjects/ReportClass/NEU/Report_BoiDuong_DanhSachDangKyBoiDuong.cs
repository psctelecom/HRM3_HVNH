using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BoiDuong;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách đăng ký bồi dưỡng")]
    public class Report_BoiDuong_DanhSachDangKyBoiDuong : StoreProcedureReport
    {
        // Fields...        
        private DangKyBoiDuong _DangKyBoiDuong;

        [ModelDefault("Caption", "Quyết định bồi dưỡng")]
        public DangKyBoiDuong DangKyBoiDuong
        {
            get
            {
                return _DangKyBoiDuong;
            }
            set
            {
                SetPropertyValue("DangKyBoiDuong", ref _DangKyBoiDuong, value);               
            }
        }

        public Report_BoiDuong_DanhSachDangKyBoiDuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@DangKyBoiDuong", DangKyBoiDuong != null ? DangKyBoiDuong.Oid : Guid.Empty);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BoiDuong_DanhSachDangKyBoiDuong", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
     
           
        }
    }

}
