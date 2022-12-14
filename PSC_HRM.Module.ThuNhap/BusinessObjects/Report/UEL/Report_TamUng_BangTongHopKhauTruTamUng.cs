using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;


namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp khấu trừ tạm ứng")]
    public class Report_TamUng_BangTongHopKhauTruTamUng : StoreProcedureReport
    {
        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        public Report_TamUng_BangTongHopKhauTruTamUng(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            Nam = current.Year;
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TamUng_BangTongHopKhauTruTamUng", System.Data.CommandType.StoredProcedure, new SqlParameter("@Nam", Nam));
            return cmd;
        }
    }

}
