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
    [DefaultProperty("TenQuanLyGiaoDuc")]
    [ModelDefault("Caption", "Quản lý giáo dục")]
    public class QuanLyGiaoDuc : BaseObject
    {
        private string _MaQuanLy;
        private string _TenQuanLyGiaoDuc;

        public QuanLyGiaoDuc(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Quản lý giáo dục")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenQuanLyGiaoDuc
        {
            get
            {
                return _TenQuanLyGiaoDuc;
            }
            set
            {
                SetPropertyValue("TenQuanLyGiaoDuc", ref _TenQuanLyGiaoDuc, value);
            }
        }

    }

}
