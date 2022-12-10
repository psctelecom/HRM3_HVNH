using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.BaoHiem
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Thông tin nghỉ BHXH")]
    public class ThongTinNghiBHXH : Notification
    {
        // Fields...
        private LyDoNghiEnum _LyDoNghi;
        private DateTime _TuNgay;

        [ModelDefault("Caption", "Lý do nghỉ")]
        public LyDoNghiEnum LyDoNghi
        {
        	get
        	{
        		return _LyDoNghi;
        	}
        	set
        	{
        	  SetPropertyValue("LyDoNghi", ref _LyDoNghi, value);
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

        public ThongTinNghiBHXH(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} hết hạn nghỉ BHXH.", this);
        }
    }

}
