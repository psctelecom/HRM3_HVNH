using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;

namespace PSC_HRM.Module.TapSu
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Hết hạn tạm hoãn tập sự")]
    public class HetHanTamHoanTapSu : BaseObject, ISupportController
    {
        // Fields...
        private BoPhan _BoPhan;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private ThongTinNhanVien _CanBoTapSu;
        private QuyetDinhTamHoanTapSu _QuyetDinhTamHoanTapSu;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        
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

        [ModelDefault("Caption", "Cán bộ tập sự")]
        public ThongTinNhanVien CanBoTapSu
        {
            get
            {
                return _CanBoTapSu;
            }
            set
            {
                SetPropertyValue("CanBoTapSu", ref _CanBoTapSu, value);
            }
        }

        [ModelDefault("Caption", "Quyết định hướng dẫn tập sự")]
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

        [ModelDefault("Caption", "Quyết định tạm hoãn tập sự")]
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

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        public HetHanTamHoanTapSu(Session session)
            : base(session)
        { }
    }
}
