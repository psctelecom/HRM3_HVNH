using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNgoaiNgu")]
    [ModelDefault("Caption", "Ngoại ngữ")]
    public class NgoaiNgu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNgoaiNgu;

        public NgoaiNgu(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên ngoại ngữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNgoaiNgu
        {
            get
            {
                return _TenNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TenNgoaiNgu", ref _TenNgoaiNgu, value);
            }
        }
    }

}
