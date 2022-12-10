using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [DefaultProperty("TenBuoi")]
    [ModelDefault("Caption", "Các buổi trong ngày")]
    [ImageName("BO_List")]
    public class CacBuoiTrongNgay : BaseObject
    {
        public CacBuoiTrongNgay(Session session) : base(session) { }

        private string _TenBuoi;
        private decimal _GiaTri;

        [ModelDefault("Caption", "Tên buổi")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string TenBuoi
        {
            get
            {
                return _TenBuoi;
            }
            set
            {
                SetPropertyValue("TenBuoi", ref _TenBuoi, value);
            }
        }

        [ModelDefault("Caption", "Giá trị")]
        public decimal GiaTri
        {
            get
            {
                return _GiaTri;
            }
            set
            {
                SetPropertyValue("GiaTri", ref _GiaTri, value);
            }
        }
    
    }

}
