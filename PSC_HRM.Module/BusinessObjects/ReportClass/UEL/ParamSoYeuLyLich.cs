using System;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public class ParamSoYeuLyLich : ReportParametersObjectBase, PSC_HRM.Module.Security.IBoPhan
    {
        public ParamSoYeuLyLich(Session session) : base(session) { }
        public override CriteriaOperator GetCriteria()
        {
            if (ThongTinNhanVien != null)
                return CriteriaOperator.Parse("Oid=?", ThongTinNhanVien.Oid);
            else
                return null;

        }
        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            sorting.Add(new SortProperty("Ten", SortingDirection.Ascending));
            return sorting;

        }
        private BoPhan _BoPhan;
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                _BoPhan=value;
            }
        }
        private ThongTinNhanVien _ThongTinNhanVien;
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        [ModelDefault("Caption", "Thông tin cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                _ThongTinNhanVien = value;
            }
        }
    }

}
