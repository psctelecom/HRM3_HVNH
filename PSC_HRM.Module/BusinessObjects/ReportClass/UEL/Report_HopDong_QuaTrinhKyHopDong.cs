using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Reports;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Quá trình ký hợp đồng")]
    public class Report_HopDong_QuaTrinhKyHopDong : StoreProcedureReport, IBoPhan
    {
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
                SetPropertyValue<BoPhan>("BoPhan", ref _BoPhan, value);
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

        public Report_HopDong_QuaTrinhKyHopDong(Session session) :
            base(session) { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HopDong_QuaTrinhKyHopDong", System.Data.CommandType.StoredProcedure, new SqlParameter("@ThongTinNhanVien", ThongTinNhanVien.Oid));
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
