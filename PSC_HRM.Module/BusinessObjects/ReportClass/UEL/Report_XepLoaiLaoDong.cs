using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using System.Collections.Generic;
using System.Text;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Đánh giá cán bộ lần 1")]
    [Appearance("Report_XepLoaiLaoDong.TatCaBoPhan", TargetItems =  "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Report_XepLoaiLaoDong : StoreProcedureReport, IBoPhan
    {
        private bool _TatCaBoPhan = true;
        private BoPhan _BoPhan;
        private DateTime _Thang = DateTime.Today;

        public Report_XepLoaiLaoDong(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        public override SqlCommand CreateCommand()
        {
            List<string> lstBP;
            if (TatCaBoPhan)
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen();
            else
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in lstBP)
            {
                sb.Append(String.Format("{0},", item));
            }

            SqlCommand cm = new SqlCommand("spd_Report_XepLoaiLaoDong1");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@BoPhan", sb.ToString());
            cm.Parameters.AddWithValue("@ThoiGian", Thang);

            return cm;
        }
    }

}
