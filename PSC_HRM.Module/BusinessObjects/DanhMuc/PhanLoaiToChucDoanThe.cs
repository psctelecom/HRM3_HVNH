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
    [DefaultProperty("TenPhanLoaiDoanThe")]
    [ModelDefault("Caption", "Phân loại tổ chức Đoàn thể")]
    [RuleCombinationOfPropertiesIsUnique("PhanLoaiDoanThe.Unique", DefaultContexts.Save, "MaQuanLy;TenPhanLoaiDoanThe")]
    public class PhanLoaiToChucDoanThe : BaseObject
    {
        public PhanLoaiToChucDoanThe(Session session) : base(session) { }

        // Fields...
        private string _MaQuanLy;
        private string _TenPhanLoaiDoanThe;

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

        [ModelDefault("Caption", "Tên phân loại Đoàn thể")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenPhanLoaiDoanThe
        {
            get
            {
                return _TenPhanLoaiDoanThe;
            }
            set
            {
                SetPropertyValue("TenPhanLoaiDoanThe", ref _TenPhanLoaiDoanThe, value);
            }
        }
    }

}
