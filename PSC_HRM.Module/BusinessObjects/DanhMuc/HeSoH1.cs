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
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Hệ số H1")]
    public class HeSoH1 : BaseObject
    {
        private decimal _TuHeSo;
        private decimal _DenHeSo;
        private decimal _HeSo;

        [ModelDefault("Caption", "Từ hệ số")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("EditMask", "N3")]
        public decimal TuHeSo
        {
            get
            {
                return _TuHeSo;
            }
            set
            {
                SetPropertyValue("TuHeSo", ref _TuHeSo, value);
            }
        }

        [ModelDefault("Caption", "Đến hệ số")]
        [ModelDefault("DisplayFormat", "N3")]
        [ModelDefault("EditMask", "N3")]
        public decimal DenHeSo
        {
            get
            {
                return _DenHeSo;
            }
            set
            {
                SetPropertyValue("DenHeSo", ref _DenHeSo, value);
            }
        }

        [ModelDefault("Caption", "Hệ số H1")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get
            {
                return _HeSo;
            }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Hệ số")]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                return String.Format("{0:n2}", HeSo);
            }
        }

        public HeSoH1(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
