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
    [DefaultProperty("TenSucKhoe")]
    [ModelDefault("Caption", "Sức khỏe")]
    public class SucKhoe : BaseObject
    {
        private string _MaQuanLy;
        private string _TenSucKhoe;

        public SucKhoe(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã Quản Lý")]
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

        [ModelDefault("Caption", "Tên Sức Khỏe")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenSucKhoe
        {
            get
            {
                return _TenSucKhoe;
            }
            set
            {
                SetPropertyValue("TenSucKhoe", ref _TenSucKhoe, value);
            }
        }
    }

}
