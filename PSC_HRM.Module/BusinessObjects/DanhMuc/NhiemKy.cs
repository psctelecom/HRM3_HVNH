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
    [ImageName("BO_List")]
    [DefaultProperty("TenNhiemKy")]
    [ModelDefault("Caption", "Nhiệm kỳ")]
    public class NhiemKy : BaseObject
    {
        // Fields...
        private int _DenNam;
        private int _TuNam;

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
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
        [ModelDefault("Caption", "Đến năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
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

        [ModelDefault("Caption", "Tên nhiệm kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhiemKy
        {
            get
            {
                return ObjectFormatter.Format("{TuNam:####} - {DenNam:####}", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
            }
        }

        public NhiemKy(Session session) : base(session) { }
    }

}
