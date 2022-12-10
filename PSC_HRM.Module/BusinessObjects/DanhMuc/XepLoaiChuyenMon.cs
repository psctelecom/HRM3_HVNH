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
    [DefaultProperty("TenXepLoai")]
    [ModelDefault("Caption", "Xếp loại chuyên môn")]
    public class XepLoaiChuyenMon : BaseObject
    {
        private string _MaQuanLy;
        private string _TenXepLoai;

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

        [ModelDefault("Caption", "Xếp loại")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string TenXepLoai
        {
            get
            {
                return _TenXepLoai;
            }
            set
            {
                SetPropertyValue("TenXepLoai", ref _TenXepLoai, value);
            }
        }

        public XepLoaiChuyenMon(Session session) : base(session) { }
    }

}
