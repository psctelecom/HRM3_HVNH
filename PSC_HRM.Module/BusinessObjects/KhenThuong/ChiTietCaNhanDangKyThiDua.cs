using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module;

namespace PSC_HRM.Module.KhenThuong
{
    [ImageName("BO_DangKyThiDua")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết cán bộ đăng ký thi đua")]
    public class ChiTietCaNhanDangKyThiDua : BaseObject, IBoPhan
    {
        private ChiTietDangKyThiDua _ChiTietDangKyThiDua;
        private DateTime _NgayDangKy;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Chi tiết đăng ký thi đua")]
        [Association("ChiTietDangKyThiDua-ListChiTietCaNhanDangKyThiDua")]
        public ChiTietDangKyThiDua ChiTietDangKyThiDua
        {
            get
            {
                return _ChiTietDangKyThiDua;
            }
            set
            {
                SetPropertyValue("ChiTietDangKyThiDua", ref _ChiTietDangKyThiDua, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
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

        [ModelDefault("Caption", "Ngày đăng ký")]
        public DateTime NgayDangKy
        {
            get
            {
                return _NgayDangKy;
            }
            set
            {
                SetPropertyValue("NgayDangKy", ref _NgayDangKy, value);
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

        public ChiTietCaNhanDangKyThiDua(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayDangKy = HamDungChung.GetServerTime();
            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNVList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }
}
