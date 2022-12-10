using System;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.KhenThuong;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Danh sách thi đua khen thưởng theo quyết định")]
    public class Report_Param_DanhSachThiDuaKhenThuongTheoQuyetDinh : ReportParametersObjectBase
    {
        public Report_Param_DanhSachThiDuaKhenThuongTheoQuyetDinh(Session session) : base(session) { }
        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("NamHoc=? and DanhHieu=?", NamHoc, DanhHieuKhenThuong);
        }
        public override SortingCollection GetSorting()
        {
            return null;
        }

        // Fields...
        private DanhHieuThiDuaKhenThuong _DanhHieuKhenThuong;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Danh hiệu")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DanhHieuThiDuaKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
            }
        }
    }

}
