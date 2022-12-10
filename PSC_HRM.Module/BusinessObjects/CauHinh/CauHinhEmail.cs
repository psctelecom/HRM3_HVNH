using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình Email")]
    public class CauHinhEmail : BaseObject
    {
        // Fields...
        private int _Port;
        private string _Email;
        private string _Pass;
        private string _Server;

        [ModelDefault("Caption", "Port")]
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                SetPropertyValue("Port", ref _Port, value);
            }
        }

        [ModelDefault("Caption", "Server")]
        public string Server
        {
            get
            {
                return _Server;
            }
            set
            {
                SetPropertyValue("Server", ref _Server, value);
            }
        }

        [ModelDefault("Caption", "Email")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        [ModelDefault("Caption", "Pass")]
        [ModelDefault("IsPassword", "True")]
        public string Pass
        {
            get
            {
                return _Pass;
            }
            set
            {
                SetPropertyValue("Pass", ref _Pass, value);
            }
        }
        public CauHinhEmail(Session session) : base(session) { }
    }

}
