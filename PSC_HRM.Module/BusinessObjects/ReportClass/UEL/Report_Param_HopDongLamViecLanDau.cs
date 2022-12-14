using System;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public class Report_Param_HopDongLamViecLanDau : ReportParametersObjectBase, PSC_HRM.Module.Security.IBoPhan
    {
        private ThongTinNhanVien _NhanVien;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue<BoPhan>("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn nhân viên")]
        public ThongTinNhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue<ThongTinNhanVien>("NhanVien", ref _NhanVien, value);
            }
        }

        public Report_Param_HopDongLamViecLanDau(Session session) : base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("ThongTinNhanVien=? AND HinhThucHopDong.TenHinhThucHopDong LIKE '%lần đầu%'", NhanVien);
        }

        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
