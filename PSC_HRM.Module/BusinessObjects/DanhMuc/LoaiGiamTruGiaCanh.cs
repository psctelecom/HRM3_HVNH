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
    [DefaultProperty("TenLoaiGiamTruGiaCanh")]
    [ModelDefault("Caption", "Loại giảm trừ gia cảnh")]
    public class LoaiGiamTruGiaCanh : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiGiamTruGiaCanh;

        public LoaiGiamTruGiaCanh(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên giảm trừ gia cảnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiGiamTruGiaCanh
        {
            get
            {
                return _TenLoaiGiamTruGiaCanh;
            }
            set
            {
                SetPropertyValue("TenLoaiGiamTruGiaCanh", ref _TenLoaiGiamTruGiaCanh, value);
            }
        }
    }

}
