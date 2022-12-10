using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Thông tin quyết định")]
    public class RptDanhSachQuyetDinh : ReportParametersObjectBase
    {
        public RptDanhSachQuyetDinh(Session session) : base(session) { }

        private string _SoQuyetDinh;
        [ModelDefault("Caption", "Số quyết định")]
        [RuleRequiredField("", DefaultContexts.Save, "Phải nhập số quyết định.")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            sorting.Add(new SortProperty("Ten", SortingDirection.Ascending));
            return sorting;

        }

        public override CriteriaOperator GetCriteria()
        {
            QuyetDinh.QuyetDinh quyetDinh = Session.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh like ?", SoQuyetDinh));
            if(quyetDinh != null)
                return CriteriaOperator.Parse("Oid=?", quyetDinh.Oid);
            else
                return CriteriaOperator.Parse("Oid=?", null);
        }
        //public override System.Data.SqlClient.SqlCommand CreateCommand()
        //{
        //    SqlCommand cm = new SqlCommand("spd_Report_QuyetDinh");
        //    cm.CommandType = System.Data.CommandType.StoredProcedure;
        //    cm.Parameters.AddWithValue("@Nam", 0);

        //    return cm;
        //}
    }

}
