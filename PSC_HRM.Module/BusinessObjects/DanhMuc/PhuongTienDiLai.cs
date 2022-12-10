using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenPhuongTienDiLai")]
    [ModelDefault("Caption", "Phương tiện đi lại")]
    public class PhuongTienDiLai : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenPhuongTienDiLai;
        
        public PhuongTienDiLai(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên phương tiện đi lại")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenPhuongTienDiLai
        {
            get
            {
                return _TenPhuongTienDiLai;
            }
            set
            {
                SetPropertyValue("TenPhuongTienDiLai", ref _TenPhuongTienDiLai, value);
            }
        }
    }

}
