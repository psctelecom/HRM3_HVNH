using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Quá trình tham gia BHXH")]
    [ImageName("BO_Report")]
    [Appearance("Report_QuaTrinhThamGiaBHXH.TatCaDonVi", TargetItems =  "BoPhan;TatCaNhanVien;NhanVien", Enabled = false, Criteria = "TatCaDonVi")]
    [Appearance("Report_QuaTrinhThamGiaBHXH.TatCaNhanVien", TargetItems =  "NhanVien", Enabled = false, Criteria = "TatCaNhanVien")]
    public class Report_QuaTrinhThamGiaBHXH : ReportParametersObjectBase, IBoPhan
    {
        private bool _TatCaDonVi = true;
        private BoPhan _BoPhan;
        private bool _TatCaNhanVien = true;
        private ThongTinNhanVien _NhanVien;

        [ModelDefault("Caption", "Tất cả đơn vị")]
        [ImmediatePostData]
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

        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn đơn vị", TargetCriteria="!TatCaDonVi")]
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

        [ModelDefault("Caption", "Tất cả cán bộ")]
        [ImmediatePostData]
        public bool TatCaNhanVien
        {
            get
            {
                return _TatCaNhanVien;
            }
            set
            {
                SetPropertyValue("TatCaNhanVien", ref _TatCaNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn cán bộ", TargetCriteria="!TatCaNhanVien")]
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

        public Report_QuaTrinhThamGiaBHXH(Session session) : base(session) { }

        public override CriteriaOperator GetCriteria()
        { 
            if(TatCaDonVi)
            {
                return new InOperator("BoPhan", HamGetCriteriaBoPhan(Session, null));
            }
            else
            {
                if (TatCaNhanVien)
                {
                    return new InOperator("BoPhan", HamGetCriteriaBoPhan(Session, BoPhan));
                }
                else
                    return CriteriaOperator.Parse("Oid=?", NhanVien.Oid);
            }
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
