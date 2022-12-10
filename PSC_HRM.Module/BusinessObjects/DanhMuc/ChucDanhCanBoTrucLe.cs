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
    [DefaultProperty("TenChucDanhCanBoTrucLe")]
    [ModelDefault("Caption", "Chức danh cán bộ trực lễ")]
    public class ChucDanhCanBoTrucLe : BaseObject
    {
        // Fields...
        private string _TenChucDanhCanBoTrucLe;
        private string _MaQuanLy;

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

        [ModelDefault("Caption", "Tên chức danh hội đồng kỷ luật")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucDanhCanBoTrucLe
        {
            get
            {
                return _TenChucDanhCanBoTrucLe;
            }
            set
            {
                SetPropertyValue("TenChucDanhCanBoTrucLe", ref _TenChucDanhCanBoTrucLe, value);
            }
        }

        public ChucDanhCanBoTrucLe(Session session) : base(session) { }
    }

}
