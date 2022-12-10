using System.Linq;
using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;

namespace PSC_HRM.Module.BusinessObjects.NonPersistentObjects.System
{
    [NonPersistent]
    [ModelDefault("Caption","Chức năng phụ")]
    public class ChonChucNangPhu : BaseObject
    {
        private bool _Chon;
        private string _Key;
        private string _Caption;
        private string _PhanHe;
        private Guid _OidObject;

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
        [ModelDefault("Caption", "Key object")]
        [Browsable(false)]
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                SetPropertyValue("Key", ref _Key, value);
            }
        }
        [ModelDefault("Caption", "Phân hệ")]
        public string PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("PhanHe", ref _PhanHe, value);
            }
        }

        [ModelDefault("Caption", "Caption")]
        public string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                SetPropertyValue("Caption", ref _Caption, value);
            }
        }

        [ModelDefault("Caption", "OidObject")]
        [Browsable(false)]
        public Guid OidObject
        {
            get
            {
                return _OidObject;
            }
            set
            {
                SetPropertyValue("OidObject", ref _OidObject, value);
            }
        }
        public ChonChucNangPhu(Session session)
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