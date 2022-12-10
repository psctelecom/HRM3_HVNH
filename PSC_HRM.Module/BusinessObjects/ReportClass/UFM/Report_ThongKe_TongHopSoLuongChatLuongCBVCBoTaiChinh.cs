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
    [ModelDefault("Caption", "Báo cáo: Tổng hợp số lượng, chất lượng viên chức bộ Tài chính")]
    [Appearance("Report_DanhSach_BaoCaoSoLuongChatLuongVienChucTheoNam.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaDonVi")]
    public class Report_ThongKe_TongHopSoLuongChatLuongCBVCBoTaiChinh : StoreProcedureReport, IBoPhan
    {
        // Fields...
        private BoPhan _BoPhan;
        private bool _TatCaDonVi = true;
        private DateTime _DenNgay;

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

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
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

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        private void UpdateBoPhanList()
        {
            if (BoPhanList == null)
                BoPhanList = new XPCollection<BoPhan>(Session);
        }

        public Report_ThongKe_TongHopSoLuongChatLuongCBVCBoTaiChinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DenNgay = HamDungChung.GetServerTime();
        }

        public override SqlCommand CreateCommand()
        {
            List<string> lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);

            StringBuilder sb = new StringBuilder();
            foreach (string item in lstBP)
            {
                sb.Append(String.Format("{0},", item));
            }

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_DanhSach_BaoCaoSoLuongChatLuongVienChucBoTaiChinh",
                System.Data.CommandType.StoredProcedure,

                new SqlParameter("@BoPhanDuocPhanQuyenList", sb.ToString()),
                new SqlParameter("@DenNgay", DenNgay),
                new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty));
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
