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
    [DefaultProperty("TenNhomMau")]
    [ModelDefault("Caption", "Nhóm máu")]
    public class NhomMau : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNhomMau;

        public NhomMau(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên nhóm máu")]
        public string TenNhomMau
        {
            get
            {
                return _TenNhomMau;
            }
            set
            {
                SetPropertyValue("TenNhomMau", ref _TenNhomMau, value);
            }
        }
    }

}
