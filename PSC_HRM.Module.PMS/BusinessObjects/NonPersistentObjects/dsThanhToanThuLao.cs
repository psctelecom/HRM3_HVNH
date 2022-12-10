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
    [ModelDefault("Caption", "Danh sách thanh toán")]
    [DefaultProperty("HoTen")]
    public class dsThanhToanThuLao : BaseObject
    {
        private Guid _ChiTietThuLaoNhanVien;
        private string _MaQuanLy;
        private string _HoTen;
        private decimal _SoTienThanhToan;
        private bool _Chon;
        [Browsable(false)]
        public Guid ChiTietThuLaoNhanVien
        {
            get { return _ChiTietThuLaoNhanVien; }
            set { SetPropertyValue("ChiTietThuLaoNhanVien", ref _ChiTietThuLaoNhanVien, value); }
        }
        [ModelDefault("Caption", "Chọn")]    
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
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

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        public dsThanhToanThuLao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
