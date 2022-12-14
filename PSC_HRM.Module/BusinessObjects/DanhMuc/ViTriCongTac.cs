using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Vị trí công tác")]
    [DefaultProperty("TenViTriCongTac")]
    public class ViTriCongTac : BaseObject
    {
        private string _MaQuanLy;
        private string _TenViTriCongTac;
        private string _GhiChu;

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

        [ModelDefault("Caption", "Tên vị trí")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenViTriCongTac
        {
            get
            {
                return _TenViTriCongTac;
            }
            set
            {
                SetPropertyValue("TenViTriCongTac", ref _TenViTriCongTac, value);
            }
        }
        
        [Size(200)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ViTriCongTac(Session session) : base(session) { }
    }

}
