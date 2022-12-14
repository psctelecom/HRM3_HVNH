using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ bị kỷ luật")]
    [ImageName("BO_Report")]
    public class Report_DanhSachCanBoBiKyLuat : ReportParametersObjectBase
    {
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn năm học")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        public Report_DanhSachCanBoBiKyLuat(Session session) : base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            if (NamHoc != null)
            {
                DateTime dt1, dt2;
                dt1 = NamHoc.NgayBatDau;
                dt2 = NamHoc.NgayKetThuc;

                return CriteriaOperator.Parse("NgayHieuLuc Between(?,?)", dt1, dt2);
            }
            return null;
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
