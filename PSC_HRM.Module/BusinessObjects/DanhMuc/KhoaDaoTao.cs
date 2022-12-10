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
    [DefaultProperty("TenKhoaDaoTao")]
    [ModelDefault("Caption", "Khóa đào tạo")]
    public class KhoaDaoTao : BaseObject
    {
        private string _Ten;
        private int _DenNam;
        private int _TuNam;

        [ModelDefault("Caption", "Tên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                SetPropertyValue("Ten", ref _Ten, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("Caption", "Đến năm")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [Persistent]
        [ModelDefault("Caption", "Tên khóa đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenKhoaDaoTao
        {
            get
            {
                return ObjectFormatter.Format("{TuNam:####}-{DenNam:####}", this);
            }
        }

        public KhoaDaoTao(Session session) : base(session) { }
    }

}
