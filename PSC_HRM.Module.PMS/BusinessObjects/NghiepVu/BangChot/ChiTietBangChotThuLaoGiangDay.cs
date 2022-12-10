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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Chi tiết bảng chốt")]
    [ModelDefault("AllowNew", "false")]
    //[Appearance("Hide_HeSo", TargetItems = "HeSoCoSo;HeSoLuong;HeSoMonMoi;HeSoGiangDayNgoaiGio", Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat <> 'QNU'")]
    [Appearance("ToMauSoTien", TargetItems = "SoTienThanhToan", BackColor = "Yellow", FontColor = "Red")]
    [Appearance("ToMauTongGio", TargetItems = "TongGio", BackColor = "Aquamarine", FontColor = "Red")]
    [Appearance("ToMauDaTinhTien", TargetItems = "Khoa", BackColor = "Yellow", FontColor = "Red", Criteria = "DaTinhThuLao = 1")]
    [Appearance("Hide_QNU", TargetItems = "SoTietKiemTra"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinBangChot.BangChotThuLao.ThongTinTruong.TenVietTat = 'QNU'")]
    [Appearance("Hide_NHH", TargetItems = "SoTietKiemTra"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinBangChot.BangChotThuLao.ThongTinTruong.TenVietTat = 'NHH'")]
    public class ChiTietBangChotThuLaoGiangDay : BaseObject
    {
        #region key
        private ThongTinBangChot _ThongTinBangChot;
        [Association("ThongTinBangChot-ListChiTietBangChot")]
        [ModelDefault("Caption", "Bảng chốt thông tin giảng dạy")]
        [Browsable(false)]
        public ThongTinBangChot ThongTinBangChot
        {
            get
            {
                return _ThongTinBangChot;
            }
            set
            {
                SetPropertyValue("ThongTinBangChot", ref _ThongTinBangChot, value);
            }
        }

        private ThongTinBangChot_Moi _ThongTinBangChot_Moi;
        [Association("ThongTinBangChot_Moi-ListChiTietBangChot")]
        [ModelDefault("Caption", "Thông tin bảng chốt (Mới)")]
        [Browsable(false)]
        public ThongTinBangChot_Moi ThongTinBangChot_Moi
        {
            get
            {
                return _ThongTinBangChot_Moi;
            }
            set
            {
                SetPropertyValue("ThongTinBangChot_Moi", ref _ThongTinBangChot_Moi, value);
            }
        }
        #endregion

        #region Khai báo

        #region Hoạt động
        private BacDaoTao _BacDaoTao;
        private string _LopHocPhan;
        private string _TenHoatDong;
        private string _LoaiChuongTrinh;
        private LoaiHoatDongEnum _LoaiHoatDong;
        private HocKy _HocKy;
        #endregion
        #region Kết quả
        private decimal _TongGioA1;
        private decimal _TongGioA2;
        private decimal _TongGio;
        private decimal _TongNo;
        private decimal _SoTienThanhToan;
        private decimal _SoTietKiemTra;
        private bool _DaTinhThuLao;
        #endregion
        private Guid _OidChiTietKhoiLuongGiangDay;
        private Guid _OidChiTietThuLaoNhanVien;
        private Guid _OidChiTiet_Web;
        private LoaiNhanVien _LoaiNhanVien;
        private decimal _DonGia;
        private Guid _DanhMucLoaiKhongPhanThoiKhoaBieu;

        #endregion


        #region Hoạt động
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        [Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Hoạt động")]
        [Size(-1)]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
        }
        [ModelDefault("Caption", "Loại chương trình")]
        [ModelDefault("AllowEdit", "False")]
        public string LoaiChuongTrinh
        {
            get { return _LoaiChuongTrinh; }
            set { SetPropertyValue("LoaiChuongTrinh", ref _LoaiChuongTrinh, value); }
        }
        [ModelDefault("Caption", "Nguồn")]
        public LoaiHoatDongEnum LoaiHoatDong
        {
            get { return _LoaiHoatDong; }
            set { SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value); }
        }
        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        #endregion

        #region Kết quả
        [ModelDefault("Caption", "Tổng giờ A1 (HĐ khác)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA1
        {
            get { return _TongGioA1; }
            set { SetPropertyValue("TongGioA1", ref _TongGioA1, value); }
        }
        [ModelDefault("Caption", "Tổng giờ A2 (Giảng dạy)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA2
        {
            get { return _TongGioA2; }
            set { SetPropertyValue("TongGioA2", ref _TongGioA2, value); }
        }
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        [ModelDefault("Caption", "Số tiền nợ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongNo
        {
            get { return _TongNo; }
            set { SetPropertyValue("TongNo", ref _TongNo, value); }
        }
        [ModelDefault("Caption", "Số tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        [ModelDefault("Caption", "Số tiết kiểm tra")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietKiemTra
        {
            get { return _SoTietKiemTra; }
            set { SetPropertyValue("SoTietKiemTra", ref _SoTietKiemTra, value); }
        }

        [ModelDefault("Caption", "Đã tính thù lao")]
        [ModelDefault("AllowEdit","False")]
        public bool DaTinhThuLao
        {
            get { return _DaTinhThuLao; }
            set { SetPropertyValue("DaTinhThuLao", ref _DaTinhThuLao, value); }
        }

        #endregion
        [Browsable(false)]
        [ModelDefault("Caption", "Oid Chi tiết khối lượng giảng dạy")]
        public Guid OidChiTietKhoiLuongGiangDay
        {
            get
            {
                return _OidChiTietKhoiLuongGiangDay;
            }
            set
            {
                SetPropertyValue("OidChiTietKhoiLuongGiangDay", ref _OidChiTietKhoiLuongGiangDay, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Oid Chi tiết thù lao nhân viên")]
        public Guid OidChiTietThuLaoNhanVien
        {
            get
            {
                return _OidChiTietThuLaoNhanVien;
            }
            set
            {
                SetPropertyValue("OidChiTietThuLaoNhanVien", ref _OidChiTietThuLaoNhanVien, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Oid Chi tiết - sử dụng cho web")]
        public Guid OidChiTiet_Web
        {
            get
            {
                return _OidChiTiet_Web;
            }
            set
            {
                SetPropertyValue("OidChiTiet_Web", ref _OidChiTiet_Web, value);
            }
        }
        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiNhanVien LoaiNhanVien
        {
            get
            {
                return _LoaiNhanVien;
            }
            set
            {
                SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [Browsable(false)]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Phân loại tkb")]
        [Browsable(false)]
        public Guid DanhMucLoaiKhongPhanThoiKhoaBieu
        {
            get
            {
                return _DanhMucLoaiKhongPhanThoiKhoaBieu;
            }
            set
            {
                SetPropertyValue("DanhMucLoaiKhongPhanThoiKhoaBieu", ref _DanhMucLoaiKhongPhanThoiKhoaBieu, value);
            }
        }

        public ChiTietBangChotThuLaoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}