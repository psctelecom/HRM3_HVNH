using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhGia;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Cán bộ vi phạm")]
    public class DanhGia_DiTreVeSom : BaseObject
    {
        // Fields...
        private DateTime _Ngay;
        private HinhThucViPham _HinhThucViPham;
        private string _GhiChu;
        private string _HinhThucNghi;
        private bool _NghiLam;
        private int _VeSom;
        private int _DiTre;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool _Chon = true;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("AllowEdit", "False")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Ngày")]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
            }
        }

        [ModelDefault("Caption", "Hình thức vi phạm")]
        public HinhThucViPham HinhThucViPham
        {
            get
            {
                return _HinhThucViPham;
            }
            set
            {
                SetPropertyValue("HinhThucViPham", ref _HinhThucViPham, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ làm")]
        public bool NghiLam
        {
            get
            {
                return _NghiLam;
            }
            set
            {
                SetPropertyValue("NghiLam", ref _NghiLam, value);
            }
        }

        [ModelDefault("Caption", "Đi trễ (phút)")]
        public int DiTre
        {
            get
            {
                return _DiTre;
            }
            set
            {
                SetPropertyValue("DiTre", ref _DiTre, value);
            }
        }

        [ModelDefault("Caption", "Về sớm (phút)")]
        public int VeSom
        {
            get
            {
                return _VeSom;
            }
            set
            {
                SetPropertyValue("VeSom", ref _VeSom, value);
            }
        }

        [ModelDefault("Caption", "Hình thức nghỉ")]
        public string HinhThucNghi
        {
            get
            {
                return _HinhThucNghi;
            }
            set
            {
                SetPropertyValue("HinhThucNghi", ref _HinhThucNghi, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public DanhGia_DiTreVeSom(Session session) : base(session) { }
    }

}
