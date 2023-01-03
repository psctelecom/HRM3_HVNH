using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê trình độ tổng kết năm học")]
    public class Report_ThongKe_SoLuongCBVC : StoreProcedureReport
    {
        // Fields...
       

        public Report_ThongKe_SoLuongCBVC(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ///Ngay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[0];
            //parameter[0] = new SqlParameter("@Ngay", Ngay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_SoLuongCBTongKetNamHoc", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
