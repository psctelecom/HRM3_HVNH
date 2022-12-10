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
    [ModelDefault("Caption", "Bảng lương theo bộ phận kỳ 1")]
    public class Luong_BangLuongBoPhanKy1Item : BaseObject, ISupportController
    {
        private string _BoPhan;
        private int _SoLuong;
        private decimal _HeSoLuong;
        private decimal _HSPCChucVu;
        private decimal _TongHeSo;
        private decimal _TienPCUD;
        private decimal _TongMucLuong;
        private decimal _SoNgayNghi;
        private decimal _SoTienNgayNghi;
        private decimal _SoNgayNghiBHXH;
        private decimal _SoTienBHXHTra;
        private decimal _BHXH;
        private decimal _BHYT;
        private decimal _BHTN;
        private decimal _NSDLDTra;
        private decimal _ThucLinh;

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

        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoLuong
        {
            get
            {
                return _SoLuong;
            }
            set
            {
                SetPropertyValue("SoLuong", ref _SoLuong, value);
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

        [ModelDefault("Caption", "Hệ số PC")]
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

        [ModelDefault("Caption", "Tổng hệ số")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal TongHeSo
        {
            get
            {
                return _TongHeSo;
            }
            set
            {
                SetPropertyValue("TongHeSo", ref _TongHeSo, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp ưu đãi")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienPCUD
        {
            get
            {
                return _TienPCUD;
            }
            set
            {
                SetPropertyValue("TienPCUD", ref _TienPCUD, value);
            }
        }

        [ModelDefault("Caption", "Tổng mức lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongMucLuong
        {
            get
            {
                return _TongMucLuong;
            }
            set
            {
                SetPropertyValue("TongMucLuong", ref _TongMucLuong, value);
            }
        }

        [ModelDefault("Caption", "Số ngày nghỉ không phép")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNgayNghi
        {
            get
            {
                return _SoNgayNghi;
            }
            set
            {
                SetPropertyValue("SoNgayNghi", ref _SoNgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Số tiền nghỉ không phép")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienNgayNghi
        {
            get
            {
                return _SoTienNgayNghi;
            }
            set
            {
                SetPropertyValue("SoTienNgayNghi", ref _SoTienNgayNghi, value);
            }
        }

        [ModelDefault("Caption", "Số ngày nghỉ BHXH")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNgayNghiBHXH
        {
            get
            {
                return _SoNgayNghiBHXH;
            }
            set
            {
                SetPropertyValue("SoNgayNghiBHXH", ref _SoNgayNghiBHXH, value);
            }
        }

        [ModelDefault("Caption", "Số tiền BHXH trả")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTienBHXHTra
        {
            get
            {
                return _SoTienBHXHTra;
            }
            set
            {
                SetPropertyValue("SoTienBHXHTra", ref _SoTienBHXHTra, value);
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
                SetPropertyValue("BHYT", ref _BHTN, value);
            }
        }

        [ModelDefault("Caption", "Người sử dụng LĐ trả")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal NguoiSuDungLDTra
        {
            get
            {
                return _NSDLDTra;
            }
            set
            {
                SetPropertyValue("NguoiSuDungLDTra", ref _NSDLDTra, value);
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

        public Luong_BangLuongBoPhanKy1Item(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
