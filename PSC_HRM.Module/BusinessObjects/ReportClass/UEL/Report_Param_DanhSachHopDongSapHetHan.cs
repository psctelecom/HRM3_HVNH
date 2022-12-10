using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalEditorState;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DungChung;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [Appearance("Report_Param_DanhSachHopDongSapHetHan", "HinhThucHopDong", Enabled = false, "TatCaHinhThucHopDong", DevExpress.ExpressApp.ViewType.DetailView)]
    public class Report_Param_DanhSachHopDongSapHetHan : ReportParametersObjectBase, PSC_HRM.Module.Security.IBoPhan
    {
        public Report_Param_DanhSachHopDongSapHetHan(Session session) : base(session) { }

        private int _SoNgay;
        private DateTime _TinhDenNgay;
        private bool _TatCaHinhThucHopDong;
        private HinhThucHopDong _HinhThucHopDong;
        private bool _TatCaDonVi;
        private BoPhan _BoPhan;

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

        [ModelDefault("Caption", "Sắp hết hạn trong (ngày)")]
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
        [ModelDefault("Caption", "Tất cả hình thức hợp đồng")]
        public bool TatCaHinhThucHopDong
        {
            get
            {
                return _TatCaHinhThucHopDong;
            }
            set
            {
                SetPropertyValue("TatCaHinhThucHopDong", ref _TatCaHinhThucHopDong, value);
            }
        }

        [ModelDefault("Caption", "Hình thức hợp đồng")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="TatCaHinhThucHopDong")]
        public HinhThucHopDong HinhThucHopDong
        {
            get
            {
                return _HinhThucHopDong;
            }
            set
            {
                SetPropertyValue("HinhThucHopDong", ref _HinhThucHopDong, value);
            }
        }

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
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TatCaDonVi")]
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

        public override CriteriaOperator GetCriteria()
        {
            if (TatCaHinhThucHopDong)
                return CriteriaOperator.Parse("DenNgay>? and DayDiff(TinhDenNgay, DenNgay)=?", TinhDenNgay, SoNgay);
            else
                return CriteriaOperator.Parse("DenNgay>? and HinhThucHopDong=? and DayDiff(TinhDenNgay, DenNgay)=?", TinhDenNgay, HinhThucHopDong, SoNgay);
        }

        public override SortingCollection GetSorting()
        {
            return null;
        }
    }

}
