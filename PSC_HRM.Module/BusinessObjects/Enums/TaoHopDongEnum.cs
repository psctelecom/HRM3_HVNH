using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum TaoHopDongEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng làm việc")]
        HopDongLamViec = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng lao động")]
        HopDongHeSo = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Hợp đồng khoán")]
        HopDongKhoan = 2,

    }
}
