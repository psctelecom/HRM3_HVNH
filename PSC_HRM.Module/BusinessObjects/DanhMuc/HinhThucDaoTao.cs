using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    /// <summary>
    /// Chính quy, tại chức, chuyên tu, từ xa, tự học...
    /// </summary>
    [DefaultClassOptions]
    [ImageName("BO_HinhThucDaoTao")]
    [DefaultProperty("TenHinhThucDaoTao")]
    [ModelDefault("Caption", "Hình thức đào tạo")]
    public class HinhThucDaoTao : BaseObject
    {
        public HinhThucDaoTao(Session session) : base(session) { }

        private string _MaQuanLy;
        private string _TenHinhThucDaoTao;

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

        [ModelDefault("Caption", "Tên hình thức đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHinhThucDaoTao
        {
            get
            {
                return _TenHinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("TenHinhThucDaoTao", ref _TenHinhThucDaoTao, value);
            }
        }
    }

}
