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
    [ModelDefault("Caption", "Báo cáo: Thống kê số lượng, chất lượng đội ngũ viên chức")]
    public class Report_ThongKe_ChatLuongVienChuc : StoreProcedureReport
    {
        // Fields...
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Tính đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
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
        public Report_ThongKe_ChatLuongVienChuc(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DenNgay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@DenNgay", DenNgay);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_BaoCaoSoLuongChatLuongDoiNguVienChuc", System.Data.CommandType.StoredProcedure, parameter);
            return cmd;
        }
    }

}
