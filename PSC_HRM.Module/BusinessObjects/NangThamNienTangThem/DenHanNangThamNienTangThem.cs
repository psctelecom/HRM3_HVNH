using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.NangThamNienTangThem
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin chi tiết")]

    //[Appearance("Hide_IUH", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    //[Appearance("Hide_UTE", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    //[Appearance("Hide_LUH", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    //[Appearance("Hide_HBU", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]
    //[Appearance("Hide_DLU", TargetItems = "MaNgachLuong;NgayVaoNganh;PhanLoai;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    [Appearance("Hide_BUH", TargetItems = "SoHieuCongChuc;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    
    public class DenHanNangThamNienTangThem : TruongBaseObject, ISupportController, IBoPhan
    {
        private string _MaQuanLy;
        private string _SoHieuCongChuc;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private NgachLuong _NgachLuong;
        private HSLTangThemTheoThamNien _HSLTangThemTheoThamNienCu;
        private HSLTangThemTheoThamNien _HSLTangThemTheoThamNienMoi;
        private DateTime _MocHuongThamNienTangThemCu;
        private DateTime _MocHuongThamNienTangThemMoi;
        private TinhTrang _TinhTrang;

        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Số hiệu công chức")]
        public string SoHieuCongChuc
        {
            get
            {
                return _SoHieuCongChuc;
            }
            set
            {
                SetPropertyValue("SoHieuCongChuc", ref _SoHieuCongChuc, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        //[VisibleInListView(false)]
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Mốc hưởng TNTT cũ")]
        public DateTime MocHuongThamNienTangThemCu
        {
            get
            {
                return _MocHuongThamNienTangThemCu;
            }
            set
            {
                SetPropertyValue("MocHuongThamNienTangThemCu", ref _MocHuongThamNienTangThemCu, value);

            }
        }

        [ModelDefault("Caption", "Hệ số TNTT cũ")]
        public HSLTangThemTheoThamNien HSLTangThemTheoThamNienCu
        {
            get
            {
                return _HSLTangThemTheoThamNienCu;
            }
            set
            {
                SetPropertyValue("HSLTangThemTheoThamNienCu", ref _HSLTangThemTheoThamNienCu, value);

            }
        }

        [ModelDefault("Caption", "Mốc tính TNTT mới")]
        public DateTime MocHuongThamNienTangThemMoi
        {
            get
            {
                return _MocHuongThamNienTangThemMoi;
            }
            set
            {
                SetPropertyValue("MocHuongThamNienTangThemMoi", ref _MocHuongThamNienTangThemMoi, value);

            }
        }

        [ModelDefault("Caption", "Hệ số TNTT mới")]
        public HSLTangThemTheoThamNien HSLTangThemTheoThamNienMoi
        {
            get
            {
                return _HSLTangThemTheoThamNienMoi;
            }
            set
            {
                SetPropertyValue("HSLTangThemTheoThamNienMoi", ref _HSLTangThemTheoThamNienMoi, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        public DenHanNangThamNienTangThem(Session session)
            : base(session)
        { }

        
    }

}
