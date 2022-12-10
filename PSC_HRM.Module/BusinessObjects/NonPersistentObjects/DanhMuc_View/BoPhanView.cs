using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects.DanhMuc_View
{
    [NonPersistent]
    [DefaultProperty("TenBoPhan")]
    public class BoPhanView : BaseObject
    {
        private string _TenBoPhan;
        private Guid _OidBoPhan;
        [Browsable(false)]
        public Guid OidBoPhan
        {
            get
            {
                return _OidBoPhan;
            }
            set
            {
                SetPropertyValue("OidBoPhan", ref _OidBoPhan, value);
            }
        }
        [ModelDefault("Caption", "Trường")]
        public string TenBoPhan
        {
            get
            {
                return _TenBoPhan;
            }
            set
            {
                SetPropertyValue("TenBoPhan", ref _TenBoPhan, value);
            }
        }
        public BoPhanView(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}