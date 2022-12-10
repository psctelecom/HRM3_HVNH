using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;


namespace PSC_HRM.Module.ThuNhap.Luong
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết thanh toán tiền lương tháng")]
    public class Luong_BangThanhToanTienLuongThangItem : BaseObject, ISupportController
    {
        private string _TenBoPhan;
        private string _HoTen;
        private string _TenChucVu;
        private string _NgachLuong;
        private decimal _HeSoLuong;
        private decimal _HSPCChucVu;
        private decimal _PhuCapUuDai;
        private decimal _VuotKhung;
        private decimal _HSPCDocHai;
        private decimal _HSPCTrachNhiem;
        private decimal _ThamNien;
        private decimal _TongHSLuongNSNN;
        private decimal _TongLuongNSNN;
        private decimal _HSPCChuyenMon;
        private decimal _HSPCQuanLy;
        private decimal _HSPCKiemNhiem1;
        private decimal _HSPCKiemNhiem2;
        private decimal _TongHSLuongTT;
        private int _Huong;
        private decimal _TienLuongTangThem;
        private decimal _TienPCTangThem;
        private decimal _TongLuongTT;
        private decimal _BHXH;
        private decimal _BHTN;
        private decimal _BHYT;
        private decimal _LFCD;
        private decimal _ThuNhapChiuThue;
        private decimal _ThueThuNhap;
        private decimal _TongCacKhoanTru;
        private decimal _ThucNhan;
        private LoaiLuongChinhEnum _LoaiLuongChinh;

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get
            {
                return _TenBoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _TenBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Tên chức vụ")]
        public string TenChucVu
        {
            get
            {
                return _TenChucVu;
            }
            set
            {
                SetPropertyValue("TenChucVu", ref _TenChucVu, value);
            }
        }

        [ModelDefault("Caption", "Ngạch lương")]
        public string NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "PC Chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
            }
        }

        [ModelDefault("Caption", "PC Ưu đãi %")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapUuDai
        {
            get
            {
                return _PhuCapUuDai;
            }
            set
            {
                SetPropertyValue("PhuCapUuDai", ref _PhuCapUuDai, value);
            }
        }

        [ModelDefault("Caption", "PC TN VK")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal VuotKhung
        {
            get
            {
                return _VuotKhung;
            }
            set
            {
                SetPropertyValue("VuotKhung", ref _VuotKhung, value);
            }
        }

        [ModelDefault("Caption", "PC Độc hại")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCDocHai
        {
            get
            {
                return _HSPCDocHai;
            }
            set
            {
                SetPropertyValue("HSPCDocHai", ref _HSPCDocHai, value);
            }
        }

        [ModelDefault("Caption", "PC Trách nhiệm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiem
        {
            get
            {
                return _HSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem", ref _HSPCTrachNhiem, value);
            }
        }

        [ModelDefault("Caption", "PC Thâm niên")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThamNien
        {
            get
            {
                return _ThamNien;
            }
            set
            {
                SetPropertyValue("ThamNien", ref _ThamNien, value);
            }
        }

        [ModelDefault("Caption", "Cộng HS NSNN")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal TongHSLuongNSNN
        {
            get
            {
                return _TongHSLuongNSNN;
            }
            set
            {
                SetPropertyValue("TongHSLuongNSNN", ref _TongHSLuongNSNN, value);
            }
        }

        [ModelDefault("Caption", "Tiền Lương NSNN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongLuongNSNN
        {
            get
            {
                return _TongLuongNSNN;
            }
            set
            {
                SetPropertyValue("TongLuongNSNN", ref _TongLuongNSNN, value);
            }
        }

        [ModelDefault("Caption", "HSPC Chuyên môn")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChuyenMon
        {
            get
            {
                return _HSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMon", ref _HSPCChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "HSPC Quản lý")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCQuanLy
        {
            get
            {
                return _HSPCQuanLy;
            }
            set
            {
                SetPropertyValue("HSPCQuanLy", ref _HSPCQuanLy, value);
            }
        }

        [ModelDefault("Caption", "HSPC Kiêm nhiệm 1")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKiemNhiem1
        {
            get
            {
                return _HSPCKiemNhiem1;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiem1", ref _HSPCKiemNhiem1, value);
            }
        }

        [ModelDefault("Caption", "HSPC Kiêm nhiệm 2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCKiemNhiem2
        {
            get
            {
                return _HSPCKiemNhiem2;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiem2", ref _HSPCKiemNhiem2, value);
            }
        }

        [ModelDefault("Caption", "Cộng HS Tăng thêm")]
        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        public decimal TongHSLuongTT
        {
            get
            {
                return _TongHSLuongTT;
            }
            set
            {
                SetPropertyValue("TongHSLuongTT", ref _TongHSLuongTT, value);
            }
        }

        [ModelDefault("Caption", "Hưởng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int Huong
        {
            get
            {
                return _Huong;
            }
            set
            {
                SetPropertyValue("Huong", ref _Huong, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương tăng thêm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienLuongTangThem
        {
            get
            {
                return _TienLuongTangThem;
            }
            set
            {
                SetPropertyValue("TienLuongTangThem", ref _TienLuongTangThem, value);
            }
        }

        [ModelDefault("Caption", "PC Tăng thêm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienPCTangThem
        {
            get
            {
                return _TienPCTangThem;
            }
            set
            {
                SetPropertyValue("TienPCTangThem", ref _TienPCTangThem, value);
            }
        }

        [ModelDefault("Caption", "Tổng Lương tăng thêm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongLuongTT
        {
            get
            {
                return _TongLuongTT;
            }
            set
            {
                SetPropertyValue("TongLuongTT", ref _TongLuongTT, value);
            }
        }

        [ModelDefault("Caption", "BHXH")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal BHXH
        {
            get
            {
                return _BHXH;
            }
            set
            {
                SetPropertyValue("BHXH", ref _BHXH, value);
            }
        }

        [ModelDefault("Caption", "BHTN")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal BHTN
        {
            get
            {
                return _BHTN;
            }
            set
            {
                SetPropertyValue("BHYT", ref _BHTN, value);
            }
        }

        [ModelDefault("Caption", "BHYT")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal BHYT
        {
            get
            {
                return _BHYT;
            }
            set
            {
                SetPropertyValue("BHYT", ref _BHYT, value);
            }
        }

        [ModelDefault("Caption", "LFCD")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LFCD
        {
            get
            {
                return _LFCD;
            }
            set
            {
                SetPropertyValue("LFCD", ref _LFCD, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập chịu thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThuNhapChiuThue
        {
            get
            {
                return _ThuNhapChiuThue;
            }
            set
            {
                SetPropertyValue("ThuNhapChiuThue", ref _ThuNhapChiuThue, value);
            }
        }

        [ModelDefault("Caption", "Thuế thu nhập")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueThuNhap
        {
            get
            {
                return _ThueThuNhap;
            }
            set
            {
                SetPropertyValue("ThueThuNhap", ref _ThueThuNhap, value);
            }
        }

        [ModelDefault("Caption", "Tổng các khoản trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongCacKhoanTru
        {
            get
            {
                return _TongCacKhoanTru;
            }
            set
            {
                SetPropertyValue("TongCacKhoanTru", ref _TongCacKhoanTru, value);
            }
        }

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucNhan
        {
            get
            {
                return _ThucNhan;
            }
            set
            {
                SetPropertyValue("ThucNhan", ref _ThucNhan, value);
            }
        }

        [ModelDefault("Caption", "Loại lương chính")]
        public LoaiLuongChinhEnum LoaiLuongChinh
        {
            get
            {
                return _LoaiLuongChinh;
            }
            set
            {
                SetPropertyValue("LoaiLuongChinh", ref _LoaiLuongChinh, value);
            }
        }

        public Luong_BangThanhToanTienLuongThangItem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
