using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    //[Appearance("Hide_HeSo_HVNH", TargetItems = "HocKy;KyTinhPMS"
    //                                            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'DNU'")]
    [ModelDefault("Caption", "Thanh lý - hợp đồng giảng dạy(thỉnh giảng)")]
    [DefaultProperty("Caption")]
    public class HopDong_ThanhLy_ThinhGiang : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private string _SoHD;
        private NhanVien _NhanVien;
        private bool _DaInHD;

        [ModelDefault("Caption", "Thông tin trường")]
        [Browsable(false)]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
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

        [ModelDefault("Caption", "Số hợp đồng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string SoHD
        {
            get
            {
                return _SoHD;
            }
            set
            {
                SetPropertyValue("SoHD", ref _SoHD, value);
            }
        }

        [ModelDefault("Caption", "Giảng viên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
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

        [ModelDefault("Caption", "Đã in")]
        [Browsable(false)]
        public bool DaInHD
        {
            get
            {
                return _DaInHD;
            }
            set
            {
                SetPropertyValue("DaInHD", ref _DaInHD, value);
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - Năm học  {1}", NhanVien != null ? NhanVien.HoTen : "", NamHoc != null ? NamHoc.TenNamHoc : "");      
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách chi tiết")]
        [Association("HopDong_ThanhLy_ThinhGiang-ListChiTietHDThinhGiang")]
        public XPCollection<ChiTietHDThinhGiang> ListChiTietHDThinhGiang
        {
            get
            {
                return GetCollection<ChiTietHDThinhGiang>("ListChiTietHDThinhGiang");
            }
        }
        public HopDong_ThanhLy_ThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }       
    }
}
