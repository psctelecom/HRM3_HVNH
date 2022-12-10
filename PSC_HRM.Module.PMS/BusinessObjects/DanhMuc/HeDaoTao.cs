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

    [ModelDefault("Caption", "Hệ đào tạo")]
    [DefaultProperty("TenHeDaoTao")]
    public class HeDaoTao : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHeDaoTao;
        private bool _ApDungCongThuc;

        [ModelDefault("Caption", "Mã quản lý")]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên hệ đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenHeDaoTao
        {
            get { return _TenHeDaoTao; }
            set { SetPropertyValue("TenHeDaoTao", ref _TenHeDaoTao, value); }
        }

        [ModelDefault("Caption", "Áp dụng công thức")]
        public bool ApDungCongThuc
        {
            get { return _ApDungCongThuc; }
            set { SetPropertyValue("ApDungCongThuc", ref _ApDungCongThuc, value); }
        }
        public HeDaoTao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ApDungCongThuc = true;
        }
    }

}
