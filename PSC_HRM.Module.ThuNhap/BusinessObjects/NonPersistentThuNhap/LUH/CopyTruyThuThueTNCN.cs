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
    [ModelDefault("Caption", "Truy thu thuế TNCN")]
    public class CopyTruyThuThueTNCN : BaseObject
    {
        // Fields...
        private ChiTietQuanLyTruyThuThueTNCN _ChiTietQuanLyTruyThuThueTNCN;

        [ModelDefault("Caption", "Copy từ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChiTietQuanLyTruyThuThueTNCN ChiTietQuanLyTruyThuThueTNCN
        {
            get
            {
                return _ChiTietQuanLyTruyThuThueTNCN;
            }
            set
            {
                SetPropertyValue("ChiTietQuanLyTruyThuThueTNCN", ref _ChiTietQuanLyTruyThuThueTNCN, value);
            }
        }

        public CopyTruyThuThueTNCN(Session session)
            : base(session)
        { }
    }

}
