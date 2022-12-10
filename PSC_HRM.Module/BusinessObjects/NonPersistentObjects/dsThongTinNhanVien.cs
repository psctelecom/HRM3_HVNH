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


namespace PSC_HRM.Module.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách giảng viên")]
    [DefaultProperty("HoTen")]
    public class dsThongTinNhanVien : BaseObject
    {
        private Guid _OidThongTinNhanVien;
        private bool _Chon;
        private string _MaQuanLy;
        private string _HoTen;
        private string _BoPhan;

        [ModelDefault("Caption", "Chọn")]
        
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Thông tin NV")]
        [Browsable(false)]
        public Guid OidThongTinNhanVien
        {
            get { return _OidThongTinNhanVien; }
            set { SetPropertyValue("OidThongTinNhanVien", ref _OidThongTinNhanVien, value); }
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

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        public dsThongTinNhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
