using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTonGiao")]
    [ModelDefault("Caption", "Tôn giáo")]
    public class TonGiao : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenTonGiao;

        public TonGiao(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã Quản Lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên Tôn Giáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTonGiao
        {
            get
            {
                return _TenTonGiao;
            }
            set
            {
                SetPropertyValue("TenTonGiao", ref _TenTonGiao, value);
            }
        }

        private decimal _CapDo;
        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }
    }

}
