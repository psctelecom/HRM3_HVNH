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
    [ModelDefault("Caption", "Báo cáo: Thống Kê Tuyển Dụng Nhân Sự")]
    public class Report_ThongKe_TuyenDungNhanSu : StoreProcedureReport
    {
        // Fields...
        private NamHoc _NamHoc;
        private decimal _DotTuyenDung;

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
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Đợt tuyển dụng nhân sự")]
        public decimal DotTuyenDung
        {
            get
            {
                return _DotTuyenDung;
            }
            set
            {
                SetPropertyValue("DotTuyenDung", ref _DotTuyenDung, value);
            }
        }

        public Report_ThongKe_TuyenDungNhanSu(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKe_TuyenDungNhanSu",
             System.Data.CommandType.StoredProcedure, new SqlParameter("@NamHoc", NamHoc.Oid), new SqlParameter("@DotTuyenDung", DotTuyenDung));

            return cmd;
        }

       

        public override void AfterConstruction()
        {
            base.AfterConstruction();
     
           
        }
    }

}
