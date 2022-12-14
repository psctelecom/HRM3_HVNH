using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách đề nghị nâng ngạch")]
    [ImageName("BO_Report")]
    public class Report_DanhSachDeNghiNangNgach : ReportParametersObjectBase
    {
        private int _Nam = DateTime.Today.Year;

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

        public Report_DanhSachDeNghiNangNgach(Session session) : base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("Nam=? AND TruongHoc=?", Nam, HamDungChung.TruongHoc(Session).Oid);
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
