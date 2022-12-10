using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiQuanHeEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Quan Hệ Gia Đình")]
        GiaDinh = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quan Hệ Thân Tộc")]
        ThanToc = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Quan Hệ Xã Hội")]
        XaHoi = 2
    }
}
