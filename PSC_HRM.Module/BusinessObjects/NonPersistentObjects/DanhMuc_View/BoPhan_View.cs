using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.NonPersistentObjects.DanhMuc_View
{
    [NonPersistent]

    [DefaultProperty("TenBoPhan")]
    [ModelDefault("Caption", "Đơn vị")]
    public class BoPhan_View : BaseObject
    {

        private string _TenBoPhan;
        private string _TenBoPhanCha;


        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBoPhan
        {
            get
            {
                return _TenBoPhan;
            }
            set
            {
                SetPropertyValue("TenBoPhan", ref _TenBoPhan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBoPhanCha
        {
            get
            {
                return _TenBoPhanCha;
            }
            set
            {
                SetPropertyValue("TenBoPhanCha", ref _TenBoPhanCha, value);
            }
        }
        public BoPhan_View(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initializaion code.
        }
    }

}