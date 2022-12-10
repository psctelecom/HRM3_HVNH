using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Copy bảng chốt thông tin tính lương")]
    public class CopyChotThongTinTinhLuong : BaseObject
    {
        // Fields...
        private BangChotThongTinTinhLuong _BangChotThongTinTinhLuong;

        [ModelDefault("Caption", "Copy từ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BangChotThongTinTinhLuong BangChotThongTinTinhLuong
        {
            get
            {
                return _BangChotThongTinTinhLuong;
            }
            set
            {
                SetPropertyValue("BangChotThongTinTinhLuong", ref _BangChotThongTinTinhLuong, value);
            }
        }

        public CopyChotThongTinTinhLuong(Session session)
            : base(session)
        { }
    }

}
