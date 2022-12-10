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
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết khối lượng giảng dạy")]
    [DefaultProperty("ThongTin")]
    [Appearance("Hide_HeSo", TargetItems = "HeSoCoSo;HeSoLuong;HeSoMonMoi;HeSoGiangDayNgoaiGio", Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat <> 'QNU'")]
    [Appearance("ToMauTongTiet", TargetItems = "TongHeSo;TongTietLyThuyetThaoLuan;TongGioTNTH_DA_BTL", BackColor = "Yellow", FontColor = "Red")]
    [Appearance("ToMauTongGio", TargetItems = "TongGio", BackColor = "Aquamarine", FontColor = "Red")]

    [Appearance("Hide_QNU", TargetItems = "SoTietKiemTra"
                                            , Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat = 'QNU'")]

    [Appearance("Hide_HVNH", TargetItems = "KhoaHoc;"
                                            +"SoTietThaoLuan;SoTietThucHanh;SoNhomThucHanh;SoGioTNTH;SoBaiTNTH;SoBaiTNTH;"
                                            +"SoTiet_DoAn;SoTiet_BaiTapLon;"
                                            + "HeSo_MonMoi;HeSo_TNTH;HeSo_ThaoLuan;"
                                            +"SoBaiTNTH_GioChuan;HeSo_DoAn;HeSo_BTL;GioQuyDoiThaoLuan;"
                                            + "GioQuyDoiThucHanh;GioQuyDoiDoAn;GioQuyDoiBTL;SoTietKiemTra;TongGioTNTH_DA_BTL;TenLopSinhVien;HeDaoTao"
                                            , Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat = 'NHH'")]
    [Appearance("Hide_DNU", TargetItems = "CoSoGiangDay;NgonNguGiangDay;MaNhom;GioGiangDay;LoaiHocPhan;"
                                            + "SoBaiTNTH;SoTiet_DoAn;SoTiet_BaiTapLon;SoNhomThucHanh;HeSo_CoSo;HeSo_DoAn;"
                                            + "HeSo_Luong;HeSo_GiangDayNgoaiGio;HeSo_MonMoi;HeSo_TinChi;HeSo_ThaoLuan;SoBaiTNTH_GioChuan;"
                                            + "HeSo_BTL;HeSo_BacDaoTao;HeSo_NgonNgu;TongHeSo;GioQuyDoiThaoLuan;TongGioLyThuyetThaoLuan;"
                                            + "GioQuyDoiChamBaiTNTH;GioQuyDoiDoAn;GioQuyDoiBT;TongGioTNTH_DA_BTL;SoBaiTNTH_GioChuan;"
                                            + "SoTietThaoLuan;SoNhomThucHanh;SoGioTNTH;GioQuyDoiBTL;TongGio;MaMonHoc"
                                            , Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat = 'DNU'")]
    public class ChiTietKhoiLuongGiangDay : BaseObject
    {
        #region key
        private KhoiLuongGiangDay _KhoiLuongGiangDay;
        [Association("KhoiLuongGiangDay-ListChiTietKhoiLuongGiangDay")]
        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        [Browsable(false)]
        public KhoiLuongGiangDay KhoiLuongGiangDay
        {
            get
            {
                return _KhoiLuongGiangDay;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay", ref _KhoiLuongGiangDay, value);
            }
        }
        #endregion

        #region Khai báo
        #region KB Thông tin nhân viên
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private string _MaGiangVien;

        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;//Học vị
        #endregion

        #region KB Môn học
        private CoSoGiangDay _CoSoGiangDay;
        private string _HocKy;

        private int _STT;
        private string _TenMonHoc;
        private NgonNguEnum? _NgonNguGiangDay;
        private decimal _SoTinChi;
        private string _LopHocPhan;
        private string _MaNhom;
        private string _MaMonHoc;
        private GioGiangDayEnum? _GioGiangDay;
        private string _LopSinhVien;
        private string _TenLopSinhVien;
        private int _SoLuongSV;
        private LoaiHocPhanEnum? _LoaiHocPhan;
        private DayOfWeek? _Thu;
        private string _GhiChu;

        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private string _KhoaHoc;
        private decimal _SoTietLyThuyet;
        private decimal _SoTietThaoLuan;
        private decimal _SoTietThucHanh;
        private int _SoNhomThucHanh;
        private decimal _SoGioTNTH;
        private decimal _SoBaiTNTH;
        private decimal _SoTiet_DoAn;
        private decimal _SoTiet_BaiTapLon;

        private decimal _SoTietKiemTra;
        private bool _CoChinhSua;
        #endregion

        #region KB Hệ số

        private decimal _HeSo_ChucDanh;
        private decimal _HeSo_LopDong;
        private decimal _HeSo_CoSo;
        private decimal _HeSo_Luong;
        private decimal _HeSo_GiangDayNgoaiGio;
        private decimal _HeSo_MonMoi;
        private decimal _HeSo_TinChi;
        private decimal _HeSo_TNTH;
        private decimal _HeSo_ThaoLuan;
        private int _SoBaiTNTH_GioChuan;
        private decimal _HeSo_DoAn;
        private decimal _HeSo_BTL;
        private decimal _HeSo_BacDaoTao;
        private decimal _HeSo_NgonNgu;
        private decimal _HeSo_HocKy; 
        #endregion

        #region Quy đổi
        private decimal _TongHeSo;
        private decimal _GioQuyDoiLyThuyet;
        private decimal _GioQuyDoiThaoLuan;
        private decimal _TongGioLyThuyetThaoLuan;
        private decimal _GioQuyDoiThucHanh;
        private decimal _GioQuyDoiChamBaiTNTH;
        private decimal _GioQuyDoiDoAn;
        private decimal _GioQuyDoiBTL;
        private decimal _TongGioTNTH_DA_BTL;
        private decimal _TongGio;
        #endregion
        #endregion

        #region Thông tin nhân viên
        [ModelDefault("Caption", "Bộ phận")]
        [Browsable(false)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Mã giảng viên")]
        public string MaGiangVien
        {
            get { return _MaGiangVien; }
            set { SetPropertyValue("MaGiangVien", ref _MaGiangVien, value); }
        }       

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]//Học vị
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }
        #endregion

        #region Môn học

        [ModelDefault("Caption", "Số thứ tự")]
        [Browsable(false)]
        public int STT
        {
            get { return _STT; }
            set { SetPropertyValue("STT", ref _STT, value); }
        }
        [ModelDefault("Caption", "Địa điểm giảng dạy")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
        }
        [ModelDefault("Caption", "Học kỳ")]
        public string HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Tên môn học")]
        //[Size(-1)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Thứ")]
        public DayOfWeek? Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }
        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguEnum? NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
        [ModelDefault("Caption", "Loại giờ giảng")]
        public GioGiangDayEnum? GioGiangDay
        {
            get { return _GioGiangDay; }
            set { SetPropertyValue(" GioGiangDay", ref _GioGiangDay, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp sinh viên")]
        //[Size(-1)]
        public string LopSinhVien
        {
            get { return _LopSinhVien; }
            set { SetPropertyValue("LopSinhVien", ref _LopSinhVien, value); }
        }
        [ModelDefault("Caption", "Tên lớp sinh viên")]
        [Size(-1)]
        public string TenLopSinhVien
        {
            get { return _TenLopSinhVien; }
            set { SetPropertyValue("TenLopSinhVien", ref _TenLopSinhVien, value); }
        }
        [ModelDefault("Caption", "Mã nhóm")]
        //[Size(-1)]
        public string MaNhom
        {
            get { return _MaNhom; }
            set { SetPropertyValue("MaNhom", ref _MaNhom, value); }
        }
        [ModelDefault("Caption", "Mã môn học")]
        //[Size(-1)]
        public string MaMonHoc
        {
            get { return _MaMonHoc; }
            set { SetPropertyValue("MaMonHoc", ref _MaMonHoc, value); }
        }

        [ModelDefault("Caption", "Số lượng SV")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }

        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhanEnum? LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        [VisibleInListView(false)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Khóa học")]
        //[Size(-1)]
        public string KhoaHoc
        {
            get { return _KhoaHoc; }
            set { SetPropertyValue("KhoaHoc", ref _KhoaHoc, value); }
        }

        [ModelDefault("Caption", "Tiết lý thuyết")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietLyThuyet
        {
            get { return _SoTietLyThuyet; }
            set { SetPropertyValue("SoTietLyThuyet", ref _SoTietLyThuyet, value); }
        }
        [ModelDefault("Caption", "Tiết thực hành")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucHanh
        {
            get { return _SoTietThucHanh; }
            set { SetPropertyValue("SoTietThucHanh", ref _SoTietThucHanh, value); }
        }
        [ModelDefault("Caption", "Số nhóm thực hành")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public int SoNhomThucHanh
        {
            get { return _SoNhomThucHanh; }
            set { SetPropertyValue("SoNhomThucHanh", ref _SoNhomThucHanh, value); }
        }
        [ModelDefault("Caption", "Số giờ TH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioTNTH
        {
            get { return _SoGioTNTH; }
            set { SetPropertyValue("SoGioTNTH", ref _SoGioTNTH, value); }
        }

        [ModelDefault("Caption", "Số bài TH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiTNTH
        {
            get { return _SoBaiTNTH; }
            set { SetPropertyValue("SoBaiTNTH", ref _SoBaiTNTH, value); }
        }

        [ModelDefault("Caption", "Số tiết đồ án")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTiet_DoAn
        {
            get { return _SoTiet_DoAn; }
            set { SetPropertyValue("SoTiet_DoAn", ref _SoTiet_DoAn, value); }
        }

        [ModelDefault("Caption", "Số tiết BTL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTiet_BaiTapLon
        {
            get { return _SoTiet_BaiTapLon; }
            set { SetPropertyValue("SoTiet_BaiTapLon", ref _SoTiet_BaiTapLon, value); }
        }

        [ModelDefault("Caption", "Số tiết kiểm tra")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTietKiemTra
        {
            get { return _SoTietKiemTra; }
            set { SetPropertyValue("SoTietKiemTra", ref _SoTietKiemTra, value); }
        }
        #endregion

        #region Quy đổi
        #region Hệ số

        [ModelDefault("Caption", "HS chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }

        [ModelDefault("Caption", "HS lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }

        [ModelDefault("Caption", "HS cơ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_CoSo
        {
            get { return _HeSo_CoSo; }
            set { SetPropertyValue("HeSo_CoSo", ref _HeSo_CoSo, value); }
        }

        [ModelDefault("Caption", "HS lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_Luong
        {
            get { return _HeSo_Luong; }
            set { SetPropertyValue("HeSo_Luong", ref _HeSo_Luong, value); }
        }

        [ModelDefault("Caption", "HS giảng dạy ngoài giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GiangDayNgoaiGio
        {
            get { return _HeSo_GiangDayNgoaiGio; }
            set { SetPropertyValue("HeSo_GiangDayNgoaiGio", ref _HeSo_GiangDayNgoaiGio, value); }
        }
        [ModelDefault("Caption", "HS môn mới")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_MonMoi
        {
            get { return _HeSo_MonMoi; }
            set { SetPropertyValue("HeSo_MonMoi", ref _HeSo_MonMoi, value); }
        }
        [ModelDefault("Caption", "HS Tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_TinChi
        {
            get { return _HeSo_TinChi; }
            set { SetPropertyValue("HeSo_TinChi", ref _HeSo_TinChi, value); }
        }
        [ModelDefault("Caption", "HS TNTH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_TNTH
        {
            get { return _HeSo_TNTH; }
            set { SetPropertyValue("HeSo_TNTH", ref _HeSo_TNTH, value); }
        }
      
        [ModelDefault("Caption", "HS thảo luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ThaoLuan
        {
            get { return _HeSo_ThaoLuan; }
            set { SetPropertyValue("HeSo_ThaoLuan", ref _HeSo_ThaoLuan, value); }
        }
        [ModelDefault("Caption", "Số bài TNTH / Giờ")]
        public int SoBaiTNTH_GioChuan
        {
            get { return _SoBaiTNTH_GioChuan; }
            set { SetPropertyValue("SoBaiTNTH_GioChuan", ref _SoBaiTNTH_GioChuan, value); }
        }
         [ModelDefault("Caption", "HS đồ án")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_DoAn
        {
            get { return _HeSo_DoAn; }
            set { SetPropertyValue("HeSo_DoAn", ref _HeSo_DoAn, value); }
        }
         [ModelDefault("Caption", "HS BTL")]
         [ModelDefault("DisplayFormat", "N2")]
         [ModelDefault("EditMask", "N2")]
         public decimal HeSo_BTL
         {
             get { return _HeSo_BTL; }
             set { SetPropertyValue("HeSo_BTL", ref _HeSo_BTL, value); }
         }
         [ModelDefault("Caption", "HS bậc đào tạo")]
         [ModelDefault("DisplayFormat", "N2")]
         [ModelDefault("EditMask", "N2")]
         public decimal HeSo_BacDaoTao
         {
             get { return _HeSo_BacDaoTao; }
             set { SetPropertyValue("HeSo_BacDaoTao", ref _HeSo_BacDaoTao, value); }
         }
         [ModelDefault("Caption", "HS ngôn ngữ")]
         [ModelDefault("DisplayFormat", "N2")]
         [ModelDefault("EditMask", "N2")]
         public decimal HeSo_NgonNgu
         {
             get { return _HeSo_NgonNgu; }
             set { SetPropertyValue("HeSo_NgonNgu", ref _HeSo_NgonNgu, value); }
         }
         [ModelDefault("Caption", "HS học kỳ")]
         [ModelDefault("DisplayFormat", "N2")]
         [ModelDefault("EditMask", "N2")]
         public decimal HeSo_HocKy
         {
             get { return _HeSo_HocKy; }
             set { SetPropertyValue("HeSo_HocKy", ref _HeSo_HocKy, value); }
         }
        #endregion

        #region QuyDoi
        [ModelDefault("Caption", "Tổng hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongHeSo
        {
            get { return _TongHeSo; }
            set { SetPropertyValue("TongHeSo", ref _TongHeSo, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi (LT)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiLyThuyet
        {
            get { return _GioQuyDoiLyThuyet; }
            set { SetPropertyValue("GioQuyDoiLyThuyet", ref _GioQuyDoiLyThuyet, value); }
        }
        [ModelDefault("Caption", "Tiết thảo luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThaoLuan
        {
            get { return _SoTietThaoLuan; }
            set { SetPropertyValue("SoTietThaoLuan", ref _SoTietThaoLuan, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi thảo luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiThaoLuan
        {
            get { return _GioQuyDoiThaoLuan; }
            set { SetPropertyValue("GioQuyDoiThaoLuan", ref _GioQuyDoiThaoLuan, value); }
        }
        [ModelDefault("Caption", "Tổng giờ (LT+TL)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioLyThuyetThaoLuan
        {
            get { return _TongGioLyThuyetThaoLuan; }
            set { SetPropertyValue("TongGioLyThuyetThaoLuan", ref _TongGioLyThuyetThaoLuan, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi (TH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiThucHanh
        {
            get { return _GioQuyDoiThucHanh; }
            set { SetPropertyValue("GioQuyDoiThucHanh", ref _GioQuyDoiThucHanh, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi chấm bài (TNTH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiChamBaiTNTH
        {
            get { return _GioQuyDoiChamBaiTNTH; }
            set { SetPropertyValue("GioQuyDoiChamBaiTNTH", ref _GioQuyDoiChamBaiTNTH, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi (DA)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiDoAn
        {
            get { return _GioQuyDoiDoAn; }
            set { SetPropertyValue("GioQuyDoiDoAn", ref _GioQuyDoiDoAn, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi (BTL)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiBTL
        {
            get { return _GioQuyDoiBTL; }
            set { SetPropertyValue("GioQuyDoiBTL", ref _GioQuyDoiBTL, value); }
        }

        [ModelDefault("Caption", "Tổng giờ (TNTH, DA, BTL)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioTNTH_DA_BTL
        {
            get { return _TongGioTNTH_DA_BTL; }
            set { SetPropertyValue("TongGioTNTH_DA_BTL", ref _TongGioTNTH_DA_BTL, value); }
        }

        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get
            {
                return _TongGio;
            }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        #endregion
        #endregion

        [ModelDefault("Caption", "Có chỉnh sửa")]
        [Browsable(false)]
        public bool CoChinhSua
        {
            get { return _CoChinhSua; }
            set { SetPropertyValue("CoChinhSua", ref _CoChinhSua, value); }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        [VisibleInDetailView(false)]
        public String ThongTin
        {
            get
            {
                return String.Format("{0}", this != null ? this.MaGiangVien + " - " + this.NhanVien.HoTen + " - " + this.TenMonHoc : "");
            }
        }
        public ChiTietKhoiLuongGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
