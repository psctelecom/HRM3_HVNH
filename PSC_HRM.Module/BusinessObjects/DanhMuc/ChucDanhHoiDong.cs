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
    [DefaultProperty("TenChucDanhHoiDong")]
    [ModelDefault("Caption", "Chức danh hội đồng")]
    public class ChucDanhHoiDong : BaseObject
    {
        // Fields...
        private string _TenChucDanhHoiDong;
        private string _MaQuanLy;

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

        [ModelDefault("Caption", "Tên chức danh hội đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucDanhHoiDong
        {
            get
            {
                return _TenChucDanhHoiDong;
            }
            set
            {
                SetPropertyValue("TenChucDanhHoiDong", ref _TenChucDanhHoiDong, value);
            }
        }

        public ChucDanhHoiDong(Session session) : base(session) { }
    }

}
