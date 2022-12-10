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

    [ModelDefault("Caption", "Bậc đào tạo")]
    [DefaultProperty("TenBacDaoTao")]
    public class BacDaoTao : BaseObject
    {
        private string _MaQuanLy;
        private string _TenBacDaoTao;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên bậc đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBacDaoTao
        {
            get { return _TenBacDaoTao; }
            set { SetPropertyValue("TenBacDaoTao", ref _TenBacDaoTao, value); }
        }
        public BacDaoTao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
