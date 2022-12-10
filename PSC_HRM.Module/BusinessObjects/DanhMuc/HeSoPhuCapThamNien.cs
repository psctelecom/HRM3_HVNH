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
    [ModelDefault("Caption", "Hệ số thâm niên")]
    [RuleCombinationOfPropertiesIsUnique("HeSoPhuCapThamNien.Identifier", DefaultContexts.Save, "TuNam;DenNam;HSPCThamNien", "Hệ số thâm niên đã tồn tại trong hệ thống.")]
    public class HeSoPhuCapThamNien : BaseObject
    {
        private int _TuNam;
        private int _DenNam;
        private decimal _HSPCThamNien;

        [ModelDefault("Caption", "Số năm từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("Caption", "đến")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [ModelDefault("Caption", "HSPC thâm niên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal HSPCThamNien
        {
            get
            {
                return _HSPCThamNien;
            }
            set
            {
                SetPropertyValue("HSPCThamNien", ref _HSPCThamNien, value);
            }
        }

        public HeSoPhuCapThamNien(Session session) : base(session) { }
    }

}
