using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ModelDefault("Caption", "Báo cáo: Sơ yếu lý lịch")]
    public class Report_SoYeuLyLich : StoreProcedureReport, IBoPhan
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        public Report_SoYeuLyLich(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter param = new SqlParameter("@Oid", ThongTinNhanVien.Oid);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_SoYeuLyLich", 
                System.Data.CommandType.StoredProcedure, 
                param);
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
