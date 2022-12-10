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
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;


namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng chi phụ cấp thâm niên nhà giáo")]
    public class PhuCap_PhuCapThamNienNhaGiaoItem : BaseObject, ISupportController
    {
        private string _HoTen;
        private string _BoPhan;
        private DateTime _NgaySinh;
        private string _ThangNamThamNien;
        private decimal _TongHeSo;
        private decimal _HeSoLuong;
        private decimal _HSPCChucVu;
        private decimal _HSVuotKhung;
        private string _MocThoiGian;
        private int _ThamNien;
        private decimal _LuongToiThieu;
        private decimal _TienPhuCapThamNien;
        private decimal _TrichNopCaNhan;
        private decimal _CoQuanTrichNop;
        private int _SoThang;
        private decimal _KinhPhi;
        private decimal _ThucLinh;

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

        [ModelDefault("Caption", "Phòng ban")]
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

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Tổng số tháng năm")]
        public string ThangNamThamNien
        {
            get
            {
                return _ThangNamThamNien;
            }
            set
            {
                SetPropertyValue("ThangNamThamNien", ref _ThangNamThamNien, value);
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

        [ModelDefault("Caption", "HSPC Chức vụ")]
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

        [ModelDefault("Caption", "HSPC Vượt khung")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSVuotKhung
        {
            get
            {
                return _HSVuotKhung;
            }
            set
            {
                SetPropertyValue("HSVuotKhung", ref _HSVuotKhung, value);
            }
        }

        [ModelDefault("Caption", "Mốc thời gian")]
        public string MocThoiGian
        {
            get
            {
                return _MocThoiGian;
            }
            set
            {
                SetPropertyValue("MocThoiGian", ref _MocThoiGian, value);
            }
        }

        [ModelDefault("Caption", "% PC thâm niên")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int ThamNien
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

        [ModelDefault("Caption", "Lương tối thiểu")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongToiThieu
        {
            get
            {
                return _LuongToiThieu;
            }
            set
            {
                SetPropertyValue("LuongToiThieu", ref _LuongToiThieu, value);
            }
        }

        [ModelDefault("Caption", "Tiền PC Thâm niên tháng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienPhuCapThamNien
        {
            get
            {
                return _TienPhuCapThamNien;
            }
            set
            {
                SetPropertyValue("TienPhuCapThamNien", ref _TienPhuCapThamNien, value);
            }
        }

        [ModelDefault("Caption", "Trích nộp cá nhân")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TrichNopCaNhan
        {
            get
            {
                return _TrichNopCaNhan;
            }
            set
            {
                SetPropertyValue("TrichNopCaNhan", ref _TrichNopCaNhan, value);
            }
        }

        [ModelDefault("Caption", "Cơ quan trích nộp")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal CoQuanTrichNop
        {
            get
            {
                return _CoQuanTrichNop;
            }
            set
            {
                SetPropertyValue("CoQuanTrichNộp", ref _CoQuanTrichNop, value);
            }
        }

        [ModelDefault("Caption", "Số tháng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
            }
        }

        [ModelDefault("Caption", "Kinh phí PCTN của năm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal KinhPhi 
        {
            get
            {
                return _KinhPhi;
            }
            set
            {
                SetPropertyValue("KinhPhi", ref _KinhPhi, value);
            }
        }

        [ModelDefault("Caption", "Cá nhân thực lĩnh")]
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

        public PhuCap_PhuCapThamNienNhaGiaoItem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
