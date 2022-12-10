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
    [Appearance("Hide_HeSo_TG", TargetItems = "HeSoCoSo;HeSoLuong;HeSoMonMoi", Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay_ThinhGiang.ThongTinTruong.TenVietTat = 'HUFLIT'")]
    [Appearance("ToMauTongTiet", TargetItems = "TongHeSo;TongTietLyThuyetThaoLuan;TongGioTNTH_DA_BTL", BackColor = "Yellow", FontColor = "Red")]
    [Appearance("ToMauTongGio", TargetItems = "TongGio", BackColor = "Aquamarine", FontColor = "Red")]
    public class ChiTietKhoiLuongGiangDay_Moi : BaseObject
    {
        #region key
        private KhoiLuongGiangDay _KhoiLuongGiangDay;
        [Association("KhoiLuongGiangDay-ListChiTietKhoiLuongGiangDay_Moi")]
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

        private KhoiLuongGiangDay_ThinhGiang _KhoiLuongGiangDay_ThinhGiang;
        [Association("KhoiLuongGiangDay_ThinhGiang-ListChiTietKhoiLuongGiangDay_ThinhGiang")]
        [ModelDefault("Caption", "Khối lượng giảng dạy(thỉnh giảng)")]
        [Browsable(false)]
        public KhoiLuongGiangDay_ThinhGiang KhoiLuongGiangDay_ThinhGiang
        {
            get
            {
                return _KhoiLuongGiangDay_ThinhGiang;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay_ThinhGiang", ref _KhoiLuongGiangDay_ThinhGiang, value);
            }
        }
        #endregion

        #region Khai báo

        #region KB Thông tin nhân viên
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        private ChucDanh _ChucDanh;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;//Học vị
        #endregion

        #region KB Môn học
        private string _HocKy;

        private string _TenMonHoc;
        private bool _ChuyenNganh;

        private string _LoaiChuongTrinh;
        private string _MaHocPhan;
        private string _LopHocPhan;

        private bool _CoXepTKB;
        private string _MaLopGhep;
        private string _MaLopSV;
        private string _TenLopSV;
        private decimal _SoTinChi;
        private int _SoLuongSV;
        private DayOfWeek _Thu;
        private int _TietBD;
        private int _TietKT; 
        private DateTime _NgayBD;
        private DateTime _NgayKT;
        private decimal _SoTietThucDay;

        private LoaiHocPhanEnum? _LoaiHocPhan;
        private LoaiMonHoc _LoaiMonHoc;
        private NgonNguEnum? _NgonNguGiangDay;
        private NgonNguGiangDay _NgonNguGiangDay_DanhMuc;
        private GioGiangDayEnum? _GioGiangDay;


        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        private CoSoGiangDay _CoSoGiangDay;
        private DanhMucTinhChatHocPhan _DanhMucTinhChatHocPhan;
        private string _PhongHoc;
        private bool _LopChatLuongCao;


        private string _GhiChu;
        private string _GhiChuThanhTra;
        private bool _LopTinh;
        #endregion

        #region KB Hệ số

        private decimal _HeSo_ChucDanh;
        private decimal _HeSo_LopDong;
        private decimal _HeSo_DaoTao;
        private decimal _HeSo_CoSo;
        private decimal _HeSo_GiangDayNgoaiGio;
        private decimal _HeSo_TinChi;
        private decimal _HeSo_TNTH;
        private decimal _HeSo_BacDaoTao;
        private decimal _HeSo_NgonNgu;
        private decimal _HeSo_DayOnline;
        private decimal _HeSo_ChuyenNghiep;
        #endregion

        #region Quy đổi

        private decimal _TongHeSo;
        private decimal _GioQuyDoiLyThuyet;
        private decimal _GioQuyDoiThucHanh;
        private decimal _TongGio;
        private bool _Import;
        private bool _DayOnline;
        #endregion

        #region VHU quy chế 94
        private string _TexPhanLoaiHocPhan;
        private Guid _OidPhanLoaiHocPhan;
        private bool _DaChinhSua;
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

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get { return _ChucDanh; }
            set { SetPropertyValue("ChucDanh", ref _ChucDanh, value); }
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

        [ModelDefault("Caption", "Học kỳ")]
        public string HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [Size(-1)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Chuyên ngành")]
        public bool ChuyenNganh
        {
            get { return _ChuyenNganh; }
            set { _ChuyenNganh = value; }
        }
        [ModelDefault("Caption", "Loại chương trình")]
        [ModelDefault("AllowEdit", "False")]
        public string LoaiChuongTrinh
        {
            get { return _LoaiChuongTrinh; }
            set { SetPropertyValue("LoaiChuongTrinh", ref _LoaiChuongTrinh, value); }
        }
        [ModelDefault("Caption", "Mã học phần")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Có xếp TKB")]
        public bool CoXepTKB
        {
            get { return _CoXepTKB; }
            set { SetPropertyValue("CoXepTKB", ref _CoXepTKB, value); }
        }
        [ModelDefault("Caption", "Mã lớp ghép")]
        public string MaLopGhep
        {
            get { return _MaLopGhep; }
            set { SetPropertyValue("MaLopGhep", ref _MaLopGhep, value); }
        }
        [ModelDefault("Caption", "Mã lớp SV")]
        public string MaLopSV
        {
            get { return _MaLopSV; }
            set { SetPropertyValue("MaLopSV", ref _MaLopSV, value); }
        }
        [ModelDefault("Caption", "Tên lớp SV")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
        [ModelDefault("Caption", "Số lượng SV")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }
        [ModelDefault("Caption", "Thứ (giảng dạy)")]
        public DayOfWeek Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }
        [ModelDefault("Caption", "Tiết bắt đầu")]
        public int TietBD
        {
            get { return _TietBD; }
            set { SetPropertyValue("TietBD", ref _TietBD, value); }
        }
        [ModelDefault("Caption", "Tiết kết thúc")]
        public int TietKT
        {
            get { return _TietKT; }
            set { SetPropertyValue("TietKT", ref _TietKT, value); }
        }

        [ModelDefault("Caption", "Ngày bắt đầu")]
        public DateTime NgayBD
        {
            get { return _NgayBD; }
            set { SetPropertyValue("NgayBD", ref _NgayBD, value); }
        }
        [ModelDefault("Caption", "Ngày kết thúc")]
        public DateTime NgayKT
        {
            get { return _NgayKT; }
            set { SetPropertyValue("NgayKT", ref _NgayKT, value); }
        }
        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
        }
        [ModelDefault("Caption", "Học phần")]
        public LoaiMonHoc LoaiMonHoc
        {
            get { return _LoaiMonHoc; }
            set { SetPropertyValue("LoaiMonHoc", ref _LoaiMonHoc, value); }
        }
        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhanEnum? LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ")]
        public NgonNguEnum? NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }
        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguGiangDay NgonNguGiangDay_DanhMuc
        {
            get { return _NgonNguGiangDay_DanhMuc; }
            set { SetPropertyValue("NgonNguGiangDay_DanhMuc", ref _NgonNguGiangDay_DanhMuc, value); }
        }
        [ModelDefault("Caption", "Loại giờ giảng")]
        public GioGiangDayEnum? GioGiangDay
        {
            get { return _GioGiangDay; }
            set { SetPropertyValue(" GioGiangDay", ref _GioGiangDay, value); }
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

        [ModelDefault("Caption", "Địa điểm giảng dạy")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
        }

        [ModelDefault("Caption", "Tính chất môn học")]
        public DanhMucTinhChatHocPhan DanhMucTinhChatHocPhan
        {
            get { return _DanhMucTinhChatHocPhan; }
            set { SetPropertyValue("DanhMucTinhChatHocPhan", ref _DanhMucTinhChatHocPhan, value); }
        }
        
        [ModelDefault("Caption", "Phòng học")]
        public string PhongHoc
        {
            get { return _PhongHoc; }
            set { SetPropertyValue("PhongHoc", ref _PhongHoc, value); }
        }
        [ModelDefault("Caption", "Lớp CLC")]
        public bool LopChatLuongCao
        {
            get { return _LopChatLuongCao; }
            set { SetPropertyValue("LopChatLuongCao", ref _LopChatLuongCao, value); }
        }

        [ModelDefault("Caption", "Lớp tỉnh")]
        [Browsable(false)]
        public bool LopTinh
        {
            get { return _LopTinh; }
            set { SetPropertyValue("LopTinh", ref _LopTinh, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        [VisibleInListView(false)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
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
        [ModelDefault("Caption", "HS đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_DaoTao
        {
            get { return _HeSo_DaoTao; }
            set { SetPropertyValue("HeSo_DaoTao", ref _HeSo_DaoTao, value); }
        }
        [ModelDefault("Caption", "HS cơ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_CoSo
        {
            get { return _HeSo_CoSo; }
            set { SetPropertyValue("HeSo_CoSo", ref _HeSo_CoSo, value); }
        }

        [ModelDefault("Caption", "HS dạy online")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_DayOnline
        {
            get { return _HeSo_DayOnline; }
            set { SetPropertyValue("HeSo_DayOnline", ref _HeSo_DayOnline, value); }
        }

        [ModelDefault("Caption", "HS giảng dạy ngoài giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GiangDayNgoaiGio
        {
            get { return _HeSo_GiangDayNgoaiGio; }
            set { SetPropertyValue("HeSo_GiangDayNgoaiGio", ref _HeSo_GiangDayNgoaiGio, value); }
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
        [ModelDefault("Caption", "HS chuyên nghiệp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ChuyenNghiep
        {
            get { return _HeSo_ChuyenNghiep; }
            set { SetPropertyValue("HeSo_ChuyenNghiep", ref _HeSo_ChuyenNghiep, value); }
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

        [ModelDefault("Caption", "Giờ quy đổi (TH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoiThucHanh
        {
            get { return _GioQuyDoiThucHanh; }
            set { SetPropertyValue("GioQuyDoiThucHanh", ref _GioQuyDoiThucHanh, value); }
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
        [ModelDefault("Caption", "Import")]
        public bool Import
        {
            get
            {
                return _Import;
            }
            set { SetPropertyValue("Import", ref _Import, value); }
        }

        [ModelDefault("Caption", "Dạy online")]
        public bool DayOnline
        {
            get
            {
                return _DayOnline;
            }
            set { SetPropertyValue("DayOnline", ref _DayOnline, value); }
        }
        #endregion
        #endregion

        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        [VisibleInDetailView(false)]
        public String ThongTin
        {
            get
            {
                return String.Format("{0}", this.NhanVien != null ? this.NhanVien.MaQuanLy + " - " + this.NhanVien.HoTen + " - " + this.TenMonHoc : "");
            }
        }

        #region ThanhTra
        private decimal _SoTietGhiNhan;
        private DateTime _ThoiDiemThanhLy;
        [ModelDefault("Caption", "Số tiết ghi nhận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietGhiNhan
        {
            get { return _SoTietGhiNhan; }
            set
            {
                SetPropertyValue("SoTietGhiNhan", ref _SoTietGhiNhan, value);
                if(!IsLoading)
                {
                    DaChinhSua = true;
                }
            }
        }
        [ModelDefault("Caption", "Thời điểm thanh lý")]
        public DateTime ThoiDiemThanhLy
        {
            get { return _ThoiDiemThanhLy; }
            set
            {
                SetPropertyValue("ThoiDiemThanhLy", ref _ThoiDiemThanhLy, value);
               
            }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChuThanhTra
        {
            get { return _GhiChuThanhTra; }
            set
            {
                SetPropertyValue("GhiChuThanhTra", ref _GhiChuThanhTra, value);
                
            }
        }

        [ModelDefault("Caption", "Tên loại học phần")]
        //[Browsable(false)]
        public string TexPhanLoaiHocPhan
        {
            get { return _TexPhanLoaiHocPhan; }
            set { SetPropertyValue("TexPhanLoaiHocPhan", ref _TexPhanLoaiHocPhan, value); }
        }


        [ModelDefault("Caption", "Loại học phần")]
        [Browsable(false)]
        public Guid OidPhanLoaiHocPhan
        {
            get { return _OidPhanLoaiHocPhan; }
            set { SetPropertyValue("OidPhanLoaiHocPhan", ref _OidPhanLoaiHocPhan, value); }
        }

        [ModelDefault("Caption", "Đã chỉnh sửa")]
        [Browsable(false)]
        public bool DaChinhSua
        {
            get { return _DaChinhSua; }
            set
            {
                SetPropertyValue("DaChinhSua", ref _DaChinhSua, value);

            }
        }
        #endregion
        public ChiTietKhoiLuongGiangDay_Moi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }
}
