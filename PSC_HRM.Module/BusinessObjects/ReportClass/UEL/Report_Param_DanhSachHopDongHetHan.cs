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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ImageName("BO_Report")]
    [Appearance("Report_Param_DanhSachHopDongHetHan.HinhThucHopDong", "HinhThucHopDong", Enabled = false, "TatCaHinhThucHopDong", DevExpress.ExpressApp.ViewType.DetailView)]
    [Appearance("Report_Param_DanhSachHopDongHetHan.BoPhan", "BoPhan", Enabled = false, "TatCaDonVi", DevExpress.ExpressApp.ViewType.DetailView)]
    public class Report_Param_DanhSachHopDongHetHan : ReportParametersObjectBase, IBoPhan
    {
        public Report_Param_DanhSachHopDongHetHan(Session session) : base(session) { }

        private bool _TatCaHinhThucHopDong;
        private HinhThucHopDong _HinhThucHopDong;
        private bool _TatCaDonVi;
        private BoPhan _BoPhan;
        
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
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="!TatCaHinhThucHopDong")]
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
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria="!TatCaDonVi")]
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
                return CriteriaOperator.Parse("DenNgay>?", HamGetServerTime());
            else
                return CriteriaOperator.Parse("DenNgay>? and HinhThucHopDong=?", HamGetServerTime(), HinhThucHopDong);
        }

        public override SortingCollection GetSorting()
        {
            return null;
        }
    }

}
