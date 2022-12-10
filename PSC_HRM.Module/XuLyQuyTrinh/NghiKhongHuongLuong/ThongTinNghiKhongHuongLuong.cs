using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.NghiKhongHuongLuong
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Thông tin nghỉ không hưởng lương")]
    public class ThongTinNghiKhongHuongLuong : Notification
    {
        // Fields...
        private DateTime _TuNgay;
        private QuyetDinhNghiKhongHuongLuong _QuyetDinhNghiKhongHuongLuong;

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinhNghiKhongHuongLuong QuyetDinhNghiKhongHuongLuong
        {
            get
            {
                return _QuyetDinhNghiKhongHuongLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNghiKhongHuongLuong", ref _QuyetDinhNghiKhongHuongLuong, value);
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

        public ThongTinNghiKhongHuongLuong(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} hết hạn nghỉ không hưởng lương.", this);
        }
    }

}
