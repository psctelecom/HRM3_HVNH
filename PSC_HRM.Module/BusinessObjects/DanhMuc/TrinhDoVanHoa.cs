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
    [DefaultProperty("TenTrinhDoVanHoa")]
    [ModelDefault("Caption", "Trình độ văn hóa")]
    public class TrinhDoVanHoa : BaseObject
    {
        private string _MaQuanLy;
        private string _TenTrinhDoVanHoa;

        public TrinhDoVanHoa(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên trình độ văn hóa")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTrinhDoVanHoa
        {
            get
            {
                return _TenTrinhDoVanHoa;
            }
            set
            {
                SetPropertyValue("TenTrinhDoVanHoa", ref _TenTrinhDoVanHoa, value);
            }
        }

    }

}
