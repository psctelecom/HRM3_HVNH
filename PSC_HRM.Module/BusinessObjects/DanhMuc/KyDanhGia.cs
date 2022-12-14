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
    [DefaultProperty("TenKyDanhGia")]
    [ModelDefault("Caption", "Kỳ đánh giá")]
    public class KyDanhGia : BaseObject
    {
        private string _MaQuanLy;
        private string _TenKyDanhGia;

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

        [ModelDefault("Caption", "Tên kỳ đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenKyDanhGia
        {
            get
            {
                return _TenKyDanhGia;
            }
            set
            {
                SetPropertyValue("TenKyDanhGia", ref _TenKyDanhGia, value);
            }
        }

        public KyDanhGia(Session session) : base(session) { }
    }

}
