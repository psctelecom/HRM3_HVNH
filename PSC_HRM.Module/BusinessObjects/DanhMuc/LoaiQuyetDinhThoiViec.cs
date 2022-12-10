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
    [ImageName("BO_DanToc")]
    [DefaultProperty("TenLoaiQuyetDinh")]
    [ModelDefault("Caption", "Loại quyết định thôi việc")]
    public class LoaiQuyetDinhThoiViec : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiQuyetDinh;

        public LoaiQuyetDinhThoiViec(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên loại quyết định")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiQuyetDinh
        {
            get
            {
                return _TenLoaiQuyetDinh;
            }
            set
            {
                SetPropertyValue("TenLoaiQuyetDinh", ref _TenLoaiQuyetDinh, value);
            }
        }
    }

}
