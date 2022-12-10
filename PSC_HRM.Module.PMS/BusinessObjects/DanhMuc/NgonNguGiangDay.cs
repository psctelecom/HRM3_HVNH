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

    [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
    [DefaultProperty("TenNgonNgu")]
    public class NgonNguGiangDay : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNgonNgu;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên ngôn ngữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNgonNgu
        {
            get { return _TenNgonNgu; }
            set { SetPropertyValue("TenNgonNgu", ref _TenNgonNgu, value); }
        }
        public NgonNguGiangDay(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
