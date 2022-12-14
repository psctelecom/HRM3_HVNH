using System;

using DevExpress.Xpo;
using System.ComponentModel;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoMat
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Phân quyền đơn vị")]
    [DefaultProperty("Ten")]
    [ImageName("BO_Category")]
    public class PhanQuyenDonVi : BaseObject
    {
        private string _Ten;
        private bool _IsAdministrator;

        [ModelDefault("Caption", "Tên")]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
            }
        }

        [ModelDefault("Caption", "IsAdministrator")]
        public bool IsAdministrator
        {
            get
            {
                return _IsAdministrator;
            }
            set
            {
                SetPropertyValue("IsAdministrator", ref _IsAdministrator, value);
            }
        }

        private string _Quyen;
        [ModelDefault("Caption", "Quyền")]
        [Browsable(false)]
        [Size(-1)]        
        public string Quyen
        {
            get
            {
                return _Quyen;
            }
            set
            {
                SetPropertyValue("Quyen", ref _Quyen, value);
            }
        }

        public PhanQuyenDonVi(Session session) : 
            base(session) 
        { }
    }

}
