using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.Report;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê danh sách tuyển dụng")]
    public class Report_ThongKe_DanhSachTuyenDung : StoreProcedureReport
    {
        public Report_ThongKe_DanhSachTuyenDung(Session session) : base(session) { }
        private int _TuNam = DateTime.Today.Year;
        //private int _DenNam = DateTime.Today.Year;

        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }
        //[ModelDefault("Caption", "Đến năm")]
        //[ModelDefault("EditMask", "####")]
        //[ModelDefault("DisplayFormat", "####")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        //public int DenNam
        //{
        //    get
        //    {
        //        return _DenNam;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DenNam", ref _DenNam, value);
        //    }
        //}
     

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            
            using (SqlDataAdapter da = new SqlDataAdapter("spd_ThongKeTuyenDung", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@TuNam", TuNam);
                //da.SelectCommand.Parameters.AddWithValue("@DenNam", DenNam);
                da.Fill(DataSource);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
    }
}
