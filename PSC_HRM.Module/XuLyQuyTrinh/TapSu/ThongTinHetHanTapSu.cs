using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BanLamViec;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BanLamViec;

namespace PSC_HRM.Module.XuLyQuyTrinh.TapSu
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Hết hạn tập sự")]
    public class ThongTinHetHanTapSu : Notification
    {
        // Fields...
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        private BoPhan _BoPhanCanBoHuongDan;
        private ThongTinNhanVien _CanBoHuongDan;

        [ModelDefault("Caption", "Quyết định hướng dẫn tập sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuyetDinhHuongDanTapSu QuyetDinhHuongDanTapSu
        {
            get
            {
                return _QuyetDinhHuongDanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongDanTapSu", ref _QuyetDinhHuongDanTapSu, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị cán bộ hướng dẫn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanCanBoHuongDan
        {
            get
            {
                return _BoPhanCanBoHuongDan;
            }
            set
            {
                SetPropertyValue("BoPhanCanBoHuongDan", ref _BoPhanCanBoHuongDan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ hướng dẫn tập sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien CanBoHuongDan
        {
            get
            {
                return _CanBoHuongDan;
            }
            set
            {
                SetPropertyValue("CanBoHuongDan", ref _CanBoHuongDan, value);
            }
        }

        public ThongTinHetHanTapSu(Session session)
            : base(session)
        { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} hết hạn tập sự.", this);
        }
    }
}
