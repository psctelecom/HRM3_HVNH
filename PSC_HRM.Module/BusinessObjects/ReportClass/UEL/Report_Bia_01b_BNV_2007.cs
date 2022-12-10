using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Bìa 01b/BNV-2007")]
    public class Report_Bia_01b_BNV_2007 : StoreProcedureReport, IBoPhan
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        public Report_Bia_01b_BNV_2007(Session session) : base(session) { }
                
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

        public override SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_Report_Bia01B_BNV_2007");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@ThongTinNhanVien", ThongTinNhanVien.Oid);

            return cm;
        }
    }

}
