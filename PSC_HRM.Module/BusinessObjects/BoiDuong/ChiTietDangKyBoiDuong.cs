using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BoiDuong
{
    [ImageName("BO_QuanLyBoiDuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đăng ký bồi dưỡng")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDangKyBoiDuong", DefaultContexts.Save, "DangKyBoiDuong;ThongTinNhanVien")]
    public class ChiTietDangKyBoiDuong : BaseObject
    {
        private DangKyBoiDuong _DangKyBoiDuong;
        private string _GhiChu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;

        [Browsable(false)]
        [Association("DangKyBoiDuong-ListChiTietDangKyBoiDuong")]
        public DangKyBoiDuong DangKyBoiDuong
        {
            get
            {
                return _DangKyBoiDuong;
            }
            set
            {
                SetPropertyValue("DangKyBoiDuong", ref _DangKyBoiDuong, value);
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
                    UpdateNVList();
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [Size(300)]
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

        public ChiTietDangKyBoiDuong(Session session) : base(session) { }

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
