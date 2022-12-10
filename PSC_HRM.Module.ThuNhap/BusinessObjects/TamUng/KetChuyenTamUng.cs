using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.TamUng
{
    [NonPersistent]
    [ImageName("BO_BangLuong")]
    [DefaultProperty("BangTamUng")]
    [ModelDefault("Caption", "Kết chuyển tạm ứng")]
    public class KetChuyenTamUng : BaseObject
    {
        // Fields...
        private BangTamUng _BangTamUng;

        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Bảng tạm ứng")]
        public BangTamUng BangTamUng
        {
            get
            {
                return _BangTamUng;
            }
            set
            {
                SetPropertyValue("BangTamUng", ref _BangTamUng, value);
            }
        }

        public KetChuyenTamUng(Session session) : base(session) { }
    }

}
