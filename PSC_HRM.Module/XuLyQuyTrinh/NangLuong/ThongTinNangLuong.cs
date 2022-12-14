using System;

using DevExpress.Xpo;

using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.NangLuong
{
    [NonPersistent]
    [ImageName("BO_NangLuong")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Thông tin nâng lương")]
    public class ThongTinNangLuong : Notification
    {
        private DateTime _MocNangLuongCu;
        private DateTime _NgayHuongLuongCu;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private int _VuotKhungCu;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private int _VuotKhungMoi;

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
        public int VuotKhungCu
        {
            get
            {
                return _VuotKhungCu;
            }
            set
            {
                SetPropertyValue("VuotKhungCu", ref _VuotKhungCu, value);
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

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    if (value != null)
                        HeSoLuongMoi = value.HeSoLuong;
                    else
                        HeSoLuongMoi = 0;
                }
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
        public int VuotKhungMoi
        {
            get
            {
                return _VuotKhungMoi;
            }
            set
            {
                SetPropertyValue("VuotKhungMoi", ref _VuotKhungMoi, value);
            }
        }

        public ThongTinNangLuong(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            HeSoLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            VuotKhungCu = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            MocNangLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong;

            if (VuotKhungCu > 0)
            {
                BacLuongMoi = BacLuongCu;
                VuotKhungMoi = VuotKhungCu + 1;
            }
            else if (NgachLuong.TotKhung != null 
                && BacLuongCu != null
                && NgachLuong.TotKhung.Oid == BacLuongCu.Oid)
            {
                BacLuongMoi = BacLuongCu;
                VuotKhungMoi = 5;
            }
            else if (BacLuongCu != null)
            {
                int bac;
                if (int.TryParse(BacLuongCu.MaQuanLy.Trim(), out bac))
                {
                    BacLuongMoi = Session.FindObject<BacLuong>(CriteriaOperator.Parse("MaQuanLy=? and NgachLuong=? and !BacLuongCu", ++bac, NgachLuong.Oid));
                }
            }
            
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} đến hạn nâng lương.", this);
        }
    }

}
