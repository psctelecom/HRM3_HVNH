using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class ChiTietDuLieu_KhoiLuongGiangDay_Non : BaseObject
    {
        private string _HocKy;
        private string _BoPhan;
        private string _NhanVien;
        private string _ChucDanh;
        private string _HocHam;
        private string _TrinhDoChuyenMon;//Học vị
        private string _TenMonHoc;
        private bool _ChuyenNganh;
        private string _LoaiChuongTrinh;
        private string _MaHocPhan;
        private string _LopHocPhan;
        private bool _CoXepTKB;
        private string _MaLopSV;
        private string _TenLopSV;
        private decimal _SoTinChi;
        private int _SoLuongSV;
        //private string _Thu;
        //private int _TietBD;
        //private int _TietKT;
        //private DateTime _NgayBD;
        //private DateTime _NgayKT;
        private decimal _SoTietThucDay;
        private string _LoaiHocPhan;
        private string _LoaiMonHoc;
        private string _NgonNguGiangDay;
        private string _GioGiangDay;
        private string _BacDaoTao;
        private string _HeDaoTao;
        private string _CoSoGiangDay;
        private string _PhongHoc;      
        private decimal _HeSo_ChucDanh;
        private decimal _HeSo_LopDong;
        private decimal _HeSo_DaoTao;
        private decimal _HeSo_CoSo;
        private decimal _HeSo_GiangDayNgoaiGio;
        private decimal _HeSo_TinChi;
        private decimal _HeSo_TNTH;
        private decimal _HeSo_BacDaoTao;
        private decimal _HeSo_NgonNgu;
        private decimal _TongHeSo;
        private decimal _GioQuyDoiLyThuyet;
        private decimal _GioQuyDoiThucHanh;
        private decimal _TongGio;
        private string _LoaiHopDong;

        [ModelDefault("Caption", "Học kỳ")]
        public string HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public string NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Chức danh")]
        public string ChucDanh
        {
            get { return _ChucDanh; }
            set { SetPropertyValue("ChucDanh", ref _ChucDanh, value); }
        }

        [ModelDefault("Caption", "Học hàm")]
        public string HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public string TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }

        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành")]
        public bool ChuyenNganh
        {
            get { return _ChuyenNganh; }
            set { SetPropertyValue("ChuyenNganh", ref _ChuyenNganh, value); }
        }

        [ModelDefault("Caption", "Loại chương trình")]
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
        //[ModelDefault("Caption", "Thứ (giảng dạy)")]
        //public string Thu
        //{
        //    get { return _Thu; }
        //    set { SetPropertyValue("Thu", ref _Thu, value); }
        //}
        //[ModelDefault("Caption", "Tiết bắt đầu")]
        //public int TietBD
        //{
        //    get { return _TietBD; }
        //    set { SetPropertyValue("TietBD", ref _TietBD, value); }
        //}
        //[ModelDefault("Caption", "Tiết kết thúc")]
        //public int TietKT
        //{
        //    get { return _TietKT; }
        //    set { SetPropertyValue("TietKT", ref _TietKT, value); }
        //}

        //[ModelDefault("Caption", "Ngày bắt đầu")]
        //public DateTime NgayBD
        //{
        //    get { return _NgayBD; }
        //    set { SetPropertyValue("NgayBD", ref _NgayBD, value); }
        //}
        //[ModelDefault("Caption", "Ngày kết thúc")]
        //public DateTime NgayKT
        //{
        //    get { return _NgayKT; }
        //    set { SetPropertyValue("NgayKT", ref _NgayKT, value); }
        //}
        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
        }

        [ModelDefault("Caption", "Học phần")]
        public string LoaiMonHoc
        {
            get { return _LoaiMonHoc; }
            set { SetPropertyValue("LoaiMonHoc", ref _LoaiMonHoc, value); }
        }
        [ModelDefault("Caption", "Loại học phần")]
        public string LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ")]
        public string NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }
      
        [ModelDefault("Caption", "Loại giờ giảng")]
        public string GioGiangDay
        {
            get { return _GioGiangDay; }
            set { SetPropertyValue(" GioGiangDay", ref _GioGiangDay, value); }
        }


        [ModelDefault("Caption", "Bậc đào tạo")]
        public string BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public string HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Địa điểm giảng dạy")]
        public string CoSoGiangDay
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
        [Browsable(false)]
        public decimal GioQuyDoiLyThuyet
        {
            get { return _GioQuyDoiLyThuyet; }
            set { SetPropertyValue("GioQuyDoiLyThuyet", ref _GioQuyDoiLyThuyet, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi (TH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Browsable(false)]
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

        [ModelDefault("Caption", "Loại hợp đồng")]
        public string LoaiHopDong
        {
            get { return _LoaiHopDong; }
            set { SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value); }
        }

        public ChiTietDuLieu_KhoiLuongGiangDay_Non(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}