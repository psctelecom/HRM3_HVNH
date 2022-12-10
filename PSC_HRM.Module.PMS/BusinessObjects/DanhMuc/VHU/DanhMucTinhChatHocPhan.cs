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

    [ModelDefault("Caption", "Tính chất học phần")]
    [DefaultProperty("TenTinhChat")]
    public class DanhMucTinhChatHocPhan : BaseObject
    {
        private string _MaQuanLy;
        private string _TenTinhChat;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên tính chất")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTinhChat
        {
            get { return _TenTinhChat; }
            set { SetPropertyValue("TenTinhChat", ref _TenTinhChat, value); }
        }
        public DanhMucTinhChatHocPhan(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
