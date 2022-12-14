using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ModelDefault("Caption", "Báo cáo - Sơ yếu lý lịch")]
    public class Report_SoYeuLyLich : StoreProcedureReport, IBoPhan
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                    ThongTinNhanVien = null;
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        public Report_SoYeuLyLich(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter param = new SqlParameter("@Oid", ThongTinNhanVien.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_SoYeuLyLich", System.Data.CommandType.StoredProcedure, param);
            return cmd;
        }
    }

}
