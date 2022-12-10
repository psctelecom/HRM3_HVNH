using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhGia
{
    [DefaultClassOptions]
    [ImageName("BO_DanhGiaCanBo")]
    [DefaultProperty("NamDanhGia")]
    [ModelDefault("Caption", "Đánh giá tổng quát")]
    [RuleCombinationOfPropertiesIsUnique("DanhGiaTongQua.Unique", DefaultContexts.Save, "NamDanhGia;KyDanhGia")]
    public class DanhGiaTongQuat : BaseObject
    {
        // Fields...
        private int _NamDanhGia;
        private KyDanhGia _KyDanhGia;

        [ModelDefault("Caption", "Năm đánh giá")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int NamDanhGia
        {
            get
            {
                return _NamDanhGia;
            }
            set
            {
                SetPropertyValue("NamDanhGia", ref _NamDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Kỳ đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public KyDanhGia KyDanhGia
        {
            get
            {
                return _KyDanhGia;
            }
            set
            {
                SetPropertyValue("KyDanhGia", ref _KyDanhGia, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DanhGiaTongQuat-ChiTietDanhGiaList")]
        public XPCollection<ChiTietDanhGia> ChiTietDanhGiaList
        {
            get
            {
                return GetCollection<ChiTietDanhGia>("ChiTietDanhGiaList");
            }
        }

        public DanhGiaTongQuat(Session session) : base(session) { }
    }

}
