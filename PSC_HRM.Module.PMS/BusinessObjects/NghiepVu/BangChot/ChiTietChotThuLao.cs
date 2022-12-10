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

    [ModelDefault("Caption", "Chi tiết bảng chốt")]
    [ModelDefault("AllowNew", "false")]
    [Appearance("Hide_HeSo", TargetItems = "HeSoCoSo;HeSoLuong;HeSoMonMoi;HeSoGiangDayNgoaiGio", Visibility = ViewItemVisibility.Hide, Criteria = "KhoiLuongGiangDay.ThongTinTruong.TenVietTat <> 'QNU'")]
    //[Appearance("Hide", TargetItems = "TongGioQuyDoi;DonGia", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinBangChotThuLao.BangChotThuLao.ThongTinTruong.TenVietTat != 'HUFLIT'")]
    [Appearance("ToMauSoTien", TargetItems = "SoTienThanhToan", BackColor = "Yellow", FontColor = "Red")]
    [Appearance("ToMauTongGio", TargetItems = "TongGio", BackColor = "Aquamarine", FontColor = "Red")]
    [Appearance("ToMauDaTinhTien", TargetItems = "Khoa", BackColor = "Yellow", FontColor = "Red", Criteria = "DaTinhThuLao = 1")]
    public class ChiTietChotThuLao : BaseObject
    {
        #region key
        private ThongTinBangChotThuLao _ThongTinBangChotThuLao;
        [Association("ThongTinBangChotThuLao-ListChiTietBangChot")]
        [ModelDefault("Caption", "Bảng chốt thông tin giảng dạy")]
        [Browsable(false)]
        public ThongTinBangChotThuLao ThongTinBangChotThuLao
        {
            get
            {
                return _ThongTinBangChotThuLao;
            }
            set
            {
                SetPropertyValue("ThongTinBangChotThuLao", ref _ThongTinBangChotThuLao, value);              
            }
        }

        #endregion

        #region Khai báo

        #region Hoạt động
        
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private string _LopHocPhan;
        private string _TenHocPhan;
        private string _TenHoatDong;
        private LoaiHoatDongEnum _LoaiHoatDong;
        private HocKy _HocKy;
        #endregion

        private Guid _OidChiTiet;
        private BoPhan _OidBoMonGiangDay;

        #region Kết quả
        private decimal _TongGioQuyDoi;
        private decimal _TongGio;
        private decimal _DonGia;
        private bool _DaTinhThuLao;
        private decimal _SoTienChiuThue;
        private decimal _SoTienKhongChiuThue;
        #endregion

        #region Hệ số
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

        #endregion


        #region Hoạt động
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
        [ModelDefault("Caption", "Lớp học phần")]
        [Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên học phần")]
        [Size(-1)]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }
        [ModelDefault("Caption", "Hoạt động")]
        [Size(-1)]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
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
        
        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }
        
        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi
        {
            get { return _TongGioQuyDoi; }
            set { SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Đã tính thù lao")]
        [ModelDefault("AllowEdit","False")]
        public bool DaTinhThuLao
        {
            get { return _DaTinhThuLao; }
            set { SetPropertyValue("DaTinhThuLao", ref _DaTinhThuLao, value); }
        }
        [ModelDefault("Caption", "Tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienChiuThue
        {
            get { return _SoTienChiuThue; }
            set { SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value); }
        }
        [ModelDefault("Caption", "Tiền không chịu thuế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienKhongChiuThue
        {
            get { return _SoTienKhongChiuThue; }
            set { SetPropertyValue("SoTienKhongChiuThue", ref _SoTienKhongChiuThue, value); }
        }
        #endregion

        #region Guid
        [Browsable(false)]
        [ModelDefault("Caption", "Oid Chi tiết - sử dụng cho web")]
        public Guid OidChiTiet
        {
            get
            {
                return _OidChiTiet;
            }
            set
            {
                SetPropertyValue("OidChiTiet", ref _OidChiTiet, value);
            }
        }
        
        [ModelDefault("Caption", "Bộ môn quản lý")]
        public BoPhan OidBoMonGiangDay
        {
            get
            {
                return _OidBoMonGiangDay;
            }
            set
            {
                SetPropertyValue("OidBoMonGiangDay", ref _OidBoMonGiangDay, value);
            }
        }
        #endregion

        #region Hệ số

        [ModelDefault("Caption", "HS chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit","False")]
        [Browsable(false)]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }

        [ModelDefault("Caption", "HS lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }
        [ModelDefault("Caption", "HS đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_DaoTao
        {
            get { return _HeSo_DaoTao; }
            set { SetPropertyValue("HeSo_DaoTao", ref _HeSo_DaoTao, value); }
        }
        [ModelDefault("Caption", "HS cơ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_CoSo
        {
            get { return _HeSo_CoSo; }
            set { SetPropertyValue("HeSo_CoSo", ref _HeSo_CoSo, value); }
        }


        [ModelDefault("Caption", "HS giảng dạy ngoài giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_GiangDayNgoaiGio
        {
            get { return _HeSo_GiangDayNgoaiGio; }
            set { SetPropertyValue("HeSo_GiangDayNgoaiGio", ref _HeSo_GiangDayNgoaiGio, value); }
        }

        [ModelDefault("Caption", "HS Tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_TinChi
        {
            get { return _HeSo_TinChi; }
            set { SetPropertyValue("HeSo_TinChi", ref _HeSo_TinChi, value); }
        }
        [ModelDefault("Caption", "HS TNTH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_TNTH
        {
            get { return _HeSo_TNTH; }
            set { SetPropertyValue("HeSo_TNTH", ref _HeSo_TNTH, value); }
        }

        [ModelDefault("Caption", "HS bậc đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_BacDaoTao
        {
            get { return _HeSo_BacDaoTao; }
            set { SetPropertyValue("HeSo_BacDaoTao", ref _HeSo_BacDaoTao, value); }
        }
        [ModelDefault("Caption", "HS ngôn ngữ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public decimal HeSo_NgonNgu
        {
            get { return _HeSo_NgonNgu; }
            set { SetPropertyValue("HeSo_NgonNgu", ref _HeSo_NgonNgu, value); }
        }
        #endregion
        public ChiTietChotThuLao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}