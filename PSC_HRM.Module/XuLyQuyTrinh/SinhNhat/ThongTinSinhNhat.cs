using System;
using DevExpress.Xpo;
using PSC_HRM.Module.BanLamViec;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.SinhNhat
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Sinh nhật")]
    public class ThongTinSinhNhat : Notification
    {
        public ThongTinSinhNhat(Session session)
            : base(session)
        { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Sinh nhật cán bộ {ThongTinNhanVien.HoTen}.", this);
        }
    }
}
