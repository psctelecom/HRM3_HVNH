using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.PMS.ThoiKhoaBieu
{
    [ModelDefault("Caption", "Chi tiết thời khóa biểu")]

    [Appearance("ToMau", TargetItems = "SoTietQuyDoi;TongHeSo;TongGio", BackColor = "Aquamarine", FontColor = "Red")]
    [Appearance("ChiTiet_ThoiKhoaBieu_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]

    [Appearance("ToMau_XacNhan", TargetItems = "XacNhan", BackColor = "Yellow", FontColor = "Red", Criteria="XacNhan = 1")]
    public class ChiTiet_ThoiKhoaBieu : ChiTietThongTinChungPMS
    {
        #region Key
        private ThoiKhoaBieu_KhoiLuongGiangDay _ThoiKhoaBieu_KhoiLuongGiangDay;
        [ModelDefault("Caption", "Thời khóa biểu")]
        [Association("ThoiKhoaBieu_KhoiLuongGiangDay-ListChiTiet")]
        [Browsable(false)]
        public ThoiKhoaBieu_KhoiLuongGiangDay ThoiKhoaBieu_KhoiLuongGiangDay
        {
            get { return _ThoiKhoaBieu_KhoiLuongGiangDay; }
            set { SetPropertyValue("ThoiKhoaBieu_KhoiLuongGiangDay", ref _ThoiKhoaBieu_KhoiLuongGiangDay, value); }
        }
        #endregion
        private BoPhan _KhoaVien;
        private BoPhan _BoMonQuanLyGiangDay;
        private string _KhoaDaoTao;
        private string _TenNamHoc;
        private string _TenHocKy;
        private string _ThoiGianGiangDay;
        private decimal _SoTietDungLop;
        
        private decimal _SoTietHeThong;

        //private decimal _HeSo_QuyMo;
        //private decimal _HeSo_DiDuong;
        private decimal _HeSo_ChucVu;
        private decimal _HeSo_TuXa;
        private decimal _HeSo_HocPhan;
        //private decimal _HeSo_CD;
        private decimal _SoTietQuyDoi;
        private decimal _SoTietThanhToanVuotGio;
        private bool _XacNhan;
        private bool _Import;
        private bool _DongBo;
        private bool _Khoa;
        private bool _KhongTinhTien;
        
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


        //[ModelDefault("Caption", "Hệ số quy mô")]
        //[ModelDefault("DisplayFormat", "N2")]
        //[ModelDefault("EditMask", "N2")]
        //public decimal HeSo_QuyMo
        //{
        //    get { return _HeSo_QuyMo; }
        //    set { SetPropertyValue("HeSo_QuyMo", ref _HeSo_QuyMo, value); }
        //}
        //[ModelDefault("Caption", "Hệ số đi đường")]
        //[ModelDefault("DisplayFormat", "N2")]
        //[ModelDefault("EditMask", "N2")]
        //public decimal HeSo_DiDuong
        //{
        //    get { return _HeSo_DiDuong; }
        //    set { SetPropertyValue("HeSo_DiDuong", ref _HeSo_DiDuong, value); }
        //}
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
        //[ModelDefault("Caption", "Hệ số CD")]
        //[ModelDefault("DisplayFormat", "N2")]
        //[ModelDefault("EditMask", "N2")]
        //public decimal HeSo_CD
        //{
        //    get { return _HeSo_CD; }
        //    set { SetPropertyValue("HeSo_CD", ref _HeSo_CD, value); }
        //}
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
        [ModelDefault("Caption", "Import")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public bool Import
        {
            get { return _Import; }
            set { _Import = value; }
        }
        [ModelDefault("Caption", "Đồng bộ UIS")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public bool DongBo
        {
            get { return _DongBo; }
            set { _DongBo = value; }
        }
        [ModelDefault("Caption", "Khóa")]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [ModelDefault("Caption", "Không tính tiền")]
        public bool KhongTinhTien
        {
            get { return _KhongTinhTien; }
            set { SetPropertyValue("KhongTinhTien", ref _KhongTinhTien, value); }
        }

        #endregion
        public ChiTiet_ThoiKhoaBieu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}