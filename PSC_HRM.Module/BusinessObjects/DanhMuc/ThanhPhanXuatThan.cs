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
    [DefaultProperty("TenThanhPhanXuatThan")]
    [ModelDefault("Caption", "Thành phần xuất thân")]
    public class ThanhPhanXuatThan : BaseObject
    {
        public ThanhPhanXuatThan(Session session) : base(session) { }

        private string _MaQuanLy;
        private string _TenThanhPhanXuatThan;


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

        [ModelDefault("Caption", "Thành phần xuất thân")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenThanhPhanXuatThan
        {
            get
            {
                return _TenThanhPhanXuatThan;
            }
            set
            {
                SetPropertyValue("TenThanhPhanXuatThan", ref _TenThanhPhanXuatThan, value);
            }
        }
    }

}
