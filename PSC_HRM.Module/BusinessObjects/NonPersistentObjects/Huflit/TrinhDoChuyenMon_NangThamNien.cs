using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.BusinessObjects.HoSo;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Nâng thâm niên")]
    [Appearance("ToMauTongGio", TargetItems = "NgayDuKienTangThamNien", BackColor = "Yellow", FontColor = "Red")]
    public class TrinhDoChuyenMon_NangThamNien : BaseObject
    {
        // Fields...
        private bool _Chon;
        private Guid _NhanVien;
        private Guid _VanBang;
        private string _HoTen;
        private string _MaGiangVien;
        private string _DonVi;
        private string _TenTrinhDo;
        private DateTime _NgayHieuLuc;
        private DateTime _NgayThucHien;        
        private ThamNien _ThamNien;
        private DateTime _NgayDuKienTangThamNien;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }
        [ModelDefault("Caption", "NhanVien")]
        [Browsable(false)]
        public Guid NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        [ModelDefault("Caption", "VanBang")]
        [Browsable(false)]
        public Guid VanBang
        {
            get
            {
                return _VanBang;
            }
            set
            {
                SetPropertyValue("VanBang", ref _VanBang, value);
            }
        }
        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }
        [ModelDefault("Caption", "Mã giảng viên")]
        public string MaGiangVien
        {
            get
            {
                return _MaGiangVien;
            }
            set
            {
                SetPropertyValue("MaGiangVien", ref _MaGiangVien, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        public string DonVi
        {
            get
            {
                return _DonVi;
            }
            set
            {
                SetPropertyValue("DonVi", ref _DonVi, value);
            }
        }
        [ModelDefault("Caption", "Trình dộ")]
        public string TenTrinhDo
        {
            get
            {
                return _TenTrinhDo;
            }
            set
            {
                SetPropertyValue("TenTrinhDo", ref _TenTrinhDo, value);
            }
        }
        [ModelDefault("Caption", "Thâm niên")]
        public ThamNien ThamNien
        {
            get
            {
                return _ThamNien;
            }
            set
            {
                SetPropertyValue("ThamNien", ref _ThamNien, value);
            }
        }
        [ModelDefault("Caption", "Ngày hiệu lực (Thâm niên)")]
        public DateTime NgayHieuLuc
        {
            get
            {
                return _NgayHieuLuc;
            }
            set
            {
                SetPropertyValue("NgayHieuLuc", ref _NgayHieuLuc, value);
            }
        }
        [ModelDefault("Caption", "Ngày chỉnh sửa")]
        public DateTime NgayThucHien
        {
            get
            {
                return _NgayThucHien;
            }
            set
            {
                SetPropertyValue("NgayThucHien", ref _NgayThucHien, value);
            }
        }
        [ModelDefault("Caption", "Dự kiến tăng thâm niên")]
        public DateTime NgayDuKienTangThamNien
        {
            get
            {
                return _NgayDuKienTangThamNien;
            }
            set
            {
                SetPropertyValue("ThamNien", ref _NgayDuKienTangThamNien, value);
            }
        }
        public TrinhDoChuyenMon_NangThamNien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
