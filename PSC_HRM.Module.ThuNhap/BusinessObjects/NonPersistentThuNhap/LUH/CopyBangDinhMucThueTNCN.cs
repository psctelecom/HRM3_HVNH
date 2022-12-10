using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.ThuNhap.Thue;

namespace PSC_HRM.Module.ThuNhap.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Copy bảng định mức thuế TNCN")]
    public class CopyBangDinhMucThueTNCN : BaseObject
    {
        // Fields...
        private BangDinhMucNopThueTNCN _BangDinhMucNopThueTNCN;

        [ModelDefault("Caption", "Copy từ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BangDinhMucNopThueTNCN BangDinhMucNopThueTNCN
        {
            get
            {
                return _BangDinhMucNopThueTNCN;
            }
            set
            {
                SetPropertyValue("BangDinhMucNopThueTNCN", ref _BangDinhMucNopThueTNCN, value);
            }
        }

        public CopyBangDinhMucThueTNCN(Session session)
            : base(session)
        { }
    }

}
