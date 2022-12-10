using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo: Quá trình bổ nhiệm")]
    [Appearance("QuaTrinhBoNhiem.TatCaDonVi", TargetItems = "BoPhan;TatCaCanBo;ThongTinNhanVien", Enabled = false, Criteria = "TatCaDonVi")]
    [Appearance("QuaTrinhBoNhiem.TatCaCanBo", TargetItems = "ThongTinNhanVien", Enabled = false, Criteria = "TatCaCanBo")]
    public class Report_SoYeuLyLich_QuaTrinhBoNhiem : StoreProcedureReport, IBoPhan
    {
        private BoPhan _BoPhan;
        private bool _TatCaCanBo = true;
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool _TatCaDonVi = true;

        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData]
        public bool TatCaDonVi
        {
            get
            {
                return _TatCaDonVi;
            }
            set
            {
                SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value);
            }
        }

         [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaDonVi")]
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
        [ModelDefault("Caption", "Tất cả cán bộ")]
        public bool TatCaCanBo
        {
            get
            {
                return _TatCaCanBo;
            }
            set
            {
                SetPropertyValue("TatCaCanBo", ref _TatCaCanBo, value);
            }
        }

         [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaCanBo")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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

        public Report_SoYeuLyLich_QuaTrinhBoNhiem(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[2];
            if (!TatCaDonVi)
                param[0] = new SqlParameter("@BoPhan", BoPhan.Oid);
            else
                param[0] = new SqlParameter("@BoPhan", DBNull.Value);
            if (!TatCaCanBo)
                param[1] = new SqlParameter("@ThongTinNhanVien", ThongTinNhanVien.Oid);
            else
                param[1] = new SqlParameter("@ThongTinNhanVien", DBNull.Value);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_SoYeuLyLich_QuaTrinhBoNhiem", System.Data.CommandType.StoredProcedure, param);
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
