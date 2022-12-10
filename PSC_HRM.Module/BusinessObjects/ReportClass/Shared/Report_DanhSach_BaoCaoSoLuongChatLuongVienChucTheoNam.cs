using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Báo cáo số lượng, chất lượng viên chức theo năm")]
    [Appearance("Report_DanhSach_BaoCaoSoLuongChatLuongVienChucTheoNam.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_DanhSach_BaoCaoSoLuongChatLuongVienChucTheoNam : StoreProcedureReport, IBoPhan
    {
        // Fields...
        private BoPhan _BoPhan;
        private bool _TatCaDonVi = true;
        private int _Nam;
       
        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả đơn vị")]
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

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCaDonVi")]
        [DataSourceProperty("BoPhanList", DataSourcePropertyIsNullMode.SelectAll)]
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

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        private void UpdateBoPhanList()
        {
            if (BoPhanList == null)
                BoPhanList = new XPCollection<BoPhan>(Session);

        }

        public Report_DanhSach_BaoCaoSoLuongChatLuongVienChucTheoNam(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = HamDungChung.GetServerTime().Year;
        }

        public override SqlCommand CreateCommand()
        {
            List<string> lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in lstBP)
            {
                sb.Append(String.Format("{0},", item));
            }

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DanhSach_BaoCaoSoLuongChatLuongVienChucTheoNam",
                System.Data.CommandType.StoredProcedure,

                new SqlParameter("@BoPhanDuocPhanQuyenList", sb.ToString()),
                new SqlParameter("@Nam", Nam),
                new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty));
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }

}
