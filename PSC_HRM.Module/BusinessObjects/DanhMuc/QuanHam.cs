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
    [DefaultProperty("TenQuanHam")]
    [ModelDefault("Caption", "Quân hàm")]
    public class QuanHam : BaseObject
    {
        private string _MaQuanLy;
        private string _TenQuanHam;

        public QuanHam(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên quân hàm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuanHam
        {
            get
            {
                return _TenQuanHam;
            }
            set
            {
                SetPropertyValue("TenQuanHam", ref _TenQuanHam, value);
            }
        }
    }

}
