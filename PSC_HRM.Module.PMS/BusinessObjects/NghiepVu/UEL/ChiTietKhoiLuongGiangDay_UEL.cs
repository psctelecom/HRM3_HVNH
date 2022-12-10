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
    [ModelDefault("Caption", "Chi tiết khối lượng giảng dạy(UEL)")]
    [Appearance("ChiTietKhoiLuongGiangDay_UEL_Khoa", TargetItems = "*", Enabled = false, Criteria = "1 = 1")]
    //[Appearance("!Hide_ThanhTra_UEL", TargetItems = "NgayDay;LoaiHocPhanObject;LayDuLieuKhoiLuongGiangDay;"
    //                                   , Visibility = ViewItemVisibility.Hide, Criteria = "Quanlythanhtra.ThongTinTruong.TenVietTat != 'UEL' ")]
    [DefaultProperty("ThongTin")]
    public class ChiTietKhoiLuongGiangDay_UEL : BaseObject
    {
        #region key
        private KhoiLuongGiangDay_UEL _KhoiLuongGiangDay_UEL;
        [Association("KhoiLuongGiangDay_UEL-ListChiTietKhoiLuongGiangDay_UEL")]
        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        [Browsable(false)]
        public KhoiLuongGiangDay_UEL KhoiLuongGiangDay_UEL
        {
            get
            {
                return _KhoiLuongGiangDay_UEL;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay_UEL", ref _KhoiLuongGiangDay_UEL, value);
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
        private string _LoaiChuongTrinh;
        private string _MaHocPhan;
        private string _LopHocPhan;
        private string _MonTiengAnh;

        private string _MaLopGhep;
        private string _MaLopSV;
        private string _TenLopSV;
        private decimal _SoTinChi;
        private int _SoLuongSV;
        private DayOfWeek _Thu;
        private int _TietBD;
        private int _TietKT;
        private DateTime _NgayDay;
        private decimal _SoTietThucDay;
        private bool _LopHocPhanTroGiang;

        private NgonNguGiangDay _NgonNguGiangDay;

        private LoaiHocPhan _LoaiHocPhanObject;

        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        private CoSoGiangDay _CoSoGiangDay;
        private string _PhongHoc;
        private bool _LopChatLuongCao;
        private decimal _GioQuyDoi;

        private string _GhiChu;
        private bool _DaLayDuLieu;
        private string _GhiChuLayDuLieu;
        #endregion
        #region KB Hệ số thông tin
        private decimal _HeSoChucDanhNhanVien;
        private decimal _HeSoDiaDiem;
        private decimal _HeSoLopDong;
        private decimal _HeSoCLC;
        private decimal _HeSoKhac;
        private decimal _TongHeSo;
        private string _Oid_ChiTietKhoiLuongThanhTra;
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
        [ModelDefault("Caption", "Ngày dạy")]
        public DateTime NgayDay
        {
            get { return _NgayDay; }
            set { SetPropertyValue("NgayDay", ref _NgayDay, value); }
        }



        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
        }
        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoi
        {
            get { return _GioQuyDoi; }
            set { SetPropertyValue("GioQuyDoi", ref _GioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Lớp học phần trợ giảng")]
        public bool LopHocPhanTroGiang
        {
            get { return _LopHocPhanTroGiang; }
            set { SetPropertyValue("LopHocPhanTroGiang", ref _LopHocPhanTroGiang, value); }
        }


        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhan LoaiHocPhanObject
        {
            get { return _LoaiHocPhanObject; }
            set { SetPropertyValue("LoaiHocPhanObject", ref _LoaiHocPhanObject, value); }
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


        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguGiangDay NgonNguGiangDay 
        {
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
            get { return _NgonNguGiangDay; }
        }

        [ModelDefault("Caption", "Địa điểm giảng dạy")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
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


        [ModelDefault("Caption", "Môn tiếng anh")]
        public string MonTiengAnh
        {
            get { return _MonTiengAnh; }
            set { SetPropertyValue("MonTiengAnh", ref _MonTiengAnh, value); }
        }


        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        [VisibleInListView(false)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption","Đã lấy dữ liệu")]
        [ModelDefault("AllowEdit","false")]
        public bool DaLayDuLieu
        {
            get { return _DaLayDuLieu; }
            set { SetPropertyValue("DaLayDuLieu", ref _DaLayDuLieu, value); }
        }

        [ModelDefault("Caption", "Ghi chú lấy dữ liệu")]
        [Browsable(false)]
        public string GhiChuLayDuLieu
        {
            get { return _GhiChuLayDuLieu; }
            set { SetPropertyValue("GhiChuLayDuLieu", ref _GhiChuLayDuLieu, value); }
        }

        #endregion
        #region
        [ModelDefault("Caption", "HS chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoChucDanhNhanVien
        {
            get { return _HeSoChucDanhNhanVien; }
            set { SetPropertyValue("HeSoChucDanhNhanVien", ref _HeSoChucDanhNhanVien, value); }
        }

        [ModelDefault("Caption", "HS Địa điểm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoDiaDiem
        {
            get { return _HeSoDiaDiem; }
            set { SetPropertyValue("HeSoDiaDiem", ref _HeSoDiaDiem, value); }
        }


        [ModelDefault("Caption", "HS lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLopDong
        {
            get { return _HeSoLopDong; }
            set { SetPropertyValue("HeSoLopDong", ref _HeSoLopDong, value); }
        }

        [ModelDefault("Caption", "HS CLC")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoCLC
        {
            get { return _HeSoCLC; }
            set { SetPropertyValue("HeSoCLC", ref _HeSoCLC, value); }
        }

        [ModelDefault("Caption", "HS Khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoKhac
        {
            get { return _HeSoKhac; }
            set { SetPropertyValue("HeSoKhac", ref _HeSoKhac, value); }
        }

        [ModelDefault("Caption", "Tổng hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongHeSo
        {
            get { return _TongHeSo; }
            set { SetPropertyValue("TongHeSo", ref _TongHeSo, value); }
        }
        [Browsable(false)]
        [Size(-1)]
        public string Oid_ChiTietThanhTraGiangDay
        {
            get { return _Oid_ChiTietKhoiLuongThanhTra; }
            set { SetPropertyValue("Oid_ChiTietThanhTraGiangDay", ref _Oid_ChiTietKhoiLuongThanhTra, value); }
        }

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
        public ChiTietKhoiLuongGiangDay_UEL(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //ThongTinTruong =  HamDungChung.ThongTinTruong(Session);
        }
    }
}
