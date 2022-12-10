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
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Chi tiết đề nghị")]
    [DefaultProperty("Caption")]
    public class ChiTietDeNghi : BaseObject
    {
        private QuanLyDeNghi _QuanLyDeNghi;

        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyDeNghi-ListChiTietDeNghi")]
        public QuanLyDeNghi QuanLyDeNghi
        {
            get
            {
                return _QuanLyDeNghi;
            }
            set
            {
                SetPropertyValue("QuanLyDeNghi", ref _QuanLyDeNghi, value);
            }
        }

        private string _SoDN;
        private NhanVien _NhanVien;
        private string _LopHocPhan;
        private string _TenMonHoc;
        private string _LopSinhVien;
        private string _TenLopSinhVien;
        private string _KhoaHoc;
        private decimal _SoTietLyThuyet;
        private decimal _GioQuyDoiLyThuyet;
        private decimal _SoLuongSV;
        private string _DiaDiemDay;
        private string _ThoiGiangDay;
        private decimal _HeSo_LopDong;
        private decimal _SoTietKiemTra;


        [ModelDefault("Caption", "Số đề nghị")]
        public string SoDN
        {
            get { return _SoDN; }
            set { SetPropertyValue("SoDN", ref _SoDN, value); }
        }

        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [Size(-1)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Lớp sinh viên")]
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

        [ModelDefault("Caption", "Khóa học")]
        public string KhoaHoc
        {
            get { return _KhoaHoc; }
            set { SetPropertyValue("KhoaHoc", ref _KhoaHoc, value); }
        }
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Số tiết lý thuyết")]
        public decimal SoTietLyThuyet
        {
            get { return _SoTietLyThuyet; }
            set { SetPropertyValue("SoTietLyThuyet", ref _SoTietLyThuyet, value); }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Giời quy đổi lý thuyết")]
        public decimal GioQuyDoiLyThuyet
        {
            get { return _GioQuyDoiLyThuyet; }
            set { SetPropertyValue("GioQuyDoiLyThuyet", ref _GioQuyDoiLyThuyet, value); }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Sỉ số")]
        public decimal SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Số tiết kiểm tra")]
        public decimal SoTietKiemTra
        {
            get { return _SoTietKiemTra; }
            set { SetPropertyValue("SoTietKiemTra", ref _SoTietKiemTra, value); }
        }

        [ModelDefault("Caption", "Địa điểm dạy")]
        [Size(-1)]
        public string DiaDiemDay
        {
            get { return _DiaDiemDay; }
            set { SetPropertyValue("DiaDiemDay", ref _DiaDiemDay, value); }
        }

        [ModelDefault("Caption", "Thời gian dạy")]
        [Size(-1)]
        public string ThoiGiangDay
        {
            get { return _ThoiGiangDay; }
            set { SetPropertyValue("ThoiGiangDay", ref _ThoiGiangDay, value); }
        }

        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "Hệ số lớp đông")]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }

        public ChiTietDeNghi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }       
    }
}
