using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TapSu
{
    [ImageName("BO_BoNhiemNgach")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đề nghị bổ nhiệm ngạch")]
    public class ChiTietDeNghiBoNhiemNgach : BaseObject
    {
        // Fields...
        private bool _Huong85PhanTramLuong;
        private DeNghiBoNhiemNgach _DeNghiBoNhiemNgach;
        private DateTime _NgayBoNhiemNgach;
        private DateTime _NgayHuongLuong;
        private DateTime _MocNangLuong;
        private decimal _HeSoLuong;
        private BacLuong _BacLuong;
        private NgachLuong _NgachLuong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _SoQuyetDinh;

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách đề nghị bổ nhiệm ngạch")]
        [Association("DeNghiBoNhiemNgach-ListChiTietDeNghiBoNhiemNgach")]
        public DeNghiBoNhiemNgach DeNghiBoNhiemNgach
        {
            get
            {
                return _DeNghiBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("DeNghiBoNhiemNgach", ref _DeNghiBoNhiemNgach, value);
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuong = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuong = value.NhanVienThongTinLuong.HeSoLuong;
                    Huong85PhanTramLuong = value.NhanVienThongTinLuong.Huong85PhanTramLuong;
                    NgayHuongLuong = value.NhanVienThongTinLuong.NgayHuongLuong;
                    MocNangLuong = value.NhanVienThongTinLuong.MocNangLuong;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                    BacLuong = null;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                    HeSoLuong = value.HeSoLuong;
              
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số lương")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Hưởng 85% lương")]
        public bool Huong85PhanTramLuong
        {
            get
            {
                return _Huong85PhanTramLuong;
            }
            set
            {
                SetPropertyValue("Huong85PhanTramLuong", ref _Huong85PhanTramLuong, value);
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return _NgayBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgach", ref _NgayBoNhiemNgach, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hưởng lương")]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mốc nâng lương")]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
            }
        }
        public ChiTietDeNghiBoNhiemNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
