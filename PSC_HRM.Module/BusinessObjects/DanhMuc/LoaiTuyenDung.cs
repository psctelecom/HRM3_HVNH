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
    [DefaultProperty("TenLoaiTuyenDung")]
    [ModelDefault("Caption", "Loại tuyển dụng")]
    public class LoaiTuyenDung : BaseObject
    {
        public LoaiTuyenDung(Session session) : base(session) { }

        private string _MaQuanLy;
        private string _TenLoaiTuyenDung;

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

        [ModelDefault("Caption", "Tên loại tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiTuyenDung
        {
            get
            {
                return _TenLoaiTuyenDung;
            }
            set
            {
                SetPropertyValue("TenLoaiTuyenDung", ref _TenLoaiTuyenDung, value);
            }
        }

      
    }

}
