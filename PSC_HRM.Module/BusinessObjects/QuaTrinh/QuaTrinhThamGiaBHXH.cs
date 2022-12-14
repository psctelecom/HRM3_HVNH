using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.CauHinh;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [DefaultProperty("HoSoBaoHiem")]
    [ModelDefault("Caption", "Quá trình tham gia BHXH")]
    [Appearance("Hide_KhacNEU", TargetItems = "PhuCapKhuVuc", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'NEU'")]
    public class QuaTrinhThamGiaBHXH : TruongBaseObject
    {
        private QuyetDinh.QuyetDinh _QuyetDinh;
        private decimal _ThamNienGiangDay;
        private HoSoBaoHiem _HoSoBaoHiem;
        private DateTime _TuNam;
        private DateTime _DenNam;
        private string _NoiLamViec;
        private decimal _LuongKhoan;
        private decimal _HeSoLuong;
        private decimal _PhuCapChucVu;
        private decimal _VuotKhung;
        private decimal _MucLuong;
        private bool _KhongThamGiaBHYT;
        private bool _KhongThamGiaBHTN;
        private LuongToiThieu _TyLeDong;
        //
        private ChucDanh _ChucDanh;
        private string _ChucDanhText;
        private decimal _PhuCapKhuVuc;

        
        [Browsable(false)]
        [ImmediatePostData]
        [Association("HoSoBaoHiem-ListQuaTrinhThamGiaBHXH")]
        public HoSoBaoHiem HoSoBaoHiem
        {
            get
            {
                return _HoSoBaoHiem;
            }
            set
            {
                SetPropertyValue("HoSoBaoHiem", ref _HoSoBaoHiem, value);
                if(!IsLoading && value != null)
                {
                    HeSoLuong = value.ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong ? value.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong * 0.85m : value.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
                    PhuCapChucVu = value.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
                    ThamNienGiangDay = value.ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien;
                    VuotKhung = value.ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ tháng năm")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                //dua ve ngay dau thang
                if (value != DateTime.MinValue && value.Day != 1)
                    value = new DateTime(value.Year, value.Month, 1);
                SetPropertyValue("TuNam", ref _TuNam, value);
                if(!IsLoading && value != DateTime.MinValue)
                {
                    TyLeDong = Session.FindObject<LuongToiThieu>(CriteriaOperator.Parse("TuNgay<=? AND DenNgay>=?", value, value));
                }
            }
        }

        [ModelDefault("Caption", "Đến tháng năm")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                //dua ve ngay cuoi thang
                if (value != DateTime.MinValue)
                    value = new DateTime(value.Year, value.Month, 1).AddMonths(1).AddDays(-1);
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Mô tả công việc")]
        public string NoiLamViec
        {
            get
            {
                return _NoiLamViec;
            }
            set
            {
                SetPropertyValue("NoiLamViec", ref _NoiLamViec, value);
            }
        }

        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [Appearance("LuongKhoan_IUH", TargetItems = "LuongKhoan", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]
        public decimal LuongKhoan
        {
            get
            {
                return _LuongKhoan;
            }
            set
            {
                SetPropertyValue("LuongKhoan", ref _LuongKhoan, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhuCapChucVu
        {
            get
            {
                return _PhuCapChucVu;
            }
            set
            {
                SetPropertyValue("PhuCapChucVu", ref _PhuCapChucVu, value);
            }
        }

        [ModelDefault("Caption", "Vượt khung")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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

        [ModelDefault("Caption", "Thâm niên")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThamNienGiangDay
        {
            get
            {
                return _ThamNienGiangDay;
            }
            set
            {
                SetPropertyValue("ThamNienGiangDay", ref _ThamNienGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp khu vực")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhuCapKhuVuc
        {
            get
            {
                return _PhuCapKhuVuc;
            }
            set
            {
                SetPropertyValue("PhuCapKhuVuc", ref _PhuCapKhuVuc, value);
            }
        }


        [ModelDefault("Caption", "Không tham gia BHYT")]
        public bool KhongThamGiaBHYT
        {
            get
            {
                return _KhongThamGiaBHYT;
            }
            set
            {
                SetPropertyValue("KhongThamGiaBHYT", ref _KhongThamGiaBHYT, value);
            }
        }
        
        [ModelDefault("Caption", "Không tham gia BHTN")]
        public bool KhongThamGiaBHTN
        {
            get
            {
                return _KhongThamGiaBHTN;
            }
            set
            {
                SetPropertyValue("KhongThamGiaBHTN", ref _KhongThamGiaBHTN, value);
            }
        }
        
        [ModelDefault("Caption", "Tỷ lệ đóng")]
        //[RuleUniqueValue("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'BUH'")]
        public LuongToiThieu TyLeDong
        {
            get
            {
                return _TyLeDong;
            }
            set
            {
                SetPropertyValue("TyLeDong", ref _TyLeDong, value);
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Chức danh text")]
        public string ChucDanhText
        {
            get
            {
                return _ChucDanhText;
            }
            set
            {
                SetPropertyValue("ChucDanhText", ref _ChucDanhText, value);
            }
        }
        [ModelDefault("Caption", "Mức lương")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [Appearance("MucLuong_HBU", TargetItems = "MucLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'HBU'")]
        public decimal MucLuong
        {
            get
            {
                return _MucLuong;
            }
            set
            {
                SetPropertyValue("MucLuong", ref _MucLuong, value);
            }
        }

        [Browsable(false)]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        public QuaTrinhThamGiaBHXH(Session session) : base(session) 
        { }
    }

}
