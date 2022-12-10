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
    [DefaultProperty("TenHinhThucViPham")]
    [ModelDefault("Caption", "Hình thức vi phạm")]
    public class HinhThucViPham : BaseObject
    {
        // Fields...
        private string _MaQuanLy;
        private string _TenHinhThucViPham;

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên hình thức vi phạm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucViPham
        {
            get
            {
                return _TenHinhThucViPham;
            }
            set
            {
                SetPropertyValue("TenHinhThucViPham", ref _TenHinhThucViPham, value);
            }
        }

        public HinhThucViPham(Session session) : base(session) { }
    }

}
