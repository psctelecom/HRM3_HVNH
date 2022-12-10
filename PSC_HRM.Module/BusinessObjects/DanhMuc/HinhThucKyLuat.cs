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
    [ImageName("BO_HinhThucKyLuat")]
    [DefaultProperty("TenHinhThucKyLuat")]
    [ModelDefault("Caption", "Hình thức kỷ luật")]
    public class HinhThucKyLuat : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHinhThucKyLuat;

        public HinhThucKyLuat(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên hình thức kỷ luật")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucKyLuat
        {
            get
            {
                return _TenHinhThucKyLuat;
            }
            set
            {
                SetPropertyValue("TenHinhThucKyLuat", ref _TenHinhThucKyLuat, value);
            }
        }
    }

}
