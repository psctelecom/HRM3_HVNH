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

namespace PSC_HRM.Module.NangThamNien
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin chi tiết")]

    [Appearance("Hide_IUH", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    [Appearance("Hide_UTE", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    [Appearance("Hide_LUH", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    [Appearance("Hide_HBU", TargetItems = "TinhTrang;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]
    [Appearance("Hide_DLU", TargetItems = "MaNgachLuong;NgayVaoNganh;PhanLoai;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    [Appearance("Hide_BUH", TargetItems = "MaNgachLuong;SoHieuCongChuc;NgayVaoNganh;PhanLoai;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    
    public class DenHanNangPhuCapThamNien : TruongBaseObject, ISupportController, IBoPhan
    {

        private bool _Chon;
        private string _MaQuanLy;
        private string _SoHieuCongChuc;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private NgachLuong _NgachLuong;
        private string _MaNgachLuong;
        private string _PhanLoai;
        private decimal _ThamNienMoi;
        private decimal _ThamNienCu;
        private DateTime _NgayHuongThamNienCu;
        private DateTime _NgayHuongThamNienMoi;
        private DateTime _NgayVaoNganh;
        private TinhTrang _TinhTrang;


        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

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
                if (!IsLoading && value != null)
                    MaNgachLuong = value.MaQuanLy;
            }
        }

        //[VisibleInDetailView(false)]
        [ModelDefault("Caption", "Mã ngạch lương")]
        public string MaNgachLuong
        {
            get
            {
                return _MaNgachLuong;
            }
            set
            {
                SetPropertyValue("MaNgachLuong", ref _MaNgachLuong, value);
            }
        }
        
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Phân loại")]
        public string PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào ngành")]
        public DateTime NgayVaoNganh
        {
            get
            {
                return _NgayVaoNganh;
            }
            set
            {
                SetPropertyValue("NgayVaoNganh", ref _NgayVaoNganh, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên cũ")]
        public DateTime NgayHuongThamNienCu
        {
            get
            {
                return _NgayHuongThamNienCu;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienCu", ref _NgayHuongThamNienCu, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên cũ")]
        public decimal ThamNienCu
        {
            get
            {
                return _ThamNienCu;
            }
            set
            {
                SetPropertyValue("ThamNienCu", ref _ThamNienCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên mới")]
        public DateTime NgayHuongThamNienMoi
        {
            get
            {
                return _NgayHuongThamNienMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienMoi", ref _NgayHuongThamNienMoi, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên mới")]
        public decimal ThamNienMoi
        {
            get
            {
                return _ThamNienMoi;
            }
            set
            {
                SetPropertyValue("ThamNienMoi", ref _ThamNienMoi, value);
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

        public DenHanNangPhuCapThamNien(Session session)
            : base(session)
        { }

        
    }

}
