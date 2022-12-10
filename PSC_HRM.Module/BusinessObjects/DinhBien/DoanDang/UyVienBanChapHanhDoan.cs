using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DoanDang
{
    [ModelDefault("Caption", "Ủy viên ban chấp hành Đoàn")]
    public class UyVienBanChapHanhDoan : BaseObject, IBoPhan
    {
        public UyVienBanChapHanhDoan(Session session) : base(session) { }

        // Fields...
        private BanChapHanhDoan _BanChapHanhDoan;
        private ChucVuDoan _ChucVuDoan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

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
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Chức vụ Đoàn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVuDoan ChucVuDoan
        {
            get
            {
                return _ChucVuDoan;
            }
            set
            {
                SetPropertyValue("ChucVuDoan", ref _ChucVuDoan, value);
            }
        }

        [ModelDefault("Caption", "Ban chấp hành Đoàn")]
        [Association("BanChapHanhDoan-UyVienBanChapHanh")]
        public BanChapHanhDoan BanChapHanhDoan
        {
            get
            {
                return _BanChapHanhDoan;
            }
            set
            {
                SetPropertyValue("BanChapHanhDoan", ref _BanChapHanhDoan, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNVList();
        }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
