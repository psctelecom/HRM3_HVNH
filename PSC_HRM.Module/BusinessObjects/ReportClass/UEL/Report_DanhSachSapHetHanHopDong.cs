using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Danh sách cán bộ sắp hết hạn hợp đồng")]
    [Appearance("Report_DanhSachSapHetHanHopDong.DonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Report_DanhSachSapHetHanHopDong : StoreProcedureReport, IBoPhan
    {
        // Fields...
        private BoPhan _BoPhan;
        private bool _TatCaBoPhan = true;
        private int _SoNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Sắp hết hạn trong")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int SoNgay
        {
            get
            {
                return _SoNgay;
            }
            set
            {
                SetPropertyValue("SoNgay", ref _SoNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả đơn vị")]
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
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="!TatCaBoPhan")]
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

        public Report_DanhSachSapHetHanHopDong(Session session) : base(session) { }

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

            SqlCommand cmd = new SqlCommand("spd_Report_DanhSachSapHetHanHopDong", (SqlConnection)Session.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TuNgay", HamDungChung.SetTime(DenNgay.AddDays(-SoNgay), 0));
            cmd.Parameters.AddWithValue("@DenNgay", HamDungChung.SetTime(DenNgay, 1));
            cmd.Parameters.AddWithValue("@BoPhan", sb.ToString());

            return cmd;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DenNgay = HamDungChung.GetServerTime();
            SoNgay = 30;
        }
    }

}
