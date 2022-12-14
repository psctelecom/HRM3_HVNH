using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Report Danh sách nghỉ hưu")]
    [ImageName("BO_Report")]
    public class Report_DanhSachNghiHuu : ReportParametersObjectBase
    {
        public Report_DanhSachNghiHuu(Session session) : 
            base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("TinhTrang.TenTinhTrang=?", "Nghỉ hưu");
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection(new SortProperty("Ten", SortingDirection.Ascending));
            return sorting;
        }
    }

}
