using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DoanDang
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Ban chấp hành Đảng")]
    [RuleCombinationOfPropertiesIsUnique("BanChapHanhDang.Unique", DefaultContexts.Save, "NhiemKy;ToChucDang")]
    public class BanChapHanhDang : BaseObject
    {
        public BanChapHanhDang(Session session) : base(session) { }

        // Fields...
        private ToChucDang _ToChucDang;
        private NhiemKy _NhiemKy;

        [ModelDefault("Caption", "Nhiệm kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhiemKy NhiemKy
        {
            get
            {
                return _NhiemKy;
            }
            set
            {
                SetPropertyValue("NhiemKy", ref _NhiemKy, value);
            }
        }

        [ModelDefault("Caption", "Tổ chức Đảng")]
        public ToChucDang ToChucDang
        {
            get
            {
                return _ToChucDang;
            }
            set
            {
                SetPropertyValue("ToChucDang", ref _ToChucDang, value);
            }
        }

        [ModelDefault("Caption", "Danh sách ủy viên ban chấp hành Đảng")]
        [Association("BanChapHanhDang-UyVienBanChapHanh")]
        public XPCollection<UyVienBanChapHanhDang> UyVienBanChapHanh
        {
            get
            {
                return GetCollection<UyVienBanChapHanhDang>("UyVienBanChapHanh");
            }
        }
    }

}
