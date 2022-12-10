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
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Chi tiết thanh toán khối lượng giảng dạy")]
    [DefaultProperty("Caption")]
    public class ChiTietThanhToanKhoiLuongGiangDay : BaseObject
    {
        #region key
        private QuanLyHoatDongKhac _QuanLyHoatDongKhac;
        [Association("QuanLyHoatDongKhac-ListThanhToanKLGD")]
        [ModelDefault("Caption", "Key")]
        [Browsable(false)]
        public QuanLyHoatDongKhac QuanLyHoatDongKhac
        {
            get
            {
                return _QuanLyHoatDongKhac;
            }
            set
            {
                SetPropertyValue("QuanLyHoatDongKhac", ref _QuanLyHoatDongKhac, value);
            }
        }
        #endregion


        #region Khai báo nhân viên
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        #endregion

        #region Khai báo
        private BacDaoTao _BacDaoTao;
        private string _TenMonHoc;
        private string _KhoanChi;
        private string _LopHocPhan;
        private bool _CuNhanTN;
        private decimal _SoTiet;
        private int _SoLuongSV;

        private decimal _HeSo_ChucDanh;
        private decimal _HeSo_CoSo;
        private decimal _HeSo_LopDong;
        private decimal _HeSo_Khac;
        private decimal _HeSo_BacDaoTao;
        private decimal _HeSo_NgonNgu;
        private decimal _HeSo_GiangDayNgoaiGio;
        private decimal _HeSo_TNTH;
        private decimal _TongHeSo;

        private decimal _SoTietLyThuyet;
        private decimal _SoTietQuyDoi;
        private decimal _ChiPhiDiLai;
        private decimal _DonGiaTietChuan;
        private decimal _ThanhTien;
        private decimal _TongTien;

        private decimal _NoGioHKTruoc;
        private decimal _NoGioHKNay;

        private decimal _TongTienNo;

        private decimal _ThueTNCNTamTru;

        private decimal _ConLaiThanhToan;


        private HeDaoTao _HeDaoTao;
        private NgonNguGiangDay _NgonNguGiangDay;
        private decimal _DonGiaQuyDoiNgonNgu;
        private decimal _DonGiaThuLaoGiangDay;
        #endregion

        #region Giá trị nhân viên
        [ModelDefault("Caption", "Bộ phận")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        #endregion
        #region Giá trị
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Tên môn học")]
        [Size(-1)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Khoản chi")]
        [Size(-1)]
        public string KhoanChi
        {
            get { return _KhoanChi; }
            set { SetPropertyValue("KhoanChi", ref _KhoanChi, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        [Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Cử nhân TN")]
        [Browsable(false)]
        public bool CuNhanTN
        {
            get { return _CuNhanTN; }
            set { SetPropertyValue("CuNhanTN", ref _CuNhanTN, value); }
        }

        [ModelDefault("Caption", "Số tiết")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTiet
        {
            get { return _SoTiet; }
            set { SetPropertyValue("SoTiet", ref _SoTiet, value); }
        }
        [ModelDefault("Caption", "Sĩ số")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }
        [ModelDefault("Caption", "HS chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }
        [ModelDefault("Caption", "HS cơ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_CoSo
        {
            get { return _HeSo_CoSo; }
            set { SetPropertyValue("HeSo_CoSo", ref _HeSo_CoSo, value); }
        }
        [ModelDefault("Caption", "HS lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }
        [ModelDefault("Caption", "HS hệ đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_Khac
        {
            get { return _HeSo_Khac; }
            set { SetPropertyValue("HeSo_Khac", ref _HeSo_Khac, value); }
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
        [ModelDefault("Caption", "HS ngoài giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GiangDayNgoaiGio
        {
            get { return _HeSo_GiangDayNgoaiGio; }
            set { SetPropertyValue("HeSo_GiangDayNgoaiGio", ref _HeSo_GiangDayNgoaiGio, value); }
        }
        [ModelDefault("Caption", "HS TNTH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_TNTH
        {
            get { return _HeSo_TNTH; }
            set { SetPropertyValue("HeSo_TNTH", ref _HeSo_TNTH, value); }
        }


        [ModelDefault("Caption", "Tổng hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongHeSo
        {
            get { return _TongHeSo; }
            set { SetPropertyValue("TongHeSo", ref _TongHeSo, value); }
        }
        [ModelDefault("Caption", "Số tiết LT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietLyThuyet
        {
            get { return _SoTietLyThuyet; }
            set { SetPropertyValue("SoTietLyThuyet", ref _SoTietLyThuyet, value); }
        }

        [ModelDefault("Caption", "Số tiết quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Browsable(false)]
        public decimal SoTietQuyDoi
        {
            get { return _SoTietQuyDoi; }
            set { SetPropertyValue("SoTietQuyDoi", ref _SoTietQuyDoi, value); }
        }
        [ModelDefault("Caption", "Chi phí đi lại")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Browsable(false)]
        public decimal ChiPhiDiLai
        {
            get { return _ChiPhiDiLai; }
            set { SetPropertyValue("ChiPhiDiLai", ref _ChiPhiDiLai, value); }
        }
        [ModelDefault("Caption", "Đơn giá tiết chuẩn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGiaTietChuan
        {
            get { return _DonGiaTietChuan; }
            set { SetPropertyValue("DonGiaTietChuan", ref _DonGiaTietChuan, value); }
        }
        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }
        [ModelDefault("Caption", "Tổng tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongTien
        {
            get { return _TongTien; }
            set { SetPropertyValue("TongTien", ref _TongTien, value); }
        }
        [ModelDefault("Caption", "Nợ giờ HK trước")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal NoGioHKTruoc
        {
            get { return _NoGioHKTruoc; }
            set { SetPropertyValue("NoGioHKTruoc", ref _NoGioHKTruoc, value); }
        }
        [ModelDefault("Caption", "Nợ giờ HK này")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal NoGioHKNay
        {
            get { return _NoGioHKNay; }
            set { SetPropertyValue("NoGioHKNay", ref _NoGioHKNay, value); }
        }
        [ModelDefault("Caption", "Tổng tiền nợ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongTienNo
        {
            get { return _TongTienNo; }
            set { SetPropertyValue("TongTienNo", ref _TongTienNo, value); }
        }

        [ModelDefault("Caption", "Thuế TNCN tạm trừ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThueTNCNTamTru
        {
            get { return _ThueTNCNTamTru; }
            set { SetPropertyValue("ThueTNCNTamTru", ref _ThueTNCNTamTru, value); }
        }

        [ModelDefault("Caption", "Còn lại thanh toán")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ConLaiThanhToan
        {
            get { return _ConLaiThanhToan; }
            set { SetPropertyValue("ConLaiThanhToan", ref _ConLaiThanhToan, value); }
        }
        #endregion

        //Trường UEL
        #region trường UEL
        private Guid _Oid_ChitietKhoiLuongGiangDay;
        private decimal _DonGiaDieuChinh;
        private decimal _DonGiaQuyDoi;
        private decimal _HeSoCLC;


        [ModelDefault("Caption", "HS CLC")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoCLC
        {
            get { return _HeSoCLC; }
            set { SetPropertyValue("HeSoCLC", ref _HeSoCLC, value); }
        }


        [ModelDefault("Caption","Đơn giá điều chỉnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaDieuChinh
        {
            get { return _DonGiaDieuChinh; }
            set { SetPropertyValue("DonGiaDieuChinh", ref _DonGiaDieuChinh, value); }
        }

        [ModelDefault("Caption", "Đơn giá quy đổi")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaQuyDoi
        {
            get { return _DonGiaQuyDoi; }
            set { SetPropertyValue("DonGiaQuyDoi", ref _DonGiaQuyDoi, value); }
        }

        [Browsable(false)]
        public Guid Oid_ChiTietKhoiLuongGiangDay
        {
            get { return _Oid_ChitietKhoiLuongGiangDay; }
            set { SetPropertyValue("Oid_ChiTietKhoiLuongGiangDay", ref _Oid_ChitietKhoiLuongGiangDay, value); }
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
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }

        [ModelDefault("Caption", "Đơn giá quy đổi ngôn ngữ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaQuyDoiNgonNgu
        {
            get { return _DonGiaQuyDoiNgonNgu; }
            set { SetPropertyValue("DonGiaQuyDoiNgonNgu", ref _DonGiaQuyDoiNgonNgu, value); }
        }

        [ModelDefault("Caption", "Đơn giá thù lao giảng dạy")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGiaThuLaoGiangDay
        {
            get { return _DonGiaThuLaoGiangDay; }
            set { SetPropertyValue("DonGiaThuLaoGiangDay", ref _DonGiaThuLaoGiangDay, value); }
        }

        #endregion
        public ChiTietThanhToanKhoiLuongGiangDay(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}