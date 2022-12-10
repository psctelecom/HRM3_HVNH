using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum HopDongLamViecEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng làm việc lần đầu")]
        HopDongLanDau = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng làm việc có thời hạn")]
        CoThoiHan = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng làm việc không thời hạn")]
        KhongThoiHan = 2
    }
}
