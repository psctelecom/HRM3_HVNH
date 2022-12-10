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
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Người sử dụng - Bậc đào tạo")]
    //[DefaultProperty("TenLoaiMonHoc")]
    public class NguoiSuDung_TheoBacDaoTao : BaseObject
    {
        private NguoiSuDung _NguoiSuDung;
        private BacDaoTao _BacDaoTao;

        [ModelDefault("Caption", "Người sử dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NguoiSuDung NguoiSuDung
        {
            get { return _NguoiSuDung; }
            set { SetPropertyValue("NguoiSuDung", ref _NguoiSuDung, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }


        public NguoiSuDung_TheoBacDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
