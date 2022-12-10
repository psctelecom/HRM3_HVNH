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
    [DefaultProperty("TenQuanLyNhaNuoc")]
    [ModelDefault("Caption", "Quản lý nhà nước")]
    public class QuanLyNhaNuoc : BaseObject
    {
        private string _MaQuanLy;
        private string _TenQuanLyNhaNuoc;

        public QuanLyNhaNuoc(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Quản Lý Nhà Nước")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuanLyNhaNuoc
        {
            get
            {
                return _TenQuanLyNhaNuoc;
            }
            set
            {
                SetPropertyValue("TenQuanLyNhaNuoc", ref _TenQuanLyNhaNuoc, value);
            }
        }

    }

}
