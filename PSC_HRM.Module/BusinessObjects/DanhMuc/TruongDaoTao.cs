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
    [ImageName("BO_TruongDaoTao")]
    [DefaultProperty("TenTruongDaoTao")]
    [ModelDefault("Caption", "Trường đào tạo")]
    public class TruongDaoTao : BaseObject
    {
        private QuocGia _QuocGia;
        private string _MaQuanLy;
        private string _TenTruongDaoTao;

        public TruongDaoTao(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Tên trường đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTruongDaoTao
        {
            get
            {
                return _TenTruongDaoTao;
            }
            set
            {
                SetPropertyValue("TenTruongDaoTao", ref _TenTruongDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }
    }

}
