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
    [ImageName("BO_HinhThucNghi")]
    [DefaultProperty("TenHinhThucNghiBHXH")]
    [ModelDefault("Caption", "Hình thức nghỉ BHXH")]
    [RuleCombinationOfPropertiesIsUnique("HinhThucNghiBHXH.Identifier", DefaultContexts.Save, "MaQuanLy;TenHinhThucNghiBHXH")]
    public class HinhThucNghiBHXH : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHinhThucNghiBHXH;

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

        [ModelDefault("Caption", "Hình thức BHXH")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucNghiBHXH
        {
            get
            {
                return _TenHinhThucNghiBHXH;
            }
            set
            {
                SetPropertyValue("TenHinhThucNghiBHXH", ref _TenHinhThucNghiBHXH, value);
            }
        }

        public HinhThucNghiBHXH(Session session) : base(session) { }
    }

}
