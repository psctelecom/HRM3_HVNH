using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.TapSu
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Hết hạn tập sự")]
    public class HetHanTapSu : BaseObject, ISupportController
    {
        // Fields...
        private bool _Chon;
        private DateTime _NgayHetHanTapSu;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        private BoPhan _BoPhanCanBoHuongDan;
        private ThongTinNhanVien _CanBoHuongDan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private DateTime _MocNangLuongCu;
        private DateTime _MocNangLuongMoi;

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

        [ImmediatePostData]
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
                if (!IsLoading && value != null)
                {
                    BoPhan = value.BoPhan;
                }
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

        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Bậc lương")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương mới")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime MocNangLuongmoi
        {
            get
            {
                return _MocNangLuongMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongMoi", ref _MocNangLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị cán bộ hướng dẫn")]
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

        [ModelDefault("Caption", "Ngày hết hạn tập sự")]
        public DateTime NgayHetHanTapSu
        {
            get
            {
                return _NgayHetHanTapSu;
            }
            set
            {
                SetPropertyValue("NgayHetHanTapSu", ref _NgayHetHanTapSu, value);
            }
        }

        public HetHanTapSu(Session session)
            : base(session)
        { }
    }
}
