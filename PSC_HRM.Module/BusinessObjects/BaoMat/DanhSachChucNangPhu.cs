using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.BusinessObjects.BaoMat
{
    [ModelDefault("Caption", "Chức năng phụ")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "PhanQuyenChucNangPhu;AppMenu", "Chức năng đã tồn tại.")]
    public class DanhSachChucNangPhu : BaseObject
    {
        private AppMenu _AppMenu;
        private PhanQuyenChucNangPhu _PhanQuyenChucNangPhu;

        [Association("PhanQuyenChucNangPhu-listChucNangPhu")]
        public PhanQuyenChucNangPhu PhanQuyenChucNangPhu
        { get { return _PhanQuyenChucNangPhu; }
            set { SetPropertyValue("PhanQuyenChucNangPhu", ref _PhanQuyenChucNangPhu, value); }
        }



        [ModelDefault("Caption", "Chức năng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public AppMenu AppMenu
        {
            get { return _AppMenu; }
            set { SetPropertyValue("AppMenu", ref _AppMenu, value); }
        }

        public DanhSachChucNangPhu(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}