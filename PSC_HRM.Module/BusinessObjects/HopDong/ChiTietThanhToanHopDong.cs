using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;




namespace PSC_HRM.Module.HopDong
{

    public class ChiTietThanhToanHopDong : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _NoiDung;
        private decimal _SoLuong;
        private DonViTinh _DonViTinh;
        private decimal _DonGia;
        private decimal _ThanhTien;
        private ChiTietHopDongThinhGiang _ChiTietHopDong;

        [ModelDefault("Caption", "Chi Tiết Thanh Toán Hợp Đồng Thỉnh Giảng ")]
        [Association("ChiTietHopDongThinhGiang-ListChiTietHopDongThinhGiang")]

        public ChiTietHopDongThinhGiang ChiTietHopDong
        {
            get
            {
                return _ChiTietHopDong;
            }
            set
            {
                SetPropertyValue("ChiTietHopDong", ref _ChiTietHopDong, value);
            
            }
        }
       
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
                
            }
        }

        [ModelDefault("Caption", "Nội dung")]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);

            }
        }
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số lượng")]
        public decimal SoLuong
        {
            get
            {
                return _SoLuong;
            }
            set
            {
                SetPropertyValue("SoLuong", ref _SoLuong, value);
                ThanhTien = SoLuong * DonGia;
            }
        }


        [ModelDefault("Caption", "Đơn vị tính")]
        public DonViTinh DonViTinh
        {
            get
            {
                return _DonViTinh;
            }
            set
            {
                SetPropertyValue("DonViTinh", ref _DonViTinh, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Đơn giá")]
        public decimal DonGia
        {
            get
            {
                return _DonGia;
            }
            set
            {
                SetPropertyValue("Dongia", ref _DonGia, value);
                ThanhTien = SoLuong * DonGia;
                
            }
        }
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thành tiền")]
        public decimal ThanhTien
        {
            get
            {
                return _ThanhTien;
            }
            set
            {
                SetPropertyValue("ThanhTien", ref _ThanhTien, value);
            }
        }

     
   
        public ChiTietThanhToanHopDong(Session session) : base(session) { }
    }

}
