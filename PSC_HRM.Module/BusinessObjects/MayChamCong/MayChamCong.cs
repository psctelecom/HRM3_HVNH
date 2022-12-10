using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BusinessObjects.MayChamCong
{
    [DefaultClassOptions]
    [DefaultProperty("ipAddress")]
    [ModelDefault("Caption", "May cham cong")]
    public class MayChamCong : BaseObject
    {
        private string _ipAddress;
        private string _portNumber;
        private string _maTruong;
        //private bool _trangThai;
        private string _trangThai;

        public string ipAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                SetPropertyValue("ipAddress", ref _ipAddress, value);
            }
        }

        public string portNumber
        {
            get
            {
                return _portNumber;
            }
            set
            {
                SetPropertyValue("portNumber", ref _portNumber, value);
            }
        }
        
        public string maTruong
        {
            get
            {
                return _maTruong;
            }
            set
            {
                SetPropertyValue("maTruong", ref _maTruong, value);
            }
        }

        [NonPersistent]
        [ModelDefault("AllowEdit","false")]
        public string TrangThai
        {
            get
            {
                return _trangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _trangThai, value);
            }
        }
        
        public MayChamCong(Session session)
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