using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System.Data;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Truy lĩnh lương")]
    [Appearance("BangLuong.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_TruyLinh_TruyLinhLuong : StoreProcedureReport
    {
        // Fields...

        private KyTinhLuong _KyTinhLuong;
        private ThongTinNhanVien _NhanVien;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }


        public Report_TruyLinh_TruyLinhLuong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_TruyLinh_TruyLinhLuong",
             System.Data.CommandType.StoredProcedure, new SqlParameter("@KyTinhLuong",KyTinhLuong.Oid), new SqlParameter("@NhanVien", NhanVien.Oid));

            return cmd;
        }

      

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", DateTime.Now.Month, DateTime.Now.Year));
        }

       
    }

}
