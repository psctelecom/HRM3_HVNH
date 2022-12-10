using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS
{
    [ModelDefault("Caption", "Thông tin chung")]
    public class ChiTietThongTinChungPMS : BaseObject
    {  
        #region KB Thông tin nhân viên
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        private ChucDanh _ChucDanh;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;//Học vị
        #endregion

        #region KB Môn học
        private string _MaMonHoc;
        private string _TenMonHoc;
        private string _MaLopHocPhan;
        private string _MaLopSV;
        private string _TenLopSV;
        private string _LopHocPhan;
        private LoaiHocPhanEnum? _LoaiHocPhan;
        private LoaiHocPhan _LoaiHocPhanDanhMuc;
        private NgonNguEnum? _NgonNguGiangDay;
        private GioGiangDayEnum? _GioGiangDay; 
        private string _MaLopGhep;
        private string _PhongHoc;
        private bool _LopChatLuongCao;
        private int _SoLuongSV;
        
        private decimal _SoTinChi;
        private decimal _SoTietThucDay;


        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        private CoSoGiangDay _CoSoGiangDay;

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

        #endregion

        #region Quy đổi
        private decimal _TongHeSo;
        private decimal _GioDinhMuc;
        private decimal _TongGio;
        #endregion
        private DateTime _NgayBD;
        private DateTime _NgayKT;
        private string _GhiChu;

        #region Thông tin nhân viên
        [ModelDefault("Caption", "Bộ phận")]
        //[Browsable(false)]
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
        [ModelDefault("Caption", "Mã môn học")]
        [Size(-1)]
        public string MaMonHoc
        {
            get { return _MaMonHoc; }
            set { SetPropertyValue("MaMonHoc", ref _MaMonHoc, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [Size(-1)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Mã lớp SV")]
        [Size(-1)]
        public string MaLopSV
        {
            get { return _MaLopSV; }
            set { SetPropertyValue("MaLopSV", ref _MaLopSV, value); }
        }
        [ModelDefault("Caption", "Tên lớp SV")]
        [Size(-1)]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }
        [ModelDefault("Caption", "Mã học phần")]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { SetPropertyValue("MaLopHocPhan", ref _MaLopHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhanEnum? LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }
        [ModelDefault("Caption", "Loại học phần danh mục")]
        public LoaiHocPhan LoaiHocPhanDanh
        {
            get { return _LoaiHocPhanDanhMuc; }
            set { SetPropertyValue("LoaiHocPhanDanhMuc", ref _LoaiHocPhanDanhMuc, value); }
        }
        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguEnum? NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }
        [ModelDefault("Caption", "Loại giờ giảng")]
        public GioGiangDayEnum? GioGiangDay
        {
            get { return _GioGiangDay; }
            set { SetPropertyValue(" GioGiangDay", ref _GioGiangDay, value); }
        }

        [ModelDefault("Caption", "Mã lớp ghép")]
        public string MaLopGhep
        {
            get { return _MaLopGhep; }
            set { SetPropertyValue("MaLopGhep", ref _MaLopGhep, value); }
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
        [ModelDefault("Caption", "Số lượng SV")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }
        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
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
        #endregion

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

        [ModelDefault("Caption", "Giờ định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioDinhMuc
        {
            get { return _GioDinhMuc; }
            //set { SetPropertyValue("TongHeSo", ref _TongHeSo, value); }
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

        [ModelDefault("Caption", "Ngày BĐ")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayBD
        {
            get
            {
                return _NgayBD;
            }
            set
            {
                SetPropertyValue("NgayBD", ref _NgayBD, value);
            }
        }

        [ModelDefault("Caption", "Ngày KT")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayKT
        {
            get
            {
                return _NgayKT;
            }
            set
            {
                SetPropertyValue("NgayKT", ref _NgayKT, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        [VisibleInListView(false)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public ChiTietThongTinChungPMS(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}