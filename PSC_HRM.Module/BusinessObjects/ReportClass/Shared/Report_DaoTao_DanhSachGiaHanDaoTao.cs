using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách gia hạn đào tạo")]
    public class Report_DaoTao_DanhSachGiaHanDaoTao : StoreProcedureReport
    {
        // Fields...
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ tháng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
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

        public Report_DaoTao_DanhSachGiaHanDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNgay = HamDungChung.GetServerTime().Date;
            DenNgay = TuNgay;
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DaoTao_DanhSachGiaHanDaoTao",
                 System.Data.CommandType.StoredProcedure,
                 new SqlParameter("@TuNgay", TuNgay),
                 new SqlParameter("@DenNgay", DenNgay));

            return cmd;
        }
    }

}
