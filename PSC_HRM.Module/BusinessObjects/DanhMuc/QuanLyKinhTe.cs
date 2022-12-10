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
    [DefaultProperty("TenQuanLyKinhTe")]
    [ModelDefault("Caption", "Quản lý kinh tế")]
    public class QuanLyKinhTe : BaseObject
    {
        private string _MaQuanLy;
        private string _TenQuanLyKinhTe;

        public QuanLyKinhTe(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Quản lý kinh tế")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuanLyKinhTe
        {
            get
            {
                return _TenQuanLyKinhTe;
            }
            set
            {
                SetPropertyValue("TenQuanLyKinhTe", ref _TenQuanLyKinhTe, value);
            }
        }

    }

}
