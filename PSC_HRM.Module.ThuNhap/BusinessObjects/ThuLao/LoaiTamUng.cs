using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace PSC_HRM.Module.ThuNhap.BusinessObjects.ThuLao
{
    [ModelDefault("Caption", "Loại tạm ứng")]
    [DefaultProperty("TenLoaiTamUng")]
    public class LoaiTamUng : BaseObject
    {
       private string _TenLoaiTamUng;
       [ModelDefault("Caption", "Loại tạm ứng")]
       [RuleRequiredField("", DefaultContexts.Save)]
       public string TenLoaiTamUng
       {
           get { return _TenLoaiTamUng; }
           set { SetPropertyValue("TenLoaiTamUng", ref _TenLoaiTamUng, value); }
       }

        public LoaiTamUng(Session session)
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