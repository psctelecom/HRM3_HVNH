using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Text;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TaoMaQuanLy;
using System.Data.SqlClient;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DatabaseUpdate;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng khoán")]
    [Appearance("HopDong_Khoan1", TargetItems = "ChucVu", Visibility = ViewItemVisibility.Hide, Criteria = "")]
    [Appearance("HopDong_Khoan.PhuLuc", TargetItems = "PhanLoai;TienLuong;Huong85PhanTramLuong;ThamGiaBHXH;HinhThucThanhToan;ChucDanhChuyenMon", Visibility = ViewItemVisibility.Hide, Criteria = "HopDongKhoan is not null")]
    [Appearance("HopDong_Khoan.ChinhThuc", TargetItems = "TapSuTuNgay;TapSuDenNgay;PhuCapTangThem;PhuCapTienXang;PhuCapTienAn;HopDongKhoan", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai=1 and HopDongKhoan is null")]
    [Appearance("HopDong_Khoan2", TargetItems = "NhanVien;QuocTich;BoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "!NguoiLaoDongCoTrongHoSo")]
    [Appearance("HopDong_Khoan3", TargetItems = "HoTen;DiaChiThuongTru;NgaySinh;NoiSinh;CMND;NgayCap;NoiCap;NoiLamViec", Visibility = ViewItemVisibility.Hide, Criteria = "NguoiLaoDongCoTrongHoSo")]

    public class HopDong_Khoan : HopDong_NhanVien
    {
        // Fields...
        private HopDong_Khoan _HopDongKhoan;
        private decimal _TienLuong;
        private decimal _PhuCapTangThem;
        private decimal _PhuCapTienXang;
        private decimal _PhuCapTienAn;
        private bool _ThamGiaBHXH;
        private bool _Huong85PhanTramLuong;
        private HopDongKhoanEnum _PhanLoai;
        private HinhThucThanhToanEnum _HinhThucThanhToan;
        private bool _NguoiLaoDongCoTrongHoSo;
        private string _HoTen;
        private string _DiaChiThuongTru;

        [ImmediatePostData]
        [ModelDefault("Caption", "Người lao động có trong hồ sơ")]
        public bool NguoiLaoDongCoTrongHoSo
        {
            get
            {
                return _NguoiLaoDongCoTrongHoSo;
            }
            set
            {
                SetPropertyValue("NguoiLaoDongCoTrongHoSo", ref _NguoiLaoDongCoTrongHoSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Người lao động")]
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

        [ModelDefault("Caption", "Địa chỉ thường trú")]
        public string DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public HopDongKhoanEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
                if (!IsLoading)
                {
                    if (value == HopDongKhoanEnum.ThuViec)
                    {
                        ThamGiaBHXH = false;
                        Huong85PhanTramLuong = true;
                        LoaiHopDong = "Hợp đồng khoán thử việc";
                    }
                    else
                    {
                        ThamGiaBHXH = true;
                        Huong85PhanTramLuong = false;
                        LoaiHopDong = "Hợp đồng khoán chính thức";
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
                if (!IsLoading && value > 0)
                    TaoTrichYeu();
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal PhuCapTienAn
        {
            get
            {
                return _PhuCapTienAn;
            }
            set
            {
                SetPropertyValue("PhuCapTienAn", ref _PhuCapTienAn, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền xăng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal PhuCapTienXang
        {
            get
            {
                return _PhuCapTienXang;
            }
            set
            {
                SetPropertyValue("PhuCapTienXang", ref _PhuCapTienXang, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tăng thêm")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal PhuCapTangThem
        {
            get
            {
                return _PhuCapTangThem;
            }
            set
            {
                SetPropertyValue("PhuCapTangThem", ref _PhuCapTangThem, value);
            }
        }

        [ModelDefault("Caption", "Hình thức thanh toán")]
        public HinhThucThanhToanEnum HinhThucThanhToan
        {
            get
            {
                return _HinhThucThanhToan;
            }
            set
            {
                SetPropertyValue("HinhThucThanhToan", ref _HinhThucThanhToan, value);
            }
        }

        [ModelDefault("Caption", "Hưởng 85% lương")]
        public bool Huong85PhanTramLuong
        {
            get
            {
                return _Huong85PhanTramLuong;
            }
            set
            {
                SetPropertyValue("Huong85PhanTramLuong", ref _Huong85PhanTramLuong, value);
            }
        }

        [ModelDefault("Caption", "Tham gia BHXH")]
        public bool ThamGiaBHXH
        {
            get
            {
                return _ThamGiaBHXH;
            }
            set
            {
                SetPropertyValue("ThamGiaBHXH", ref _ThamGiaBHXH, value);
            }
        }

        //dùng để tạo phụ lục
        [ImmediatePostData]
        [ModelDefault("Caption", "Hợp đồng khoán")]
        public HopDong_Khoan HopDongKhoan
        {
            get
            {
                return _HopDongKhoan;
            }
            set
            {
                SetPropertyValue("HopDongKhoan", ref _HopDongKhoan, value);
                if (!IsLoading && value != null)
                {
                    NhanVien = value.NhanVien;
                    BoPhan = value.BoPhan;
                    HinhThucHopDong = value.HinhThucHopDong;
                    TuNgay = value.TuNgay;
                    DenNgay = value.DenNgay;
                    NgayKy = value.NgayKy;
                    LoaiHopDong = "Phụ lục hợp đồng khoán";
                }
            }
        }

        public HopDong_Khoan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            PhanLoai = HopDongKhoanEnum.ThuViec;
            HinhThucThanhToan = HinhThucThanhToanEnum.ThanhToanQuaThe;
            NguoiLaoDongCoTrongHoSo = true;
            UpdateNhanVienList();

            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void AfterNhanVienChanged()
        {
            if (NhanVien != null)
            {
                //quốc tịch
                QuocTich = NhanVien.QuocTich;                
                TienLuong = NhanVien.NhanVienThongTinLuong.LuongKhoan;             
            }
        }

        protected override void TaoSoHopDong()
        {
            if (QuanLyHopDong != null)
            {
                SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDong.Oid);
                SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongKhoan, param);
            }
        }

        protected override void TaoTrichYeu()
        {
            StringBuilder sb = new StringBuilder("Hợp đồng khoán");

            if (TuNgay != DateTime.MinValue)
                sb.Append("; Từ ngày " + TuNgay.ToString("dd/MM/yyyy"));
            if (DenNgay != DateTime.MinValue)
                sb.Append(" đến ngày " + DenNgay.ToString("dd/MM/yyyy"));

            sb.Append(String.Format("; Tiền lương {0:N0}; PC tiền ăn: {1:N0}; PC tiền xăng: {2:N0}; PC tăng thêm: {3:N0}", TienLuong, PhuCapTienAn, PhuCapTienXang, PhuCapTangThem));

            if (GiayToHoSo != null)
                GiayToHoSo.TrichYeu = sb.ToString();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && HopDongCu == false)
            {
                if (HopDongKhoan != null)
                {
                    NhanVien.NhanVienThongTinLuong.PhuCapTienAn = PhuCapTienAn;
                    NhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXang;
                    NhanVien.NhanVienThongTinLuong.PhuCapTangThem = PhuCapTangThem;
                }
                else
                {
                    if (PhanLoai == HopDongKhoanEnum.ThuViec)
                    {
                        NhanVien.NhanVienThongTinLuong.LuongKhoan = TienLuong;
                        NhanVien.NhanVienThongTinLuong.PhuCapTienAn = PhuCapTienAn;
                        NhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXang;
                        NhanVien.NhanVienThongTinLuong.PhuCapTangThem = PhuCapTangThem;
                        NhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = Huong85PhanTramLuong;
                        if (ThamGiaBHXH)
                            NhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongKhoanCoBHXH;
                        else
                            NhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongKhoanKhongBHXH;
                    }
                    else
                    {
                        NhanVien.NhanVienThongTinLuong.LuongKhoan = TienLuong;
                        NhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = Huong85PhanTramLuong;
                        if (ThamGiaBHXH)
                            NhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongKhoanCoBHXH;
                        else
                            NhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongKhoanKhongBHXH;
                    }
                }
            }
        }
    }

}
