using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuyTrinh
{
    [DefaultClassOptions]
    [DefaultProperty("TenQuyTrinh")]
    [ModelDefault("Caption", "Quy trình")]
    public class QuyTrinh : BaseObject
    {
        // Fields...
        private string _TenQuyTrinh;

        [RuleUniqueValue(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên quy trình")]
        public string TenQuyTrinh
        {
            get
            {
                return _TenQuyTrinh;
            }
            set
            {
                SetPropertyValue("TenQuyTrinh", ref _TenQuyTrinh, value);
            }
        }

        [Aggregated]
        [Association("QuyTrinh-ListChiTietQuyTrinh")]
        [ModelDefault("Caption", "Chi tiết quy trình")]
        public XPCollection<ChiTietQuyTrinh> ListChiTietQuyTrinh
        {
            get
            {
                return GetCollection<ChiTietQuyTrinh>("ListChiTietQuyTrinh");
            }
        }

        public QuyTrinh(Session session) : base(session) { }
    }
}
