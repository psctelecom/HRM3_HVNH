using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Import quá trình")]
    public class HoSo_ImportQuaTrinhNPO : BaseObject
    {
        // Fields...
        private LoaiImportQuaTrinhEnum _LoaiImportQuaTrinh;

        [ModelDefault("Caption", "Import")]       
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiImportQuaTrinhEnum LoaiImportQuaTrinhEnum
        {
            get
            {
                return _LoaiImportQuaTrinh;
            }
            set
            {
                SetPropertyValue("LoaiImportQuaTrinhEnum", ref _LoaiImportQuaTrinh, value);
            }
        }

        public HoSo_ImportQuaTrinhNPO(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
