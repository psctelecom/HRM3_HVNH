using System;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption", "Báo cáo - Sơ yếu lý lịch 2")]
    public class Report_Param_SoYeuLyLich1 : ReportParametersObjectBase, PSC_HRM.Module.Security.IBoPhan
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        public Report_Param_SoYeuLyLich1(Session session) : base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            if (ThongTinNhanVien != null)
                return CriteriaOperator.Parse("ThongTinNhanVien.Oid = ?", ThongTinNhanVien.Oid);
            else
                return null;
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ Phận")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                _BoPhan = value;
            }
        }

        [ModelDefault("Caption", "Họ tên Nhân viên")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get { return _ThongTinNhanVien; }
            set { _ThongTinNhanVien = value; }
        }       
    }

}
