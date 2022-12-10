using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Danh sách nhân viên hết hạn đào tạo")]
    public class Report_DaoTao_DanhSachHetHanDaoTao : StoreProcedureReport
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
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
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

        public Report_DaoTao_DanhSachHetHanDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            DenNgay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_DaoTao_DanhSachNhanVienHetHanDaoTao");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;          
            cmd.Parameters.AddWithValue("@DenNgay", DenNgay);

            return cmd;
        }
    }

}
