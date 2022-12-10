using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL
{
    [DefaultClassOptions]
    [NonPersistent]
    [ModelDefault("Caption","Chi tiết khối lượng thanh tra")]
    [Appearance("ChiTietKhoiLuongThanhTra_Non_UEL_AllowEditTrue", TargetItems = "SoTietGhiNhan;GhiChu;" , Enabled = true , Criteria = "Chon = 1")]
    [Appearance("ChiTietKhoiLuongThanhTra_Non_UEL_AllowEditFalse", TargetItems = "SoTietGhiNhan;GhiChu;", Enabled = false, Criteria = "Chon = 0")]
    public class ChiTietKhoiLuongThanhTra_Non_UEL : BaseObject
    {
        private Guid _OidChiTiet;
        [Browsable(false)]
        public Guid OidChiTiet
        {
            get { return _OidChiTiet; }
            set { SetPropertyValue("OidChiTiet", ref _OidChiTiet, value); }
        }
        private bool _Chon;
        private BoPhan _DonVi;
        private NhanVien _NhanVien;
        private string _TenMonHoc;
        private int _LoaiHocPhan;
        private string _LoaiChuongTrinh;
        private string _MaHocPhan;
        private string _LopHocPhan;
        private decimal _SoTinChi;
        private string _LopSinhVien;
        private int _SoLuongSV;

        private DayOfWeek _Thu;
        private DateTime _NgayDay;
        private int _TietBD;
        private int _TietKT;
        private decimal _SoTietThucDay;
        private bool _DaThanhTra;

        private string _CoSoGiangDay;
        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }
        [ModelDefault("Caption", "Bộ phận")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "false")]
        public BoPhan BoPhan
        {
            get { return _DonVi; }
            set { SetPropertyValue("DonVi", ref _DonVi, value); }
        }
        [ModelDefault("Caption", "Giảng viên")]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [ModelDefault("AllowEdit", "false")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Loại học phần")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public int LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }
        [ModelDefault("Caption", "Loại chương trình")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public string LoaiChuongTrinh
        {
            get { return _LoaiChuongTrinh; }
            set { SetPropertyValue("LoaiChuongTrinh", ref _LoaiChuongTrinh, value); }
        }
        [ModelDefault("Caption", "Mã học phần")]
        [ModelDefault("AllowEdit", "false")]
        [Browsable(false)]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp sinh viên")]
        [ModelDefault("AllowEdit", "false")]
        [Size(2000)]
        public string LopSinhVien
        {
            get { return _LopSinhVien; }
            set { SetPropertyValue("LopSinhVien", ref _LopSinhVien, value); }
        }

        [ModelDefault("Caption", "Số lượng SV")]
        [ModelDefault("AllowEdit", "false")]
        public int SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }
        [ModelDefault("Caption", "Thứ (giảng dạy)")]
        [ModelDefault("AllowEdit", "false")]
        public DayOfWeek Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }

        [ModelDefault("Caption", "Ngày dạy")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime NgayDay
        {
            get { return _NgayDay; }
            set { SetPropertyValue("NgayDay", ref _NgayDay, value); }
        }


        [ModelDefault("Caption", "Tiết bắt đầu")]
        [ModelDefault("AllowEdit", "false")]
        public int TietBD
        {
            get { return _TietBD; }
            set { SetPropertyValue("TietBD", ref _TietBD, value); }
        }
        [ModelDefault("Caption", "Tiết kết thúc")]
        [ModelDefault("AllowEdit", "false")]
        public int TietKT
        {
            get { return _TietKT; }
            set { SetPropertyValue("TietKT", ref _TietKT, value); }
        }
        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
        }



        [ModelDefault("Caption", "Cơ sở giảng dạy")]
        [ModelDefault("AllowEdit", "false")]
        public string CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
        }

        private decimal _SoTietGhiNhan;
        private DateTime _ThoiDiemThanhLy;
        private string _GhiChu;
        [ModelDefault("Caption", "Số tiết ghi nhận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietGhiNhan
        {
            get { return _SoTietGhiNhan; }
            set
            {
                SetPropertyValue("SoTietGhiNhan", ref _SoTietGhiNhan, value);
                //if (!IsLoading)
                //    //CheckChon();
            }
        }
        [ModelDefault("Caption", "Thời điểm thanh lý")]
        [Browsable(false)]
        public DateTime ThoiDiemThanhLy
        {
            get { return _ThoiDiemThanhLy; }
            set
            {
                SetPropertyValue("ThoiDiemThanhLy", ref _ThoiDiemThanhLy, value);
            }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
                //if (!IsLoading)
                //    CheckChon();
            }
        }


        [ModelDefault("Caption","Đã thanh tra")]
        public bool DaThanhTra
        {
            get { return _DaThanhTra; }
            set { SetPropertyValue("DaThanhTra", ref _DaThanhTra, value); }
        }

        public ChiTietKhoiLuongThanhTra_Non_UEL(Session session)
            : base(session)
        {
        }
        
    }
}