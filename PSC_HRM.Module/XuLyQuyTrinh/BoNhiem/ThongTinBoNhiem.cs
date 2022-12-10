using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.BoNhiem
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Thông tin bổ nhiệm")]
    [Appearance("QuyetDinh.Khac", TargetItems = "TaiBoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "!KiemNhiem")]
    public class ThongTinBoNhiem : Notification
    {
        // Fields...
        private BoPhan _TaiBoPhan;
        private QuyetDinhCaNhan _QuyetDinh;
        private bool _KiemNhiem;
        private DateTime _NgayBoNhiem;
        private ChucVu _ChucVu;

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinhCaNhan QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiem", ref _QuyetDinh, value);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Kiêm nhiệm")]
        public bool KiemNhiem
        {
            get
            {
                return _KiemNhiem;
            }
            set
            {
                SetPropertyValue("KiemNhiem", ref _KiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Tại đơn vị")]
        public BoPhan TaiBoPhan
        {
            get
            {
                return _TaiBoPhan;
            }
            set
            {
                SetPropertyValue("TaiBoPhan", ref _TaiBoPhan, value);
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

        public ThongTinBoNhiem(Session session) : base(session) { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} hết nhiệm kỳ chức vụ.", this);
        }
    }

}
