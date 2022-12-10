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
    [DefaultProperty("TenTinhTrangHonNhan")]
    [ModelDefault("Caption", "Tình trạng hôn nhân")]
    public class TinhTrangHonNhan : BaseObject
    {
        private string _MaQuanLy;
        private string _TenTinhTrangHonNhan;

        public TinhTrangHonNhan(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTinhTrangHonNhan
        {
            get
            {
                return _TenTinhTrangHonNhan;
            }
            set
            {
                SetPropertyValue("TenTinhTrangHonNhan", ref _TenTinhTrangHonNhan, value);
            }
        }
    }

}
