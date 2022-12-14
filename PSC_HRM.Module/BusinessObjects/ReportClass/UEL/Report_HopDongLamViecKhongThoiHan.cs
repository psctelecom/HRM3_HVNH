using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Hợp đồng làm việc không thời hạn")]
    public class Report_HopDongLamViecKhongThoiHan : ReportParametersObjectBase, IBoPhan
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
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        public Report_HopDongLamViecKhongThoiHan(Session session) : base(session) { }
        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("ThongTinNhanVien=? AND HinhThucHopDong.TenHinhThucHopDong LIKE '%không%thời hạn'", NhanVien);
        }
        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
