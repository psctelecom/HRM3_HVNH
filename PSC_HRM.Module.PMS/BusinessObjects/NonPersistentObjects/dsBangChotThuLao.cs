using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;


namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách bảng chốt thù lao")]
    [DefaultProperty("HoTen")]
    public class dsBangChotThuLao : BaseObject
    {
        private Guid _OidThongTinBangChot;
        private string _OidChiTietBangChotThuLaoGiangDay;
        private string _MaQuanLy;
        private string _HoTen;
        private string _LopHocPhan;
        private string _TenHoatDong;
        private decimal _TongGio;
        private decimal _SoTienThanhToan;
        private bool _Chon;

        [ModelDefault("Caption", "Chọn")]    
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }
        [ModelDefault("Caption", "Oid ThongTinBangChot")]
        [Browsable(false)]
        public Guid OidThongTinBangChot
        {
            get { return _OidThongTinBangChot; }
            set { SetPropertyValue("OidThongTinBangChot", ref _OidThongTinBangChot, value); }
        }

        [ModelDefault("Caption", "Oid Chi tiết bảng chốt thù lao")]
        [Browsable(false)]
        public string OidChiTietBangChotThuLaoGiangDay
        {
            get { return _OidChiTietBangChotThuLaoGiangDay; }
            set { SetPropertyValue("OidChiTietBangChotThuLaoGiangDay", ref _OidChiTietBangChotThuLaoGiangDay, value); }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Lớp học phần")]
        //[Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Hoạt động")]
        //[Size(-1)]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
        }

        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        public dsBangChotThuLao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
