using System;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Reports;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public class Report_HopDongLaoDong : ReportParametersObjectBase, PSC_HRM.Module.Security.IBoPhan
    {
        private ThongTinNhanVien _NhanVien;
        private HinhThucHopDong _LoaiHD;
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

        [ModelDefault("Caption", "Hình thức hợp đồng")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn loại hợp đồng")]
        [DataSourceCriteria("NOT(TenHinhThucHopDong LIKE '%lần đầu' OR TenHinhThucHopDong LIKE '%không thời hạn')")]
        public HinhThucHopDong LoaiHD
        {
            get
            {
                return _LoaiHD;
            }
            set
            {
                SetPropertyValue("LoaiHD", ref _LoaiHD, value);
            }
        }

        public Report_HopDongLaoDong(Session session) : 
            base(session) { }
        
        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("ThongTinNhanVien=? AND HinhThucHopDong=?", NhanVien, LoaiHD);
        }

        public override SortingCollection GetSorting()
        {
            return null;
        }
    }

}
