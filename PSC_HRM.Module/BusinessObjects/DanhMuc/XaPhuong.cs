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
    [DefaultProperty("TenXaPhuong")]
    [ModelDefault("Caption", "Xã phường")]
    public class XaPhuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenXaPhuong;
        private QuanHuyen _QuanHuyen;

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

        [ModelDefault("Caption", "Tên xã phường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenXaPhuong
        {
            get
            {
                return _TenXaPhuong;
            }
            set
            {
                SetPropertyValue("TenXaPhuong", ref _TenXaPhuong, value);
            }
        }

        [ModelDefault("Caption", "Quận huyện")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("QuanHuyen-XaPhuongList")]
        public QuanHuyen QuanHuyen
        {
            get
            {
                return _QuanHuyen;
            }
            set
            {
                SetPropertyValue("QuanHuyen", ref _QuanHuyen, value);
            }
        }

        public XaPhuong(Session session) : base(session) { }
    }

}
