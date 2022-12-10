using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.HopDong
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Thông tin hợp đồng")]
    public class ThongTinHopDong : Notification
    {
        // Fields...
        private DateTime _TuNgay;
        private HopDong_NhanVien _HopDong;

        [ModelDefault("Caption", "Hợp đồng")]
        public HopDong_NhanVien HopDong
        {
            get
            {
                return _HopDong;
            }
            set
            {
                SetPropertyValue("HopDong", ref _HopDong, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        public ThongTinHopDong(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} hết hạn hợp đồng.", this);
        }
    }

}
