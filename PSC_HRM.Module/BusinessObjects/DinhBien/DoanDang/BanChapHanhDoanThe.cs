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
    [ModelDefault("Caption", "Ban chấp hành Đoàn thể")]
    [RuleCombinationOfPropertiesIsUnique("BanChapHanhDoanThe.Unique", DefaultContexts.Save, "NhiemKy;ToChucDoanThe")]
    public class BanChapHanhDoanThe : BaseObject
    {
        public BanChapHanhDoanThe(Session session) : base(session) { }

        // Fields...
        private ToChucDoanThe _ToChucDoanThe;
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

        [ModelDefault("Caption", "Tổ chức Đoàn thể")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ToChucDoanThe ToChucDoanThe
        {
            get
            {
                return _ToChucDoanThe;
            }
            set
            {
                SetPropertyValue("ToChucDoanThe", ref _ToChucDoanThe, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách ủy viên ban chấp hành Đoàn thể")]
        [Association("BanChapHanhDoanThe-UyVienBanChapHanh")]
        public XPCollection<UyVienBanChapHanhDoanThe> UyVienBanChapHanh
        {
            get
            {
                return GetCollection<UyVienBanChapHanhDoanThe>("UyVienBanChapHanh");
            }
        }
    }

}
