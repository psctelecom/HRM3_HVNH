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
    [ModelDefault("Caption", "HSL tăng thêm theo TN")]
    public class HSLTangThemTheoThamNien : BaseObject
    {
        private int _TuNam;
        private int _DenNam;
        private decimal _HeSoPhuCap;
        private int _ThoiGianNangBac;
        private int _Bac;
        
        [ModelDefault("Caption", "Từ trên năm")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Đến năm")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Hệ số phụ cấp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoPhuCap
        {
            get
            {
                return _HeSoPhuCap;
            }
            set
            {
                SetPropertyValue("HeSoPhuCap", ref _HeSoPhuCap, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Hệ số")]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                return String.Format("{0:n2}", HeSoPhuCap);
            }
        }

        [ModelDefault("Caption", "Thời gian nâng bậc (tháng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int ThoiGianNangBac
        {
            get
            {
                return _ThoiGianNangBac;
            }
            set
            {
                SetPropertyValue("ThoiGianNangBac", ref _ThoiGianNangBac, value);
            }
        }

        [ModelDefault("Caption", "Bậc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Bac
        {
            get
            {
                return _Bac;
            }
            set
            {
                SetPropertyValue("Bac", ref _Bac, value);
            }
        }

        public HSLTangThemTheoThamNien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
