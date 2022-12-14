using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Report danh sách CBVC đang đi học")]
    public class Report_Param_DanhSachCBVCDangDiHoc : ReportParametersObjectBase
    {
        private Trong_NgoaiNuocEnum _Trong_NgoaiNuoc;

        [ModelDefault("Caption", "Trong/ngoài nước")]
        public Trong_NgoaiNuocEnum Trong_NgoaiNuoc
        {
            get { return _Trong_NgoaiNuoc; }
            set { SetPropertyValue<Trong_NgoaiNuocEnum>("Trong_NgoaiNuoc", ref _Trong_NgoaiNuoc, value); }
        }

        public Report_Param_DanhSachCBVCDangDiHoc(Session session) : base(session) { }
        
        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("Trong_NgoaiNuoc=? AND DenNgay>?", Trong_NgoaiNuoc, DateTime.Now);
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
