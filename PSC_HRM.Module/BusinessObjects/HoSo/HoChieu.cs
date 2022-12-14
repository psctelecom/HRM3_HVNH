using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.HoSo
{
    [ModelDefault("Caption", "Hộ chiếu")]
    [DefaultProperty("SoHoChieu")]
    public class HoChieu : BaseObject
    {
        private string _SoHoChieu;
        private LoaiHoChieu _LoaiHoChieu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _CoQuanCap;
        private DiaChi _NoiCap;
        private DateTime _NgayCap;
        private DateTime _NgayHetHan;


        [ModelDefault("Caption", "Số hộ chiếu")]
        public string SoHoChieu
        {
            get
            {
                return _SoHoChieu;
            }
            set
            {
                SetPropertyValue("SoHoChieu", ref _SoHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Loại hộ chiếu")]
        public LoaiHoChieu LoaiHoChieu
        {
            get
            {
                return _LoaiHoChieu;
            }
            set
            {
                SetPropertyValue("LoaiHoChieu", ref _LoaiHoChieu, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
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

        [ModelDefault("Caption", "Cơ quan cấp")]
        public string CoQuanCap
        {
            get
            {
                return _CoQuanCap;
            }
            set
            {
                SetPropertyValue("CoQuanCap", ref _CoQuanCap, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Nơi cấp")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        public DiaChi NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return _NgayHetHan;
            }
            set
            {
                SetPropertyValue("NgayHetHan", ref _NgayHetHan, value);
            }
        }

        public HoChieu(Session session) : base(session) { }
    }

}
