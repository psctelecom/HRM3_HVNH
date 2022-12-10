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


namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng lương chi tiết kỳ 2")]
    public class Luong_BangLuongChiTietKy2Item : BaseObject, ISupportController
    {
        private string _MaSo;
        private string _HoTen;
        private string _BoPhan;
        private decimal _HeSoLuong;
        private decimal _LuongKy2;
        private string _DanhGia;
        private decimal _SoNgayCong;
        private decimal _ThanhTien;
        private decimal _TienNDC;
        private decimal _TienCongDoan;
        private decimal _TamUng;
        private decimal _KhauTruKhac;
        private decimal _ThuNhapTruocThue;
        private decimal _ThuNhapChiuThue;
        private decimal _ThueThuNhap;
        private decimal _KhauTruUngHo;
        private decimal _ThucLinh;

        [ModelDefault("Caption", "Mã số")]
        public string MaSo
        {
            get
            {
                return _MaSo;
            }
            set
            {
                SetPropertyValue("MaSo", ref _MaSo, value);
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

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
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

        [ModelDefault("Caption", "Lương kỳ 2")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKy2
        {
            get
            {
                return _LuongKy2;
            }
            set
            {
                SetPropertyValue("LuongKy2", ref _LuongKy2, value);
            }
        }

        [ModelDefault("Caption", "Đánh giá")]
        public string DanhGia
        {
            get
            {
                return _DanhGia;
            }
            set
            {
                SetPropertyValue("DanhGia", ref _DanhGia, value);
            }
        }

        [ModelDefault("Caption", "Số ngày công")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNgayCong
        {
            get
            {
                return _SoNgayCong;
            }
            set
            {
                SetPropertyValue("SoNgayCong", ref _SoNgayCong, value);
            }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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

        [ModelDefault("Caption", "Tiền BT + NĐC")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienNDC
        {
            get
            {
                return _TienNDC;
            }
            set
            {
                SetPropertyValue("TienNDC", ref _TienNDC, value);
            }
        }

        [ModelDefault("Caption", "Tiền công đoàn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienCongDoan
        {
            get
            {
                return _TienCongDoan;
            }
            set
            {
                SetPropertyValue("TienCongDoan", ref _TienCongDoan, value);
            }
        }

        [ModelDefault("Caption", "Tạm ứng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TamUng
        {
            get
            {
                return _TamUng;
            }
            set
            {
                SetPropertyValue("TamUng", ref _TamUng, value);
            }
        }

        [ModelDefault("Caption", "Khấu trừ khác")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KhauTruKhac
        {
            get
            {
                return _KhauTruKhac;
            }
            set
            {
                SetPropertyValue("KhauTruKhac", ref _KhauTruKhac, value);
            }
        }

        [ModelDefault("Caption", "Thu nhập trước thuế")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThuNhapTruocThue
        {
            get
            {
                return _ThuNhapTruocThue;
            }
            set
            {
                SetPropertyValue("ThuNhapTruocThue", ref _ThuNhapTruocThue, value);
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

        [ModelDefault("Caption", "Khấu trừ ủng hộ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KhauTruUngHo
        {
            get
            {
                return _KhauTruUngHo;
            }
            set
            {
                SetPropertyValue("KhauTruUngHo", ref _KhauTruUngHo, value);
            }
        }

        [ModelDefault("Caption", "Thực lĩnh")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThucLinh
        {
            get
            {
                return _ThucLinh;
            }
            set
            {
                SetPropertyValue("ThucLinh", ref _ThucLinh, value);
            }
        }

        public Luong_BangLuongChiTietKy2Item(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
