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
    [ModelDefault("Caption", "Báo cáo: Thống kê số lượng cán bộ viên chức theo từng bộ phận công tác")]
    public class Report_ThongKe_SoLuongCanBoVienChucTheoTungBoPhanCongTac : StoreProcedureReport
    {
        public Report_ThongKe_SoLuongCanBoVienChucTheoTungBoPhanCongTac(Session session) : base(session) { }
        private DateTime _DenNgay; 

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] para = new SqlParameter[1];

            if (DenNgay == DateTime.MinValue)
                para[0] = new SqlParameter("@DenNgay", DBNull.Value);
            else
                para[0] = new SqlParameter("@DenNgay", DenNgay);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThongKeSoLuongCanBoVienChucTheoTungBoPhanCongTac", System.Data.CommandType.StoredProcedure, para);
            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            DenNgay = HamDungChung.GetServerTime();
        }
    }
}
