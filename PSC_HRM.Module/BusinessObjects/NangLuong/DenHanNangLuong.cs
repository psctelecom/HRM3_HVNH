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

namespace PSC_HRM.Module.NangLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin chi tiết")]
    public class DenHanNangLuong : TruongBaseObject, ISupportController, IBoPhan
    {
        private string _MaQuanLy;
        private string _SoHieuCongChuc;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _MocNangLuongCu;
        private DateTime _MocNangLuongDieuChinh;
        private DateTime _NgayHuongLuongCu;
        private NgachLuong _NgachLuong;
        private string _MaNgachLuong;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private int _PhanTramVuotKhungCu;
        private DateTime _MocNangLuongMoi;
        private DateTime _NgayHuongLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private int _PhanTramVuotKhungMoi;
        private bool _NangLuongSom;
        private bool _NangLuongTruocKhiNghiHuu;
        private string _GhiChu;
        private NangLuongEnum _PhanLoai;
        private TinhTrang _TinhTrang;
        private bool _Chon;
        private string _Ho;
        private string _Ten;

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

        [ModelDefault("Caption", "Mã nhân sự")]
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

        [ModelDefault("Caption", "Họ")]
        public string Ho
        {
            get
            {
                return _Ho;
            }
            set
            {
                SetPropertyValue("Ho", ref _Ho, value);
            }
        }
        [ModelDefault("Caption", "Tên")]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
            }
        }

        [ImmediatePostData]
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
                if (!IsLoading && value != null)
                {
                    BoPhan = value.BoPhan;
                    MocNangLuongCu = value.NhanVienThongTinLuong.MocNangLuong;
                    MocNangLuongDieuChinh = value.NhanVienThongTinLuong.MocNangLuongDieuChinh;
                    NgayHuongLuongCu = value.NhanVienThongTinLuong.NgayHuongLuong;
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuongCu = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuongCu = value.NhanVienThongTinLuong.HeSoLuong;
                    PhanTramVuotKhungCu = value.NhanVienThongTinLuong.VuotKhung;
                    SoHieuCongChuc = value.SoHieuCongChuc;
                    if (value.NhanVienThongTinLuong.NgachLuong != null)
                        MaNgachLuong = value.NhanVienThongTinLuong.NgachLuong.MaQuanLy;
                    //
                    PhanLoai = NangLuongEnum.ThuongXuyen;
                    TinhTrang = value.TinhTrang;
                }
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

        [VisibleInListView(false)]
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

        [VisibleInDetailView(false)]
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

        [ModelDefault("Caption", "Bậc lương cũ")]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương cũ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuongCu
        {
            get
            {
                return _HeSoLuongCu;
            }
            set
            {
                SetPropertyValue("HeSoLuongCu", ref _HeSoLuongCu, value);
            }
        }

        [ModelDefault("Caption", "% vượt khung cũ")]
        public int PhanTramVuotKhungCu
        {
            get
            {
                return _PhanTramVuotKhungCu;
            }
            set
            {
                SetPropertyValue("PhanTramVuotKhungCu", ref _PhanTramVuotKhungCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh")]
        public DateTime MocNangLuongDieuChinh
        {
            get
            {
                return _MocNangLuongDieuChinh;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinh", ref _MocNangLuongDieuChinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Bậc lương mới")]
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương mới")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuongMoi
        {
            get
            {
                return _HeSoLuongMoi;
            }
            set
            {
                SetPropertyValue("HeSoLuongMoi", ref _HeSoLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "% vượt khung mới")]
        public int PhanTramVuotKhungMoi
        {
            get
            {
                return _PhanTramVuotKhungMoi;
            }
            set
            {
                SetPropertyValue("PhanTramVuotKhungMoi", ref _PhanTramVuotKhungMoi, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương mới")]
        public DateTime MocNangLuongMoi
        {
            get
            {
                return _MocNangLuongMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongMoi", ref _MocNangLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nâng lương sớm")]
        public bool NangLuongSom
        {
            get
            {
                return _NangLuongSom;
            }
            set
            {
                SetPropertyValue("NangLuongSom", ref _NangLuongSom, value);
                if (!IsLoading && NangLuongSom == true)
                {
                    PhanLoai = NangLuongEnum.CoThanhTichXuatSac;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nâng lương trước khi nghỉ hưu")]
        public bool NangLuongTruocKhiNghiHuu
        {
            get
            {
                return _NangLuongTruocKhiNghiHuu;
            }
            set
            {
                SetPropertyValue("NangLuongTruocKhiNghiHuu", ref _NangLuongTruocKhiNghiHuu, value);
                if (!IsLoading && NangLuongTruocKhiNghiHuu == true)
                {
                    PhanLoai = NangLuongEnum.TruocKhiNghiHuu;
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Phân loại")]
        public NangLuongEnum PhanLoai
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

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public DenHanNangLuong(Session session)
            : base(session)
        { }

        
    }

}
