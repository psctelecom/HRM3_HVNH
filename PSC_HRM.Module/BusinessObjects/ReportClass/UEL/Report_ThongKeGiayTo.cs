using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.HoSo;
using System.Collections.Generic;
using System.Text;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Thống kê cán bộ thiếu giấy tờ hồ sơ")]
    [Appearance("Report_ThongKeGiayTo.TatCaBoPhan", TargetItems = "BoPhan;TatCaCanBo;ThongTinNhanVien", Enabled = false, Criteria = "TatCaBoPhan")]
    [Appearance("Report_ThongKeGiayTo.TatCaCanBo", TargetItems = "ThongTinNhanVien", Enabled = false, Criteria = "TatCaCanBo")]
    public class Report_ThongKeGiayTo : StoreProcedureReport, IBoPhan
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool _TatCaCanBo = true;
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;

        public Report_ThongKeGiayTo(Session session) : base(session) { }

        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData()]
        public bool TatCaBoPhan
        {
            get
            {
                return _TatCaBoPhan;
            }
            set
            {
                SetPropertyValue("TatCaBoPhan", ref _TatCaBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TatCaBoPhan")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
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

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="!TatCaCanBo")]
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

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = new SqlCommand("spd_Report_DanhSachCanBoThieuGiayTo");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (TatCaBoPhan)
                cmd.Parameters.AddWithValue("@BoPhan", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            if (TatCaCanBo)
                cmd.Parameters.AddWithValue("@ThongTinNhanVien", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ThongTinNhanVien", ThongTinNhanVien.Oid);

            return cmd;
        }
    }

}
