using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenLoaiGiayTo")]
    [ModelDefault("Caption", "Loại giấy tờ")]
    [RuleCombinationOfPropertiesIsUnique("LoaiGiayTo.Unique", DefaultContexts.Save, "MaQuanLy;TenLoaiGiayTo")]
    public class LoaiGiayTo : BaseObject
    {
        // Fields...
        private string _MaQuanLy;
        private string _TenLoaiGiayTo;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên loại giấy tờ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiGiayTo
        {
            get
            {
                return _TenLoaiGiayTo;
            }
            set
            {
                SetPropertyValue("TenLoaiGiayTo", ref _TenLoaiGiayTo, value);
            }
        }

        public LoaiGiayTo(Session session) : base(session) { }
    }

}
