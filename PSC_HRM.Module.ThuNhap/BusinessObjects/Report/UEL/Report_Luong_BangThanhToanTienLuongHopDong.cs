using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng thanh toan tiền lương hợp đồng")]
    public class Report_Luong_BangThanhToanTienLuongHopDong : StoreProcedureReport
    {
        // Fields...
        private ChungTu.ChungTu _ChungTu;

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        public Report_Luong_BangThanhToanTienLuongHopDong(Session session) : base(session) { }
        
        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_Luong_BangThanhToanTienLuongHopDong", 
                System.Data.CommandType.StoredProcedure,
                new SqlParameter("@ChungTu", ChungTu.Oid));
            return cmd;
        }
    }

}
