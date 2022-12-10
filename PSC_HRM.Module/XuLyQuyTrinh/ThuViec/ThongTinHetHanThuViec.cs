using System;

using DevExpress.Xpo;

using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.BanLamViec;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.ThuViec
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Hết hạn thử việc")]
    public class ThongTinHetHanThuViec : Notification
    {
        private HopDong_LaoDong _HopDongHienTai;

        [ModelDefault("Caption", "Hộp đồng lao động")]
        public HopDong_LaoDong HopDongHienTai
        {
            get
            {
                return _HopDongHienTai;
            }
            set
            {
                SetPropertyValue("HopDongHienTai", ref _HopDongHienTai, value);
            }
        }

        public ThongTinHetHanThuViec(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} sắp hết hạn thử việc.", this);
        }
    }

}
