using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{
    public class CoiThi_ChamThi : BaseObject
    {
        #region 1. Key
        private ChiTietCoiThi_ChamThi _ChiTietCoiThi_ChamThi;
        [Association("ChiTietCoiThi_ChamThi-ListCoiThi_ChamThi")]
        [ModelDefault("Caption", "Quản lý coi thi / chấm thi")]
        [Browsable(false)]
        public ChiTietCoiThi_ChamThi ChiTietCoiThi_ChamThi
        {
            get
            {
                return _ChiTietCoiThi_ChamThi;
            }
            set
            {
                SetPropertyValue("ChiTietCoiThi_ChamThi", ref _ChiTietCoiThi_ChamThi, value);
            }
        }
        #endregion
        #region 2. Khai báo
        private HocKy _HocKy;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private string _VaiTroCoiThi;
        private string _MaLopHP;
        private string _MaMon;
        private string _TenMon;
        private DateTime _NgayThi;
        private string _GioBatDau;
        private string _PhongThi;
        private string _MaHinhThucThi;
        private decimal _TGThi;
        private decimal _GC_SoBai;
        private decimal _TGQuyDoi;
        private LoaiKhaoThi? _LoaiKhaoThi; //0: CoiThi ; 1: ChamThi
        #endregion

        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }
        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }
        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        [ModelDefault("Caption", "Vai trò coi thi")]
        public string VaiTroCoiThi
        {
            get { return _VaiTroCoiThi; }
            set
            {
                SetPropertyValue("VaiTroCoiThi", ref _VaiTroCoiThi, value);
            }
        }
        [ModelDefault("Caption", "Mã lớp HP")]
        public string MaLopHP
        {
            get { return _MaLopHP; }
            set
            {
                SetPropertyValue("MaLopHP", ref _MaLopHP, value);
            }
        }
        [ModelDefault("Caption", "Mã môn")]
        public string MaMon
        {
            get { return _MaMon; }
            set { SetPropertyValue("MaMon", ref _MaMon, value); }
        }
        [ModelDefault("Caption", "Tên môn")]
        public string TenMon
        {
            get { return _TenMon; }
            set { SetPropertyValue("TenMon", ref _TenMon, value); }
        }
        [ModelDefault("Caption", "Ngày thi")]
        public DateTime NgayThi
        {
            get { return _NgayThi; }
            set { SetPropertyValue("NgayThi", ref _NgayThi, value); }
        }
        [ModelDefault("Caption", "Giờ bắt đầu")]
        public string GioBatDau
        {
            get { return _GioBatDau; }
            set { SetPropertyValue("GioBatDau", ref _GioBatDau, value); }
        }
        [ModelDefault("Caption", "Phòng thi")]
        public string PhongThi
        {
            get { return _PhongThi; }
            set { SetPropertyValue("PhongThi", ref _PhongThi, value); }
        }
        [ModelDefault("Caption", "Mã hình thức thi")]
        public string MaHinhThucThi
        {
            get { return _MaHinhThucThi; }
            set { SetPropertyValue("MaHinhThucThi", ref _MaHinhThucThi, value); }
        }
        [ModelDefault("Caption", "Thời gian thi(phút)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TGThi
        {
            get { return _TGThi; }
            set
            {
                SetPropertyValue("TGThi", ref _TGThi, value);
            }
        }
        [ModelDefault("Caption", "GC/Số bài")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GC_SoBai
        {
            get { return _GC_SoBai; }
            set
            {
                SetPropertyValue("GC_SoBai", ref _GC_SoBai, value);
            }
        }
        [ModelDefault("Caption", "TG quy đổi(giờ)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TGQuyDoi
        {
            get { return _TGQuyDoi; }
            set
            {
                SetPropertyValue("_TGQuyDoi", ref _TGQuyDoi, value);
            }
        }
        [ModelDefault("Caption", "Loại khảo thí")]
        public LoaiKhaoThi? LoaiKhaoThi
        {
            get { return _LoaiKhaoThi; }
            set { SetPropertyValue("LoaiKhaoThi", ref _LoaiKhaoThi, value); }
        }
        public CoiThi_ChamThi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
