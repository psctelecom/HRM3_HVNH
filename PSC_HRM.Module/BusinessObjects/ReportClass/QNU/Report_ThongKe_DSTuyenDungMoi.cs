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
    [ModelDefault("Caption", "Báo cáo: Thống Kê Danh Sách Tuyển Dụng Mới")]
    public class Report_ThongKe_DSTuyenDungMoi : StoreProcedureReport
    {
          private int _DenNgay = DateTime.Today.Year;

        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public Report_ThongKe_DSTuyenDungMoi(Session session) : base(session) { }
        
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            //DenNgay = HamDungChung.GetServerTime();  
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@DenNgay", DenNgay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_DSTuyenDungMoi", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
