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
    [ModelDefault("Caption", "Ban chấp hành Đoàn")]
    [RuleCombinationOfPropertiesIsUnique("BanChapHanhDoan.Unique", DefaultContexts.Save, "NhiemKy;ToChucDoan")]
    public class BanChapHanhDoan : BaseObject
    {
        public BanChapHanhDoan(Session session) : base(session) { }

        // Fields...
        private ToChucDoan _ToChucDoan;
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

        [ModelDefault("Caption", "Tổ chức Đoàn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ToChucDoan ToChucDoan
        {
            get
            {
                return _ToChucDoan;
            }
            set
            {
                SetPropertyValue("ToChucDoan", ref _ToChucDoan, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách ủy viên ban chấp hành Đoàn")]
        [Association("BanChapHanhDoan-UyVienBanChapHanh")]
        public XPCollection<UyVienBanChapHanhDoan> UyVienBanChapHanh
        {
            get
            {
                return GetCollection<UyVienBanChapHanhDoan>("UyVienBanChapHanh");
            }
        }
    }

}
