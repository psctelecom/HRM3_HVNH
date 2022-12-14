using System;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;


namespace PSC_HRM.Module.XuLyQuyTrinh.NghiHuu
{
    [NonPersistent]
    [ImageName("BO_NghiHuu")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Thông tin nghỉ hưu")]
    
    public class ThongTinNghiHuu : Notification
    {
        private string _SoHieuCongChuc;
        private GioiTinhEnum _GioiTinh;
        private DateTime _NgaySinh;
        
        [ModelDefault("Caption", "Số hồ sơ")]
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

        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        public ThongTinNghiHuu(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            SoHieuCongChuc = ThongTinNhanVien.SoHieuCongChuc;
            GioiTinh = ThongTinNhanVien.GioiTinh;
            NgaySinh = ThongTinNhanVien.NgaySinh;

            if (ThongTinNhanVien.NgayNghiHuu == DateTime.MinValue)
            {
                TuoiNghiHuu tuoiNghiHuu = Session.FindObject<TuoiNghiHuu>(CriteriaOperator.Parse("GioiTinh=?", ThongTinNhanVien.GioiTinh));
               if(TruongConfig.MaTruong.Equals("QNU"))
               {
                if (tuoiNghiHuu != null)
                    Ngay = new DateTime(ThongTinNhanVien.NgaySinh.AddYears(tuoiNghiHuu.Tuoi).Year, ThongTinNhanVien.NgaySinh.AddMonths(1).Month, 1);
                else if (ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                    Ngay = new DateTime(ThongTinNhanVien.NgaySinh.AddYears(60).Year, ThongTinNhanVien.NgaySinh.AddMonths(1).Month, 1);
                else
                    Ngay = new DateTime(ThongTinNhanVien.NgaySinh.AddYears(55).Year, ThongTinNhanVien.NgaySinh.AddMonths(1).Month, 1);
               }
              else
               {
                if (tuoiNghiHuu != null)
                    Ngay = ThongTinNhanVien.NgaySinh.AddYears(tuoiNghiHuu.Tuoi);
                else if (ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                    Ngay = ThongTinNhanVien.NgaySinh.AddYears(60);
                else
                    Ngay = ThongTinNhanVien.NgaySinh.AddYears(55);
               }
            }
            else
                Ngay = ThongTinNhanVien.NgayNghiHuu;
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} đến tuổi nghỉ hưu", this);
        }
    }

}
