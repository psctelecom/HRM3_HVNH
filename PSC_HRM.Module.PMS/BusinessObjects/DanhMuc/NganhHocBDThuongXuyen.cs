using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Ngành học BDTX")]
    [DefaultProperty("TenNganh")]
    public class NganhHocBDThuongXuyen : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNganh;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Ngành học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNganh
        {
            get { return _TenNganh; }
            set { SetPropertyValue("TenNganh", ref _TenNganh, value); }
        }

        public NganhHocBDThuongXuyen(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
