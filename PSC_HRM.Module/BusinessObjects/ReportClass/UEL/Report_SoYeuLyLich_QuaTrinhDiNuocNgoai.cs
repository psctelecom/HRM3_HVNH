using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Quá Trình Đi công Tác Nước Ngoài")]
    public class Report_SoYeuLyLich_QuaTrinhDiNuocNgoai : StoreProcedureReport, IBoPhan
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

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
                {
                    UpdateNVList();
                    ThongTinNhanVien = null;
                }
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList")]
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

        public Report_SoYeuLyLich_QuaTrinhDiNuocNgoai(Session session) : base(session) { }
        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_SoYeuLyLich_QuaTrinhDiNuocNgoai", System.Data.CommandType.StoredProcedure, new SqlParameter("@ThongTinNhanVien", ThongTinNhanVien.Oid));
            return cmd;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = PSC_HRM.Module.HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
