using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Thêm đơn vị")]
    public class BoPhan_ThemBoPhan : BaseObject
    {
        // Fields...
        private LoaiBoPhanEnum _LoaiBoPhan = LoaiBoPhanEnum.PhongBan;

        [ModelDefault("Caption", "Loại đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiBoPhanEnum LoaiBoPhan
        {
            get
            {
                return _LoaiBoPhan;
            }
            set
            {
                SetPropertyValue("LoaiBoPhan", ref _LoaiBoPhan, value);
            }
        }

        public BoPhan_ThemBoPhan(Session session)
            : base(session)
        { }
    }

}
