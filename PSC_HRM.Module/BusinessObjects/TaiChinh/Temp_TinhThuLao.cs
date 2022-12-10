using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.TaiChinh
{
    [ModelDefault("Caption", "Bảng tạm dùng tính thù lao")]
    public class Temp_TinhThuLao : BaseObject
    {
        private Guid _BangThuLaoNhanVien;
        private NamHoc _NamHoc;
        private NhanVien _NhanVien;
        private decimal _TongGio;
        private decimal _ThanhTien;
        private Guid _OidChiTietGioGiang;
        private decimal _TamUng;
        public Guid BangThuLaoNhanVien
        {
            get { return _BangThuLaoNhanVien; }
            set { SetPropertyValue("BangThuLaoNhanVien", ref _BangThuLaoNhanVien, value); }
        }
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }

        public decimal TamUng
        {
            get { return _TamUng; }
            set { SetPropertyValue("TamUng", ref _TamUng, value); }
        }
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }
        public Guid OidChiTietGioGiang
        {
            get { return _OidChiTietGioGiang; }
            set { SetPropertyValue("OidChiTietGioGiang", ref _OidChiTietGioGiang, value); }
        }
        public Temp_TinhThuLao(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}