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
    [ModelDefault("Caption", "Chi tiết bảng lương tháng")]
    public class Luong_BangLuongThangItem : BaseObject, ISupportController
    {
        private string _MaQuanLy; 
        private string _TenBoPhan;
        private string _HoTen;
        private string _TenChucVu;
        private decimal _PhuCapKhac;
        private decimal _HeSoLuong;
        private decimal _VuotKhung;
        private decimal _HSPCVuotKhung;
        private decimal _HSPCChucVu;
        private decimal _ThamNien;
        private decimal _HSPCThamNienNhaGiao;
        private decimal _HSPCDocHai;
        private decimal _PhuCapUuDai;
        private decimal _HSPCUuDai;
        private decimal _HSPCKhac;
        private decimal _TongHeSoLuong;
        private decimal _PhanTramHuongLuong;
        private decimal _TongLuongHeSo;
        private decimal _TienTruNghiBHXH;
        private decimal _TienTruNghiKhongLuong;
        private decimal _BHXH;
        private decimal _BHTN;
        private decimal _BHYT;
        private decimal _ThueTNCNTamTru;
        private decimal _ThueTNCNQuyetToan;
        private decimal _TruyThu;
        private decimal _TongKhoanTru;
        private decimal _Luong2;
        private decimal _Luong3;
        private decimal _HSPCTrachNhiem;
        private decimal _PhuCapQuanLy;
        private decimal _PCDienThoai;
        private decimal _PCAnTrua;
        private decimal _PhuCapNganh;
        private decimal _HSPCThamNienHC;
        private decimal _PCThamNienHC;
        private decimal _TruyLinh;
        private decimal _TongKhoanCong;
        private decimal _ThucLanh;

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

        [ModelDefault("Caption", "Phụ cấp khác")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapKhac
        {
            get
            {
                return _PhuCapKhac;
            }
            set
            {
                SetPropertyValue("PhuCapKhac", ref _PhuCapKhac, value);
            }
        }       

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "Vượt khung")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "Hệ số phụ cấp vượt khung")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal HSPCVuotKhung
        {
            get
            {
                return _HSPCVuotKhung;
            }
            set
            {
                SetPropertyValue("HSPCVuotKhung", ref _HSPCVuotKhung, value);
            }
        }

        [ModelDefault("Caption", "Hệ số phụ cấp chức vụ")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "Thâm niên")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "HSPC thâm niên nhà giáo")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal HSPCThamNienNhaGiao
        {
            get
            {
                return _HSPCThamNienNhaGiao;
            }
            set
            {
                SetPropertyValue("HSPCThamNienNhaGiao", ref _HSPCThamNienNhaGiao, value);
            }
        }

        [ModelDefault("Caption", "HSPC độc hại")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "Phụ cấp độc hại")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
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

        [ModelDefault("Caption", "HSPC Ưu đãi")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal HSPCUuDai
        {
            get
            {
                return _HSPCUuDai;
            }
            set
            {
                SetPropertyValue("HSPCUuDai", ref _HSPCUuDai, value);
            }
        }

        [ModelDefault("Caption", "Tổng hệ só thu nhập")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TongHeSoLuong
        {
            get
            {
                return _TongHeSoLuong;
            }
            set
            {
                SetPropertyValue("TongHeSoLuong", ref _TongHeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Phần trăm hưởng lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramHuongLuong
        {
            get
            {
                return _PhanTramHuongLuong;
            }
            set
            {
                SetPropertyValue("PhanTramHuongLuong", ref _PhanTramHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "Tổng lương hệ số")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongLuongHeSo
        {
            get
            {
                return _TongLuongHeSo;
            }
            set
            {
                SetPropertyValue("TongLuongHeSo", ref _TongLuongHeSo, value);
            }
        }

        [ModelDefault("Caption", "Tiền trừ nghỉ BHXH")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienTruNghiBHXH
        {
            get
            {
                return _TienTruNghiBHXH;
            }
            set
            {
                SetPropertyValue("TienTruNghiBHXH", ref _TienTruNghiBHXH, value);
            }
        }

        [ModelDefault("Caption", "Tiền trừ nghỉ không lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienTruNghiKhongLuong
        {
            get
            {
                return _TienTruNghiKhongLuong;
            }
            set
            {
                SetPropertyValue("TienTruNghiKhongLuong", ref _TienTruNghiKhongLuong, value);
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
                SetPropertyValue("BHTN", ref _BHTN, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN tạm trừ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNTamTru
        {
            get
            {
                return _ThueTNCNTamTru;
            }
            set
            {
                SetPropertyValue("ThueTNCNTamTru", ref _ThueTNCNTamTru, value);
            }
        }


        [ModelDefault("Caption", "Thuế TNCN Quyết toán")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThueTNCNQuyetToan
        {
            get
            {
                return _ThueTNCNQuyetToan;
            }
            set
            {
                SetPropertyValue("ThueTNCNQuyetToan", ref _ThueTNCNQuyetToan, value);
            }
        }

        [ModelDefault("Caption", "Truy thu")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TruyThu
        {
            get
            {
                return _TruyThu;
            }
            set
            {
                SetPropertyValue("TruyThu", ref _TruyThu, value);
            }
        }

        [ModelDefault("Caption", "Truy lĩnh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TruyLinh
        {
            get
            {
                return _TruyLinh;
            }
            set
            {
                SetPropertyValue("TruyLinh", ref _TruyLinh, value);
            }
        }

        [ModelDefault("Caption", "Tổng khoản trừ")]
        public decimal TongKhoanTru
        {
            get
            {
                return _TongKhoanTru;
            }
            set
            {
                SetPropertyValue("TongKhoanTru", ref _TongKhoanTru, value);
            }
        }

        [ModelDefault("Caption", "Lương 2")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal Luong2
        {
            get
            {
                return _Luong2;
            }
            set
            {
                SetPropertyValue("Luong2", ref _Luong2, value);
            }
        }

        [ModelDefault("Caption", "Lương 3")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal Luong3
        {
            get
            {
                return _Luong3;
            }
            set
            {
                SetPropertyValue("Luong2", ref _Luong3, value);
            }
        }

        [ModelDefault("Caption", "HSPC trách nghiệm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        [ModelDefault("Caption", "Phụ cấp quản lý")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapQuanLy
        {
            get
            {
                return _PhuCapQuanLy;
            }
            set
            {
                SetPropertyValue("PhuCapQuanLy", ref _PhuCapQuanLy, value);
            }
        }

        [ModelDefault("Caption", "PC điện thoại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PCDienThoai
        {
            get
            {
                return _PCDienThoai;
            }
            set
            {
                SetPropertyValue("PCDienThoai", ref _PCDienThoai, value);
            }
        }

        [ModelDefault("Caption", "PC ăn trưa")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PCAnTrua
        {
            get
            {
                return _PCAnTrua;
            }
            set
            {
                SetPropertyValue("PCAnTrua", ref _PCAnTrua, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp ngành")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapNganh
        {
            get
            {
                return _PhuCapNganh;
            }
            set
            {
                SetPropertyValue("PhuCapNganh", ref _PhuCapNganh, value);
            }
        }


        [ModelDefault("Caption", "HSPC ThamNienHC")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal HSPCThamNienHC
        {
            get
            {
                return _HSPCThamNienHC;
            }
            set
            {
                SetPropertyValue("HSPCThamNienHC", ref _HSPCThamNienHC, value);
            }
        }

        [ModelDefault("Caption", "Tổng khoản cộng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongKhoanCong
        {
            get
            {
                return _TongKhoanCong;
            }
            set
            {
                SetPropertyValue("TongKhoanCong", ref _TongKhoanCong, value);
            }
        }

        [ModelDefault("Caption", "Thực lãnh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucLanh
        {
            get
            {
                return _ThucLanh;
            }
            set
            {
                SetPropertyValue("ThucLanh", ref _ThucLanh, value);
            }
        }

        public Luong_BangLuongThangItem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
