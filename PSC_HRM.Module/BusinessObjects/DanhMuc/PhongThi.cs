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
    [DefaultProperty("TenPhongThi")]
    [ModelDefault("Caption", "Phòng thi")]
    public class PhongThi : BaseObject
    {
        // Fields...
        private string _TenPhongThi;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã phòng thi")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên phòng thi")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string TenPhongThi
        {
            get
            {
                return _TenPhongThi;
            }
            set
            {
                SetPropertyValue("TenPhongThi", ref _TenPhongThi, value);
            }
        }

        public PhongThi(Session session) : base(session) { }
    }

}
