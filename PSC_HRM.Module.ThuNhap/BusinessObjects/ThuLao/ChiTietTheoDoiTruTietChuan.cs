using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.ThuLao
{
    [ImageName("BO_ThuLao")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("Caption", "Chi tiết theo dõi tiết chuẩn")]   
    public class ChiTietTheoDoiTruTietChuan : BaseObject
    {
        private BangThuLaoNhanVien _BangThuLaoNhanVien;
        private NhanVien _NhanVien;
        private decimal _TietChuanTruocThanhToan;
        private decimal _TietChuanDaTruThanhToan;
        private decimal _TietChuanConLaiSauThanhToan;
        private string _GhiChu;

       
        [Browsable(false)]
        [ModelDefault("Caption", "Bảng thù lao nhân viên")]
        [Association("BangThuLaoNhanVien-ListChiTietTheoDoiTruTietChuan")]
        public BangThuLaoNhanVien BangThuLaoNhanVien
        {
            get
            {
                return _BangThuLaoNhanVien;
            }
            set
            {
                SetPropertyValue("BangThuLaoNhanVien", ref _BangThuLaoNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Tiết chuẩn trước thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TietChuanTruocThanhToan
        {
            get
            {
                return _TietChuanTruocThanhToan;
            }
            set
            {
                SetPropertyValue("TietChuanTruocThanhToan", ref _TietChuanTruocThanhToan, value);
            }
        }
        [ModelDefault("Caption", "Tiết chuẩn đã trừ thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TietChuanDaTruThanhToan
        {
            get
            {
                return _TietChuanDaTruThanhToan;
            }
            set
            {
                SetPropertyValue("TietChuanDaTruThanhToan", ref _TietChuanDaTruThanhToan, value);
            }
        }
        [ModelDefault("Caption", "Tiết chuẩn đã trừ thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TietChuanConLaiSauThanhToan
        {
            get
            {
                return _TietChuanConLaiSauThanhToan;
            }
            set
            {
                SetPropertyValue("TietChuanConLaiSauThanhToan", ref _TietChuanConLaiSauThanhToan, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public ChiTietTheoDoiTruTietChuan(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
