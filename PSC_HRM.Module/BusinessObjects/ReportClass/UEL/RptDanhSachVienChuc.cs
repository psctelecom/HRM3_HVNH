using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo danh sách viên chức")]
    [Appearance("RptDanhSachVienChuc.TatCaBoPhan", Enabled = false, Criteria = "TatCaBoPhan", TargetItems ="BoPhan")]
    public class RptDanhSachVienChuc : StoreProcedureReport, IBoPhan
    {
        public RptDanhSachVienChuc(Session session) : base(session) { }

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
            [DevExpress.Xpo.DisplayName("Theo mẫu chuẩn cho cán bộ, công chức (Mẫu 3)")]
            Chuan = 0,
            [DevExpress.Xpo.DisplayName("Theo mẫu cho Hợp đồng nghị định 68 (Mẫu 3a)")]
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
        [RuleRequiredField("", DefaultContexts.Save)]
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

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            SqlCommand cm;
            if(MauBaoCao == MauBaoCaoEnum.Chuan)
                cm = new SqlCommand("spd_DanhSachVienChuc");
            else
                cm = new SqlCommand("spd_DanhSachVienChuc_Mau3a");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            if (BoPhan != null)
                cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            else
                cm.Parameters.AddWithValue("@BoPhan", DBNull.Value);

            return cm;
        }
    }

}
