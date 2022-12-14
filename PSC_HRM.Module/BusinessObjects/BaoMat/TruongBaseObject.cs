using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.BaoMat
{
    [NonPersistent]
    public abstract class TruongBaseObject : BaseObject
    {
        [NonPersistent]
        protected string MaTruong { get; set; }

        public TruongBaseObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            MaTruong = TruongConfig.MaTruong;    
        }
    }
}
