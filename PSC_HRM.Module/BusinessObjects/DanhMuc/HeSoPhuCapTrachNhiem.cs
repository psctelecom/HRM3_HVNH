using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Hệ số trách nhiệm")]
    [RuleCombinationOfPropertiesIsUnique("HeSoPhuCapTrachNhiem.Identifier", DefaultContexts.Save, "TuThang;DenThang;HSPCTrachNhiem", "Hệ số trách nhiệm đã tồn tại trong hệ thống.")]
    public class HeSoPhuCapTrachNhiem : BaseObject
    {
       private int _TuThang;
        private int _DenThang;
        private decimal _HSPCTrachNhiem;

        [ModelDefault("Caption", "Số tháng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }

        [ModelDefault("Caption", "đến")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }

        [ModelDefault("Caption", "HSPC trách nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal HSPCTrachNhiem
        {
            get
            {
                return _HSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem", ref _HSPCTrachNhiem, value);
            }
        }

        public HeSoPhuCapTrachNhiem(Session session) : base(session) { }
    }

}
