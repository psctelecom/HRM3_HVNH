using System;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public class Report_Param_QuyetDinhDaoTao : ReportParametersObjectBase
    {
        public enum DieuKienEnum
        {
            [DevExpress.Xpo.DisplayName("Lớn hơn hoặc bằng 3 tháng")]
            LonHon3Thang = 0,
            [DevExpress.Xpo.DisplayName("Nhỏ hơn 3 tháng")]
            NhoHon3Thang = 1
        }

        private DateTime _TruocThang;
        private Trong_NgoaiNuocEnum _TrongNgoaiNuoc;
        private DieuKienEnum _DieuKien;

        [ModelDefault("Caption", "Trước tháng")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập thời gian")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TruocThang
        {
            get
            {
                return _TruocThang;
            }
            set
            {
                SetPropertyValue("TruocThang", ref _TruocThang, value);
            }
        }

        [ModelDefault("Caption", "Trong/ngoài nước")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn Trong/ngoài nước")]
        public Trong_NgoaiNuocEnum TrongNgoaiNuoc
        {
            get
            {
                return _TrongNgoaiNuoc;
            }
            set
            {
                SetPropertyValue("TrongNgoaiNuoc", ref _TrongNgoaiNuoc, value);
            }
        }

        [ModelDefault("Caption", "Điều kiện")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn điều kiện")]
        public DieuKienEnum DieuKien
        {
            get
            {
                return _DieuKien;
            }
            set
            {
                SetPropertyValue("DieuKien", ref _DieuKien, value);
            }
        }

        public Report_Param_QuyetDinhDaoTao(Session session) :
            base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            if (DieuKien == DieuKienEnum.LonHon3Thang)
                return CriteriaOperator.Parse("ThoiGianDaoTao >= 3 AND TuNgay < ? AND Trong_NgoaiNuoc=?", TruocThang, TrongNgoaiNuoc);
            return CriteriaOperator.Parse("ThoiGianDaoTao < 3 AND TuNgay < ? AND Trong_NgoaiNuoc=?", TruocThang, TrongNgoaiNuoc);
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TruocThang = DateTime.Today;
            TrongNgoaiNuoc = Trong_NgoaiNuocEnum.TrongNuoc;
            DieuKien = DieuKienEnum.LonHon3Thang;
        }
    }

}
