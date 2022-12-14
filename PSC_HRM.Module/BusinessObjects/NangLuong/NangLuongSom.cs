using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NangLuong
{
    [NonPersistent]
    [ImageName("BO_NangLuong")]
    [ModelDefault("Caption", "Chi tiết nâng lương sớm")]
    public class NangLuongSom : BaseObject, IBoPhan
    {
        private int _SoNamCongTac;
        private DateTime _NgayVeTruong;
        private bool _Chon;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _MocNangLuongCu;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private int _PhanTramVuotKhungCu;
        private DateTime _MocNangLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private int _PhanTramVuotKhungMoi;
        private string _GhiChu;

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

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [ModelDefault("AllowEdit", "False")]
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
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuongCu = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuongCu = value.NhanVienThongTinLuong.HeSoLuong;
                    PhanTramVuotKhungCu = value.NhanVienThongTinLuong.VuotKhung;
                    MocNangLuongCu = value.NhanVienThongTinLuong.MocNangLuong;
                    NgayVeTruong = value.NgayVaoCoQuan;
                }
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("AllowEdit", "False")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày về trường")]
        public DateTime NgayVeTruong
        {
            get
            {
                return _NgayVeTruong;
            }
            set
            {
                SetPropertyValue("NgayVeTruong", ref _NgayVeTruong, value);
                
            }
        }

        [ModelDefault("Caption", "Số năm công tác")]
        public int SoNamCongTac
        {
            get
            {
                return _SoNamCongTac;
            }
            set
            {
                SetPropertyValue("SoNamCongTac", ref _SoNamCongTac, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
                if (!IsLoading && value != DateTime.MinValue)
                    MocNangLuongMoi = MocNangLuongCu.AddMonths(24);
            }
        }

        [ModelDefault("Caption", "Ngạch lương")]
        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "Bậc lương cũ")]
        [ModelDefault("AllowEdit", "False")]
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
        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "% vượt khung mới")]
        [ModelDefault("AllowEdit", "False")]
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

        [Size(500)]
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

        public NangLuongSom(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            MocNangLuongMoi = HamDungChung.GetServerTime();
        }
    }

}
