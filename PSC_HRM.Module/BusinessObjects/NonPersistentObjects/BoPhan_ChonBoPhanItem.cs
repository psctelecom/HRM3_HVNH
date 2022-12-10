using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn đơn vị")]
    public class BoPhan_ChonBoPhanItem : BaseObject
    {
        // Fields...
        private BoPhan _boPhan;
        private bool _Chon;

        [ModelDefault("Caption", "Chọn")]
        [ModelDefault("AllowEdit", "True")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("AllowEdit", "True")]
        public BoPhan BoPhan
        {
            get
            {
                return _boPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _boPhan, value);
            }
        }

        public BoPhan_ChonBoPhanItem(Session session)
            : base(session)
        { }
    }

}
