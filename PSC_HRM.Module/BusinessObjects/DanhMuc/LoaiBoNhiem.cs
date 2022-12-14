using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenLoaiBoNhiem")]
    [ModelDefault("Caption", "Loại bổ nhiệm")]
   // [RuleCombinationOfPropertiesIsUnique("LoaiBoNhiem.Identifier", DefaultContexts.Save, "MaQuanLy;TenLoaiBoNhiem", "Loại bổ nhiệm đã tồn tại trong hệ thống.")]
    public class LoaiBoNhiem : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiBoNhiem;

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

        [ModelDefault("Caption", "Tên loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiBoNhiem
        {
            get
            {
                return _TenLoaiBoNhiem;
            }
            set
            {
                SetPropertyValue("TenLoaiBoNhiem", ref _TenLoaiBoNhiem, value);
            }
        }
        public LoaiBoNhiem(Session session) : base(session) { }
    }

}
