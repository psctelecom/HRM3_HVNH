using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Tạo đánh giá cá nhân")]
    public class DanhGia_TaoDanhGia : BaseObject
    {
        // Fields...
        private DoiTuongDanhGia _DoiTuongDanhGia;

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đối tượng đánh giá")]
        public DoiTuongDanhGia DoiTuongDanhGia
        {
            get
            {
                return _DoiTuongDanhGia;
            }
            set
            {
                SetPropertyValue("DoiTuongDanhGia", ref _DoiTuongDanhGia, value);
            }
        }

        public DanhGia_TaoDanhGia(Session session)
            : base(session)
        { }
    }
}
