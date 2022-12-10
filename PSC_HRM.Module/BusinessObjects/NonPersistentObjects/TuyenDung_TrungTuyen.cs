using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Trúng tuyển")]
    public class TuyenDung_TrungTuyen : BaseObject
    {
        // Fields...
        private bool _Chon;
        private TrungTuyen _TrungTuyen;

        [ModelDefault("Caption", "Chọn")]
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

        [ModelDefault("Caption", "Trúng tuyển")]
        public TrungTuyen TrungTuyen
        {
            get
            {
                return _TrungTuyen;
            }
            set
            {
                SetPropertyValue("TrungTuyen", ref _TrungTuyen, value);
            }
        }

        public TuyenDung_TrungTuyen(Session session) : base(session) { }
    }

}
