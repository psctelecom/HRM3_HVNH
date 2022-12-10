using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    /// <summary>
    /// sử dụng trong quyết định bồi dưỡng
    /// </summary>
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChungChi")]
    [ModelDefault("Caption", "Loại chứng chỉ")]
    public class LoaiChungChi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChungChi;

        public LoaiChungChi(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Chứng chỉ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChungChi
        {
            get
            {
                return _TenChungChi;
            }
            set
            {
                SetPropertyValue("TenChungChi", ref _TenChungChi, value);
            }
        }

    }

}
