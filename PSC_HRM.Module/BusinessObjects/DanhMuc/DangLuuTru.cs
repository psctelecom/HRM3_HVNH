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
    [ImageName("BO_DangLuuTru")]
    [ModelDefault("Caption", "Dạng lưu trữ")]
    [DefaultProperty("TenDangLuuTru")]
    [RuleCombinationOfPropertiesIsUnique("DangLuuTru.Unique", DefaultContexts.Save, "MaQuanLy;TenDangLuuTru")]
    public class DangLuuTru : BaseObject
    {
        // Fields...
        private string _TenDangLuuTru;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên dạng lưu trữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDangLuuTru
        {
            get
            {
                return _TenDangLuuTru;
            }
            set
            {
                SetPropertyValue("TenDangLuuTru", ref _TenDangLuuTru, value);
            }
        }

        public DangLuuTru(Session session) : base(session) { }
    }

}
