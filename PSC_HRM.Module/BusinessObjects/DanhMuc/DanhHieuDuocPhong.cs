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
    [ImageName("BO_DanhHieuDuocPhong")]
    [DefaultProperty("TenDanhHieu")]
    [ModelDefault("Caption", "Danh hiệu được phong")]
    public class DanhHieuDuocPhong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDanhHieu;

        public DanhHieuDuocPhong(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên danh hiệu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDanhHieu
        {
            get
            {
                return _TenDanhHieu;
            }
            set
            {
                SetPropertyValue("TenDanhHieu", ref _TenDanhHieu, value);
            }
        }
    }

}
