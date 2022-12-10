using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.ThanhTra
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết thanh tra giảng dạy")]
    public class ChiTietThanhTraGiangDay : BaseObject
    {
        private Guid _OidChiTiet;
        [Browsable(false)]
        public Guid OidChiTiet
        {
            get { return _OidChiTiet; }
            set { SetPropertyValue("OidChiTiet", ref _OidChiTiet, value); }
        }
        #region HienThi
        private bool _Chon;
        private string _DonVi;
        private string _NhanVien;
        private string _TenMonHoc;
        private int _LoaiHocPhan;
        private string _LoaiChuongTrinh;
        private string _MaHocPhan;
        private string _LopHocPhan;
        private decimal _SoTinChi;
        private int _SoLuongSV;

        private DayOfWeek _Thu;
        private DateTime _NgayBD;
        private DateTime _NgayKT;
        private int _TietBD;
        private int _TietKT;
        private decimal _SoTietThucDay;

        private string _CoSoGiangDay;
        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }
        [ModelDefault("Caption", "Đơn vị")]
        public string DonVi
        {
            get { return _DonVi; }
            set { SetPropertyValue("DonVi", ref _DonVi, value); }
        }
        [ModelDefault("Caption", "Giảng viên")]
        public string NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [Size(-1)]
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
        [Browsable(false)]
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

        [ModelDefault("Caption", "Ngày BD")]
        public DateTime NgayBD
        {
            get { return _NgayBD; }
            set { SetPropertyValue("NgayBD", ref _NgayBD, value); }
        }
        [ModelDefault("Caption", "Ngày KT")]
        public DateTime NgayKT
        {
            get { return _NgayKT; }
            set { SetPropertyValue("NgayKT", ref _NgayKT, value); }
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
        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
        }

        [ModelDefault("Caption", "Cơ sở giảng dạy")]        
        public string CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
        }
        #endregion

        #region ChinhSua
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
                if (!IsLoading)
                    CheckChon();
            }
        }
        [ModelDefault("Caption", "Thời điểm thanh lý")]
        public DateTime ThoiDiemThanhLy
        {
            get { return _ThoiDiemThanhLy; }
            set
            {
                SetPropertyValue("ThoiDiemThanhLy", ref _ThoiDiemThanhLy, value);
                if (!IsLoading)
                    CheckChon();
            }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
                if (!IsLoading)
                    CheckChon();
            }
        }
        #endregion
        public ChiTietThanhTraGiangDay(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        void CheckChon()
        {
            if (ThoiDiemThanhLy != DateTime.MinValue || SoTietGhiNhan != 0 || GhiChu != "")
            {
                if (!Chon)
                    Chon = true;
            }
            else
                Chon = false;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            SoTietGhiNhan = 0;
            GhiChu = "";
        }
    }

}