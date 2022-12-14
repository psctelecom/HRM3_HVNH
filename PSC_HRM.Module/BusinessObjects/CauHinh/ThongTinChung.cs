using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.CauHinh
{
    [DefaultProperty("Caption")]
    [ImageName("BO_Money_Calculator")]
    [ModelDefault("Caption", "Thông tin chung")]
    public class ThongTinChung : TruongBaseObject
    {
        private bool _TinhThueTNCNLuyTien;
        private decimal _PTTrichDuPhong;
        private decimal _PTCongDoanCTY;
        private int _NgayTinhLuong;
        private decimal _ChenhLechLuongCoBan;
        private decimal _LuongCoBanCu;
        private decimal _TienAn;
        private decimal _DonGiaXang;
        private int _SoNgayPhepMoiLanTang;
        private int _ThoiGianTangSoNgayPhepNam;
        private int _SoNgayPhepNam;
        private int _SoNgayThangNhaNuoc;
        private decimal _LuongCoBan;
        private decimal _SoNgayThang;
        private int _SoGioNgay;
        private decimal _PTBHXH;
        private decimal _PTBHYT;
        private decimal _PTBHTN;
        private decimal _PTCongDoan;
        private decimal _PTBHXHCTY;
        private decimal _PTBHYTCTY;
        private decimal _PTBHTNCTY;
        private decimal _GiamTruBanThan;
        private decimal _GiamTruNguoiPhuThuoc;
        private decimal _LaiSuatBHXH;
        private decimal _LaiSuatBHYT;
        private decimal _NganSachNhaNuoc;
        private decimal _DonGiaPhuCap;
        private decimal _MucTroCapThoiViec;
        //
        private decimal _MucChiTraHS;
        private decimal _HSThuNhapTangThem;
        //
        private ThuNhapTangThemEnum _TinhTangThemDenThang;
        private int _TinhTangThemDenNgay;
        private ThuNhapTangThemEnum _TinhTangThemTuThang;
        private int _TinhTangThemTuNgay;

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("Lương cơ bản {LuongCoBan:N0}...", this);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lương cơ sở")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongCoBan
        {
            get
            {
                return _LuongCoBan;
            }
            set
            {
                SetPropertyValue("LuongCoBan", ref _LuongCoBan, value);
                if (!IsLoading)
                    ChenhLechLuongCoBan = LuongCoBan - LuongCoBanCu > 100000 ? 0 : LuongCoBan - LuongCoBanCu;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lương cơ bản cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongCoBanCu
        {
            get
            {
                return _LuongCoBanCu;
            }
            set
            {
                SetPropertyValue("LuongCoBanCu", ref _LuongCoBanCu, value);
                if (!IsLoading)
                    ChenhLechLuongCoBan = LuongCoBan - LuongCoBanCu > 100000 ? 0 : LuongCoBan - LuongCoBanCu;
            }
        }

        [ModelDefault("Caption", "Mức chênh lệch")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ChenhLechLuongCoBan
        {
            get
            {
                return _ChenhLechLuongCoBan;
            }
            set
            {
                SetPropertyValue("ChenhLechLuongCoBan", ref _ChenhLechLuongCoBan, value);
            }
        }
        
        [ModelDefault("Caption", "Đơn giá xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal DonGiaXang
        {
            get
            {
                return _DonGiaXang;
            }
            set
            {
                SetPropertyValue("DonGiaXang", ref _DonGiaXang, value);
            }
        }

        [ModelDefault("Caption", "Tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienAn
        {
            get
            {
                return _TienAn;
            }
            set
            {
                SetPropertyValue("TienAn", ref _TienAn, value);
            }
        }

        [ModelDefault("Caption", "Ngày tính lương")]
        [RuleRange("", DefaultContexts.Save, 1, 31)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int NgayTinhLuong
        {
            get
            {
                return _NgayTinhLuong;
            }
            set
            {
                SetPropertyValue("NgayTinhLuong", ref _NgayTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Số ngày nghỉ phép/năm")]
        public int SoNgayPhepNam
        {
            get
            {
                return _SoNgayPhepNam;
            }
            set
            {
                SetPropertyValue("SoNgayPhepNam", ref _SoNgayPhepNam, value);
            }
        }

        [ModelDefault("Caption", "Thời gian tăng số ngày phép/năm (năm)")]
        public int ThoiGianTangSoNgayPhepNam
        {
            get
            {
                return _ThoiGianTangSoNgayPhepNam;
            }
            set
            {
                SetPropertyValue("ThoiGianTangSoNgayPhepNam", ref _ThoiGianTangSoNgayPhepNam, value);
            }
        }

        [ModelDefault("Caption", "Số ngày phép năm mỗi lần tăng (ngày)")]
        public int SoNgayPhepMoiLanTang
        {
            get
            {
                return _SoNgayPhepMoiLanTang;
            }
            set
            {
                SetPropertyValue("SoNgayPhepMoiLanTang", ref _SoNgayPhepMoiLanTang, value);
            }
        }

        [ModelDefault("Caption", "Số ngày/tháng (nhà nước)")]
        //[RuleRange("", DefaultContexts.Save, 1, 30)]
        public int SoNgayThangNhaNuoc
        {
            get
            {
                return _SoNgayThangNhaNuoc;
            }
            set
            {
                SetPropertyValue("SoNgayThangNhaNuoc", ref _SoNgayThangNhaNuoc, value);
            }
        }

        [ModelDefault("Caption", "Số ngày/tháng (trường)")]
        //[RuleRange("", DefaultContexts.Save, 1, 30)]
        [ModelDefault("EditMask", "n1")]
        [ModelDefault("DisplayFormat", "n1")]
        public decimal SoNgayThang
        {
            get
            {
                return _SoNgayThang;
            }
            set
            {
                SetPropertyValue("SoNgayThang", ref _SoNgayThang, value);
            }
        }

        [ModelDefault("Caption", "Số giờ/ngày")]
        //[RuleRange("", DefaultContexts.Save, 0, 24)]
        public int SoGioNgay
        {
            get
            {
                return _SoGioNgay;
            }
            set
            {
                SetPropertyValue("SoGioNgay", ref _SoGioNgay, value);
            }
        }

        [ModelDefault("Caption", "% BHXH NLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHXH
        {
            get
            {
                return _PTBHXH;
            }
            set
            {
                SetPropertyValue("PTBHXH", ref _PTBHXH, value);
            }
        }

        [ModelDefault("Caption", "% BHYT NLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHYT
        {
            get
            {
                return _PTBHYT;
            }
            set
            {
                SetPropertyValue("PTBHYT", ref _PTBHYT, value);
            }
        }

        [ModelDefault("Caption", "% BHTN NLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHTN
        {
            get
            {
                return _PTBHTN;
            }
            set
            {
                SetPropertyValue("PTBHTN", ref _PTBHTN, value);
            }
        }

        [ModelDefault("Caption", "% Công Đoàn")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTCongDoan
        {
            get
            {
                return _PTCongDoan;
            }
            set
            {
                SetPropertyValue("PTCongDoan", ref _PTCongDoan, value);
            }
        }

        [ModelDefault("Caption", "% Công đoàn NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTCongDoanCTY
        {
            get
            {
                return _PTCongDoanCTY;
            }
            set
            {
                SetPropertyValue("PTCongDoanCTY", ref _PTCongDoanCTY, value);
            }
        }

        [ModelDefault("Caption", "% BHXH NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHXHCTY
        {
            get
            {
                return _PTBHXHCTY;
            }
            set
            {
                SetPropertyValue("PTBHXHCTY", ref _PTBHXHCTY, value);
            }
        }

        [ModelDefault("Caption", "% BHYT NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHYTCTY
        {
            get
            {
                return _PTBHYTCTY;
            }
            set
            {
                SetPropertyValue("PTBHYTCTY", ref _PTBHYTCTY, value);
            }
        }

        [ModelDefault("Caption", "% BHTN NSDLĐ đóng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTBHTNCTY
        {
            get
            {
                return _PTBHTNCTY;
            }
            set
            {
                SetPropertyValue("PTBHTNCTY", ref _PTBHTNCTY, value);
            }
        }

        [ModelDefault("Caption", "% Trích dự phòng")]
        [ModelDefault("EditMask", "n2")]
        [ModelDefault("DisplayFormat", "n2")]
        [RuleRange("", DefaultContexts.Save, 0, 100)]
        public decimal PTTrichDuPhong
        {
            get
            {
                return _PTTrichDuPhong;
            }
            set
            {
                SetPropertyValue("PTTrichDuPhong", ref _PTTrichDuPhong, value);
            }
        }

        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("Caption", "Lãi suất BHXH, BHTN (%)")]
        public decimal LaiSuatBHXH
        {
            get
            {
                return _LaiSuatBHXH;
            }
            set
            {
                SetPropertyValue("LaiSuatBHXH", ref _LaiSuatBHXH, value);
            }
        }

        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("Caption", "Lãi suất BHYT (%)")]
        public decimal LaiSuatBHYT
        {
            get
            {
                return _LaiSuatBHYT;
            }
            set
            {
                SetPropertyValue("LaiSuatBHYT", ref _LaiSuatBHYT, value);
            }
        }

        [ModelDefault("Caption", "Tính thuế TNCN lũy tiến")]
        public bool TinhThueTNCNLuyTien
        {
            get
            {
                return _TinhThueTNCNLuyTien;
            }
            set
            {
                SetPropertyValue("TinhThueTNCNLuyTien", ref _TinhThueTNCNLuyTien, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ bản thân")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal GiamTruBanThan
        {
            get
            {
                return _GiamTruBanThan;
            }
            set
            {
                SetPropertyValue("GiamTruBanThan", ref _GiamTruBanThan, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ người phụ thuộc")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal GiamTruNguoiPhuThuoc
        {
            get
            {
                return _GiamTruNguoiPhuThuoc;
            }
            set
            {
                SetPropertyValue("GiamTruNguoiPhuThuoc", ref _GiamTruNguoiPhuThuoc, value);
            }
        }

        [ModelDefault("Caption", "Ngân sách nhà nước")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal NganSachNhaNuoc
        {
            get
            {
                return _NganSachNhaNuoc;
            }
            set
            {
                SetPropertyValue("NganSachNhaNuoc", ref _NganSachNhaNuoc, value);
            }
        }


        [ModelDefault("Caption", "Đơn giá phụ cấp")]
        public decimal DonGiaPhuCap
        {
            get
            {
                return _DonGiaPhuCap;
            }
            set
            {
                SetPropertyValue("DonGiaPhuCap", ref _DonGiaPhuCap, value);
            }
        }

        [ModelDefault("Caption", "Mức trợ cấp thôi việc")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal MucTroCapThoiViec
        {
            get
            {
                return _MucTroCapThoiViec;
            }
            set
            {
                SetPropertyValue("MucTroCapThoiViec", ref _MucTroCapThoiViec, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRange("", DefaultContexts.Save, 1, 31)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int TinhTangThemTuNgay
        {
            get
            {
                return _TinhTangThemTuNgay;
            }
            set
            {
                SetPropertyValue("TinhTangThemTuNgay", ref _TinhTangThemTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public ThuNhapTangThemEnum TinhTangThemTuThang
        {
            get
            {
                return _TinhTangThemTuThang;
            }
            set
            {
                SetPropertyValue("TinhTangThemTuThang", ref _TinhTangThemTuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRange("", DefaultContexts.Save, 1, 31)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int TinhTangThemDenNgay
        {
            get
            {
                return _TinhTangThemDenNgay;
            }
            set
            {
                SetPropertyValue("TinhTangThemDenNgay", ref _TinhTangThemDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public ThuNhapTangThemEnum TinhTangThemDenThang
        {
            get
            {
                return _TinhTangThemDenThang;
            }
            set
            {
                SetPropertyValue("TinhTangThemDenThang", ref _TinhTangThemDenThang, value);
            }
        }

        [ModelDefault("Caption", "Mức chi trả hệ số")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal MucChiTraHS
        {
            get
            {
                return _MucChiTraHS;
            }
            set
            {
                SetPropertyValue("MucChiTraHS", ref _MucChiTraHS, value);
            }
        }

        [ModelDefault("Caption", "Hệ số thu nhập tăng thêm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSThuNhapTangThem
        {
            get
            {
                return _HSThuNhapTangThem;
            }
            set
            {
                SetPropertyValue("HSThuNhapTangThem", ref _HSThuNhapTangThem, value);
            }
        }

        
        public ThongTinChung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LuongCoBan = 1490000;
            PTBHXH = 8m;
            PTBHYT = 1.5m;
            PTBHTN = 1m;
            PTBHXHCTY = 18m;
            PTBHYTCTY = 3m;
            PTBHTNCTY = 1m;
            PTCongDoan = 1m;
            SoNgayPhepNam = 12;
            ThoiGianTangSoNgayPhepNam = 5;
            SoNgayPhepMoiLanTang = 1;
            SoGioNgay = 8;
            GiamTruBanThan = 11000000;
            GiamTruNguoiPhuThuoc = 4400000;
            DonGiaPhuCap = 800000;
            MucTroCapThoiViec = 0.5m;
            //
            MucChiTraHS = 1650000;
            HSThuNhapTangThem = 0.25m;
            //
            MaTruong = TruongConfig.MaTruong;
            SoNgayThangNhaNuoc = 26;
            SoNgayThang = 22;
            TinhTangThemTuNgay = 16;
            TinhTangThemDenNgay = 15;
            TinhTangThemTuThang = ThuNhapTangThemEnum.ThangTruoc;
            TinhTangThemDenThang = ThuNhapTangThemEnum.ThangNay;
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            MaTruong = TruongConfig.MaTruong;
        }
    }
}
