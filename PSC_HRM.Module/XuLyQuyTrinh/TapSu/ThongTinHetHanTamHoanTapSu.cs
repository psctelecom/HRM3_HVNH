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
    [ModelDefault("Caption", "Hết hạn tạm hoãn tập sự")]
    public class ThongTinHetHanTamHoanTapSu : Notification
    {
        // Fields...
        private DateTime _TamHoanDenNgay;
        private DateTime _TamHoanTuNgay;
        private QuyetDinhTamHoanTapSu _QuyetDinhTamHoanTapSu;
        private DateTime _TapSuDenNgay;
        private DateTime _TapSuTuNgay;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        private BoPhan _BoPhanCanBoHuongDan;
        private ThongTinNhanVien _CanBoHuongDan;

        [ModelDefault("Caption", "QĐ hướng dẫn tập sự")]
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

        [ModelDefault("Caption", "Tập sự từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TapSuTuNgay
        {
            get
            {
                return _TapSuTuNgay;
            }
            set
            {
                SetPropertyValue("TapSuTuNgay", ref _TapSuTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Tập sự đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TapSuDenNgay
        {
            get
            {
                return _TapSuDenNgay;
            }
            set
            {
                SetPropertyValue("TapSuDenNgay", ref _TapSuDenNgay, value);
            }
        }

        [ModelDefault("Caption", "QĐ tạm hoãn tập sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuyetDinhTamHoanTapSu QuyetDinhTamHoanTapSu
        {
            get
            {
                return _QuyetDinhTamHoanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhTamHoanTapSu", ref _QuyetDinhTamHoanTapSu, value);
            }
        }

        [ModelDefault("Caption", "Tạm hoãn từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TamHoanTuNgay
        {
            get
            {
                return _TamHoanTuNgay;
            }
            set
            {
                SetPropertyValue("TamHoanTuNgay", ref _TamHoanTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Tạm hoãn đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TamHoanDenNgay
        {
            get
            {
                return _TamHoanDenNgay;
            }
            set
            {
                SetPropertyValue("TamHoanDenNgay", ref _TamHoanDenNgay, value);
            }
        }

        public ThongTinHetHanTamHoanTapSu(Session session)
            : base(session)
        { }

        protected override void AfterThongTinNhanVienChanged()
        {
            GhiChu = ObjectFormatter.Format("Cán bộ {ThongTinNhanVien.HoTen} hết hạn tạm hoãn tập sự.", this);
        }
    }
}
