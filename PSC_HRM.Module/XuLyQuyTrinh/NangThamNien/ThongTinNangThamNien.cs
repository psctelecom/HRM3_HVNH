using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.NangThamNien
{
    [NonPersistent]
    [ImageName("BO_Group5")]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Thông tin nâng thâm niên")]
    public class ThongTinNangThamNien : Notification
    {
        // Fields...
        private DateTime _NgayHuongThamNienCu;
        private decimal _ThamNienMoi;
        private decimal _ThamNienCu;
        private DateTime _NgayVaoNganh;

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

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    if (value == 0)
                        ThamNienMoi = 5;
                    else
                        ThamNienMoi = value + 1;
                }
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

        public ThongTinNangThamNien(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            NgayVaoNganh = ThongTinNhanVien.NgayVaoNganhGiaoDuc;
            ThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
            NgayHuongThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien;
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} đến hạn nâng thâm niên nhà giáo.", this);
        }
    }

}
