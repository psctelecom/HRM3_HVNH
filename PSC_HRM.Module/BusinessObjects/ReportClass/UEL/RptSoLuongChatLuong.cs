using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo số lượng, chất lượng công chức")]
    [Appearance("RptSoLuongChatLuong.TatCaBoPhan", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class RptSoLuongChatLuong : StoreProcedureReport, IBoPhan
    {
        public RptSoLuongChatLuong(Session session) : base(session) { }

        private bool _TatCaBoPhan = true;
        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData()] //Load dữ liệu ngay lập tức
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

        private BoPhan _BoPhan;
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

        public enum MauBaoCaoEnum
        {
            [DevExpress.Xpo.DisplayName("Theo mẫu chuẩn cho cán bộ, công chức (Mẫu 7)")]
            Chuan = 0,
            [DevExpress.Xpo.DisplayName("Theo mẫu cho Hợp đồng nghị định 68 (Mẫu 7a)")]
            NghiDinh = 1
        }

        private MauBaoCaoEnum _MauBaoCao;
        [ModelDefault("Caption", "Mẫu báo cáo")]
        public MauBaoCaoEnum MauBaoCao
        {
            get
            {
                return _MauBaoCao;
            }
            set
            {
                SetPropertyValue("MauBaoCao", ref _MauBaoCao, value);
            }
        }

        private DateTime _TinhDenNgay = DateTime.Today;
        [ModelDefault("Caption", "Tính đến ngày")]
        public DateTime TinhDenNgay
        {
            get
            {
                return _TinhDenNgay;
            }
            set
            {
                SetPropertyValue("TinhDenNgay", ref _TinhDenNgay, value);
            }
        }

        public override SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_Report_SoLuongChatLuongCanBo");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            if (MauBaoCao == MauBaoCaoEnum.Chuan)
                cm.Parameters.AddWithValue("@LoaiBaoCao", 1);
            else
                cm.Parameters.AddWithValue("@LoaiBaoCao", 0);
            if (BoPhan != null)
                cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@BoPhan", DBNull.Value);
            return cm;
        }
    }

}
