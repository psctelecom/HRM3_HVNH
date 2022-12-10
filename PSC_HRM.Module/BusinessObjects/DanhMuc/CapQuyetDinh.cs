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
    [DefaultProperty("TenCapQuyetDinh")]
    [ModelDefault("Caption", "Cấp quyết định")]
    public class CapQuyetDinh : BaseObject
    {
        public CapQuyetDinh(Session session) : base(session) { }
        
        private string _MaQuanLy;
        private string _TenCapQuyetDinh;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("",DefaultContexts.Save)]
        [RuleRequiredField("",DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên cấp quyết định")]        
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenCapQuyetDinh
        {
            get
            {
                return _TenCapQuyetDinh;
            }
            set
            {
                SetPropertyValue("TenCapQuyetDinh", ref _TenCapQuyetDinh, value);
            }
        }
        
    }

}
