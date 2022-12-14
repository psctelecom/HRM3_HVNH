using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public class Report_Param_DanhSachCBVCCuDiDaoTao : ReportParametersObjectBase
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get { return _TuNgay; }
            set { SetPropertyValue("TuNgay", ref _TuNgay, value); }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get { return _DenNgay; }
            set { SetPropertyValue("DenNgay", ref _DenNgay, value); }
        }

        public Report_Param_DanhSachCBVCCuDiDaoTao(Session session) : base(session) { }
        public override CriteriaOperator GetCriteria()
        {
            if (DateTime.Compare(TuNgay, DateTime.MinValue) > 0 &&
                DateTime.Compare(DenNgay, DateTime.MinValue) > 0)
                return CriteriaOperator.Parse("TuNgay>=? AND TuNgay<=?", TuNgay, DenNgay);
            else if (DateTime.Compare(TuNgay, DateTime.MinValue) > 0)
                return CriteriaOperator.Parse("TuNgay>=?", TuNgay);
            else if (DateTime.Compare(DenNgay, DateTime.MinValue) > 0)
                return CriteriaOperator.Parse("TuNgay<=?", DenNgay);
            else
                return CriteriaOperator.Parse("");
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
