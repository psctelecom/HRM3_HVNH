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
    [DefaultProperty("TenXepLoaiSucKhoe")]
    [ModelDefault("Caption", "Xếp loại sức khỏe")]
    public class XepLoaiSucKhoe : BaseObject
    {
        // Fields...
        private string _MaQuanLy;
        private string _TenXepLoaiSucKhoe;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên xếp loại sức khỏe")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenXepLoaiSucKhoe
        {
            get
            {
                return _TenXepLoaiSucKhoe;
            }
            set
            {
                SetPropertyValue("TenXepLoaiSucKhoe", ref _TenXepLoaiSucKhoe, value);
            }
        }

        public XepLoaiSucKhoe(Session session) : base(session) { }
    }

}
