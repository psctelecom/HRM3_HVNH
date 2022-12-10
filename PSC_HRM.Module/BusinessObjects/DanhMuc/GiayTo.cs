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
    [ImageName("BO_GiayTo")]
    [DefaultProperty("TenGiayTo")]
    [ModelDefault("Caption", "Giấy tờ")]
    [RuleCombinationOfPropertiesIsUnique("GiayTo.Unique", DefaultContexts.Save, "MaQuanLy;LoaiGiayTo;TenGiayTo")]
    public class GiayTo : BaseObject
    {
        // Fields...
        private bool _BatBuoc;
        private LoaiGiayTo _LoaiGiayTo;
        private string _TenGiayTo;
        private string _MaQuanLy;

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

        [ModelDefault("Caption", "Tên giấy tờ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenGiayTo
        {
            get
            {
                return _TenGiayTo;
            }
            set
            {
                SetPropertyValue("TenGiayTo", ref _TenGiayTo, value);
            }
        }

        [ModelDefault("Caption", "Loại giấy tờ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiGiayTo LoaiGiayTo
        {
            get
            {
                return _LoaiGiayTo;
            }
            set
            {
                SetPropertyValue("LoaiGiayTo", ref _LoaiGiayTo, value);
            }
        }

        [ModelDefault("Caption", "Giấy tờ bắt buộc phải có trong hồ sơ")]
        public bool BatBuoc
        {
            get
            {
                return _BatBuoc;
            }
            set
            {
                SetPropertyValue("BatBuoc", ref _BatBuoc, value);
            }
        }

        public GiayTo(Session session) : base(session) { }
    }

}
