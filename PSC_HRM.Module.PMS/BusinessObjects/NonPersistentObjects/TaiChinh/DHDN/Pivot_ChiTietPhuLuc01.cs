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
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;


namespace PSC_HRM.Module.PMS.NonPersistentObjects.TaiChinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết phụ lục 01")]
    [DefaultProperty("Caption")]
    public class Pivot_ChiTietPhuLuc01 : BaseObject
    {

        private string _MaGV;
        private string _HoTen;
        private string _SoTKNH;
        private string _TenNganHang;
        private string _TieuDe;
        private decimal _ThanhTien;
        private decimal _DonGia;
        private decimal _SoLuong;

        [ModelDefault("Caption", "Mã giảng viên")]
        public string MaGV
        {
            get { return _MaGV; }
            set { _MaGV = value; }
        }       

        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get { return _HoTen; }
            set { _HoTen = value; }
        }

        [ModelDefault("Caption", "Số tài khoản ngân hàng")]
        public string SoTKNH
        {
            get { return _SoTKNH; }
            set { _SoTKNH = value; }
        }
        
        [ModelDefault("Caption", "Tên ngân hàng")]
        public string TenNganHang
        {
            get { return _TenNganHang; }
            set { _TenNganHang = value; }
        }
        
        [ModelDefault("Caption", "Tiêu đề")]
        [Size(-1)]
        public string TieuDe
        {
            get { return _TieuDe; }
            set { _TieuDe = value; }
        }
        
        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { _ThanhTien = value; }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { _DonGia = value; }
        }

        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; }
        }       

        public Pivot_ChiTietPhuLuc01(Session session) : base(session) { }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }        
    }
}
