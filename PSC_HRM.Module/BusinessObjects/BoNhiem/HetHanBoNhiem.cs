using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.BoNhiem
{
    [NonPersistent]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết hết hạn bổ nhiệm")]
    public class HetHanBoNhiem : BaseObject, IBoPhan, ISupportController
    {
        // Fields...
        private QuyetDinhCaNhan _QuyetDinh;
        private bool _ChucVuKiemNhiem;
        private DateTime _NgayHetNhiemKy;
        private DateTime _NgayBoNhiem;
        private ChucVu _ChucVu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private bool _Chon;

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

        [ModelDefault("Caption", "Đơn vị")]
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

        [ModelDefault("Caption", "Quyết định bổ nhiệm")]
        public QuyetDinhCaNhan QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public bool ChucVuKiemNhiem
        {
            get
            {
                return _ChucVuKiemNhiem;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiem", ref _ChucVuKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }

        public HetHanBoNhiem(Session session) : base(session) { }
    }

}
