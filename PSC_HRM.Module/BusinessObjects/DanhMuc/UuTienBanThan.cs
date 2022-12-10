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
    [DefaultProperty("TenUuTienBanThan")]
    [ModelDefault("Caption", "Ưu tiên bản thân")]
    public class UuTienBanThan : BaseObject
    {
        private string _MaQuanLy;
        private string _TenUuTienBanThan;

        public UuTienBanThan(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên ưu tiên bản thân")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenUuTienBanThan
        {
            get
            {
                return _TenUuTienBanThan;
            }
            set
            {
                SetPropertyValue("TenUuTienBanThan", ref _TenUuTienBanThan, value);
            }
        }
    }

}
