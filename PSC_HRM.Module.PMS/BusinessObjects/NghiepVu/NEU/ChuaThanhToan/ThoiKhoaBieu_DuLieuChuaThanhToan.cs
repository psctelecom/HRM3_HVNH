using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.PMS.ThoiKhoaBieu
{
    [ModelDefault("Caption", "Dữ liệu chưa thanh toán")]

    [Appearance("ToMau_XacNhan", TargetItems = "XacNhan", BackColor = "Yellow", FontColor = "Red", Criteria="XacNhan = 1")]
    public class ThoiKhoaBieu_DuLieuChuaThanhToan : ChiTietThongTinChungPMS
    {
        private BoPhan _KhoaVien;
        private BoPhan _BoMonQuanLyGiangDay;
        private string _KhoaDaoTao;
        private string _TenNamHoc;
        private string _TenHocKy;
        private string _ThoiGianGiangDay;
        private decimal _SoTietDungLop;     
        private decimal _SoTietHeThong;
        private decimal _HeSo_ChucVu;
        private decimal _HeSo_TuXa;
        private decimal _HeSo_HocPhan;
        private decimal _SoTietQuyDoi;
        private decimal _SoTietThanhToanVuotGio;
        private NamHoc _NamHocLuu;
        private HocKy _HocKyLuu;
        private string _GhiChuLuu;
        private bool _XacNhan;
        

        #region Thông tin chung
        [ModelDefault("Caption", "Khoa - Viện")]
        public BoPhan KhoaVien
        {
            get { return _KhoaVien; }
            set { SetPropertyValue("KhoaVien", ref _KhoaVien, value); }
        }
        [ModelDefault("Caption", "Bộ môn quản lý")]
        public BoPhan BoMonQuanLyGiangDay
        {
            get { return _BoMonQuanLyGiangDay; }
            set { SetPropertyValue("BoMonQuanLyGiangDay", ref _BoMonQuanLyGiangDay, value); }
        }
        
        [ModelDefault("Caption", "Khóa đào tạo")]
        public string KhoaDaoTao
        {
            get { return _KhoaDaoTao; }
            set { SetPropertyValue("KhoaDaoTao", ref _KhoaDaoTao, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        [ModelDefault("AllowEdit", "False")]
        public string TenNamHoc
        {
            get { return _TenNamHoc; }
            set { SetPropertyValue("TenNamHoc", ref _TenNamHoc, value); }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [ModelDefault("AllowEdit", "False")]
        public string TenHocKy
        {
            get { return _TenHocKy; }
            set { SetPropertyValue("TenHocKy", ref _TenHocKy, value); }
        }
        #endregion

        #region Đào tạo chính quy
        [ModelDefault("Caption", "Thời gian giảng dạy")]
        [Size(-1)]
        public string ThoiGianGiangDay
        {
            get { return _ThoiGianGiangDay; }
            set { SetPropertyValue("ThoiGianGiangDay", ref _ThoiGianGiangDay, value); }
        }

        [ModelDefault("Caption", "Số tiết đứng lớp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietDungLop
        {
            get { return _SoTietDungLop; }
            set { SetPropertyValue("SoTietDungLop", ref _SoTietDungLop, value); }
        }
        [ModelDefault("Caption", "Số tiết hệ thống")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietHeThong
        {
            get { return _SoTietHeThong; }
            set { SetPropertyValue("SoTietHeThong", ref _SoTietHeThong, value); }
        }

        [ModelDefault("Caption", "Hệ số chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ChucVu
        {
            get { return _HeSo_ChucVu; }
            set { SetPropertyValue("HeSo_ChucVu", ref _HeSo_ChucVu, value); }
        }

        [ModelDefault("Caption", "Hệ số từ xa")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_TuXa
        {
            get { return _HeSo_TuXa; }
            set { SetPropertyValue("HeSo_TuXa", ref _HeSo_TuXa, value); }
        }
        [ModelDefault("Caption", "Hệ số học phần")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_HocPhan
        {
            get { return _HeSo_HocPhan; }
            set { SetPropertyValue("HeSo_HocPhan", ref _HeSo_HocPhan, value); }
        }
   
        [ModelDefault("Caption", "Số tiết quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietQuyDoi
        {
            get { return _SoTietQuyDoi; }
            set { SetPropertyValue("SoTietQuyDoi", ref _SoTietQuyDoi, value); }
        }
        [ModelDefault("Caption", "Số tiết thanh toán vượt giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThanhToanVuotGio
        {
            get { return _SoTietThanhToanVuotGio; }
            set { SetPropertyValue("SoTietThanhToanVuotGio", ref _SoTietThanhToanVuotGio, value); }
        }
        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }

        [ModelDefault("Caption", "Năm học lưu")]
        [Browsable(false)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHocLuu
        {
            get { return _NamHocLuu; }
            set { SetPropertyValue("NamHocLuu", ref _NamHocLuu, value); }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Xác nhận")]
        public HocKy HocKyLuu
        {
            get { return _HocKyLuu; }
            set { SetPropertyValue("HocKyLuu", ref _HocKyLuu, value); }
        }
        [ModelDefault("Caption", "Ghi chú lưu")]
        [Browsable(false)]
        [Size(-1)]
        public string GhiChuLuu
        {
            get { return _GhiChuLuu; }
            set { SetPropertyValue("GhiChuLuu", ref _GhiChuLuu, value); }
        }
      
        #endregion
        public ThoiKhoaBieu_DuLieuChuaThanhToan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}