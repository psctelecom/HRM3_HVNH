using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ThuNhap.KhauTru;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Copy bảng khấu trừ thuế thu nhập")]
    public class CopyKhauTruThueThuNhap : BaseObject
    {
        // Fields...
        private BangKhauTruLuong _BangKhauTruLuong;

        [ModelDefault("Caption", "Copy từ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BangKhauTruLuong BangKhauTruLuong
        {
            get
            {
                return _BangKhauTruLuong;
            }
            set
            {
                SetPropertyValue("BangKhauTruLuong", ref _BangKhauTruLuong, value);
            }
        }

        public CopyKhauTruThueThuNhap(Session session)
            : base(session)
        { }
    }

}
