using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiDaoTaoEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trong nước")]
        TrongNuoc = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Ngoài nước")]
        NgoaiNuoc = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tất cả")]
        TatCa = 2
    }
}
