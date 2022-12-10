using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.Drawing;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BanLamViec
{
    [DefaultClassOptions]
    [DefaultProperty("TenNhomCongViec")]
    [ModelDefault("Caption", "Nhóm công việc")]
    public class NhomCongViec : BaseObject
    {
        // Fields...
        private Color _Mau;
        private string _TenNhomCongViec;

        [ModelDefault("Caption", "Tên nhóm công việc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomCongViec
        {
            get
            {
                return _TenNhomCongViec;
            }
            set
            {
                SetPropertyValue("TenNhomCongViec", ref _TenNhomCongViec, value);
            }
        }

        [ModelDefault("Caption", "Màu")]
        [ValueConverter(typeof(ColorValueConverter))]
        public Color Mau
        {
            get
            {
                return _Mau;
            }
            set
            {
                SetPropertyValue("Mau", ref _Mau, value);
            }
        }

        public NhomCongViec(Session session)
            : base(session)
        { }
    }
}